#region

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using dnlib.DotNet;
using dnlib.DotNet.Writer;
using KoiVM.RT;
using KoiVM.VMIL;

#endregion

namespace KoiVM
{
    [Obfuscation(Exclude = false, Feature = "+koi;-ref proxy")]
    public class Virtualizer : IDarksVMSettings
    {
        private readonly bool debug;
        private readonly HashSet<MethodDef> doInstantiation = new HashSet<MethodDef>();
        private readonly GenericInstantiation instantiation = new GenericInstantiation();
        private readonly Dictionary<MethodDef, bool> methodList = new Dictionary<MethodDef, bool>();
        private readonly HashSet<ModuleDef> processed = new HashSet<ModuleDef>();
        private string runtimeName;
        private readonly int seed;
        private MethodVirtualizer vr;

        public Virtualizer(int seed, bool debug)
        {
            this.Runtime = null;
            this.seed = seed;
            this.debug = debug;

            this.instantiation.ShouldInstantiate += spec => this.doInstantiation.Contains(spec.Method.ResolveMethodDefThrow());
        }

        public ModuleDef RuntimeModule => this.Runtime.Module;

        public DarksVMRuntime Runtime
        {
            get;
            set;
        }

        bool IDarksVMSettings.IsExported(MethodDef method) => !this.methodList.TryGetValue(method, out bool ret) ? false : ret;

        bool IDarksVMSettings.IsVirtualized(MethodDef method) => this.methodList.ContainsKey(method);

        int IDarksVMSettings.Seed => this.seed;

        bool IDarksVMSettings.IsDebug => this.debug;

        public bool ExportDbgInfo
        {
            get;
            set;
        }

        public bool DoStackWalk
        {
            get;
            set;
        }

        public void Initialize(ModuleDef runtimeLib)
        {
            this.Runtime = new DarksVMRuntime(this, runtimeLib);
            this.runtimeName = runtimeLib.Assembly.Name;
            this.vr = new MethodVirtualizer(this.Runtime);
        }

        public void AddModule(ModuleDef module)
        {
            foreach(Tuple<MethodDef, bool> method in new Scanner(module).Scan())
                this.AddMethod(method.Item1, method.Item2);
        }

        public void AddMethod(MethodDef method, bool isExport)
        {
            //TODO : Here is the place where method are being added to the
            //koiVM protection queue. So you can decide which one must be protected or not.
            if(!method.HasBody)
                return;
            if(method.HasGenericParameters) return;
            this.methodList.Add(method, isExport);

            if(!isExport)
            {
                // Need to force set declaring type because will be used in VM compilation
                TypeSig thisParam = method.HasThis ? method.Parameters[0].Type : null;

                TypeDef declType = method.DeclaringType;
                declType.Methods.Remove(method);
                if(method.SemanticsAttributes != 0)
                {
                    foreach(PropertyDef prop in declType.Properties)
                    {
                        if(prop.GetMethod == method)
                            prop.GetMethod = null;
                        if(prop.SetMethod == method)
                            prop.SetMethod = null;
                    }
                    foreach(EventDef evt in declType.Events)
                    {
                        if(evt.AddMethod == method)
                            evt.AddMethod = null;
                        if(evt.RemoveMethod == method)
                            evt.RemoveMethod = null;
                        if(evt.InvokeMethod == method)
                            evt.InvokeMethod = null;
                    }
                }
                method.DeclaringType2 = declType;

                if(thisParam != null)
                    method.Parameters[0].Type = thisParam;
            }
        }

        public IEnumerable<MethodDef> GetMethods() => this.methodList.Keys;

        public void ProcessMethods(ModuleDef module, Action<int, int> progress = null)
        {
            if(this.processed.Contains(module))
                throw new InvalidOperationException("Module already processed.");

            if(progress == null)
                progress = (num, total) => { };

            var targets = this.methodList.Keys.Where(method => method.Module == module).ToList();

            for(int i = 0; i < targets.Count; i++)
            {
                MethodDef method = targets[i];
                this.instantiation.EnsureInstantiation(method, (spec, instantation) =>
                {
                    if(instantation.Module == module || this.processed.Contains(instantation.Module))
                        targets.Add(instantation);
                    this.methodList[instantation] = false;
                });
                try
                {
                    this.ProcessMethod(method, this.methodList[method]);
                }
                catch(Exception)
                {
                    Console.WriteLine($"! error on process method : {method.FullName}");
                }
                progress(i, targets.Count);
            }
            progress(targets.Count, targets.Count);
            this.processed.Add(module);
        }

        public IModuleWriterListener CommitModule(ModuleDefMD module, Action<int, int> progress = null)
        {
            if(progress == null)
                progress = (num, total) => { };

            MethodDef[] methods = this.methodList.Keys.Where(method => method.Module == module).ToArray();
            for(int i = 0; i < methods.Length; i++)
            {
                MethodDef method = methods[i];
                this.PostProcessMethod(method, this.methodList[method]);
                progress(i, this.methodList.Count);
            }
            progress(methods.Length, methods.Length);

            return this.Runtime.CommitModule(module);
        }

        public void CommitRuntime(ModuleDef targetModule = null) => this.Runtime.CommitRuntime(targetModule);

        private void ProcessMethod(MethodDef method, bool isExport) => this.vr.Load(method, isExport);

        private void PostProcessMethod(MethodDef method, bool isExport)
        {
            CFG.ScopeBlock scope = this.Runtime.LookupMethod(method);

            var ilTransformer = new ILPostTransformer(method, scope, this.Runtime);
            ilTransformer.Transform();
        }

        public string SaveRuntime(string directory)
        {
            string rtPath = Path.Combine(directory, $"{this.runtimeName}.dll");

            File.WriteAllBytes(rtPath, this.Runtime.RuntimeLibrary);
            if(this.Runtime.RuntimeSymbols.Length > 0)
                File.WriteAllBytes(Path.ChangeExtension(rtPath, "pdb"), this.Runtime.RuntimeSymbols);
            return rtPath;
        }
    }
}