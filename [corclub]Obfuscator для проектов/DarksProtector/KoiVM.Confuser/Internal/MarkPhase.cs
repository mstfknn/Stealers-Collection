#region

using System.Collections.Generic;
using System.IO;
using System.Linq;
using Confuser.Core;
using Confuser.Core.Services;
using Confuser.Protections;
using dnlib.DotNet;
using dnlib.DotNet.Emit;
using dnlib.DotNet.Writer;
using SR = System.Reflection;

#endregion

namespace KoiVM.Confuser.Internal
{
    [SR.ObfuscationAttribute(Exclude = false, Feature = "+koi;")]
    public class MarkPhase : ProtectionPhase
    {
        public MarkPhase(Protection parent)
            : base(parent)
        {
        }

        public override ProtectionTargets Targets => ProtectionTargets.Methods;

        public override string Name => "Mark methods to virtualize";

        protected override void Execute(ConfuserContext context, ProtectionParameters parameters)
        {
            Virtualizer vr = context.Annotations.Get<Virtualizer>(context, Fish.VirtualizerKey);

            IMarkerService marker = context.Registry.GetService<IMarkerService>();
            IReferenceProxyService refProxy = context.Registry.GetService<IReferenceProxyService>();
            IAntiTamperService antiTamper = context.Registry.GetService<IAntiTamperService>();
            ICompressionService compression = context.Registry.GetService<ICompressionService>();

            var methods = new HashSet<MethodDef>(parameters.Targets.OfType<MethodDef>());
            var refRepl = new Dictionary<IMemberRef, IMemberRef>();

            TypeDef oldType = context.CurrentModule.GlobalType;
            var newType = new TypeDefUser(oldType.Name);
            oldType.Namespace = "What_a_great_VM";
            oldType.Name = "VM";
            oldType.BaseType = context.CurrentModule.CorLibTypes.GetTypeRef("System", "Object");
            context.CurrentModule.Types.Insert(0, newType);

            MethodDef old_cctor = oldType.FindOrCreateStaticConstructor();
            MethodDef cctor = newType.FindOrCreateStaticConstructor();
            old_cctor.Name = "Load";
            old_cctor.IsRuntimeSpecialName = false;
            old_cctor.IsSpecialName = false;
            old_cctor.Access = MethodAttributes.PrivateScope;
            cctor.Body = new CilBody(true, new List<Instruction>
            {
                Instruction.Create(OpCodes.Call, old_cctor),
                Instruction.Create(OpCodes.Ret)
            }, new List<ExceptionHandler>(), new List<Local>());

            marker.Mark(cctor, this.Parent);
            antiTamper.ExcludeMethod(context, cctor);

            for(int i = 0; i < oldType.Methods.Count; i++)
            {
                MethodDef nativeMethod = oldType.Methods[i];
                if(nativeMethod.IsNative)
                {
                    var methodStub = new MethodDefUser(nativeMethod.Name, nativeMethod.MethodSig.Clone());
                    methodStub.Attributes = MethodAttributes.Assembly | MethodAttributes.Static;
                    methodStub.Body = new CilBody();
                    methodStub.Body.Instructions.Add(new Instruction(OpCodes.Jmp, nativeMethod));
                    methodStub.Body.Instructions.Add(new Instruction(OpCodes.Ret));

                    oldType.Methods[i] = methodStub;
                    newType.Methods.Add(nativeMethod);
                    refRepl[nativeMethod] = methodStub;
                    marker.Mark(methodStub, this.Parent);
                    antiTamper.ExcludeMethod(context, methodStub);
                }
            }

            compression.TryGetRuntimeDecompressor(context.CurrentModule, def =>
            {
                if(def is MethodDef)
                    methods.Remove((MethodDef) def);
            });

            var toProcess = new Dictionary<ModuleDef, List<MethodDef>>();
            foreach(System.Tuple<MethodDef, bool> entry in new Scanner(context.CurrentModule, methods).Scan().WithProgress(context.Logger))
            {
                bool isExport = entry.Item2;
                isExport |= context.Annotations.Get<object>(entry.Item1, Fish.ExportKey) != null;
                isExport |= refProxy.IsTargeted(context, entry.Item1);

                if(!isExport)
                    antiTamper.ExcludeMethod(context, entry.Item1);
                vr.AddMethod(entry.Item1, isExport);
                toProcess.AddListEntry(entry.Item1.Module, entry.Item1);
                context.CheckCancellation();
            }

            context.CurrentModuleWriterListener.OnWriterEvent += new Listener
            {
                ctx = context,
                vr = vr,
                methods = toProcess,
                refRepl = refRepl
            }.OnWriterEvent;
        }

        private class Listener
        {
            private IModuleWriterListener commitListener;
            public ConfuserContext ctx;
            public Dictionary<ModuleDef, List<MethodDef>> methods;
            public Dictionary<IMemberRef, IMemberRef> refRepl;
            public Virtualizer vr;

            public void OnWriterEvent(object sender, ModuleWriterListenerEventArgs e)
            {
                var writer = (ModuleWriter) sender;
                if(this.commitListener != null)
                    this.commitListener.OnWriterEvent(writer, e.WriterEvent);

                if (e.WriterEvent != ModuleWriterEvent.MDBeginWriteMethodBodies || !this.methods.ContainsKey(writer.Module))
                {
                    if (this.commitListener != null && e.WriterEvent == ModuleWriterEvent.End && this.vr.ExportDbgInfo)
                    {
                        string mapName = Path.ChangeExtension(writer.Module.Name, "map");
                        string mapPath = Path.GetFullPath(Path.Combine(this.ctx.OutputDirectory, mapName));
                        Directory.CreateDirectory(this.ctx.OutputDirectory);
                        File.WriteAllBytes(mapPath, this.vr.Runtime.DebugInfo);
                    }
                }
                else
                {

                    this.vr.ProcessMethods(writer.Module, (num, total) =>
                    {
                        this.ctx.Logger.Progress(num, total);
                        this.ctx.CheckCancellation();
                    });
                    this.ctx.Logger.EndProgress();

                    foreach (KeyValuePair<IMemberRef, IMemberRef> repl in this.refRepl)
                        this.vr.Runtime.Descriptor.Data.ReplaceReference(repl.Key, repl.Value);

                    this.commitListener = this.vr.CommitModule(this.ctx.CurrentModule, (num, total) =>
                    {
                        this.ctx.Logger.Progress(num, total);
                        this.ctx.CheckCancellation();
                    });
                }
            }
        }
    }
}