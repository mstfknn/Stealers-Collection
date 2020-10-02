using System;
using System.Runtime.InteropServices;

namespace MsnPasswordFinder
{
    class NativeWin32API
    {

        [StructLayout(LayoutKind.Sequential,CharSet=CharSet.Unicode)]
        public struct CREDENTIAL
        {
            public uint Flags;
            public uint Type;
            public string                                               TargetName;
            public string                                               Comment;
            public System.Runtime.InteropServices.ComTypes.FILETIME     LastWritten;
            public uint CredentialBlobSize;
            public IntPtr                                               CredentialBlob;
            public uint Persist;
            public uint AttributeCount;
            public IntPtr                                               CredAttributes;
            public string                                               TargetAlias;
            public string                                               UserName;
        }
        

        [DllImport("advapi32.dll", EntryPoint = "CredEnumerateW", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern bool CredEnumerate(string Filter, uint Flags, out uint Count, out IntPtr Credentials);


        [DllImport("advapi32.dll", EntryPoint = "CredFree", SetLastError = true)]
        public static extern void CredFree(IntPtr Buffer);
        
    }
}
