#region

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using KoiVM.Runtime.Data;
using KoiVM.Runtime.Dynamic;
using KoiVM.Runtime.Execution;
using KoiVM.Runtime.Execution.Internal;

#endregion

namespace What_a_great_VM
{
    internal unsafe class DarksVMInstance
    {
        [ThreadStatic] private static Dictionary<Module, DarksVMInstance> instances;
        private static readonly object initLock = new object();
        private static readonly Dictionary<Module, int> initialized = new Dictionary<Module, int>();

        private readonly Stack<DarksVMContext> ctxStack = new Stack<DarksVMContext>();
        private DarksVMContext currentCtx;

        private DarksVMInstance(DarksVMData data) => this.Data = data;

        public DarksVMData Data
        {
            get;
        }

        public static DarksVMInstance Instance(uint num, Module module)
        {
            if (instances == null) instances = new Dictionary<Module, DarksVMInstance>();
            if (!instances.TryGetValue(module, out DarksVMInstance inst))
            {
                inst = new DarksVMInstance(DarksVMData.Instance(module));
                instances[module] = inst;
                lock(initLock)
                {
                    if(!initialized.ContainsKey(module))
                    {
                        inst.Initialize();
                        initialized.Add(module, initialized.Count);
                    }
                }
            }
            return inst;
        }

        public static DarksVMInstance Instance(uint num, int id)
        {
            foreach(KeyValuePair<Module, int> entry in initialized)
                if(entry.Value == id)
                    return Instance(num, entry.Key);
            return null;
        }

        public static int GetModuleId(Module module) => initialized[module];

        private void Initialize()
        {
            DarksVMExportInfo initFunc = this.Data.LookupExport(DarksVMConstants.HELPER_INIT);
            ulong codeAddr = (ulong) (this.Data.KoiSection + initFunc.CodeOffset);
            this.Load(codeAddr, initFunc.EntryKey, initFunc.Signature, new object[0]);
        }

        public object Load(uint s2, uint s3, uint id, object[] arguments)
        {
            DarksVMExportInfo export = this.Data.LookupExport(id / 5 / 63493);
            ulong codeAddr = (ulong) (this.Data.KoiSection + export.CodeOffset);
            return this.Load(codeAddr, export.EntryKey, export.Signature, arguments);
        }

        public object Load(ulong codeAddr, uint key, uint sigId, object[] arguments)
        {
            DarksVMFuncSig sig = this.Data.LookupExport(sigId).Signature;
            return this.Load(codeAddr, key, sig, arguments);
        }

        public void Load(uint s2, uint s3, uint id, void*[] typedRefs, void* retTypedRef)
        {
            DarksVMExportInfo export = this.Data.LookupExport(id / 5 / 63493);
            ulong codeAddr = (ulong) (this.Data.KoiSection + export.CodeOffset);
            this.Load(codeAddr, export.EntryKey, export.Signature, typedRefs, retTypedRef);
        }

        public void Load(ulong codeAddr, uint key, uint sigId, void*[] typedRefs, void* retTypedRef)
        {
            DarksVMFuncSig sig = this.Data.LookupExport(sigId).Signature;
            this.Load(codeAddr, key, sig, typedRefs, retTypedRef);
        }

        private object Load(ulong codeAddr, uint key, DarksVMFuncSig sig, object[] arguments)
        {
            if(this.currentCtx != null)
                this.ctxStack.Push(this.currentCtx);
            this.currentCtx = new DarksVMContext(this);

