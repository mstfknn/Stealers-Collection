namespace DomaNet.SystemInfo
{
    using System;
    using System.Runtime.InteropServices;

    public class Structures
    {
        [StructLayout(LayoutKind.Sequential)]
        public struct OSVERSIONINFOEX
        {
            public int dwOSVersionInfoSize;
            public int dwMajorVersion;
            public int dwMinorVersion;
            public int dwBuildNumber;
            public int dwPlatformId;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string szCSDVersion;
            public short wServicePackMajor;
            public short wServicePackMinor;
            public short wSuiteMask;
            public byte wProductType;
            public byte wReserved;
        }
        [StructLayout(LayoutKind.Sequential)]
        public struct SYSTEM_INFO
        {
            internal PROCESSOR_INFO_UNION uProcessorInfo;
            public uint dwPageSize;
            internal IntPtr lpMinimumApplicationAddress;
            internal IntPtr lpMaximumApplicationAddress;
            internal IntPtr dwActiveProcessorMask;
            public uint dwNumberOfProcessors;
            public uint dwProcessorType;
            public uint dwAllocationGranularity;
            public ushort dwProcessorLevel;
            public ushort dwProcessorRevision;
        }
        [StructLayout(LayoutKind.Explicit)]
        public struct PROCESSOR_INFO_UNION
        {
            [FieldOffset(0)]
            internal uint dwOemId;
            [FieldOffset(0)]
            internal ushort wProcessorArchitecture;
            [FieldOffset(2)]
            internal ushort wReserved;
        }
        [StructLayout(LayoutKind.Sequential)]
        internal struct PROCESS_INFORMATION
        {
            public IntPtr hProcess;
            public IntPtr hThread;
            public int dwProcessId;
            public int dwThreadId;
        }
        [StructLayout(LayoutKind.Sequential)]
        internal struct IELAUNCHURLINFO
        {
            public int cbSize;
            public int dwCreationFlags;
        }
        [StructLayout(LayoutKind.Sequential)]
        public struct SYSTEMTIME
        {
            public short Day;
            public short DayOfWeek;
            public short Hour;
            public short Milliseconds;
            public short Minute;
            public short Month;
            public short Second;
            public short Year;
        }
        [StructLayout(LayoutKind.Sequential)]
        public struct SHFILEINFO
        {
            internal IntPtr hIcon;
            internal IntPtr iIcon;
            public uint dwAttributes;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
            public string szDisplayName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
            public string szTypeName;
        }
        [StructLayout(LayoutKind.Sequential)]
        public struct IEAutoComplteSecretHeader
        {
            public uint dwSize;
            public uint dwSecretInfoSize;
            public uint dwSecretSize;
            public IESecretInfoHeader IESecretHeader;
        }
        [StructLayout(LayoutKind.Sequential)]
        public struct IESecretInfoHeader
        {
            public uint dwIdHeader;
            public uint dwSize;
            public uint dwTotalSecrets;
            public uint unknown;
            public uint id4;
            public uint unknownZero;
        }
        [StructLayout(LayoutKind.Explicit)]
        public struct SecretEntry
        {
            [FieldOffset(12)]
            public uint dwLength;
            [FieldOffset(0)]
            public uint dwOffset;
            [FieldOffset(4)]
            public byte SecretId;
            [FieldOffset(5)]
            public byte SecretId1;
            [FieldOffset(6)]
            public byte SecretId2;
            [FieldOffset(7)]
            public byte SecretId3;
            [FieldOffset(8)]
            public byte SecretId4;
            [FieldOffset(9)]
            public byte SecretId5;
            [FieldOffset(10)]
            public byte SecretId6;
            [FieldOffset(11)]
            public byte SecretId7;
        }
    }
}