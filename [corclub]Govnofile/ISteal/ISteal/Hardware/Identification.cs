using Microsoft.Win32;

namespace ISteal.Hardware
{
    public class Identification
    {
        public static string GetID(bool Recursive, string GuID, string MachineGuid = @"SOFTWARE\Microsoft\Cryptography")
        {
            try
            {
                using (var Key = Registry.LocalMachine.OpenSubKey(MachineGuid, Recursive))
                {
                    return Key?.GetValue(GuID)?.ToString().ToUpper().Replace("-",string.Empty);
                }
            }
            catch
            {
                return "HWID not found";
            }
        }
    }
}
