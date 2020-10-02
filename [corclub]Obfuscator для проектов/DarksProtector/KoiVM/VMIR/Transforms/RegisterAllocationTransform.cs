#region

using KoiVM.VMIR.RegAlloc;

#endregion

namespace KoiVM.VMIR.Transforms
{
    public class RegisterAllocationTransform : ITransform
    {
        public static readonly object RegAllocatorKey = new object();
        private RegisterAllocator allocator;

        public void Initialize(IRTransformer tr)
        {
            this.allocator = new RegisterAllocator(tr);
            this.allocator.Initialize();
            tr.Annotations[RegAllocatorKey] = this.allocator;
        }

        public void Transform(IRTransformer tr) => this.allocator.Allocate(tr.Block);
    }
}