using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;

namespace NoiseMe.Drags.App.Models.CredentialManagement
{
	// Token: 0x02000144 RID: 324
	public abstract class BaseCredentialsPrompt : ICredentialsPrompt, IDisposable
	{
		// Token: 0x06000A5A RID: 2650 RVA: 0x00021AE8 File Offset: 0x0001FCE8
		static BaseCredentialsPrompt()
		{
			object lockObject = BaseCredentialsPrompt._lockObject;
			lock (lockObject)
			{
				BaseCredentialsPrompt._unmanagedCodePermission = new SecurityPermission(SecurityPermissionFlag.UnmanagedCode);
			}
		}

		// Token: 0x06000A5B RID: 2651 RVA: 0x00007F25 File Offset: 0x00006125
		protected void AddFlag(bool add, int flag)
		{
			if (add)
			{
				this._dialogFlags |= flag;
				return;
			}
			this._dialogFlags &= ~flag;
		}

		// Token: 0x06000A5C RID: 2652 RVA: 0x00021B30 File Offset: 0x0001FD30
		protected virtual NativeMethods.CREDUI_INFO CreateCREDUI_INFO(IntPtr owner)
		{
			NativeMethods.CREDUI_INFO credui_INFO = default(NativeMethods.CREDUI_INFO);
			credui_INFO.cbSize = Marshal.SizeOf(credui_INFO);
			credui_INFO.hwndParent = owner;
			credui_INFO.pszCaptionText = this.Title;
			credui_INFO.pszMessageText = this.Message;
			return credui_INFO;
		}

		// Token: 0x06000A5D RID: 2653 RVA: 0x00007F48 File Offset: 0x00006148
		protected void CheckNotDisposed()
		{
			if (this._disposed)
			{
				throw new ObjectDisposedException("CredentialsPrompt object is already disposed.");
			}
		}

		// Token: 0x06000A5E RID: 2654 RVA: 0x00007F5D File Offset: 0x0000615D
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000A5F RID: 2655 RVA: 0x00021B7C File Offset: 0x0001FD7C
		~BaseCredentialsPrompt()
		{
			this.Dispose(false);
		}

		// Token: 0x06000A60 RID: 2656 RVA: 0x00007F6C File Offset: 0x0000616C
		private void Dispose(bool disposing)
		{
			if (!this._disposed)
			{
			}
			this._disposed = true;
		}

		// Token: 0x1700027D RID: 637
		// (get) Token: 0x06000A61 RID: 2657 RVA: 0x00007F7F File Offset: 0x0000617F
		// (set) Token: 0x06000A62 RID: 2658 RVA: 0x00007F8D File Offset: 0x0000618D
		public bool SaveChecked
		{
			get
			{
				this.CheckNotDisposed();
				return this._saveChecked;
			}
			set
			{
				this.CheckNotDisposed();
				this._saveChecked = value;
			}
		}

		// Token: 0x1700027E RID: 638
		// (get) Token: 0x06000A63 RID: 2659 RVA: 0x00007F9C File Offset: 0x0000619C
		// (set) Token: 0x06000A64 RID: 2660 RVA: 0x00007FAA File Offset: 0x000061AA
		public string Message
		{
			get
			{
				this.CheckNotDisposed();
				return this._message;
			}
			set
			{
				this.CheckNotDisposed();
				if (string.IsNullOrEmpty(value))
				{
					throw new ArgumentNullException("value");
				}
				if (value.Length > 32767)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._message = value;
			}
		}

		// Token: 0x1700027F RID: 639
		// (get) Token: 0x06000A65 RID: 2661 RVA: 0x00007FE4 File Offset: 0x000061E4
		// (set) Token: 0x06000A66 RID: 2662 RVA: 0x00007FF2 File Offset: 0x000061F2
		public string Title
		{
			get
			{
				this.CheckNotDisposed();
				return this._title;
			}
			set
			{
				this.CheckNotDisposed();
				if (string.IsNullOrEmpty(value))
				{
					throw new ArgumentNullException("value");
				}
				if (value.Length > 128)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._title = value;
			}
		}

