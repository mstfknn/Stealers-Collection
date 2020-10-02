using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;

namespace NoiseMe.Drags.App.Models.CredentialManagement
{
	// Token: 0x02000156 RID: 342
	public class XPPrompt : BaseCredentialsPrompt
	{
		// Token: 0x1700029E RID: 670
		// (get) Token: 0x06000AD4 RID: 2772 RVA: 0x0000847F File Offset: 0x0000667F
		// (set) Token: 0x06000AD5 RID: 2773 RVA: 0x0000848D File Offset: 0x0000668D
		public string Target
		{
			get
			{
				base.CheckNotDisposed();
				return this._target;
			}
			set
			{
				base.CheckNotDisposed();
				if (string.IsNullOrEmpty(value))
				{
					throw new ArgumentNullException("value");
				}
				this._target = value;
			}
		}

		// Token: 0x1700029F RID: 671
		// (get) Token: 0x06000AD6 RID: 2774 RVA: 0x000084AF File Offset: 0x000066AF
		// (set) Token: 0x06000AD7 RID: 2775 RVA: 0x000084BD File Offset: 0x000066BD
		public Bitmap Banner
		{
			get
			{
				base.CheckNotDisposed();
				return this._banner;
			}
			set
			{
				base.CheckNotDisposed();
				if (this._banner != null)
				{
					this._banner.Dispose();
				}
				this._banner = value;
			}
		}

		// Token: 0x170002A0 RID: 672
		// (get) Token: 0x06000AD8 RID: 2776 RVA: 0x000084DF File Offset: 0x000066DF
		// (set) Token: 0x06000AD9 RID: 2777 RVA: 0x000084F6 File Offset: 0x000066F6
		public bool CompleteUsername
		{
			get
			{
				base.CheckNotDisposed();
				return (2048 & base.DialogFlags) != 0;
			}
			set
			{
				base.CheckNotDisposed();
				base.AddFlag(value, 2048);
			}
		}

		// Token: 0x170002A1 RID: 673
		// (get) Token: 0x06000ADA RID: 2778 RVA: 0x00008439 File Offset: 0x00006639
		// (set) Token: 0x06000ADB RID: 2779 RVA: 0x0000844C File Offset: 0x0000664C
		public bool DoNotPersist
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

		// Token: 0x170002A2 RID: 674
		// (get) Token: 0x06000ADC RID: 2780 RVA: 0x0000850A File Offset: 0x0000670A
		// (set) Token: 0x06000ADD RID: 2781 RVA: 0x0000851D File Offset: 0x0000671D
		public bool ExcludeCertificates
		{
			get
			{
				base.CheckNotDisposed();
				return (8 & base.DialogFlags) != 0;
			}
			set
			{
				base.CheckNotDisposed();
				base.AddFlag(value, 8);
			}
		}

		// Token: 0x170002A3 RID: 675
		// (get) Token: 0x06000ADE RID: 2782 RVA: 0x0000852D File Offset: 0x0000672D
		// (set) Token: 0x06000ADF RID: 2783 RVA: 0x00008544 File Offset: 0x00006744
		public bool ExpectConfirmation
		{
			get
			{
				base.CheckNotDisposed();
				return (131072 & base.DialogFlags) != 0;
			}
			set
			{
				base.CheckNotDisposed();
				base.AddFlag(value, 131072);
			}
		}

		// Token: 0x170002A4 RID: 676
		// (get) Token: 0x06000AE0 RID: 2784 RVA: 0x0000845C File Offset: 0x0000665C
		// (set) Token: 0x06000AE1 RID: 2785 RVA: 0x0000846F File Offset: 0x0000666F
		public bool IncorrectPassword
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

		// Token: 0x170002A5 RID: 677
		// (get) Token: 0x06000AE2 RID: 2786 RVA: 0x00008558 File Offset: 0x00006758
		// (set) Token: 0x06000AE3 RID: 2787 RVA: 0x0000856F File Offset: 0x0000676F
		public bool Persist
		{
			get
			{
				base.CheckNotDisposed();
				return (4096 & base.DialogFlags) != 0;
			}
			set
			{
				base.CheckNotDisposed();
				base.AddFlag(value, 4096);
			}
		}

