using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace LastPassRecovery
{
    internal class FirefoxDecryptor
    {
        private string StoredPassword { get; set; }
        internal string StoredEmailAddress { get; private set; }


        public string TryDecrypting(string prefsFilePath)
        {
            RetrieveStoredPasswordAndEmail(prefsFilePath);
            if (string.IsNullOrEmpty(StoredPassword)) throw new InvalidDataException("Could not find extensions.lastpass.loginpws entry in the prefs.js.");
            if (string.IsNullOrEmpty(StoredEmailAddress)) throw new InvalidDataException("Could not find extensions.lastpass.loginusers entry in the prefs.js.");

            return DecryptPassword(GetEmailSha256Hash(), ConvertStoredPasswordToEncryptedForm()).Replace("\b", "");
        }

        private void RetrieveStoredPasswordAndEmail(string prefsFilePath)
        {
            //try to find the password and email in the Firefox profile
            var regex = new Regex(@"^user_pref\(""extensions.lastpass.login(pws|users)"", ""([^""]+)""\);$");
            using (var reader = new StreamReader(prefsFilePath))
                while (!reader.EndOfStream && (string.IsNullOrEmpty(StoredPassword) || string.IsNullOrEmpty(StoredEmailAddress))) {
                    var line = reader.ReadLine();
                    if (line == null) continue;

                    var match = regex.Match(line);
                    if (!match.Success) continue;

                    var key = match.Groups[1].Value;
                    var value = match.Groups[2].Value;
                    if (key == "pws") StoredPassword = value;
                    if (key == "users") StoredEmailAddress = Uri.UnescapeDataString(value);
                }
        }
        
        private byte[] ConvertStoredPasswordToEncryptedForm()
        {
            var passwordArray = Convert.FromBase64String(StoredPassword);
            var emailArray = Encoding.ASCII.GetBytes(StoredEmailAddress);
            var encryptedData = new byte[passwordArray.Length + emailArray.Length];
            Array.Copy(passwordArray, encryptedData, passwordArray.Length);
            Array.Copy(emailArray, 0, encryptedData, passwordArray.Length, emailArray.Length);
            var decryptedData = ProtectedData.Unprotect(encryptedData, null, DataProtectionScope.CurrentUser);
            var decryptedString = Uri.UnescapeDataString(Encoding.ASCII.GetString(decryptedData));
            var encryptedPasswordStart = decryptedString.IndexOf("=", StringComparison.Ordinal) + 1;
            if (encryptedPasswordStart <= 1)
                throw new InvalidDataException("Cound not find encrypted password in retrieved data.");
            var encryptedPassword = decryptedString.Substring(encryptedPasswordStart);
            encryptedPassword = encryptedPassword.Remove(encryptedPassword.Length - 1);

            return Convert.FromBase64String(encryptedPassword);
        }

        private static string DecryptPassword(byte[] shaEmail, byte[] encryptedPasswordData)
        {
            using (var aes = new RijndaelManaged() { Mode = CipherMode.ECB, Padding = PaddingMode.None }) {
                aes.Key = shaEmail;
                var decryptor = aes.CreateDecryptor(aes.Key, null);
                using (var memoryStream = new MemoryStream(encryptedPasswordData))
                using (var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                using (var streamReader = new StreamReader(cryptoStream))

                    return streamReader.ReadToEnd();
            }
        }

        private byte[] GetEmailSha256Hash()
        {
            var sha256 = new SHA256Managed();
            var shaEmail = sha256.ComputeHash(Encoding.UTF8.GetBytes(StoredEmailAddress));

            return shaEmail;
        }
    }
}