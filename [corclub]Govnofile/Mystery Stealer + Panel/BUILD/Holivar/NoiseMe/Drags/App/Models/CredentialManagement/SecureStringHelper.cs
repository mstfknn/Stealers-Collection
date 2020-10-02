using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;

namespace NoiseMe.Drags.App.Models.CredentialManagement
{
	// Token: 0x02000154 RID: 340
	[SuppressUnmanagedCodeSecurity]
	public static class SecureStringHelper
	{
		// Token: 0x06000AC9 RID: 2761 RVA: 0x000220B0 File Offset: 0x000202B0
		public unsafe static SecureString CreateSecureString(string plainString)
		{
			if (string.IsNullOrEmpty(plainString))
			{
				return new SecureString();
			}
			SecureString secureString;
			fixed (string text = plainString)
			{
				char* ptr = text;
				if (ptr != null)
				{
					ptr += RuntimeHelpers.OffsetToStringData / 2;
				}
				secureString = new SecureString(ptr, plainString.Length);
				secureString.MakeReadOnly();
			}
			return secureString;
		}

		// Token: 0x06000ACA RID: 2762 RVA: 0x000220F0 File Offset: 0x000202F0
		public static string CreateString(SecureString secureString)
		{
			IntPtr intPtr = IntPtr.Zero;
			if (secureString == null || secureString.Length == 0)
			{
				return string.Empty;
			}
			string result;
			try
			{
				intPtr = Marshal.SecureStringToBSTR(secureString);
				result = Marshal.PtrToStringBSTR(intPtr);
			}
			finally
			{
				if (intPtr != IntPtr.Zero)
				{
					Marshal.ZeroFreeBSTR(intPtr);
				}
			}
			return result;
		}
	}
}
