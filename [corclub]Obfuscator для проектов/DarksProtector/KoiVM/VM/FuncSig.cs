#region

using dnlib.DotNet;

#endregion

namespace KoiVM.VM
{
    public class FuncSig
    {
        public byte Flags;
        public ITypeDefOrRef[] ParamSigs;
        public ITypeDefOrRef RetType;

        public override int GetHashCode()
        {
            var comparer = new SigComparer();
            int hashCode = this.Flags;
            foreach(ITypeDefOrRef param in this.ParamSigs)
                hashCode = (hashCode * 7) + comparer.GetHashCode(param);
            return (hashCode * 7) + comparer.GetHashCode(this.RetType);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is FuncSig other) || other.Flags != this.Flags)
                return false;

            if (other.ParamSigs.Length != this.ParamSigs.Length)
                return false;
            var comparer = new SigComparer();
            for(int i = 0; i < this.ParamSigs.Length; i++)
                if(!comparer.Equals(this.ParamSigs[i], other.ParamSigs[i]))
                    return false;
            return !comparer.Equals(this.RetType, other.RetType) ? false : true;
        }
    }
}