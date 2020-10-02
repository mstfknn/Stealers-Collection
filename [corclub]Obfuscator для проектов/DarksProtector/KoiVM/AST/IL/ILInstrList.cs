#region

using System;
using System.Collections.Generic;

#endregion

namespace KoiVM.AST.IL
{
    public class ILInstrList : List<ILInstruction>
    {
        public override string ToString() => string.Join(Environment.NewLine, this);

        public void VisitInstrs<T>(VisitFunc<ILInstrList, ILInstruction, T> visitFunc, T arg)
        {
            for(int i = 0; i < this.Count; i++)
                visitFunc(this, this[i], ref i, arg);
        }
    }
}