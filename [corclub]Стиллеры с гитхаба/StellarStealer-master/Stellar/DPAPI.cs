using System;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace Stellar
{
	// Token: 0x02000009 RID: 9
	internal class DPAPI
	{
		// Token: 0x06000026 RID: 38
		[DllImport("crypt32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern bool CryptProtectData(ref DPAPI.DATA_BLOB pPlainText, string szDescription, ref DPAPI.DATA_BLOB pEntropy, IntPtr pReserved, ref DPAPI.CRYPTPROTECT_PROMPTSTRUCT pPrompt, int dwFlags, ref DPAPI.DATA_BLOB pCipherText);

		// Token: 0x06000027 RID: 39 RVA: 0x00005092 File Offset: 0x00004092
		internal static void Decrypt()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000028 RID: 40
		[DllImport("crypt32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern bool CryptUnprotectData(ref DPAPI.DATA_BLOB pCipherText, ref string pszDescription, ref DPAPI.DATA_BLOB pEntropy, IntPtr pReserved, ref DPAPI.CRYPTPROTECT_PROMPTSTRUCT pPrompt, int dwFlags, ref DPAPI.DATA_BLOB pPlainText);

		// Token: 0x06000029 RID: 41 RVA: 0x0000509C File Offset: 0x0000409C
		public static byte[] Decrypt(byte[] cipherTextBytes, byte[] entropyBytes, out string description)
		{
			DPAPI.DATA_BLOB data_BLOB = default(DPAPI.DATA_BLOB);
			DPAPI.DATA_BLOB data_BLOB2 = default(DPAPI.DATA_BLOB);
			DPAPI.DATA_BLOB data_BLOB3 = default(DPAPI.DATA_BLOB);
			DPAPI.CRYPTPROTECT_PROMPTSTRUCT cryptprotect_PROMPTSTRUCT = default(DPAPI.CRYPTPROTECT_PROMPTSTRUCT);
			cryptprotect_PROMPTSTRUCT.cbSize = Marshal.SizeOf(typeof(DPAPI.CRYPTPROTECT_PROMPTSTRUCT));
			cryptprotect_PROMPTSTRUCT.dwPromptFlags = 0;
			cryptprotect_PROMPTSTRUCT.hwndApp = IntPtr.Zero;
			cryptprotect_PROMPTSTRUCT.szPrompt = null;
			description = string.Empty;
			byte[] result;
			try
			{
				try
				{
					if (cipherTextBytes == null)
					{
						cipherTextBytes = new byte[0];
					}
					data_BLOB2.pbData = Marshal.AllocHGlobal(cipherTextBytes.Length);
					if (data_BLOB2.pbData == IntPtr.Zero)
					{
						throw new Exception(string.Empty);
					}
					data_BLOB2.cbData = cipherTextBytes.Length;
					Marshal.Copy(cipherTextBytes, 0, data_BLOB2.pbData, cipherTextBytes.Length);
				}
				catch (Exception innerException)
				{
					throw new Exception(string.Empty, innerException);
				}
				try
				{
					if (entropyBytes == null)
					{
						entropyBytes = new byte[0];
					}
					data_BLOB3.pbData = Marshal.AllocHGlobal(entropyBytes.Length);
					if (data_BLOB3.pbData == IntPtr.Zero)
					{
						throw new Exception(string.Empty);
					}
					data_BLOB3.cbData = entropyBytes.Length;
					Marshal.Copy(entropyBytes, 0, data_BLOB3.pbData, entropyBytes.Length);
				}
				catch (Exception innerException2)
				{
					throw new Exception(string.Empty, innerException2);
				}
				int dwFlags = 1;
				if (!DPAPI.CryptUnprotectData(ref data_BLOB2, ref description, ref data_BLOB3, IntPtr.Zero, ref cryptprotect_PROMPTSTRUCT, dwFlags, ref data_BLOB))
				{
					int lastWin32Error = Marshal.GetLastWin32Error();
					throw new Exception(string.Empty, new Win32Exception(lastWin32Error));
				}
				byte[] array = new byte[data_BLOB.cbData];
				Marshal.Copy(data_BLOB.pbData, array, 0, data_BLOB.cbData);
				result = array;
			}
			catch (Exception innerException3)
			{
				throw new Exception(string.Empty, innerException3);
			}
			finally
			{
				if (data_BLOB.pbData != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(data_BLOB.pbData);
				}
				if (data_BLOB2.pbData != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(data_BLOB2.pbData);
				}
				if (data_BLOB3.pbData != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(data_BLOB3.pbData);
				}
			}
			return result;
		}

		// Token: 0x0200000A RID: 10
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct DATA_BLOB
		{
			// Token: 0x04000021 RID: 33
			public int cbData;

			// Token: 0x04000022 RID: 34
			public IntPtr pbData;
		}

		// Token: 0x0200000B RID: 11
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct CRYPTPROTECT_PROMPTSTRUCT
		{
			// Token: 0x04000023 RID: 35
			public int cbSize;

			// Token: 0x04000024 RID: 36
			public int dwPromptFlags;

			// Token: 0x04000025 RID: 37
			public IntPtr hwndApp;

			// Token: 0x04000026 RID: 38
			public string szPrompt;
		}
	}
}
