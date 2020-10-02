namespace SuperStealer
{
    using System;

    public enum STATURL_QUERYFLAGS : uint
    {
        STATURL_QUERYFLAG_ISCACHED = 0x10000,
        STATURL_QUERYFLAG_NOTITLE = 0x40000,
        STATURL_QUERYFLAG_NOURL = 0x20000,
        STATURL_QUERYFLAG_TOPLEVEL = 0x80000
    }
}

