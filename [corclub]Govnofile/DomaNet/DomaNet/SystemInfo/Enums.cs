namespace DomaNet.SystemInfo
{
    internal class Enums
    {
        public enum SoftwareArchitecture
        {
            Unknown = 0,
            Bit32 = 1,
            Bit64 = 2
        }
        public enum ProcessorArchitecture
        {
            Unknown = 0,
            Bit32 = 1,
            Bit64 = 2,
            Itanium64 = 3
        }
        public enum ALG_ID
        {
            CALG_MD5 = 0x8003,
            CALG_SHA1 = 0x8004
        }
        public enum HashParameters
        {
            HP_ALGID = 1,
            HP_HASHSIZE = 4,
            HP_HASHVAL = 2
        }
    }
}