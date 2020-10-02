#region

using dnlib.DotNet;
using dnlib.DotNet.Emit;
using KoiVM.AST.IR;

#endregion

namespace KoiVM.AST
{
    public class InstrAnnotation
    {
        public static readonly InstrAnnotation JUMP = new InstrAnnotation("JUMP");

        public InstrAnnotation(string name) => this.Name = name;

        public string Name
        {
            get;
        }

        public override string ToString() => this.Name;
    }

    public class InstrCallInfo : InstrAnnotation
    {
        public InstrCallInfo(string name)
            : base(name)
        {
        }

        public ITypeDefOrRef ConstrainType
        {
            get;
            set;
        }

        public IMethod Method
        {
            get;
            set;
        }

        public IIROperand[] Arguments
        {
            get;
            set;
        }

        public IIROperand ReturnValue
        {
            get;
            set;
        }

        public IRRegister ReturnRegister
        {
            get;
            set;
        }

        public IRPointer ReturnSlot
        {
            get;
            set;
        }

        public bool IsECall
        {
            get;
            set;
        }

        public override string ToString() => $"{base.ToString()} {this.Method}";
    }

    public class PointerInfo : InstrAnnotation
    {
        public PointerInfo(string name, ITypeDefOrRef ptrType)
            : base(name) => this.PointerType = ptrType;

        public ITypeDefOrRef PointerType
        {
            get;
            set;
        }
    }

    public class EHInfo : InstrAnnotation
    {
        public EHInfo(ExceptionHandler eh)
            : base($"EH_{eh.GetHashCode()}") => this.ExceptionHandler = eh;

        public ExceptionHandler ExceptionHandler
        {
            get;
            set;
        }
    }
}