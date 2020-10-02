namespace PCInfo
{
    using System;
    using System.Management;
    using System.Runtime.InteropServices;

    public partial class OSInfo
    {
        private static Structures.OSVERSIONINFOEX osVersionInfo = new Structures.OSVERSIONINFOEX();

        public static string GetServicePack()
        {
            var servicePack = string.Empty;
            if (NativeMethods.GetVersionEx(ref osVersionInfo))
            {
                servicePack = new Structures.OSVERSIONINFOEX
                {
                    dwOSVersionInfoSize = Marshal.SizeOf(typeof(Structures.OSVERSIONINFOEX))
                }.szCSDVersion;
            }

            return servicePack;
        }

        public static readonly OperatingSystem osVersion = Environment.OSVersion;
        public static readonly string MachineName = Environment.MachineName;
        public static readonly string UserName = Environment.UserName;
        public static readonly string SystemDir = Environment.SystemDirectory;
        public static readonly int ProcessorCount = Environment.ProcessorCount;
        private bool True = true;

        public static string OSBit
        {
            get
            {
                if (Environment.Is64BitOperatingSystem == True)
                {
                    return "x64";
                }
                else
                {
                    return "x32";
                }
            }
        }
        public static string OSName
        {
            get
            {
                try
                {
                    using (ManagementObjectCollection Mj = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_OperatingSystem").Get())
                    {
                        foreach (ManagementBaseObject cr in Mj)
                        {
                            return $"{cr["Caption"]} | (Ver: {cr["Version"]}) ";
                        }
                        return null;
                    }
                }
                catch { return "Unknown"; }
            }
        }

        private static string s_Name;

        #region PRODUCT

        private const int PRODUCT_UNDEFINED = 0x00000000;
        private const int PRODUCT_ULTIMATE = 0x00000001;
        private const int PRODUCT_HOME_BASIC = 0x00000002;
        private const int PRODUCT_HOME_PREMIUM = 0x00000003;
        private const int PRODUCT_ENTERPRISE = 0x00000004;
        private const int PRODUCT_HOME_BASIC_N = 0x00000005;
        private const int PRODUCT_BUSINESS = 0x00000006;
        private const int PRODUCT_STANDARD_SERVER = 0x00000007;
        private const int PRODUCT_DATACENTER_SERVER = 0x00000008;
        private const int PRODUCT_SMALLBUSINESS_SERVER = 0x00000009;
        private const int PRODUCT_ENTERPRISE_SERVER = 0x0000000A;
        private const int PRODUCT_STARTER = 0x0000000B;
        private const int PRODUCT_DATACENTER_SERVER_CORE = 0x0000000C;
        private const int PRODUCT_STANDARD_SERVER_CORE = 0x0000000D;
        private const int PRODUCT_ENTERPRISE_SERVER_CORE = 0x0000000E;
        private const int PRODUCT_ENTERPRISE_SERVER_IA64 = 0x0000000F;
        private const int PRODUCT_BUSINESS_N = 0x00000010;
        private const int PRODUCT_WEB_SERVER = 0x00000011;
        private const int PRODUCT_CLUSTER_SERVER = 0x00000012;
        private const int PRODUCT_HOME_SERVER = 0x00000013;
        private const int PRODUCT_STORAGE_EXPRESS_SERVER = 0x00000014;
        private const int PRODUCT_STORAGE_STANDARD_SERVER = 0x00000015;
        private const int PRODUCT_STORAGE_WORKGROUP_SERVER = 0x00000016;
        private const int PRODUCT_STORAGE_ENTERPRISE_SERVER = 0x00000017;
        private const int PRODUCT_SERVER_FOR_SMALLBUSINESS = 0x00000018;
        private const int PRODUCT_SMALLBUSINESS_SERVER_PREMIUM = 0x00000019;
        private const int PRODUCT_HOME_PREMIUM_N = 0x0000001A;
        private const int PRODUCT_ENTERPRISE_N = 0x0000001B;
        private const int PRODUCT_ULTIMATE_N = 0x0000001C;
        private const int PRODUCT_WEB_SERVER_CORE = 0x0000001D;
        private const int PRODUCT_MEDIUMBUSINESS_SERVER_MANAGEMENT = 0x0000001E;
        private const int PRODUCT_MEDIUMBUSINESS_SERVER_SECURITY = 0x0000001F;
        private const int PRODUCT_MEDIUMBUSINESS_SERVER_MESSAGING = 0x00000020;
        private const int PRODUCT_SERVER_FOUNDATION = 0x00000021;
        private const int PRODUCT_HOME_PREMIUM_SERVER = 0x00000022;
        private const int PRODUCT_SERVER_FOR_SMALLBUSINESS_V = 0x00000023;
        private const int PRODUCT_STANDARD_SERVER_V = 0x00000024;
        private const int PRODUCT_DATACENTER_SERVER_V = 0x00000025;
        private const int PRODUCT_ENTERPRISE_SERVER_V = 0x00000026;
        private const int PRODUCT_DATACENTER_SERVER_CORE_V = 0x00000027;
        private const int PRODUCT_STANDARD_SERVER_CORE_V = 0x00000028;
        private const int PRODUCT_ENTERPRISE_SERVER_CORE_V = 0x00000029;
        private const int PRODUCT_HYPERV = 0x0000002A;
        private const int PRODUCT_STORAGE_EXPRESS_SERVER_CORE = 0x0000002B;
        private const int PRODUCT_STORAGE_STANDARD_SERVER_CORE = 0x0000002C;
        private const int PRODUCT_STORAGE_WORKGROUP_SERVER_CORE = 0x0000002D;
        private const int PRODUCT_STORAGE_ENTERPRISE_SERVER_CORE = 0x0000002E;
        private const int PRODUCT_STARTER_N = 0x0000002F;
        private const int PRODUCT_PROFESSIONAL = 0x00000030;
        private const int PRODUCT_PROFESSIONAL_N = 0x00000031;
        private const int PRODUCT_SB_SOLUTION_SERVER = 0x00000032;
        private const int PRODUCT_SERVER_FOR_SB_SOLUTIONS = 0x00000033;
        private const int PRODUCT_STANDARD_SERVER_SOLUTIONS = 0x00000034;
        private const int PRODUCT_STANDARD_SERVER_SOLUTIONS_CORE = 0x00000035;
        private const int PRODUCT_SB_SOLUTION_SERVER_EM = 0x00000036;
        private const int PRODUCT_SERVER_FOR_SB_SOLUTIONS_EM = 0x00000037;
        private const int PRODUCT_SOLUTION_EMBEDDEDSERVER = 0x00000038;
        private const int PRODUCT_SOLUTION_EMBEDDEDSERVER_CORE = 0x00000039;
        private const int PRODUCT_ESSENTIALBUSINESS_SERVER_MGMT = 0x0000003B;
        private const int PRODUCT_ESSENTIALBUSINESS_SERVER_ADDL = 0x0000003C;
        private const int PRODUCT_ESSENTIALBUSINESS_SERVER_MGMTSVC = 0x0000003D;
        private const int PRODUCT_ESSENTIALBUSINESS_SERVER_ADDLSVC = 0x0000003E;
        private const int PRODUCT_SMALLBUSINESS_SERVER_PREMIUM_CORE = 0x0000003F;
        private const int PRODUCT_CLUSTER_SERVER_V = 0x00000040;
        private const int PRODUCT_EMBEDDED = 0x00000041;
        private const int PRODUCT_STARTER_E = 0x00000042;
        private const int PRODUCT_HOME_BASIC_E = 0x00000043;
        private const int PRODUCT_HOME_PREMIUM_E = 0x00000044;
        private const int PRODUCT_PROFESSIONAL_E = 0x00000045;
        private const int PRODUCT_ENTERPRISE_E = 0x00000046;
        private const int PRODUCT_ULTIMATE_E = 0x00000047;

        #endregion
        #region VERSIONS

        private const int VER_NT_WORKSTATION = 1;
        private const int VER_NT_DOMAIN_CONTROLLER = 2;
        private const int VER_NT_SERVER = 3;
        private const int VER_SUITE_SMALLBUSINESS = 1;
        private const int VER_SUITE_ENTERPRISE = 2;
        private const int VER_SUITE_TERMINAL = 16;
        private const int VER_SUITE_DATACENTER = 128;
        private const int VER_SUITE_SINGLEUSERTS = 256;
        private const int VER_SUITE_PERSONAL = 512;
        private const int VER_SUITE_BLADE = 1024;

        #endregion

        public static string Name
        {
            get
            {
                if (s_Name != null)
                {
                    return s_Name;
                }

                var name = "Unknown";
                osVersionInfo.dwOSVersionInfoSize = Marshal.SizeOf(typeof(Structures.OSVERSIONINFOEX));

                if (NativeMethods.GetVersionEx(ref osVersionInfo))
                {
                    switch (osVersion.Platform)
                    {
                        case PlatformID.Win32S: name = "Windows 3.1"; break;
                        case PlatformID.WinCE: name = "Windows CE"; break;

                        case PlatformID.Win32Windows:
                            {
                                if (osVersion.Version.Major == 4)
                                {
                                    switch (osVersion.Version.Minor)
                                    {
                                        case 0:
                                            {
                                                if (osVersionInfo.szCSDVersion == "B" || osVersionInfo.szCSDVersion == "C")
                                                {
                                                    name = "Windows 95 OSR2";
                                                }
                                                else
                                                {
                                                    name = "Windows 95";
                                                }
                                                break;
                                            }
                                        case 10:
                                            {
                                                if (osVersionInfo.szCSDVersion == "A")
                                                {
                                                    name = "Windows 98 Second Edition";
                                                }
                                                else
                                                {
                                                    name = "Windows 98";
                                                }
                                                break;
                                            }
                                        case 90:
                                            {
                                                name = "Windows Me";
                                                break;
                                            }
                                        default:
                                            break;
                                    }
                                }
                                break;
                            }
                        case PlatformID.Win32NT:
                            {
                                switch (osVersion.Version.Major)
                                {
                                    case 3:
                                        {
                                            name = "Windows NT 3.51"; break;
                                        }
                                    case 4:
                                        {
                                            switch (osVersionInfo.wProductType)
                                            {
                                                case 1:
                                                    {
                                                        name = "Windows NT 4.0";
                                                        break;
                                                    }
                                                case 3:
                                                    {
                                                        name = "Windows NT 4.0 Server";
                                                        break;
                                                    }
                                                default:
                                                    break;
                                            }
                                            break;
                                        }
                                    case 5:
                                        {
                                            switch (osVersion.Version.Minor)
                                            {
                                                case 0:
                                                    {
                                                        name = "Windows 2000";
                                                        break;
                                                    }
                                                case 1:
                                                    {
                                                        name = "Windows XP";
                                                        break;
                                                    }
                                                case 2:
                                                    {
                                                        name = "Windows Server 2003";
                                                        break;
                                                    }
                                                case 3:
                                                    {
                                                        name = "";
                                                        break;
                                                    }
                                                default:
                                                    break;
                                            }
                                            break;
                                        }
                                    case 6:
                                        switch (osVersion.Version.Minor)
                                        {
                                            case 0:
                                                switch (osVersionInfo.wProductType)
                                                {
                                                    case 1:
                                                        {
                                                            name = "Windows Vista";
                                                            break;
                                                        }
                                                    case 3:
                                                        {
                                                            name = "Windows Server 2008";
                                                            break;
                                                        }
                                                    default:
                                                        break;
                                                }
                                                break;

                                            case 1:
                                                switch (osVersionInfo.wProductType)
                                                {
                                                    case 1:
                                                        {
                                                            name = "Windows 7";
                                                            break;
                                                        }
                                                    case 3:
                                                        {
                                                            name = "Windows Server 2008 R2";
                                                            break;
                                                        }
                                                    default:
                                                        break;
                                                }
                                                break;
                                            default:
                                                break;
                                        }
                                        break;
                                    default:
                                        break;
                                }
                                break;
                            }
                        default:
                            break;
                    }
                }
                s_Name = name;
                return name;
            }
        }
    }
}
