using System;
using System.Runtime.InteropServices;

namespace BrowserPasswords.MozillaPasswords
{
	// Token: 0x02000029 RID: 41
	public class MozillaSDR : IDisposable
	{
		// Token: 0x06000144 RID: 324
		[DllImport("nss3.dll", CallingConvention = CallingConvention.Cdecl)]
		private static extern int PK11_SetPasswordFunc(MulticastDelegate callback);

		// Token: 0x06000145 RID: 325
		[DllImport("nss3.dll", CallingConvention = CallingConvention.Cdecl)]
		private static extern int NSS_Init([MarshalAs(UnmanagedType.LPStr)] string profile_dir);

		// Token: 0x06000146 RID: 326
		[DllImport("nss3.dll", CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr NSSBase64_DecodeBuffer(IntPtr p1, IntPtr p2, [MarshalAs(UnmanagedType.LPStr)] string encoded, int encoded_len);

		// Token: 0x06000147 RID: 327
		[DllImport("nss3.dll", CallingConvention = CallingConvention.Cdecl)]
		private static extern int PK11SDR_Decrypt(IntPtr encrypted_item, ref MozillaSDR.SECItem text, int p1);

		// Token: 0x06000148 RID: 328
		[DllImport("nss3.dll", CallingConvention = CallingConvention.Cdecl)]
		private static extern int SECITEM_FreeItem(ref MozillaSDR.SECItem item, int bDestroy);

		// Token: 0x06000149 RID: 329
		[DllImport("nss3.dll", CallingConvention = CallingConvention.Cdecl)]
		private static extern int SECITEM_FreeItem(IntPtr item, int bDestroy);

		// Token: 0x0600014A RID: 330
		[DllImport("nss3.dll", CallingConvention = CallingConvention.Cdecl)]
		private static extern int NSS_Shutdown();

		// Token: 0x0600014B RID: 331
		[DllImport("libnspr4.dll", CallingConvention = CallingConvention.Cdecl)]
		private static extern int PR_Cleanup();

		// Token: 0x0600014C RID: 332 RVA: 0x00004DD0 File Offset: 0x00002FD0
		private static IntPtr My_GetModulePassword(IntPtr slot, int retry, IntPtr arg)
		{
			IntPtr result;
			if (retry != 0 && MozillaSDR.password == null)
			{
				result = Marshal.StringToHGlobalAnsi(MozillaSDR.password);
			}
			else
			{
				result = IntPtr.Zero;
			}
			return result;
		}

		// Token: 0x0600014D RID: 333 RVA: 0x00004E00 File Offset: 0x00003000
		private int NSS_Init(string profile_dir, string master_passwd)
		{
			MozillaSDR.password = master_passwd;
			MulticastDelegate callback = CallConvDelegateBuilder.CreateDelegateInstance(typeof(MozillaSDR), "My_GetModulePassword", CallingConvention.Cdecl, null);
			MozillaSDR.PK11_SetPasswordFunc(callback);
			int num = MozillaSDR.NSS_Init(profile_dir);
			int result;
			if (num != 0)
			{
				result = -1;
			}
			else
			{
				result = 0;
			}
			return result;
		}

		// Token: 0x0600014E RID: 334 RVA: 0x00004E48 File Offset: 0x00003048
		private int nss_decrypt(string b64_encrypted_data, ref string buf)
		{
			int result = 0;
			MozillaSDR.SECItem secitem = default(MozillaSDR.SECItem);
			IntPtr intPtr = IntPtr.Zero;
			buf = string.Empty;
			secitem.data = IntPtr.Zero;
			secitem.len = 0;
			intPtr = MozillaSDR.NSSBase64_DecodeBuffer(IntPtr.Zero, IntPtr.Zero, b64_encrypted_data, b64_encrypted_data.Length);
			if (intPtr == IntPtr.Zero)
			{
				result = -1;
			}
			else
			{
				int num = MozillaSDR.PK11SDR_Decrypt(intPtr, ref secitem, 0);
				if (num != 0)
				{
					result = -2;
				}
				else
				{
					buf = Marshal.PtrToStringAnsi(secitem.data, secitem.len);
					MozillaSDR.SECITEM_FreeItem(ref secitem, 0);
				}
				MozillaSDR.SECITEM_FreeItem(intPtr, 1);
			}
			if (secitem.data != IntPtr.Zero)
			{
				Marshal.FreeHGlobal(secitem.data);
			}
			return result;
		}

		// Token: 0x0600014F RID: 335 RVA: 0x00004F08 File Offset: 0x00003108
		private int nss_decode(string b64_encrypted_data, ref string buf)
		{
			int result = 0;
			MozillaSDR.SECItem secitem = default(MozillaSDR.SECItem);
			IntPtr intPtr = IntPtr.Zero;
			buf = string.Empty;
			intPtr = MozillaSDR.NSSBase64_DecodeBuffer(IntPtr.Zero, IntPtr.Zero, b64_encrypted_data, b64_encrypted_data.Length);
			if (intPtr == IntPtr.Zero)
			{
				result = -1;
			}
			else
			{
				secitem = (MozillaSDR.SECItem)Marshal.PtrToStructure(intPtr, typeof(MozillaSDR.SECItem));
				buf = Marshal.PtrToStringAnsi(secitem.data, secitem.len);
				MozillaSDR.SECITEM_FreeItem(intPtr, 1);
			}
			return result;
		}

		// Token: 0x06000150 RID: 336 RVA: 0x00004F94 File Offset: 0x00003194
		private int nss_free()
		{
			int result;
			if (MozillaSDR.NSS_Shutdown() != 0)
			{
				result = -1;
			}
			else
			{
				result = 0;
			}
			return result;
		}

		// Token: 0x06000151 RID: 337 RVA: 0x00004FB4 File Offset: 0x000031B4
		public MozillaSDR(string profile_dir, string password)
		{
			this.NSS_Init(profile_dir, password);
		}

		// Token: 0x06000152 RID: 338 RVA: 0x00004FC8 File Offset: 0x000031C8
		public string DecryptPassword(string encrypted)
		{
			string text = null;
			string result;
			if (this.nss_decrypt(encrypted, ref text) != 0)
			{
				result = string.Empty;
			}
			else
			{
				result = text;
			}
			return result;
		}

		// Token: 0x06000153 RID: 339 RVA: 0x00004FF4 File Offset: 0x000031F4
		public string DecodePassword(string encoded)
		{
			string text = null;
			string result;
			if (this.nss_decode(encoded, ref text) != 0)
			{
				result = string.Empty;
			}
			else
			{
				result = text;
			}
			return result;
		}

		// Token: 0x06000154 RID: 340 RVA: 0x00005020 File Offset: 0x00003220
		public void Dispose()
		{
			this.nss_free();
		}

		// Token: 0x0400011D RID: 285
		private static string password = null;

		// Token: 0x0200002A RID: 42
		private struct SECItem
		{
			// Token: 0x0400011E RID: 286
			public int type;

			// Token: 0x0400011F RID: 287
			public IntPtr data;

			// Token: 0x04000120 RID: 288
			public int len;
		}
	}
}
