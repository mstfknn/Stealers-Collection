#region

using KoiVM.RT;

#endregion

namespace KoiVM.AST.IL
{
    public class ILRelReference : IILOperand
    {
        public ILRelReference(IHasOffset target, IHasOffset relBase)
        {
            this.Target = target;
            this.Base = relBase;
        }

        public IHasOffset Target
        {
            get;
            set;
        }

        public IHasOffset Base
        {
            get;
            set;
        }

        public virtual uint Resolve(DarksVMRuntime runtime)
        {
            uint relBase = this.Base.Offset;
            if(this.Base is ILInstruction)
                relBase += runtime.serializer.ComputeLength((ILInstruction)this.Base);
            return this.Target.Offset - relBase;
        }

        public override string ToString() => $"[{this.Base.Offset:x8}:{this.Target.Offset:x8}]";
    }
}