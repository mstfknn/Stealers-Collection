using System;

public partial class Enums
{
    [Flags]
    public enum Shlwapi_URL : uint
    {
        URL_DONT_SIMPLIFY = 0x8000000,
        URL_ESCAPE_PERCENT = 0x1000,
        URL_ESCAPE_SPACES_ONLY = 0x4000000,
        URL_ESCAPE_UNSAFE = 0x20000000,
        URL_PLUGGABLE_PROTOCOL = 0x40000000,
        URL_UNESCAPE = 0x10000000
    }

    [Flags]
    public enum STATURLFLAGS : uint
    {
        STATURLFLAG_ISCACHED = 1,
        STATURLFLAG_ISTOPLEVEL = 2
    }

    [Flags]
    public enum STATURL_QUERYFLAGS : uint
    {
        STATURL_QUERYFLAG_ISCACHED = 0x10000,
        STATURL_QUERYFLAG_NOTITLE = 0x40000,
        STATURL_QUERYFLAG_NOURL = 0x20000,
        STATURL_QUERYFLAG_TOPLEVEL = 0x80000
    }

    [Flags]
    public enum ADDURL_FLAG : uint
    {
        ADDURL_ADDTOCACHE = 1,
        ADDURL_ADDTOHISTORYANDCACHE = 0
    }

    [Flags]
    public enum ALG_ID : uint
    {
        CALG_MD5 = 0x8003,
        CALG_SHA1 = 0x8004
    }

    [Flags]
    public enum HashParameters : uint
    {
        HP_ALGID = 1,
        HP_HASHSIZE = 4,
        HP_HASHVAL = 2
    }
}