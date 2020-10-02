using System.Runtime.InteropServices;

namespace MozillaDecrypt
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