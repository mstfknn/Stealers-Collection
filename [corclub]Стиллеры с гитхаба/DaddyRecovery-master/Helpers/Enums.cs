namespace DaddyRecovery.Helpers
{
    using System;

    public static class Enums
    {
        public enum KeyIndexWin : int
        {
            KEY_START_INDEX = 0x34,
            KEY_END_INDEX = KEY_START_INDEX + 0xF,
            DECODE_LENGTH = 0x1D,
            DECODE_STRING = 0xF
        }

        public enum Type
        {
            Sequence = 0x30, // 48
            Integer = 0x02,
            BitString = 0x03,
            OctetString = 0x04,
            Null = 0x05,
            ObjectIdentifier = 0x06
        }
    }
}