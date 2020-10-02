using Microsoft.Win32.SafeHandles;
using System;
using System.Runtime.InteropServices;
using System.Text;

namespace NoiseMe.Drags.App.Models.CredentialManagement
{
	public class NativeMethods
	{
		public struct CREDENTIAL
		{
			public int Flags;

			public int Type;

			[MarshalAs(UnmanagedType.LPWStr)]
			public string TargetName;

			[MarshalAs(UnmanagedType.LPWStr)]
			public string Comment;

			public long LastWritten;

			public int CredentialBlobSize;

			public IntPtr CredentialBlob;

			public int Persist;

			public int AttributeCount;

			public IntPtr Attributes;

			[MarshalAs(UnmanagedType.LPWStr)]
			public string TargetAlias;

			[MarshalAs(UnmanagedType.LPWStr)]
			public string UserName;
		}

		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		public struct CREDUI_INFO
		{
			public int cbSize;

			public IntPtr hwndParent;

			public string pszMessageText;

			public string pszCaptionText;

			public IntPtr hbmBanner;
		}

		[Flags]
		public enum WINXP_CREDUI_FLAGS
		{
			INCORRECT_PASSWORD = 0x1,
			DO_NOT_PERSIST = 0x2,
			REQUEST_ADMINISTRATOR = 0x4,
			EXCLUDE_CERTIFICATES = 0x8,
			REQUIRE_CERTIFICATE = 0x10,
			SHOW_SAVE_CHECK_BOX = 0x40,
			ALWAYS_SHOW_UI = 0x80,
			REQUIRE_SMARTCARD = 0x100,
			PASSWORD_ONLY_OK = 0x200,
			VALIDATE_USERNAME = 0x400,
			COMPLETE_USERNAME = 0x800,
			PERSIST = 0x1000,
			SERVER_CREDENTIAL = 0x4000,
			EXPECT_CONFIRMATION = 0x20000,
			GENERIC_CREDENTIALS = 0x40000,
			USERNAME_TARGET_CREDENTIALS = 0x80000,
			KEEP_USERNAME = 0x100000
		}

		[Flags]
		public enum WINVISTA_CREDUI_FLAGS
		{
			CREDUIWIN_GENERIC = 0x1,
			CREDUIWIN_CHECKBOX = 0x2,
			CREDUIWIN_AUTHPACKAGE_ONLY = 0x10,
			CREDUIWIN_IN_CRED_ONLY = 0x20,
			CREDUIWIN_ENUMERATE_ADMINS = 0x100,
			CREDUIWIN_ENUMERATE_CURRENT_USER = 0x200,
			CREDUIWIN_SECURE_PROMPT = 0x1000,
			CREDUIWIN_PACK_32_WOW = 0x10000000
		}

		public enum CredUIReturnCodes
		{
			NO_ERROR = 0,
			ERROR_CANCELLED = 1223,
			ERROR_NO_SUCH_LOGON_SESSION = 1312,
			ERROR_NOT_FOUND = 1168,
			ERROR_INVALID_ACCOUNT_NAME = 1315,
			ERROR_INSUFFICIENT_BUFFER = 122,
			ERROR_BAD_ARGUMENTS = 160,
			ERROR_INVALID_PARAMETER = 87,
			ERROR_INVALID_FLAGS = 1004
		}

		public enum CREDErrorCodes
		{
			NO_ERROR = 0,
			ERROR_NOT_FOUND = 1168,
			ERROR_NO_SUCH_LOGON_SESSION = 1312,
			ERROR_INVALID_PARAMETER = 87,
			ERROR_INVALID_FLAGS = 1004,
			ERROR_BAD_USERNAME = 2202,
			SCARD_E_NO_READERS_AVAILABLE = -2146435026,
			SCARD_E_NO_SMARTCARD = -2146435060,
			SCARD_W_REMOVED_CARD = -2146434967,
			SCARD_W_WRONG_CHV = -2146434965
		}

		public sealed class CriticalCredentialHandle : CriticalHandleZeroOrMinusOneIsInvalid
		{
			public CriticalCredentialHandle(IntPtr preexistingHandle)
			{
				SetHandle(preexistingHandle);
			}

			public CREDENTIAL GetCredential()
			{
				if (!IsInvalid)
				{
					return (CREDENTIAL)Marshal.PtrToStructure(handle, typeof(CREDENTIAL));
				}
				throw new InvalidOperationException("Invalid CriticalHandle!");
			}

			protected override bool ReleaseHandle()
			{
				if (!IsInvalid)
				{
					CredFree(handle);
					SetHandleAsInvalid();
					return true;
				}
				return false;
			}
		}

		public const int CREDUI_MAX_USERNAME_LENGTH = 513;

		public const int CREDUI_MAX_PASSWORD_LENGTH = 256;

		public const int CREDUI_MAX_MESSAGE_LENGTH = 32767;

		public const int CREDUI_MAX_CAPTION_LENGTH = 128;

		[DllImport("Advapi32.dll", CharSet = CharSet.Unicode, EntryPoint = "CredReadW", SetLastError = true)]
		public static extern bool CredRead(string target, CredentialType type, int reservedFlag, out IntPtr CredentialPtr);

		[DllImport("Advapi32.dll", CharSet = CharSet.Unicode, EntryPoint = "CredWriteW", SetLastError = true)]
		public static extern bool CredWrite([In] ref CREDENTIAL userCredential, [In] uint flags);

		[DllImport("Advapi32.dll", SetLastError = true)]
		public static extern bool CredFree([In] IntPtr cred);

		[DllImport("advapi32.dll", CharSet = CharSet.Unicode, EntryPoint = "CredDeleteW")]
		public static extern bool CredDelete(StringBuilder target, CredentialType type, int flags);

		[DllImport("advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		public static extern bool CredEnumerateW(string filter, int flag, out uint count, out IntPtr pCredentials);

		[DllImport("credui.dll")]
		public static extern CredUIReturnCodes CredUIPromptForCredentials(ref CREDUI_INFO creditUR, string targetName, IntPtr reserved1, int iError, StringBuilder userName, int maxUserName, StringBuilder password, int maxPassword, [MarshalAs(UnmanagedType.Bool)] ref bool pfSave, int flags);

		[DllImport("credui.dll", CharSet = CharSet.Unicode)]
		public static extern CredUIReturnCodes CredUIPromptForWindowsCredentials(ref CREDUI_INFO notUsedHere, int authError, ref uint authPackage, IntPtr InAuthBuffer, uint InAuthBufferSize, out IntPtr refOutAuthBuffer, out uint refOutAuthBufferSize, ref bool fSave, int flags);

		[DllImport("ole32.dll")]
		public static extern void CoTaskMemFree(IntPtr ptr);

		[DllImport("credui.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		public static extern bool CredPackAuthenticationBuffer(int dwFlags, StringBuilder pszUserName, StringBuilder pszPassword, IntPtr pPackedCredentials, ref int pcbPackedCredentials);

		[DllImport("credui.dll", CharSet = CharSet.Auto)]
		public static extern bool CredUnPackAuthenticationBuffer(int dwFlags, IntPtr pAuthBuffer, uint cbAuthBuffer, StringBuilder pszUserName, ref int pcchMaxUserName, StringBuilder pszDomainName, ref int pcchMaxDomainame, StringBuilder pszPassword, ref int pcchMaxPassword);
	}
}