            try
            {
                Debug.Assert(sig.ParamTypes.Length == arguments.Length);
                this.currentCtx.Stack.SetTopPosition((uint) arguments.Length + 1);
                for(uint i = 0; i < arguments.Length; i++) this.currentCtx.Stack[i + 1] = DarksVMSlot.FromObject(arguments[i], sig.ParamTypes[i]);
                this.currentCtx.Stack[(uint) arguments.Length + 1] = new DarksVMSlot {U8 = 1};

                this.currentCtx.Registers[DarksVMConstants.REG_K1] = new DarksVMSlot {U4 = key};
                this.currentCtx.Registers[DarksVMConstants.REG_BP] = new DarksVMSlot {U4 = 0};
                this.currentCtx.Registers[DarksVMConstants.REG_SP] = new DarksVMSlot {U4 = (uint) arguments.Length + 1};
                this.currentCtx.Registers[DarksVMConstants.REG_IP] = new DarksVMSlot {U8 = codeAddr};
                DarksVMDispatcher.Load(this.currentCtx);
                Debug.Assert(this.currentCtx.EHStack.Count == 0);

                object retVal = null;
                if(sig.RetType != typeof(void))
                {
                    DarksVMSlot retSlot = this.currentCtx.Registers[DarksVMConstants.REG_R0];
                    retVal = Type.GetTypeCode(sig.RetType) == TypeCode.String && retSlot.O == null
                        ? this.Data.LookupString(retSlot.U4)
                        : retSlot.ToObject(sig.RetType);
                }

                return retVal;
            }
            finally
            {
                this.currentCtx.Stack.FreeAllLocalloc();

                if(this.ctxStack.Count > 0)
                    this.currentCtx = this.ctxStack.Pop();
            }
        }

        private void Load(ulong codeAddr, uint key, DarksVMFuncSig sig, void*[] arguments, void* retTypedRef)
        {
            if(this.currentCtx != null)
                this.ctxStack.Push(this.currentCtx);
            this.currentCtx = new DarksVMContext(this);

            try
            {
                Debug.Assert(sig.ParamTypes.Length == arguments.Length);
                this.currentCtx.Stack.SetTopPosition((uint) arguments.Length + 1);
                for(uint i = 0; i < arguments.Length; i++)
                {
                    Type paramType = sig.ParamTypes[i];
                    if (!paramType.IsByRef)
                    {
                        TypedReference typedRef = *(TypedReference*)arguments[i];
                        this.currentCtx.Stack[i + 1] = DarksVMSlot.FromObject(TypedReference.ToObject(typedRef), __reftype(typedRef));
                    }
                    else
                    {
                        this.currentCtx.Stack[i + 1] = new DarksVMSlot { O = new TypedRef(arguments[i]) };
                    }
                }
                this.currentCtx.Stack[(uint) arguments.Length + 1] = new DarksVMSlot {U8 = 1};

                this.currentCtx.Registers[DarksVMConstants.REG_K1] = new DarksVMSlot {U4 = key};
                this.currentCtx.Registers[DarksVMConstants.REG_BP] = new DarksVMSlot {U4 = 0};
                this.currentCtx.Registers[DarksVMConstants.REG_SP] = new DarksVMSlot {U4 = (uint) arguments.Length + 1};
                this.currentCtx.Registers[DarksVMConstants.REG_IP] = new DarksVMSlot {U8 = codeAddr};
                DarksVMDispatcher.Load(this.currentCtx);
                Debug.Assert(this.currentCtx.EHStack.Count == 0);

                if(sig.RetType != typeof(void))
                    if (!sig.RetType.IsByRef)
                    {
                        DarksVMSlot retSlot = this.currentCtx.Registers[DarksVMConstants.REG_R0];
                        object retVal = Type.GetTypeCode(sig.RetType) == TypeCode.String && retSlot.O == null
                            ? this.Data.LookupString(retSlot.U4)
                            : retSlot.ToObject(sig.RetType);
                        TypedReferenceHelpers.SetTypedRef(retVal, retTypedRef);
                    }
                    else
                    {
                        object retRef = this.currentCtx.Registers[DarksVMConstants.REG_R0].O;
                        if (!(retRef is IReference))
                            //   throw new ExecutionEngineException();
                            ((IReference)retRef).ToTypedReference(this.currentCtx, retTypedRef, sig.RetType.GetElementType());
                    }
            }
            finally
            {
                this.currentCtx.Stack.FreeAllLocalloc();

                if(this.ctxStack.Count > 0)
                    this.currentCtx = this.ctxStack.Pop();
            }
        }
    }
}