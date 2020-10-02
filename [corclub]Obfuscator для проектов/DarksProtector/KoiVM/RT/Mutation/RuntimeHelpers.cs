#region

using dnlib.DotNet;
using dnlib.DotNet.Emit;
using KoiVM.AST.IL;
using KoiVM.AST.IR;
using KoiVM.CFG;
using KoiVM.VM;
using KoiVM.VMIL;
using KoiVM.VMIR;

#endregion

namespace KoiVM.RT.Mutation
{
    public class RuntimeHelpers
    {
        private RTConstants constants;

        private MethodDef methodINIT;
        private readonly DarksVMRuntime rt;
        private TypeDef rtHelperType;
        private readonly ModuleDef rtModule;

        public RuntimeHelpers(RTConstants constants, DarksVMRuntime rt, ModuleDef rtModule)
        {
            this.rt = rt;
            this.rtModule = rtModule;
            this.constants = constants;
            this.rtHelperType = new TypeDefUser("KoiVM.Runtime", "Helpers");
            this.AllocateHelpers();
        }

        public uint INIT
        {
            get;
            private set;
        }

        private MethodDef CreateHelperMethod(string name)
        {
            var helper = new MethodDefUser(name, MethodSig.CreateStatic(this.rtModule.CorLibTypes.Void))
            {
                Body = new CilBody()
            };
            return helper;
        }

        private void AllocateHelpers()
        {
            this.methodINIT = this.CreateHelperMethod("INIT");
            this.INIT = this.rt.Descriptor.Data.GetExportId(this.methodINIT);
        }

        public void AddHelpers()
        {
            var scope = new ScopeBlock();

            var initBlock = new BasicBlock<IRInstrList>(1, new IRInstrList
            {
                new IRInstruction(IROpCode.RET)
            });
            scope.Content.Add(initBlock);

            var retnBlock = new BasicBlock<IRInstrList>(0, new IRInstrList
            {
                new IRInstruction(IROpCode.VCALL, IRConstant.FromI4(this.rt.Descriptor.Runtime.VMCall[DarksVMCalls.EXIT]))
            });
            scope.Content.Add(initBlock);

            this.CompileHelpers(this.methodINIT, scope);

            DarksVMMethodInfo info = this.rt.Descriptor.Data.LookupInfo(this.methodINIT);
            scope.ProcessBasicBlocks<ILInstrList>(block =>
            {
                if(block.Id == 1)
                {
                    this.AddHelper(null, this.methodINIT, (ILBlock) block);
                    VMBlockKey blockKey = info.BlockKeys[block];
                    info.EntryKey = blockKey.EntryKey;
                    info.ExitKey = blockKey.ExitKey;
                }
                this.rt.AddBlock(this.methodINIT, (ILBlock) block);
            });
        }

        private void AddHelper(DarksVMMethodInfo info, MethodDef method, ILBlock block)
        {
            var helperScope = new ScopeBlock();
            block.Id = 0;
            helperScope.Content.Add(block);
            if(info != null)
            {
                var helperInfo = new DarksVMMethodInfo();
                VMBlockKey keys = info.BlockKeys[block];
                helperInfo.RootScope = helperScope;
                helperInfo.EntryKey = keys.EntryKey;
                helperInfo.ExitKey = keys.ExitKey;
                this.rt.Descriptor.Data.SetInfo(method, helperInfo);
            }
            this.rt.AddHelper(method, helperScope, block);
        }

        private void CompileHelpers(MethodDef method, ScopeBlock scope)
        {
            var methodCtx = new IRContext(method, method.Body)
            {
                IsRuntime = true
            };
            var irTransformer = new IRTransformer(scope, methodCtx, this.rt);
            irTransformer.Transform();

            var ilTranslator = new ILTranslator(this.rt);
            var ilTransformer = new ILTransformer(method, scope, this.rt);
            ilTranslator.Translate(scope);
            ilTransformer.Transform();

            var postTransformer = new ILPostTransformer(method, scope, this.rt);
            postTransformer.Transform();
        }
    }
}