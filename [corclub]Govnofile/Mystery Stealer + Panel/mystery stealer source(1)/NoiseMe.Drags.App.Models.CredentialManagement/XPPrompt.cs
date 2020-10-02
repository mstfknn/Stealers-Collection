using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;

namespace NoiseMe.Drags.App.Models.CredentialManagement
{
	public class XPPrompt : BaseCredentialsPrompt
	{
		private string _target;

		private Bitmap _banner;

		public string Target
		{
			get
			{
				CheckNotDisposed();
				return _target;
			}
			set
			{
				CheckNotDisposed();
				if (string.IsNullOrEmpty(value))
				{
					throw new ArgumentNullException("value");
				}
				_target = value;
			}
		}

		public Bitmap Banner
		{
			get
			{
				CheckNotDisposed();
				return _banner;
			}
			set
			{
				CheckNotDisposed();
				if (_banner != null)
				{
					_banner.Dispose();
				}
				_banner = value;
			}
		}

		public bool CompleteUsername
		{
			get
			{
				CheckNotDisposed();
				return (0x800 & base.DialogFlags) != 0;
			}
			set
			{
				CheckNotDisposed();
				AddFlag(value, 2048);
			}
		}

		public bool DoNotPersist
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

		public bool ExcludeCertificates
		{
			get
			{
				CheckNotDisposed();
				return (8 & base.DialogFlags) != 0;
			}
			set
			{
				CheckNotDisposed();
				AddFlag(value, 8);
			}
		}

		public bool ExpectConfirmation
		{
			get
			{
				CheckNotDisposed();
				return (0x20000 & base.DialogFlags) != 0;
			}
			set
			{
				CheckNotDisposed();
				AddFlag(value, 131072);
			}
		}

		public bool IncorrectPassword
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

		public bool Persist
		{
			get
			{
				CheckNotDisposed();
				return (0x1000 & base.DialogFlags) != 0;
			}
			set
			{
				CheckNotDisposed();
				AddFlag(value, 4096);
			}
		}

		public bool RequestAdministrator
		{
			get
			{
				CheckNotDisposed();
				return (4 & base.DialogFlags) != 0;
			}
			set
			{
				CheckNotDisposed();
				AddFlag(value, 4);
			}
		}

		public bool RequireCertificate
		{
			get
			{
				CheckNotDisposed();
				return (0x10 & base.DialogFlags) != 0;
			}
			set
			{
				CheckNotDisposed();
				AddFlag(value, 16);
			}
		}

		public bool RequireSmartCard
		{
			get
			{
				CheckNotDisposed();
				return (0x100 & base.DialogFlags) != 0;
			}
			set
			{
				CheckNotDisposed();
				AddFlag(value, 256);
			}
		}

		public bool UsernameReadOnly
		{
			get
			{
				CheckNotDisposed();
				return (0x100000 & base.DialogFlags) != 0;
			}
			set
			{
				CheckNotDisposed();
				AddFlag(value, 1048576);
			}
		}

		public bool ValidateUsername
		{
			get
			{
				CheckNotDisposed();
				return (0x400 & base.DialogFlags) != 0;
			}
			set
			{
				CheckNotDisposed();
				AddFlag(value, 1024);
			}
		}

		public override bool ShowSaveCheckBox
		{
			get
			{
				CheckNotDisposed();
				return (0x40 & base.DialogFlags) != 0;
			}
			set
			{
				CheckNotDisposed();
				AddFlag(value, 64);
			}
		}

		public override bool GenericCredentials
		{
			get
			{
				CheckNotDisposed();
				return (0x40000 & base.DialogFlags) != 0;
			}
			set
			{
				CheckNotDisposed();
				AddFlag(value, 262144);
			}
		}

		public bool AlwaysShowUI
		{
			get
			{
				CheckNotDisposed();
				return (0x80 & base.DialogFlags) != 0;
			}
			set
			{
				CheckNotDisposed();
				AddFlag(value, 128);
			}
		}

		protected override NativeMethods.CREDUI_INFO CreateCREDUI_INFO(IntPtr owner)
		{
			NativeMethods.CREDUI_INFO result = base.CreateCREDUI_INFO(owner);
			result.hbmBanner = ((Banner == null) ? IntPtr.Zero : Banner.GetHbitmap());
			return result;
		}

		public override DialogResult ShowDialog(IntPtr owner)
		{
			CheckNotDisposed();
			NativeMethods.CREDUI_INFO creditUR = CreateCREDUI_INFO(owner);
			StringBuilder stringBuilder = new StringBuilder(1000);
			StringBuilder stringBuilder2 = new StringBuilder(1000);
			bool pfSave = base.SaveChecked;
			if (string.IsNullOrEmpty(Target))
			{
				throw new InvalidOperationException("Target must always be specified.");
			}
			if (AlwaysShowUI && !GenericCredentials)
			{
				throw new InvalidOperationException("AlwaysShowUI must be specified with GenericCredentials property.");
			}
			switch (NativeMethods.CredUIPromptForCredentials(ref creditUR, Target, IntPtr.Zero, base.ErrorCode, stringBuilder, 513, stringBuilder2, 256, ref pfSave, base.DialogFlags))
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
				base.Username = stringBuilder.ToString();
				base.Password = stringBuilder2.ToString();
				if (stringBuilder2.Length > 0)
				{
					stringBuilder2.Remove(0, stringBuilder2.Length);
				}
				return DialogResult.OK;
			}
		}
	}
}