		// Token: 0x170002A6 RID: 678
		// (get) Token: 0x06000AE4 RID: 2788 RVA: 0x00008583 File Offset: 0x00006783
		// (set) Token: 0x06000AE5 RID: 2789 RVA: 0x00008596 File Offset: 0x00006796
		public bool RequestAdministrator
		{
			get
			{
				base.CheckNotDisposed();
				return (4 & base.DialogFlags) != 0;
			}
			set
			{
				base.CheckNotDisposed();
				base.AddFlag(value, 4);
			}
		}

		// Token: 0x170002A7 RID: 679
		// (get) Token: 0x06000AE6 RID: 2790 RVA: 0x000085A6 File Offset: 0x000067A6
		// (set) Token: 0x06000AE7 RID: 2791 RVA: 0x000085BA File Offset: 0x000067BA
		public bool RequireCertificate
		{
			get
			{
				base.CheckNotDisposed();
				return (16 & base.DialogFlags) != 0;
			}
			set
			{
				base.CheckNotDisposed();
				base.AddFlag(value, 16);
			}
		}

		// Token: 0x170002A8 RID: 680
		// (get) Token: 0x06000AE8 RID: 2792 RVA: 0x000085CB File Offset: 0x000067CB
		// (set) Token: 0x06000AE9 RID: 2793 RVA: 0x000085E2 File Offset: 0x000067E2
		public bool RequireSmartCard
		{
			get
			{
				base.CheckNotDisposed();
				return (256 & base.DialogFlags) != 0;
			}
			set
			{
				base.CheckNotDisposed();
				base.AddFlag(value, 256);
			}
		}

		// Token: 0x170002A9 RID: 681
		// (get) Token: 0x06000AEA RID: 2794 RVA: 0x000085F6 File Offset: 0x000067F6
		// (set) Token: 0x06000AEB RID: 2795 RVA: 0x0000860D File Offset: 0x0000680D
		public bool UsernameReadOnly
		{
			get
			{
				base.CheckNotDisposed();
				return (1048576 & base.DialogFlags) != 0;
			}
			set
			{
				base.CheckNotDisposed();
				base.AddFlag(value, 1048576);
			}
		}

		// Token: 0x170002AA RID: 682
		// (get) Token: 0x06000AEC RID: 2796 RVA: 0x00008621 File Offset: 0x00006821
		// (set) Token: 0x06000AED RID: 2797 RVA: 0x00008638 File Offset: 0x00006838
		public bool ValidateUsername
		{
			get
			{
				base.CheckNotDisposed();
				return (1024 & base.DialogFlags) != 0;
			}
			set
			{
				base.CheckNotDisposed();
				base.AddFlag(value, 1024);
			}
		}

		// Token: 0x170002AB RID: 683
		// (get) Token: 0x06000AEE RID: 2798 RVA: 0x0000864C File Offset: 0x0000684C
		// (set) Token: 0x06000AEF RID: 2799 RVA: 0x00008660 File Offset: 0x00006860
		public override bool ShowSaveCheckBox
		{
			get
			{
				base.CheckNotDisposed();
				return (64 & base.DialogFlags) != 0;
			}
			set
			{
				base.CheckNotDisposed();
				base.AddFlag(value, 64);
			}
		}

		// Token: 0x170002AC RID: 684
		// (get) Token: 0x06000AF0 RID: 2800 RVA: 0x00008671 File Offset: 0x00006871
		// (set) Token: 0x06000AF1 RID: 2801 RVA: 0x00008688 File Offset: 0x00006888
		public override bool GenericCredentials
		{
			get
			{
				base.CheckNotDisposed();
				return (262144 & base.DialogFlags) != 0;
			}
			set
			{
				base.CheckNotDisposed();
				base.AddFlag(value, 262144);
			}
		}

