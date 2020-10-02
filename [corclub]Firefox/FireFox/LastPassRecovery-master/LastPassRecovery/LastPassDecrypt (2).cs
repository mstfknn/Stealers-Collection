using System;
using System.IO;


namespace LastPassRecovery
{
    internal class LastPassDecrypt
    {
        const string PrefsFileName = "prefs.js";

        public void DecryptPasswordStoredInFirefox()
        {
            if (!Directory.Exists(GetFirefoxProfilesDirectory()))
                throw new IOException("Cannot find Firefox profiles folder");

            foreach (var folder in Directory.GetDirectories(GetFirefoxProfilesDirectory())) {
                var prefsFilePath = Path.Combine(folder, PrefsFileName);
                if (!File.Exists(prefsFilePath)) continue;

                var firefoxDecryptor = new FirefoxDecryptor();
                Console.WriteLine("Found password: {0} for email {1}", firefoxDecryptor.TryDecrypting(prefsFilePath), firefoxDecryptor.StoredEmailAddress);
            }
        }

        private static string GetFirefoxProfilesDirectory()
        {
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Mozilla", "Firefox", "Profiles");
        }
    }
}
