using System;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.Win32.SafeHandles;

namespace NoiseMe.Drags.App.Models.CredentialManagement
{
	// Token: 0x0200014B RID: 331
	public class NativeMethods
	{
		// Token: 0x06000ABB RID: 2747
		[DllImport("Advapi32.dll", CharSet = CharSet.Unicode, EntryPoint = "CredReadW", SetLastError = true)]
		public static extern bool CredRead(string target, CredentialType type, int reservedFlag, out IntPtr CredentialPtr);

		// Token: 0x06000ABC RID: 2748
		[DllImport("Advapi32.dll", CharSet = CharSet.Unicode, EntryPoint = "CredWriteW", SetLastError = true)]
		public static extern bool CredWrite([In] ref NativeMethods.CREDENTIAL userCredential, [In] uint flags);

		// Token: 0x06000ABD RID: 2749
		[DllImport("Advapi32.dll", SetLastError = true)]
		public static extern bool CredFree([In] IntPtr cred);

		// Token: 0x06000ABE RID: 2750
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode, EntryPoint = "CredDeleteW")]
		public static extern bool CredDelete(StringBuilder target, CredentialType type, int flags);

		// Token: 0x06000ABF RID: 2751
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		public static extern bool CredEnumerateW(string filter, int flag, out uint count, out IntPtr pCredentials);

		// Token: 0x06000AC0 RID: 2752
		[DllImport("credui.dll")]
		public static extern NativeMethods.CredUIReturnCodes CredUIPromptForCredentials(ref NativeMethods.CREDUI_INFO creditUR, string targetName, IntPtr reserved1, int iError, StringBuilder userName, int maxUserName, StringBuilder password, int maxPassword, [MarshalAs(UnmanagedType.Bool)] ref bool pfSave, int flags);

		// Token: 0x06000AC1 RID: 2753
		[DllImport("credui.dll", CharSet = CharSet.Unicode)]
		public static extern NativeMethods.CredUIReturnCodes CredUIPromptForWindowsCredentials(ref NativeMethods.CREDUI_INFO notUsedHere, int authError, ref uint authPackage, IntPtr InAuthBuffer, uint InAuthBufferSize, out IntPtr refOutAuthBuffer, out uint refOutAuthBufferSize, ref bool fSave, int flags);

		// Token: 0x06000AC2 RID: 2754
		[DllImport("ole32.dll")]
		public static extern void CoTaskMemFree(IntPtr ptr);

		// Token: 0x06000AC3 RID: 2755
		[DllImport("credui.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		public static extern bool CredPackAuthenticationBuffer(int dwFlags, StringBuilder pszUserName, StringBuilder pszPassword, IntPtr pPackedCredentials, ref int pcbPackedCredentials);

		// Token: 0x06000AC4 RID: 2756
		[DllImport("credui.dll", CharSet = CharSet.Auto)]
		public static extern bool CredUnPackAuthenticationBuffer(int dwFlags, IntPtr pAuthBuffer, uint cbAuthBuffer, StringBuilder pszUserName, ref int pcchMaxUserName, StringBuilder pszDomainName, ref int pcchMaxDomainame, StringBuilder pszPassword, ref int pcchMaxPassword);

		// Token: 0x040003E9 RID: 1001
		public const int CREDUI_MAX_USERNAME_LENGTH = 513;

		// Token: 0x040003EA RID: 1002
		public const int CREDUI_MAX_PASSWORD_LENGTH = 256;

		// Token: 0x040003EB RID: 1003
		public const int CREDUI_MAX_MESSAGE_LENGTH = 32767;

		// Token: 0x040003EC RID: 1004
		public const int CREDUI_MAX_CAPTION_LENGTH = 128;

		// Token: 0x0200014C RID: 332
		public struct CREDENTIAL
		{
			// Token: 0x040003ED RID: 1005
			public int Flags;

			// Token: 0x040003EE RID: 1006
			public int Type;

			// Token: 0x040003EF RID: 1007
			[MarshalAs(UnmanagedType.LPWStr)]
			public string TargetName;

			// Token: 0x040003F0 RID: 1008
			[MarshalAs(UnmanagedType.LPWStr)]
			public string Comment;

			// Token: 0x040003F1 RID: 1009
			public long LastWritten;

			// Token: 0x040003F2 RID: 1010
			public int CredentialBlobSize;

			// Token: 0x040003F3 RID: 1011
			public IntPtr CredentialBlob;

			// Token: 0x040003F4 RID: 1012
			public int Persist;

			// Token: 0x040003F5 RID: 1013
			public int AttributeCount;

			// Token: 0x040003F6 RID: 1014
			public IntPtr Attributes;

			// Token: 0x040003F7 RID: 1015
			[MarshalAs(UnmanagedType.LPWStr)]
			public string TargetAlias;

			// Token: 0x040003F8 RID: 1016
			[MarshalAs(UnmanagedType.LPWStr)]
			public string UserName;
		}

		// Token: 0x0200014D RID: 333
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		public struct CREDUI_INFO
		{
			// Token: 0x040003F9 RID: 1017
			public int cbSize;

			// Token: 0x040003FA RID: 1018
			public IntPtr hwndParent;

			// Token: 0x040003FB RID: 1019
			public string pszMessageText;

			// Token: 0x040003FC RID: 1020
			public string pszCaptionText;

			// Token: 0x040003FD RID: 1021
			public IntPtr hbmBanner;
		}

		// Token: 0x0200014E RID: 334
		[Flags]
		public enum WINXP_CREDUI_FLAGS
		{
			// Token: 0x040003FF RID: 1023
			INCORRECT_PASSWORD = 1,
			// Token: 0x04000400 RID: 1024
			DO_NOT_PERSIST = 2,
			// Token: 0x04000401 RID: 1025
			REQUEST_ADMINISTRATOR = 4,
			// Token: 0x04000402 RID: 1026
			EXCLUDE_CERTIFICATES = 8,
			// Token: 0x04000403 RID: 1027
			REQUIRE_CERTIFICATE = 16,
			// Token: 0x04000404 RID: 1028
			SHOW_SAVE_CHECK_BOX = 64,
			// Token: 0x04000405 RID: 1029
			ALWAYS_SHOW_UI = 128,
			// Token: 0x04000406 RID: 1030
			REQUIRE_SMARTCARD = 256,
			// Token: 0x04000407 RID: 1031
			PASSWORD_ONLY_OK = 512,
			// Token: 0x04000408 RID: 1032
			VALIDATE_USERNAME = 1024,
			// Token: 0x04000409 RID: 1033
			COMPLETE_USERNAME = 2048,
			// Token: 0x0400040A RID: 1034
			PERSIST = 4096,
			// Token: 0x0400040B RID: 1035
			SERVER_CREDENTIAL = 16384,
			// Token: 0x0400040C RID: 1036
			EXPECT_CONFIRMATION = 131072,
			// Token: 0x0400040D RID: 1037
			GENERIC_CREDENTIALS = 262144,
			// Token: 0x0400040E RID: 1038
			USERNAME_TARGET_CREDENTIALS = 524288,
			// Token: 0x0400040F RID: 1039
			KEEP_USERNAME = 1048576
		}

		// Token: 0x0200014F RID: 335
		[Flags]
		public enum WINVISTA_CREDUI_FLAGS
		{
			// Token: 0x04000411 RID: 1041
			CREDUIWIN_GENERIC = 1,
			// Token: 0x04000412 RID: 1042
			CREDUIWIN_CHECKBOX = 2,
			// Token: 0x04000413 RID: 1043
			CREDUIWIN_AUTHPACKAGE_ONLY = 16,
			// Token: 0x04000414 RID: 1044
			CREDUIWIN_IN_CRED_ONLY = 32,
			// Token: 0x04000415 RID: 1045
			CREDUIWIN_ENUMERATE_ADMINS = 256,
			// Token: 0x04000416 RID: 1046
			CREDUIWIN_ENUMERATE_CURRENT_USER = 512,
			// Token: 0x04000417 RID: 1047
			CREDUIWIN_SECURE_PROMPT = 4096,
			// Token: 0x04000418 RID: 1048
			CREDUIWIN_PACK_32_WOW = 268435456
		}

		// Token: 0x02000150 RID: 336
		public enum CredUIReturnCodes
		{
			// Token: 0x0400041A RID: 1050
			NO_ERROR,
			// Token: 0x0400041B RID: 1051
			ERROR_CANCELLED = 1223,
			// Token: 0x0400041C RID: 1052
			ERROR_NO_SUCH_LOGON_SESSION = 1312,
			// Token: 0x0400041D RID: 1053
			ERROR_NOT_FOUND = 1168,
			// Token: 0x0400041E RID: 1054
			ERROR_INVALID_ACCOUNT_NAME = 1315,
			// Token: 0x0400041F RID: 1055
			ERROR_INSUFFICIENT_BUFFER = 122,
			// Token: 0x04000420 RID: 1056
			ERROR_BAD_ARGUMENTS = 160,
			// Token: 0x04000421 RID: 1057
			ERROR_INVALID_PARAMETER = 87,
			// Token: 0x04000422 RID: 1058
			ERROR_INVALID_FLAGS = 1004
		}

		// Token: 0x02000151 RID: 337
		public enum CREDErrorCodes
		{
			// Token: 0x04000424 RID: 1060
			NO_ERROR,
			// Token: 0x04000425 RID: 1061
			ERROR_NOT_FOUND = 1168,
			// Token: 0x04000426 RID: 1062
			ERROR_NO_SUCH_LOGON_SESSION = 1312,
			// Token: 0x04000427 RID: 1063
			ERROR_INVALID_PARAMETER = 87,
			// Token: 0x04000428 RID: 1064
			ERROR_INVALID_FLAGS = 1004,
			// Token: 0x04000429 RID: 1065
			ERROR_BAD_USERNAME = 2202,
			// Token: 0x0400042A RID: 1066
			SCARD_E_NO_READERS_AVAILABLE = -2146435026,
			// Token: 0x0400042B RID: 1067
			SCARD_E_NO_SMARTCARD = -2146435060,
			// Token: 0x0400042C RID: 1068
			SCARD_W_REMOVED_CARD = -2146434967,
			// Token: 0x0400042D RID: 1069
			SCARD_W_WRONG_CHV = -2146434965
		}

		// Token: 0x02000152 RID: 338
		public sealed class CriticalCredentialHandle : CriticalHandleZeroOrMinusOneIsInvalid
		{
			// Token: 0x06000AC6 RID: 2758 RVA: 0x00008399 File Offset: 0x00006599
			public CriticalCredentialHandle(IntPtr preexistingHandle)
			{
				base.SetHandle(preexistingHandle);
			}

			// Token: 0x06000AC7 RID: 2759 RVA: 0x000083A8 File Offset: 0x000065A8
			public NativeMethods.CREDENTIAL GetCredential()
			{
				if (!this.IsInvalid)
				{
					return (NativeMethods.CREDENTIAL)Marshal.PtrToStructure(this.handle, typeof(NativeMethods.CREDENTIAL));
				}
				throw new InvalidOperationException("Invalid CriticalHandle!");
			}

			// Token: 0x06000AC8 RID: 2760 RVA: 0x000083D7 File Offset: 0x000065D7
			protected override bool ReleaseHandle()
			{
				if (!this.IsInvalid)
				{
					NativeMethods.CredFree(this.handle);
					base.SetHandleAsInvalid();
					return true;
				}
				return false;
			}
		}
	}
}
