#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using dnlib.DotNet;
using dnlib.DotNet.Emit;
using KoiVM.VM;
using KoiVM.VMIL;

#endregion

namespace KoiVM.RT.Mutation
{
    public class RTConstants
    {
        private readonly Dictionary<string, int> constants = new Dictionary<string, int>();

        private void AddField(string fieldName, int fieldValue) => this.constants[fieldName] = fieldValue;

        private void Conclude(Random random, IList<Instruction> instrs, TypeDef constType)
        {
            var constValues = this.constants.ToList();
            random.Shuffle(constValues);
            foreach(KeyValuePair<string, int> c in constValues)
            {
                instrs.Add(new Instruction(OpCodes.Ldnull));
                instrs.Add(new Instruction(OpCodes.Ldc_I4, c.Value));
                instrs.Add(new Instruction(OpCodes.Stfld, constType.FindField(RTMap.VMConstMap[c.Key])));
            }
        }

        public int? GetConstant(string name) => !this.constants.TryGetValue(name, out int ret) ? null : (int?)ret;

        public void InjectConstants(ModuleDef rtModule, VMDescriptor desc, RuntimeHelpers helpers)
        {
            TypeDef constants = rtModule.Find(RTMap.kraDConstants, true);
            MethodDef cctor = constants.FindOrCreateStaticConstructor();
            IList<Instruction> instrs = cctor.Body.Instructions;
            instrs.Clear();

            for(int i = 0; i < (int) DarksVMRegisters.Max; i++)
            {
                var reg = (DarksVMRegisters) i;
                byte regId = desc.Architecture.Registers[reg];
                string regField = reg.ToString();
                this.AddField(regField, regId);
            }

            for(int i = 0; i < (int)DarksVMFlags.Max; i++)
            {
                var fl = (DarksVMFlags) i;
                int flId = desc.Architecture.Flags[fl];
                string flField = fl.ToString();
                this.AddField(flField, 1 << flId);
            }

            for(int i = 0; i < (int) ILOpCode.Max; i++)
            {
                var op = (ILOpCode) i;
                byte opId = desc.Architecture.OpCodes[op];
                string opField = op.ToString();
                this.AddField(opField, opId);
            }

            for(int i = 0; i < (int) DarksVMCalls.Max; i++)
            {
                var vc = (DarksVMCalls) i;
                int vcId = desc.Runtime.VMCall[vc];
                string vcField = vc.ToString();
                this.AddField(vcField, vcId);
            }

            this.AddField(ConstantFields.E_CALL.ToString(), (int) desc.Runtime.VCallOps.ECALL_CALL);
            this.AddField(ConstantFields.E_CALLVIRT.ToString(), (int) desc.Runtime.VCallOps.ECALL_CALLVIRT);
            this.AddField(ConstantFields.E_NEWOBJ.ToString(), (int) desc.Runtime.VCallOps.ECALL_NEWOBJ);
            this.AddField(ConstantFields.E_CALLVIRT_CONSTRAINED.ToString(), (int) desc.Runtime.VCallOps.ECALL_CALLVIRT_CONSTRAINED);

            this.AddField(ConstantFields.INIT.ToString(), (int) helpers.INIT);

            this.AddField(ConstantFields.INSTANCE.ToString(), desc.Runtime.RTFlags.INSTANCE);

            this.AddField(ConstantFields.CATCH.ToString(), desc.Runtime.RTFlags.EH_CATCH);
            this.AddField(ConstantFields.FILTER.ToString(), desc.Runtime.RTFlags.EH_FILTER);
            this.AddField(ConstantFields.FAULT.ToString(), desc.Runtime.RTFlags.EH_FAULT);
            this.AddField(ConstantFields.FINALLY.ToString(), desc.Runtime.RTFlags.EH_FINALLY);

            this.Conclude(desc.Random, instrs, constants);
            instrs.Add(Instruction.Create(OpCodes.Ret));
            cctor.Body.OptimizeMacros();
        }
    }

    [Obfuscation(Exclude = false, ApplyToMembers = false, Feature = "+rename(forceRen=true);")]
    internal enum ConstantFields
    {
        E_CALL,
        E_CALLVIRT,
        E_NEWOBJ,
        E_CALLVIRT_CONSTRAINED,

        INIT,

        INSTANCE,

        CATCH,
        FILTER,
        FAULT,
        FINALLY
    }
}