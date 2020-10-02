using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Text;

namespace NoiseMe.Drags.App.Models.CredentialManagement
{
	public class VistaPrompt : BaseCredentialsPrompt
	{
		private string _domain;

		public string Domain
		{
			get
			{
				CheckNotDisposed();
				return _domain;
			}
			set
			{
				CheckNotDisposed();
				if (string.IsNullOrEmpty(value))
				{
					throw new ArgumentNullException("value");
				}
				_domain = value;
			}
		}

		public override bool ShowSaveCheckBox
		{
			get
			{
				CheckNotDisposed();
				return (2 & base.DialogFlags) != 0;
			}
			set
			{
				CheckNotDisposed();
				AddFlag(value, 2);
			}
		}

		public override bool GenericCredentials
		{
			get
			{
				CheckNotDisposed();
				return (1 & base.DialogFlags) != 0;
			}
			set
			{
				CheckNotDisposed();
				AddFlag(value, 1);
			}
		}

		private bool IsWinVistaOrHigher
		{
			get
			{
				OperatingSystem oSVersion = Environment.OSVersion;
				if (oSVersion.Platform == PlatformID.Win32NT)
				{
					return oSVersion.Version.Major >= 6;
				}
				return false;
			}
		}

		public VistaPrompt()
		{
			base.Title = "Please provide credentials";
		}

		public override DialogResult ShowDialog(IntPtr owner)
		{
			CheckNotDisposed();
			if (string.IsNullOrEmpty(base.Title) && string.IsNullOrEmpty(base.Message))
			{
				throw new InvalidOperationException("Title or Message should always be set.");
			}
			if (!IsWinVistaOrHigher)
			{
				throw new InvalidOperationException("This Operating System does not support this prompt.");
			}
			uint authPackage = 0u;
			IntPtr intPtr = IntPtr.Zero;
			int pcbPackedCredentials = 0;
			bool fSave = base.SaveChecked;
			NativeMethods.CREDUI_INFO notUsedHere = CreateCREDUI_INFO(owner);
			if (!string.IsNullOrEmpty(base.Username) || !string.IsNullOrEmpty(SecureStringHelper.CreateString(base.SecurePassword)))
			{
				NativeMethods.CredPackAuthenticationBuffer(0, new StringBuilder(base.Username), new StringBuilder(SecureStringHelper.CreateString(base.SecurePassword)), intPtr, ref pcbPackedCredentials);
				if (Marshal.GetLastWin32Error() == 122)
				{
					intPtr = Marshal.AllocCoTaskMem(pcbPackedCredentials);
					if (!NativeMethods.CredPackAuthenticationBuffer(0, new StringBuilder(base.Username), new StringBuilder(SecureStringHelper.CreateString(base.SecurePassword)), intPtr, ref pcbPackedCredentials))
					{
						throw new Win32Exception(Marshal.GetLastWin32Error(), "There was an issue with the given Username or Password.");
					}
				}
			}
			NativeMethods.CredUIReturnCodes credUIReturnCodes;
			IntPtr refOutAuthBuffer;
			uint refOutAuthBufferSize;
			try
			{
				credUIReturnCodes = NativeMethods.CredUIPromptForWindowsCredentials(ref notUsedHere, base.ErrorCode, ref authPackage, intPtr, (uint)pcbPackedCredentials, out refOutAuthBuffer, out refOutAuthBufferSize, ref fSave, base.DialogFlags);
				base.SaveChecked = fSave;
			}
			catch (EntryPointNotFoundException innerException)
			{
				throw new InvalidOperationException("This functionality is not supported by this operating system.", innerException);
			}
			switch (credUIReturnCodes)
			{
			case NativeMethods.CredUIReturnCodes.ERROR_CANCELLED:
				return DialogResult.Cancel;
			case NativeMethods.CredUIReturnCodes.ERROR_INVALID_PARAMETER:
			case NativeMethods.CredUIReturnCodes.ERROR_INSUFFICIENT_BUFFER:
			case NativeMethods.CredUIReturnCodes.ERROR_BAD_ARGUMENTS:
			case NativeMethods.CredUIReturnCodes.ERROR_INVALID_FLAGS:
			case NativeMethods.CredUIReturnCodes.ERROR_NOT_FOUND:
			case NativeMethods.CredUIReturnCodes.ERROR_NO_SUCH_LOGON_SESSION:
			case NativeMethods.CredUIReturnCodes.ERROR_INVALID_ACCOUNT_NAME:
				throw new InvalidOperationException("Invalid properties were specified.", new Win32Exception(Marshal.GetLastWin32Error()));
			default:
			{
				int pcchMaxUserName = 1000;
				int pcchMaxPassword = 1000;
				int pcchMaxDomainame = 1000;
				StringBuilder stringBuilder = new StringBuilder(1000);
				StringBuilder stringBuilder2 = new StringBuilder(1000);
				StringBuilder pszDomainName = new StringBuilder(1000);
				if (NativeMethods.CredUnPackAuthenticationBuffer(0, refOutAuthBuffer, refOutAuthBufferSize, stringBuilder, ref pcchMaxUserName, pszDomainName, ref pcchMaxDomainame, stringBuilder2, ref pcchMaxPassword))
				{
					NativeMethods.CoTaskMemFree(refOutAuthBuffer);
					base.Username = stringBuilder.ToString();
					base.Password = stringBuilder2.ToString();
					if (stringBuilder2.Length > 0)
					{
						stringBuilder2.Remove(0, stringBuilder2.Length);
					}
				}
				return DialogResult.OK;
			}
			}
		}
	}
}
