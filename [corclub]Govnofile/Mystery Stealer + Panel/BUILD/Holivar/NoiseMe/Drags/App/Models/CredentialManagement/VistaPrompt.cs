using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Text;

namespace NoiseMe.Drags.App.Models.CredentialManagement
{
	// Token: 0x02000155 RID: 341
	public class VistaPrompt : BaseCredentialsPrompt
	{
		// Token: 0x06000ACB RID: 2763 RVA: 0x000083F6 File Offset: 0x000065F6
		public VistaPrompt()
		{
			base.Title = "Please provide credentials";
		}

		// Token: 0x1700029A RID: 666
		// (get) Token: 0x06000ACC RID: 2764 RVA: 0x00008409 File Offset: 0x00006609
		// (set) Token: 0x06000ACD RID: 2765 RVA: 0x00008417 File Offset: 0x00006617
		public string Domain
		{
			get
			{
				base.CheckNotDisposed();
				return this._domain;
			}
			set
			{
				base.CheckNotDisposed();
				if (string.IsNullOrEmpty(value))
				{
					throw new ArgumentNullException("value");
				}
				this._domain = value;
			}
		}

		// Token: 0x1700029B RID: 667
		// (get) Token: 0x06000ACE RID: 2766 RVA: 0x00008439 File Offset: 0x00006639
		// (set) Token: 0x06000ACF RID: 2767 RVA: 0x0000844C File Offset: 0x0000664C
		public override bool ShowSaveCheckBox
		{
			get
			{
				base.CheckNotDisposed();
				return (2 & base.DialogFlags) != 0;
			}
			set
			{
				base.CheckNotDisposed();
				base.AddFlag(value, 2);
			}
		}

		// Token: 0x1700029C RID: 668
		// (get) Token: 0x06000AD0 RID: 2768 RVA: 0x0000845C File Offset: 0x0000665C
		// (set) Token: 0x06000AD1 RID: 2769 RVA: 0x0000846F File Offset: 0x0000666F
		public override bool GenericCredentials
		{
			get
			{
				base.CheckNotDisposed();
				return (1 & base.DialogFlags) != 0;
			}
			set
			{
				base.CheckNotDisposed();
				base.AddFlag(value, 1);
			}
		}

		// Token: 0x06000AD2 RID: 2770 RVA: 0x0002214C File Offset: 0x0002034C
		public override DialogResult ShowDialog(IntPtr owner)
		{
			base.CheckNotDisposed();
			if (string.IsNullOrEmpty(base.Title) && string.IsNullOrEmpty(base.Message))
			{
				throw new InvalidOperationException("Title or Message should always be set.");
			}
			if (!this.IsWinVistaOrHigher)
			{
				throw new InvalidOperationException("This Operating System does not support this prompt.");
			}
			uint num = 0u;
			IntPtr intPtr = IntPtr.Zero;
			int num2 = 0;
			bool saveChecked = base.SaveChecked;
			NativeMethods.CREDUI_INFO credui_INFO = this.CreateCREDUI_INFO(owner);
			if (!string.IsNullOrEmpty(base.Username) || !string.IsNullOrEmpty(SecureStringHelper.CreateString(base.SecurePassword)))
			{
				NativeMethods.CredPackAuthenticationBuffer(0, new StringBuilder(base.Username), new StringBuilder(SecureStringHelper.CreateString(base.SecurePassword)), intPtr, ref num2);
				if (Marshal.GetLastWin32Error() == 122)
				{
					intPtr = Marshal.AllocCoTaskMem(num2);
					if (!NativeMethods.CredPackAuthenticationBuffer(0, new StringBuilder(base.Username), new StringBuilder(SecureStringHelper.CreateString(base.SecurePassword)), intPtr, ref num2))
					{
						throw new Win32Exception(Marshal.GetLastWin32Error(), "There was an issue with the given Username or Password.");
					}
				}
			}
			IntPtr intPtr2;
			uint cbAuthBuffer;
			NativeMethods.CredUIReturnCodes credUIReturnCodes;
			try
			{
				credUIReturnCodes = NativeMethods.CredUIPromptForWindowsCredentials(ref credui_INFO, base.ErrorCode, ref num, intPtr, (uint)num2, out intPtr2, out cbAuthBuffer, ref saveChecked, base.DialogFlags);
				base.SaveChecked = saveChecked;
			}
			catch (EntryPointNotFoundException innerException)
			{
				throw new InvalidOperationException("This functionality is not supported by this operating system.", innerException);
			}
			if (credUIReturnCodes <= NativeMethods.CredUIReturnCodes.ERROR_INVALID_FLAGS)
			{
				if (credUIReturnCodes <= NativeMethods.CredUIReturnCodes.ERROR_INSUFFICIENT_BUFFER)
				{
					if (credUIReturnCodes != NativeMethods.CredUIReturnCodes.ERROR_INVALID_PARAMETER && credUIReturnCodes != NativeMethods.CredUIReturnCodes.ERROR_INSUFFICIENT_BUFFER)
					{
						goto IL_198;
					}
				}
				else if (credUIReturnCodes != NativeMethods.CredUIReturnCodes.ERROR_BAD_ARGUMENTS && credUIReturnCodes != NativeMethods.CredUIReturnCodes.ERROR_INVALID_FLAGS)
				{
					goto IL_198;
				}
			}
			else if (credUIReturnCodes <= NativeMethods.CredUIReturnCodes.ERROR_CANCELLED)
			{
				if (credUIReturnCodes != NativeMethods.CredUIReturnCodes.ERROR_NOT_FOUND)
				{
					if (credUIReturnCodes != NativeMethods.CredUIReturnCodes.ERROR_CANCELLED)
					{
						goto IL_198;
					}
					return DialogResult.Cancel;
				}
			}
			else if (credUIReturnCodes != NativeMethods.CredUIReturnCodes.ERROR_NO_SUCH_LOGON_SESSION && credUIReturnCodes != NativeMethods.CredUIReturnCodes.ERROR_INVALID_ACCOUNT_NAME)
			{
				goto IL_198;
			}
			throw new InvalidOperationException("Invalid properties were specified.", new Win32Exception(Marshal.GetLastWin32Error()));
			IL_198:
			int num3 = 1000;
			int num4 = 1000;
			int num5 = 1000;
			StringBuilder stringBuilder = new StringBuilder(1000);
			StringBuilder stringBuilder2 = new StringBuilder(1000);
			StringBuilder pszDomainName = new StringBuilder(1000);
			if (NativeMethods.CredUnPackAuthenticationBuffer(0, intPtr2, cbAuthBuffer, stringBuilder, ref num3, pszDomainName, ref num5, stringBuilder2, ref num4))
			{
				NativeMethods.CoTaskMemFree(intPtr2);
				base.Username = stringBuilder.ToString();
				base.Password = stringBuilder2.ToString();
				if (stringBuilder2.Length > 0)
				{
					stringBuilder2.Remove(0, stringBuilder2.Length);
				}
			}
			return DialogResult.OK;
		}

		// Token: 0x1700029D RID: 669
		// (get) Token: 0x06000AD3 RID: 2771 RVA: 0x0002238C File Offset: 0x0002058C
		private bool IsWinVistaOrHigher
		{
			get
			{
				OperatingSystem osversion = Environment.OSVersion;
				return osversion.Platform == PlatformID.Win32NT && osversion.Version.Major >= 6;
			}
		}

		// Token: 0x04000432 RID: 1074
		private string _domain;
	}
}
