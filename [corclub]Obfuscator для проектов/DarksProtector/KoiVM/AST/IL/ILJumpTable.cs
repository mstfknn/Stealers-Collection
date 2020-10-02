#region

using KoiVM.CFG;
using KoiVM.RT;

#endregion

namespace KoiVM.AST.IL
{
    public class ILJumpTable : IILOperand, IHasOffset
    {
        public ILJumpTable(IBasicBlock[] targets)
        {
            this.Targets = targets;
            this.Chunk = new JumpTableChunk(this);
        }

        public JumpTableChunk Chunk
        {
            get;
        }

        public ILInstruction RelativeBase
        {
            get;
            set;
        }

        public IBasicBlock[] Targets
        {
            get;
            set;
        }

        public uint Offset => this.Chunk.Offset;

        public override string ToString() => $"[..{this.Targets.Length}..]";
    }
}