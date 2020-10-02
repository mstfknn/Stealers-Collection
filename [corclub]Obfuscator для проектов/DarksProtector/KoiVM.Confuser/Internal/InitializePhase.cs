#region

using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using Confuser.Core;
using Confuser.Core.Services;
using Confuser.Protections;
using Confuser.Renamer;
using dnlib.DotNet;

#endregion

namespace KoiVM.Confuser.Internal
{
    [Obfuscation(Exclude = false, Feature = "+koi;")]
    public class InitializePhase : ProtectionPhase
    {
        private static readonly string Version, Copyright;
        private readonly string koiDir;

        static InitializePhase()
        {
            Assembly assembly = typeof(Fish).Assembly;
            var nameAttr = (AssemblyProductAttribute) assembly.GetCustomAttributes(typeof(AssemblyProductAttribute), false)[0];
            var verAttr =
                (AssemblyInformationalVersionAttribute)
                assembly.GetCustomAttributes(typeof(AssemblyInformationalVersionAttribute), false)[0];
            var cpAttr = (AssemblyCopyrightAttribute) assembly.GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false)[0];
            Version = string.Format("{0} {1}", nameAttr.Product, verAttr.InformationalVersion);
            Copyright = cpAttr.Copyright;
        }

        public InitializePhase(Protection parent, string koiDir)
            : base(parent) => this.koiDir = koiDir;// ExpirationChecker.Init(koiDir);

        public override ProtectionTargets Targets => ProtectionTargets.Modules;

        public override string Name => "Virtualization initialization";

        protected override void Execute(ConfuserContext context, ProtectionParameters parameters)
        {
            context.Logger.Log("Initializing DarksVM");

            foreach (ModuleDefMD module in context.Modules)
            {
                context.Logger.LogFormat("Protecting '{0}' with DarksVM...", module.Name);
            }


            IRandomService random = context.Registry.GetService<IRandomService>();
            IReferenceProxyService refProxy = context.Registry.GetService<IReferenceProxyService>();
            INameService nameSrv = context.Registry.GetService<INameService>();
            int seed = random.GetRandomGenerator(this.Parent.FullId).NextInt32();

            string rtName = null;
            bool dbg = false, stackwalk = false;
            ModuleDef merge = null;
            foreach(ModuleDefMD module in context.Modules)
            {
                if(rtName == null)
                    rtName = parameters.GetParameter<string>(context, module, "rtName");
                if(dbg == false)
                    dbg = parameters.GetParameter<bool>(context, module, "dbgInfo");
                if(stackwalk == false)
                    stackwalk = parameters.GetParameter<bool>(context, module, "stackwalk");
               
                        merge = module;
                    rtName = "Virtualization";
                
            }
            rtName = rtName ?? "KoiVM.Runtime--test";

            ModuleDefMD rtModule;
            Stream resStream = typeof(Virtualizer).Assembly.GetManifestResourceStream("KoiVM.Runtime.dll");
            if(resStream != null)
            {
                rtModule = ModuleDefMD.Load(resStream, context.Resolver.DefaultModuleContext);
            }
            else
            {
                string rtPath = Path.Combine(this.koiDir, "KoiVM.Runtime.dll");
                rtModule = ModuleDefMD.Load(rtPath, context.Resolver.DefaultModuleContext);
            }
            rtModule.Assembly.Name = rtName;
            rtModule.Name = rtName + ".dll";
            var vr = new Virtualizer(seed, context.Project.Debug)
            {
                ExportDbgInfo = dbg,
                DoStackWalk = stackwalk
            };
            vr.Initialize(rtModule);

            context.Annotations.Set(context, Fish.VirtualizerKey, vr);
            context.Annotations.Set(context, Fish.MergeKey, merge);

            if(merge != null)
            {
                var types = new List<TypeDef>(vr.RuntimeModule.GetTypes());
                types.Remove(vr.RuntimeModule.GlobalType);
                vr.CommitRuntime(merge);
                foreach(TypeDef type in types)
                foreach(IDnlibDef def in type.FindDefinitions())
                {
                    if(def is TypeDef && def != type) // nested type
                        continue;
                    nameSrv.SetCanRename(def, false);
                    ProtectionParameters.SetParameters(context, def, new ProtectionSettings());
                }
            }
            else
            {
                vr.CommitRuntime(merge);
            }

            ConstructorInfo ctor = typeof(InternalsVisibleToAttribute).GetConstructor(new[] {typeof(string)});
            foreach(ModuleDef module in context.Modules)
            {
                var methods = new HashSet<MethodDef>();
                foreach(TypeDef type in module.GetTypes())
                foreach(MethodDef method in type.Methods)
                    if(ProtectionParameters.GetParameters(context, method).ContainsKey(this.Parent))
                        methods.Add(method);

                if(methods.Count > 0)
                {
                    var ca = new CustomAttribute((ICustomAttributeType) module.Import(ctor));
                    ca.ConstructorArguments.Add(new CAArgument(module.CorLibTypes.String, vr.RuntimeModule.Assembly.Name.String));
                    module.Assembly.CustomAttributes.Add(ca);
                }

                foreach(Tuple<MethodDef, bool> entry in new Scanner(module, methods).Scan().WithProgress(context.Logger))
                {
                    if (!entry.Item2)
                    {
                        refProxy.ExcludeTarget(context, entry.Item1);
                    }
                    else
                        context.Annotations.Set(entry.Item1, Fish.ExportKey, Fish.ExportKey);
                    context.CheckCancellation();
                }
            }
        }
    }
}