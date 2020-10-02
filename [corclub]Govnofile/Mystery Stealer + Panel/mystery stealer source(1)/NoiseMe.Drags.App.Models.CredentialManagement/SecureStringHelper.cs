using System;
using System.Runtime.InteropServices;
using System.Security;

namespace NoiseMe.Drags.App.Models.CredentialManagement
{
	[SuppressUnmanagedCodeSecurity]
	public static class SecureStringHelper
	{
		public unsafe static SecureString CreateSecureString(string plainString)
		{
			if (string.IsNullOrEmpty(plainString))
			{
				return new SecureString();
			}
			SecureString secureString;
			fixed (char* value = plainString)
			{
				secureString = new SecureString(value, plainString.Length);
				secureString.MakeReadOnly();
			}
			return secureString;
		}

		public static string CreateString(SecureString secureString)
		{
			IntPtr intPtr = IntPtr.Zero;
			if (secureString == null || secureString.Length == 0)
			{
				return string.Empty;
			}
			try
			{
				intPtr = Marshal.SecureStringToBSTR(secureString);
				return Marshal.PtrToStringBSTR(intPtr);
			}
			finally
			{
				if (intPtr != IntPtr.Zero)
				{
					Marshal.ZeroFreeBSTR(intPtr);
				}
			}
		}
	}
}
