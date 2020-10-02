#region

using System;
using System.Collections.Generic;
using dnlib.DotNet;
using dnlib.DotNet.Emit;

#endregion

namespace KoiVM
{
    public class Scanner
    {
        private readonly HashSet<MethodDef> exclude = new HashSet<MethodDef>();
        private readonly HashSet<MethodDef> export = new HashSet<MethodDef>();
        private readonly HashSet<MethodDef> methods;
        private readonly ModuleDef module;
        private readonly List<Tuple<MethodDef, bool>> results = new List<Tuple<MethodDef, bool>>();

        public Scanner(ModuleDef module)
            : this(module, null)
        {
        }

        public Scanner(ModuleDef module, HashSet<MethodDef> methods)
        {
            this.module = module;
            this.methods = methods;
        }

        public IEnumerable<Tuple<MethodDef, bool>> Scan()
        {
            this.ScanMethods(this.FindExclusion);
            this.ScanMethods(this.ScanExport);
            this.ScanMethods(this.PopulateResult);
            return this.results;
        }

        private void ScanMethods(Action<MethodDef> scanFunc)
        {
            foreach(TypeDef type in this.module.GetTypes())
            foreach(MethodDef method in type.Methods)
                scanFunc(method);
        }

        private void FindExclusion(MethodDef method)
        {
            if(!method.HasBody || this.methods != null && !this.methods.Contains(method))
                this.exclude.Add(method);
            else if(method.HasGenericParameters)
                foreach(Instruction instr in method.Body.Instructions)
                {
                    if (instr.Operand is IMethod target && target.IsMethod &&
                       (target = target.ResolveMethodDef()) != null &&
                       (this.methods == null || this.methods.Contains((MethodDef)target)))
                        this.export.Add((MethodDef)target);
                }
        }

        private void ScanExport(MethodDef method)
        {
            if(!method.HasBody)
                return;

            bool shouldExport = false;
            shouldExport |= method.IsPublic;
            shouldExport |= method.SemanticsAttributes != 0;
            shouldExport |= method.IsConstructor;
            shouldExport |= method.IsVirtual;
            shouldExport |= method.Module.EntryPoint == method;
            if(shouldExport)
                this.export.Add(method);

            bool excluded = this.exclude.Contains(method) || method.DeclaringType.HasGenericParameters;
            foreach(Instruction instr in method.Body.Instructions)
                if(instr.OpCode == OpCodes.Callvirt ||
                   instr.Operand is IMethod && excluded)
                {
                    MethodDef target = ((IMethod) instr.Operand).ResolveMethodDef();
                    if(target != null && (this.methods == null || this.methods.Contains(target)))
                        this.export.Add(target);
                }
        }

        private void PopulateResult(MethodDef method)
        {
            if(this.exclude.Contains(method) || method.DeclaringType.HasGenericParameters)
                return;
            this.results.Add(Tuple.Create(method, this.export.Contains(method)));
        }
    }
}