namespace PEngine.Helpers
{
    internal static class Enums
    {
        public enum INTERNET_OPTION : int
        {
            INTERNET_OPTION_REFRESH = 0x25,
            INTERNET_OPTION_SETTINGS_CHANGED = 0x27,
        }

        public enum KeyIndexWin : int
        {
            KEY_START_INDEX = 0x34,
            KEY_END_INDEX = KEY_START_INDEX + 0xF,
            DECODE_LENGTH = 0x1D,
            DECODE_STRING = 0xF
        }
    }
}