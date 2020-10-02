using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Confuser.Renamer
{
    public class ReversibleRenamer {
        readonly RijndaelManaged cipher;
        readonly byte[] key;

		public ReversibleRenamer(string password) {
            this.cipher = new RijndaelManaged();
			using (var sha = SHA256.Create())
                this.cipher.Key = this.key = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
		}

        static string Base64Encode(byte[] buf) => Convert.ToBase64String(buf).Trim('=').Replace('+', '$').Replace('/', '_');

        static byte[] Base64Decode(string str) {
			str = str.Replace('$', '+').Replace('_', '/').PadRight((str.Length + 3) & ~3, '=');
			return Convert.FromBase64String(str);
		}

		byte[] GetIV(byte ivId) {
			byte[] iv = new byte[this.cipher.BlockSize / 8];
			for (int i = 0; i < iv.Length; i++)
				iv[i] = (byte)(ivId ^ this.key[i]);
			return iv;
		}

		byte GetIVId(string str) {
			byte x = (byte)str[0];
			for (int i = 1; i < str.Length; i++)
				x = (byte)(x * 3 + (byte)str[i]);
			return x;
		}

		public string Encrypt(string name) {
			byte ivId = this.GetIVId(name);
            this.cipher.IV = this.GetIV(ivId);
            byte[] buf = Encoding.UTF8.GetBytes(name);

			using (var ms = new MemoryStream()) {
				ms.WriteByte(ivId);
				using (var stream = new CryptoStream(ms, this.cipher.CreateEncryptor(), CryptoStreamMode.Write))
					stream.Write(buf, 0, buf.Length);

				buf = ms.ToArray();
				return Base64Encode(buf);
			}
		}

		public string Decrypt(string name) {
			using (var ms = new MemoryStream(Base64Decode(name))) {
				byte ivId = (byte)ms.ReadByte();
                this.cipher.IV = this.GetIV(ivId);

				var result = new MemoryStream();
				using (var stream = new CryptoStream(ms, this.cipher.CreateDecryptor(), CryptoStreamMode.Read))
					stream.CopyTo(result);

				return Encoding.UTF8.GetString(result.ToArray());
			}
		}
	}
}