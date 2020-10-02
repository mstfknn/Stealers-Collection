#region

using System;
using System.Diagnostics;
using System.Reflection;
using System.Reflection.Emit;
using KoiVM.Runtime.Dynamic;
using KoiVM.Runtime.Execution;
using KoiVM.Runtime.Execution.Internal;

#endregion

namespace KoiVM.Runtime.VCalls
{
    internal unsafe class Ecall : IVCall
    {
        public byte Code => DarksVMConstants.VCALL_ECALL;

        public void Load(DarksVMContext ctx, out ExecutionState state)
        {
            uint sp = ctx.Registers[DarksVMConstants.REG_SP].U4;
            DarksVMSlot mSlot = ctx.Stack[sp--];

            uint mId = mSlot.U4 & 0x3fffffff;
            byte opCode = (byte) (mSlot.U4 >> 30);
            var targetMethod = (MethodBase) ctx.Instance.Data.LookupReference(mId);
            bool typedInvoke = opCode == DarksVMConstants.ECALL_CALLVIRT_CONSTRAINED;
            if(!typedInvoke)
                typedInvoke = NeedTypedInvoke(ctx, sp, targetMethod, opCode == DarksVMConstants.ECALL_NEWOBJ);

            if (!typedInvoke)
            {
                this.InvokeNormal(ctx, targetMethod, opCode, ref sp, out state);
            }
            else
            {
                this.InvokeTyped(ctx, targetMethod, opCode, ref sp, out state);
            }
        }

        private static object PopObject(DarksVMContext ctx, Type type, ref uint sp)
        {
            DarksVMSlot arg = ctx.Stack[sp--];
            return Type.GetTypeCode(type) == TypeCode.String && arg.O == null ? ctx.Instance.Data.LookupString(arg.U4) : arg.ToObject(type);
        }

        private static IReference PopRef(DarksVMContext ctx, Type type, ref uint sp)
        {
            DarksVMSlot arg = ctx.Stack[sp];

            if(type.IsByRef)
            {
                sp--;
                type = type.GetElementType();
                return arg.O is Pointer ? new PointerRef(Pointer.Unbox(arg.O)) : arg.O is IReference ? (IReference) arg.O : new PointerRef((void*) arg.U8);
            }
            if (Type.GetTypeCode(type) == TypeCode.String && arg.O == null)
            {
                arg.O = ctx.Instance.Data.LookupString(arg.U4);
                ctx.Stack[sp] = arg;
            }
            return new StackRef(sp--);
        }

        private static bool NeedTypedInvoke(DarksVMContext ctx, uint sp, MethodBase method, bool isNewObj)
        {
            if(!isNewObj && !method.IsStatic)
                if(method.DeclaringType.IsValueType)
                    return true;
            foreach(ParameterInfo param in method.GetParameters())
                if(param.ParameterType.IsByRef)
                    return true;
            return method is MethodInfo && ((MethodInfo) method).ReturnType.IsByRef ? true : false;
        }

        private void InvokeNormal(DarksVMContext ctx, MethodBase targetMethod, byte opCode, ref uint sp, out ExecutionState state)
        {
            uint _sp = sp;
            ParameterInfo[] parameters = targetMethod.GetParameters();
            object self = null;
            object[] args = new object[parameters.Length];
            if (opCode != DarksVMConstants.ECALL_CALL || !targetMethod.IsVirtual)
            {
                args = new object[parameters.Length];
                for (int i = parameters.Length - 1; i >= 0; i--)
                    args[i] = PopObject(ctx, parameters[i].ParameterType, ref sp);
                if (!targetMethod.IsStatic && opCode != DarksVMConstants.ECALL_NEWOBJ)
                {
                    self = PopObject(ctx, targetMethod.DeclaringType, ref sp);

                    if (self != null && !targetMethod.DeclaringType.IsInstanceOfType(self))
                    {
                        // ConfuserEx sometimes produce this to circumvent peverify (see ref proxy)
                        // Reflection won't allow it, so use typed invoke
                        this.InvokeTyped(ctx, targetMethod, opCode, ref _sp, out state);
                        return;
                    }
                }
            }
            else
            {
                int indexOffset = targetMethod.IsStatic ? 0 : 1;
                args = new object[parameters.Length + indexOffset];
                for (int i = parameters.Length - 1; i >= 0; i--)
                    args[i + indexOffset] = PopObject(ctx, parameters[i].ParameterType, ref sp);
                if (!targetMethod.IsStatic)
                    args[0] = PopObject(ctx, targetMethod.DeclaringType, ref sp);

                targetMethod = DirectCall.GetDirectInvocationProxy(targetMethod);
            }

            object result;
            if (opCode != DarksVMConstants.ECALL_NEWOBJ)
            {
                if (!targetMethod.IsStatic && self == null)
                    throw new NullReferenceException();

                Type selfType;
                if (self == null || !(selfType = self.GetType()).IsArray || targetMethod.Name != "SetValue")
                {
                    try
                    {
                        result = targetMethod.Invoke(self, args);
                    }
                    catch (TargetInvocationException ex)
                    {
                        DarksVMDispatcher.DoThrow(ctx, ex.InnerException);
                        throw;
                    }
                }
                else
                {
                    Type valueType = args[0] == null ? selfType.GetElementType() : args[0].GetType();
                    ArrayStoreHelpers.SetValue((Array)self, (int)args[1], args[0], valueType, selfType.GetElementType());
                    result = null;
                }
            }
            else
            {
                try
                {
                    result = ((ConstructorInfo)targetMethod).Invoke(args);
                }
                catch (TargetInvocationException ex)
                {
                    EHHelper.Rethrow(ex.InnerException, null);
                    throw;
                }
            }

            if (targetMethod is MethodInfo && ((MethodInfo) targetMethod).ReturnType != typeof(void)) ctx.Stack[++sp] = DarksVMSlot.FromObject(result, ((MethodInfo) targetMethod).ReturnType);
            else if(opCode == DarksVMConstants.ECALL_NEWOBJ) ctx.Stack[++sp] = DarksVMSlot.FromObject(result, targetMethod.DeclaringType);

            ctx.Stack.SetTopPosition(sp);
            ctx.Registers[DarksVMConstants.REG_SP].U4 = sp;
            state = ExecutionState.Next;
        }

