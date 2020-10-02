#region

using KoiVM.RT;

#endregion

namespace KoiVM.AST.IL
{
    public class ILDataTarget : IILOperand, IHasOffset
    {
        public ILDataTarget(BinaryChunk target) => this.Target = target;

        public BinaryChunk Target
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public uint Offset => this.Target.Offset;

        public override string ToString() => this.Name;
    }
}