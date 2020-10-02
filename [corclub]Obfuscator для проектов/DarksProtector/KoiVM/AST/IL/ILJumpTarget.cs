#region

using KoiVM.CFG;

#endregion

namespace KoiVM.AST.IL
{
    public class ILBlockTarget : IILOperand, IHasOffset
    {
        public ILBlockTarget(IBasicBlock target) => this.Target = target;

        public IBasicBlock Target
        {
            get;
            set;
        }

        public uint Offset => ((ILBlock)this.Target).Content[0].Offset;

        public override string ToString() => $"Block_{this.Target.Id:x2}";
    }
}