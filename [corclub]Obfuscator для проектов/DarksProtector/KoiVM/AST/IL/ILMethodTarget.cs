#region

using dnlib.DotNet;
using KoiVM.RT;

#endregion

namespace KoiVM.AST.IL
{
    public class ILMethodTarget : IILOperand, IHasOffset
    {
        private ILBlock methodEntry;

        public ILMethodTarget(MethodDef target) => this.Target = target;

        public MethodDef Target
        {
            get;
            set;
        }

        public uint Offset => this.methodEntry == null ? 0 : this.methodEntry.Content[0].Offset;

        public void Resolve(DarksVMRuntime runtime) => runtime.LookupMethod(this.Target, out this.methodEntry);

        public override string ToString() => this.Target.ToString();
    }
}