		// Token: 0x17000280 RID: 640
		// (get) Token: 0x06000A67 RID: 2663 RVA: 0x0000802C File Offset: 0x0000622C
		// (set) Token: 0x06000A68 RID: 2664 RVA: 0x00008043 File Offset: 0x00006243
		public string Username
		{
			get
			{
				this.CheckNotDisposed();
				return this._username ?? string.Empty;
			}
			set
			{
				this.CheckNotDisposed();
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (value.Length > 513)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._username = value;
			}
		}

		// Token: 0x17000281 RID: 641
		// (get) Token: 0x06000A69 RID: 2665 RVA: 0x00008078 File Offset: 0x00006278
		// (set) Token: 0x06000A6A RID: 2666 RVA: 0x00021BAC File Offset: 0x0001FDAC
		public string Password
		{
			get
			{
				return SecureStringHelper.CreateString(this.SecurePassword);
			}
			set
			{
				this.CheckNotDisposed();
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (value.Length > 256)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.SecurePassword = SecureStringHelper.CreateSecureString(string.IsNullOrEmpty(value) ? string.Empty : value);
			}
		}

		// Token: 0x17000282 RID: 642
		// (get) Token: 0x06000A6B RID: 2667 RVA: 0x00008085 File Offset: 0x00006285
		// (set) Token: 0x06000A6C RID: 2668 RVA: 0x000080B0 File Offset: 0x000062B0
		public SecureString SecurePassword
		{
			get
			{
				this.CheckNotDisposed();
				BaseCredentialsPrompt._unmanagedCodePermission.Demand();
				if (this._password != null)
				{
					return this._password.Copy();
				}
				return new SecureString();
			}
			set
			{
				this.CheckNotDisposed();
				if (this._password != null)
				{
					this._password.Clear();
					this._password.Dispose();
				}
				this._password = ((value == null) ? new SecureString() : value.Copy());
			}
		}

		// Token: 0x17000283 RID: 643
		// (get) Token: 0x06000A6D RID: 2669 RVA: 0x000080EC File Offset: 0x000062EC
		// (set) Token: 0x06000A6E RID: 2670 RVA: 0x000080FA File Offset: 0x000062FA
		public int ErrorCode
		{
			get
			{
				this.CheckNotDisposed();
				return this._errorCode;
			}
			set
			{
				this.CheckNotDisposed();
				this._errorCode = value;
			}
		}

		// Token: 0x17000284 RID: 644
		// (get) Token: 0x06000A6F RID: 2671
		// (set) Token: 0x06000A70 RID: 2672
		public abstract bool ShowSaveCheckBox { get; set; }

		// Token: 0x17000285 RID: 645
		// (get) Token: 0x06000A71 RID: 2673
		// (set) Token: 0x06000A72 RID: 2674
		public abstract bool GenericCredentials { get; set; }

		// Token: 0x17000286 RID: 646
		// (get) Token: 0x06000A73 RID: 2675 RVA: 0x00008109 File Offset: 0x00006309
		protected int DialogFlags
		{
			get
			{
				return this._dialogFlags;
			}
		}

		// Token: 0x06000A74 RID: 2676 RVA: 0x00008111 File Offset: 0x00006311
		public virtual DialogResult ShowDialog()
		{
			return this.ShowDialog(IntPtr.Zero);
		}

		// Token: 0x06000A75 RID: 2677
		public abstract DialogResult ShowDialog(IntPtr owner);

		// Token: 0x040003C3 RID: 963
		private bool _disposed;

		// Token: 0x040003C4 RID: 964
		private static SecurityPermission _unmanagedCodePermission;

		// Token: 0x040003C5 RID: 965
		private static object _lockObject = new object();

		// Token: 0x040003C6 RID: 966
		private string _username;

		// Token: 0x040003C7 RID: 967
		private SecureString _password;

		// Token: 0x040003C8 RID: 968
		private bool _saveChecked;

		// Token: 0x040003C9 RID: 969
		private string _message;

		// Token: 0x040003CA RID: 970
		private string _title;

		// Token: 0x040003CB RID: 971
		private int _errorCode;

		// Token: 0x040003CC RID: 972
		private int _dialogFlags;
	}
}
