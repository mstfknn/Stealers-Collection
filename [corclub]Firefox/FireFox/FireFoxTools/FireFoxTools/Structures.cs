using System.Runtime.InteropServices;

namespace FireFoxTools
{
    public class Structures
    {
        [StructLayout(LayoutKind.Sequential)]
        public struct TSECItem
        {
            public int SECItemType;
            public int SECItemData;
            public int SECItemLen;
        }
    }
}
