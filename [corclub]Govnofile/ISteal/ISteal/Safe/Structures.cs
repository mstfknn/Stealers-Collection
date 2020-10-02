using System;

namespace ISteal.Safe
{
    public class Structures
    {
        public struct CryptprotectPromptstruct
        {
            public int cbSize;
            public int dwPromptFlags;
            internal IntPtr hwndApp;
            public string szPrompt;
        }

        public struct DataBlob 
        {
            public int cbData;
            internal IntPtr pbData;
        }
    }
}