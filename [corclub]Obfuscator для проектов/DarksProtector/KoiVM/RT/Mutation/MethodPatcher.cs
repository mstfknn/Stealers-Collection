#region

using dnlib.DotNet;
using dnlib.DotNet.Emit;

#endregion

namespace KoiVM.RT.Mutation
{
    internal class MethodPatcher
    {
        private readonly MethodDef vmEntryNormal, vmEntryTyped;

        public MethodPatcher(ModuleDef rtModule)
        {
            foreach(MethodDef entry in rtModule.Find(RTMap.DarksVMEntry, true).FindMethods(RTMap.DarksVMRun))
                switch (entry.Parameters.Count)
                {
                    case 6:
                        this.vmEntryNormal = entry;
                        break;
                    default:
                        this.vmEntryTyped = entry;
                        break;
                }
        }

        private static bool ShouldBeTyped(MethodDef method)
        {
            if(!method.IsStatic && method.DeclaringType.IsValueType)
                return true;
            foreach(Parameter param in method.Parameters)
                if(param.Type.IsByRef)
                    return true;
            return method.ReturnType.IsByRef ? true : false;
        }

        public void PatchMethodStub(MethodDef method, uint id)
        {
            if (!ShouldBeTyped(method))
            {
                this.PatchNormal(method.Module, method, (int)id);
            }
            else
                this.PatchTyped(method.Module, method, (int)id);
        }

        private void PatchNormal(ModuleDef module, MethodDef method, int id)
        {
            var body = new CilBody();
            method.Body = body;
            id = 63493 * id;
            body.Instructions.Add(Instruction.Create(OpCodes.Ldc_I4, id * 10));
            body.Instructions.Add(Instruction.Create(OpCodes.Ldc_I4, id * 5));
            body.Instructions.Add(Instruction.Create(OpCodes.Ldtoken, method.DeclaringType));
            body.Instructions.Add(Instruction.Create(OpCodes.Ldc_I4, id * 20));
            body.Instructions.Add(Instruction.Create(OpCodes.Ldc_I4, id * 15));
            body.Instructions.Add(Instruction.Create(OpCodes.Ldc_I4, method.Parameters.Count));
            body.Instructions.Add(Instruction.Create(OpCodes.Newarr, method.Module.CorLibTypes.Object.ToTypeDefOrRef()));

            foreach(Parameter param in method.Parameters)
            {
                body.Instructions.Add(Instruction.Create(OpCodes.Dup));
                body.Instructions.Add(Instruction.Create(OpCodes.Ldc_I4, param.Index));
                body.Instructions.Add(Instruction.Create(OpCodes.Ldarg, param));
                if (!param.Type.IsValueType)
                {
                    if (param.Type.IsPointer)
                    {
                        body.Instructions.Add(Instruction.Create(OpCodes.Conv_U));
                        body.Instructions.Add(Instruction.Create(OpCodes.Box, method.Module.CorLibTypes.UIntPtr.ToTypeDefOrRef()));
                    }
                }
                else
                {
                    body.Instructions.Add(Instruction.Create(OpCodes.Box, param.Type.ToTypeDefOrRef()));
                }
                body.Instructions.Add(Instruction.Create(OpCodes.Stelem_Ref));
            }
            body.Instructions.Add(Instruction.Create(OpCodes.Call, method.Module.Import(this.vmEntryNormal)));
            if (method.ReturnType.ElementType != ElementType.Void)
            {
                if (!method.ReturnType.IsValueType)
                {
                    body.Instructions.Add(Instruction.Create(OpCodes.Castclass, method.ReturnType.ToTypeDefOrRef()));
                }
                else
                    body.Instructions.Add(Instruction.Create(OpCodes.Unbox_Any, method.ReturnType.ToTypeDefOrRef()));
            }
            else body.Instructions.Add(Instruction.Create(OpCodes.Pop));
            body.Instructions.Add(Instruction.Create(OpCodes.Ret));

            body.OptimizeMacros();
        }

        private void PatchTyped(ModuleDef module, MethodDef method, int id)
        {
            var body = new CilBody();
            method.Body = body;
            id = 45 * id;
            body.Instructions.Add(Instruction.Create(OpCodes.Ldc_I4, id * 10));
            body.Instructions.Add(Instruction.Create(OpCodes.Ldc_I4, id * 5));
            body.Instructions.Add(Instruction.Create(OpCodes.Ldtoken, method.DeclaringType));
            body.Instructions.Add(Instruction.Create(OpCodes.Ldc_I4, id * 20));
            body.Instructions.Add(Instruction.Create(OpCodes.Ldc_I4, id * 15));
            body.Instructions.Add(Instruction.Create(OpCodes.Ldc_I4, method.Parameters.Count));
            body.Instructions.Add(Instruction.Create(OpCodes.Newarr, new PtrSig(method.Module.CorLibTypes.Void).ToTypeDefOrRef()));

            foreach(Parameter param in method.Parameters)
            {
                body.Instructions.Add(Instruction.Create(OpCodes.Dup));
                body.Instructions.Add(Instruction.Create(OpCodes.Ldc_I4, param.Index));
                if (!param.Type.IsByRef)
                {
                    body.Instructions.Add(Instruction.Create(OpCodes.Ldarga, param));
                    body.Instructions.Add(Instruction.Create(OpCodes.Mkrefany, param.Type.ToTypeDefOrRef()));
                }
                else
                {
                    body.Instructions.Add(Instruction.Create(OpCodes.Ldarg, param));
                    body.Instructions.Add(Instruction.Create(OpCodes.Mkrefany, param.Type.Next.ToTypeDefOrRef()));
                }
                var local = new Local(method.Module.CorLibTypes.TypedReference);
                body.Variables.Add(local);
                body.Instructions.Add(Instruction.Create(OpCodes.Stloc, local));
                body.Instructions.Add(Instruction.Create(OpCodes.Ldloca, local));
                body.Instructions.Add(Instruction.Create(OpCodes.Conv_I));
                body.Instructions.Add(Instruction.Create(OpCodes.Stelem_I));
            }

            if(method.ReturnType.GetElementType() != ElementType.Void)
            {
                var retVar = new Local(method.ReturnType);
                var retRef = new Local(method.Module.CorLibTypes.TypedReference);
                body.Variables.Add(retVar);
                body.Variables.Add(retRef);
                body.Instructions.Add(Instruction.Create(OpCodes.Ldloca, retVar));
                body.Instructions.Add(Instruction.Create(OpCodes.Mkrefany, method.ReturnType.ToTypeDefOrRef()));
                body.Instructions.Add(Instruction.Create(OpCodes.Stloc, retRef));
                body.Instructions.Add(Instruction.Create(OpCodes.Ldloca, retRef));
                body.Instructions.Add(Instruction.Create(OpCodes.Call, method.Module.Import(this.vmEntryTyped)));

                body.Instructions.Add(Instruction.Create(OpCodes.Ldloc, retVar));
            }
            else
            {
                body.Instructions.Add(Instruction.Create(OpCodes.Ldnull));
                body.Instructions.Add(Instruction.Create(OpCodes.Call, method.Module.Import(this.vmEntryTyped)));
            }
            body.Instructions.Add(Instruction.Create(OpCodes.Ret));

            body.OptimizeMacros();
        }
    }
}