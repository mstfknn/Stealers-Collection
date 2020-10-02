#region

using System.Collections.Generic;
using KoiVM.AST.IR;

#endregion

namespace KoiVM.VMIR.RegAlloc
{
    public class BlockLiveness
    {
        private BlockLiveness(HashSet<IRVariable> inLive, HashSet<IRVariable> outLive)
        {
            this.InLive = inLive;
            this.OutLive = outLive;
        }

        public HashSet<IRVariable> InLive
        {
            get;
        }

        public HashSet<IRVariable> OutLive
        {
            get;
        }

        internal static BlockLiveness Empty() => new BlockLiveness(new HashSet<IRVariable>(), new HashSet<IRVariable>());

        internal BlockLiveness Clone() => new BlockLiveness(new HashSet<IRVariable>(this.InLive), new HashSet<IRVariable>(this.OutLive));

        public override string ToString() => $"In=[{string.Join(", ", this.InLive)}], Out=[{string.Join(", ", this.OutLive)}]";
    }
}