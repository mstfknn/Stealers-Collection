#region

using System;
using System.Collections.Generic;
using dnlib.DotNet;
using dnlib.DotNet.Emit;
using KoiVM.AST;
using KoiVM.AST.ILAST;
using KoiVM.AST.IR;

#endregion

namespace KoiVM.VMIR
{
    public class IRContext
    {
        private readonly IRVariable[] args, locals;
        private readonly Dictionary<ExceptionHandler, IRVariable> ehVars;
        private readonly Dictionary<ILASTVariable, IRVariable> varMap = new Dictionary<ILASTVariable, IRVariable>();
        private readonly List<IRVariable> vRegs = new List<IRVariable>();

        public IRContext(MethodDef method, CilBody body)
        {
            this.Method = method;
            this.IsRuntime = false;

            this.locals = new IRVariable[body.Variables.Count];
            for(int i = 0; i < this.locals.Length; i++)
            {
                if(body.Variables[i].Type.IsPinned)
                    throw new NotSupportedException("Pinned variables are not supported.");

                this.locals[i] = new IRVariable
                {
                    Id = i,
                    Name = $"local_{i}",
                    Type = TypeInference.ToASTType(body.Variables[i].Type),
                    RawType = body.Variables[i].Type,
                    VariableType = IRVariableType.Local
                };
            }

            this.args = new IRVariable[method.Parameters.Count];
            for(int i = 0; i < this.args.Length; i++)
                this.args[i] = new IRVariable
                {
                    Id = i,
                    Name = $"arg_{i}",
                    Type = TypeInference.ToASTType(method.Parameters[i].Type),
                    RawType = method.Parameters[i].Type,
                    VariableType = IRVariableType.Argument
                };

            this.ehVars = new Dictionary<ExceptionHandler, IRVariable>();
            int id = -1;
            foreach(ExceptionHandler eh in body.ExceptionHandlers)
            {
                id++;
                if(eh.HandlerType == ExceptionHandlerType.Fault ||
                   eh.HandlerType == ExceptionHandlerType.Finally)
                    continue;
                var type = eh.CatchType.ToTypeSig();
                this.ehVars.Add(eh, new IRVariable
                {
                    Id = id,
                    Name = $"ex_{id}",
                    Type = TypeInference.ToASTType(type),
                    RawType = type,
                    VariableType = IRVariableType.VirtualRegister
                });
            }
        }

        public MethodDef Method
        {
            get;
        }

        public bool IsRuntime
        {
            get;
            set;
        }

        public IRVariable AllocateVRegister(ASTType type)
        {
            var vReg = new IRVariable
            {
                Id = this.vRegs.Count,
                Name = $"vreg_{this.vRegs.Count}",
                Type = type,
                VariableType = IRVariableType.VirtualRegister
            };
            this.vRegs.Add(vReg);
            return vReg;
        }

        public IRVariable ResolveVRegister(ILASTVariable variable)
        {
            if(variable.VariableType == ILASTVariableType.ExceptionVar)
                return this.ResolveExceptionVar((ExceptionHandler) variable.Annotation);

            if (this.varMap.TryGetValue(variable, out IRVariable vReg))
                return vReg;
            vReg = this.AllocateVRegister(variable.Type);
            this.varMap[variable] = vReg;
            return vReg;
        }

        public IRVariable ResolveParameter(Parameter param) => this.args[param.Index];

        public IRVariable ResolveLocal(Local local) => this.locals[local.Index];

        public IRVariable[] GetParameters() => this.args;

        public IRVariable ResolveExceptionVar(ExceptionHandler eh) => this.ehVars[eh];
    }
}