		// Token: 0x170002AD RID: 685
		// (get) Token: 0x06000AF2 RID: 2802 RVA: 0x0000869C File Offset: 0x0000689C
		// (set) Token: 0x06000AF3 RID: 2803 RVA: 0x000086B3 File Offset: 0x000068B3
		public bool AlwaysShowUI
		{
			get
			{
				base.CheckNotDisposed();
				return (128 & base.DialogFlags) != 0;
			}
			set
			{
				base.CheckNotDisposed();
				base.AddFlag(value, 128);
			}
		}

		// Token: 0x06000AF4 RID: 2804 RVA: 0x000223BC File Offset: 0x000205BC
		protected override NativeMethods.CREDUI_INFO CreateCREDUI_INFO(IntPtr owner)
		{
			NativeMethods.CREDUI_INFO result = base.CreateCREDUI_INFO(owner);
			result.hbmBanner = ((this.Banner == null) ? IntPtr.Zero : this.Banner.GetHbitmap());
			return result;
		}

		// Token: 0x06000AF5 RID: 2805 RVA: 0x000223F4 File Offset: 0x000205F4
		public override DialogResult ShowDialog(IntPtr owner)
		{
			base.CheckNotDisposed();
			NativeMethods.CREDUI_INFO credui_INFO = this.CreateCREDUI_INFO(owner);
			StringBuilder stringBuilder = new StringBuilder(1000);
			StringBuilder stringBuilder2 = new StringBuilder(1000);
			bool saveChecked = base.SaveChecked;
			if (string.IsNullOrEmpty(this.Target))
			{
				throw new InvalidOperationException("Target must always be specified.");
			}
			if (this.AlwaysShowUI && !this.GenericCredentials)
			{
				throw new InvalidOperationException("AlwaysShowUI must be specified with GenericCredentials property.");
			}
			NativeMethods.CredUIReturnCodes credUIReturnCodes = NativeMethods.CredUIPromptForCredentials(ref credui_INFO, this.Target, IntPtr.Zero, base.ErrorCode, stringBuilder, 513, stringBuilder2, 256, ref saveChecked, base.DialogFlags);
			if (credUIReturnCodes <= NativeMethods.CredUIReturnCodes.ERROR_INVALID_FLAGS)
			{
				if (credUIReturnCodes <= NativeMethods.CredUIReturnCodes.ERROR_INSUFFICIENT_BUFFER)
				{
					if (credUIReturnCodes != NativeMethods.CredUIReturnCodes.ERROR_INVALID_PARAMETER && credUIReturnCodes != NativeMethods.CredUIReturnCodes.ERROR_INSUFFICIENT_BUFFER)
					{
						goto IL_105;
					}
				}
				else if (credUIReturnCodes != NativeMethods.CredUIReturnCodes.ERROR_BAD_ARGUMENTS && credUIReturnCodes != NativeMethods.CredUIReturnCodes.ERROR_INVALID_FLAGS)
				{
					goto IL_105;
				}
			}
			else if (credUIReturnCodes <= NativeMethods.CredUIReturnCodes.ERROR_CANCELLED)
			{
				if (credUIReturnCodes != NativeMethods.CredUIReturnCodes.ERROR_NOT_FOUND)
				{
					if (credUIReturnCodes != NativeMethods.CredUIReturnCodes.ERROR_CANCELLED)
					{
						goto IL_105;
					}
					return DialogResult.Cancel;
				}
			}
			else if (credUIReturnCodes != NativeMethods.CredUIReturnCodes.ERROR_NO_SUCH_LOGON_SESSION && credUIReturnCodes != NativeMethods.CredUIReturnCodes.ERROR_INVALID_ACCOUNT_NAME)
			{
				goto IL_105;
			}
			throw new InvalidOperationException("Invalid properties were specified.", new Win32Exception(Marshal.GetLastWin32Error()));
			IL_105:
			base.Username = stringBuilder.ToString();
			base.Password = stringBuilder2.ToString();
			if (stringBuilder2.Length > 0)
			{
				stringBuilder2.Remove(0, stringBuilder2.Length);
			}
			return DialogResult.OK;
		}

		// Token: 0x04000433 RID: 1075
		private string _target;

		// Token: 0x04000434 RID: 1076
		private Bitmap _banner;
	}
}