        private void InvokeTyped(DarksVMContext ctx, MethodBase targetMethod, byte opCode, ref uint sp, out ExecutionState state)
        {
            ParameterInfo[] parameters = targetMethod.GetParameters();
            int paramCount = parameters.Length;
            if(!targetMethod.IsStatic && opCode != DarksVMConstants.ECALL_NEWOBJ)
                paramCount++;

            Type constrainType = null;
            if(opCode == DarksVMConstants.ECALL_CALLVIRT_CONSTRAINED) constrainType = (Type) ctx.Instance.Data.LookupReference(ctx.Stack[sp--].U4);

            int indexOffset = targetMethod.IsStatic || opCode == DarksVMConstants.ECALL_NEWOBJ ? 0 : 1;
            var references = new IReference[paramCount];
            var types = new Type[paramCount];
            for(int i = paramCount - 1; i >= 0; i--)
            {
                Type paramType;
                if(!targetMethod.IsStatic && opCode != DarksVMConstants.ECALL_NEWOBJ)
                    if(i == 0)
                    {
                        if(!targetMethod.IsStatic)
                        {
                            DarksVMSlot thisSlot = ctx.Stack[sp];
                            if(thisSlot.O is ValueType && !targetMethod.DeclaringType.IsValueType)
                            {
                                Debug.Assert(targetMethod.DeclaringType.IsInterface);
                                Debug.Assert(opCode == DarksVMConstants.ECALL_CALLVIRT);
                                // Interface dispatch on valuetypes => use constrained. invocation
                                constrainType = thisSlot.O.GetType();
                            }
                        }

                        paramType = constrainType != null ? constrainType.MakeByRefType() : targetMethod.DeclaringType.IsValueType ? targetMethod.DeclaringType.MakeByRefType() : targetMethod.DeclaringType;
                    }
                    else
                    {
                        paramType = parameters[i - 1].ParameterType;
                    }
                else paramType = parameters[i].ParameterType;
                references[i] = PopRef(ctx, paramType, ref sp);
                if(paramType.IsByRef)
                    paramType = paramType.GetElementType();
                types[i] = paramType;
            }

            OpCode callOp;
            Type retType;
            if (opCode != DarksVMConstants.ECALL_CALL)
            {
                if (opCode != DarksVMConstants.ECALL_CALLVIRT &&
                                   opCode != DarksVMConstants.ECALL_CALLVIRT_CONSTRAINED)
                {
                    if (opCode != DarksVMConstants.ECALL_NEWOBJ)
                    {
                        throw new InvalidProgramException();
                    }
                    else
                    {
                        callOp = System.Reflection.Emit.OpCodes.Newobj;
                        retType = targetMethod.DeclaringType;
                    }
                }
                else
                {
                    callOp = System.Reflection.Emit.OpCodes.Callvirt;
                    retType = targetMethod is MethodInfo ? ((MethodInfo)targetMethod).ReturnType : typeof(void);
                }
            }
            else
            {
                callOp = System.Reflection.Emit.OpCodes.Call;
                retType = targetMethod is MethodInfo ? ((MethodInfo)targetMethod).ReturnType : typeof(void);
            }
            DirectCall.TypedInvocation proxy = DirectCall.GetTypedInvocationProxy(targetMethod, callOp, constrainType);

            object result = proxy(ctx, references, types);

            if (retType != typeof(void))
            {
                ctx.Stack[++sp] = DarksVMSlot.FromObject(result, retType);
            }
            else
            {
                if (opCode == DarksVMConstants.ECALL_NEWOBJ) ctx.Stack[++sp] = DarksVMSlot.FromObject(result, retType);
            }

            ctx.Stack.SetTopPosition(sp);
            ctx.Registers[DarksVMConstants.REG_SP].U4 = sp;
            state = ExecutionState.Next;
        }
    }
}