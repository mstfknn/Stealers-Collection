using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Text;

namespace NoiseMe.Drags.App.Models.CredentialManagement
{
	// Token: 0x02000145 RID: 325
	public class Credential : IDisposable
	{
		// Token: 0x06000A77 RID: 2679 RVA: 0x00021C00 File Offset: 0x0001FE00
		static Credential()
		{
			object lockObject = Credential._lockObject;
			lock (lockObject)
			{
				Credential._unmanagedCodePermission = new SecurityPermission(SecurityPermissionFlag.UnmanagedCode);
			}
		}

		// Token: 0x06000A78 RID: 2680 RVA: 0x0000811E File Offset: 0x0000631E
		public Credential() : this(null)
		{
		}

		// Token: 0x06000A79 RID: 2681 RVA: 0x00008127 File Offset: 0x00006327
		public Credential(string username) : this(username, null)
		{
		}

		// Token: 0x06000A7A RID: 2682 RVA: 0x00008131 File Offset: 0x00006331
		public Credential(string username, string password) : this(username, password, null)
		{
		}

		// Token: 0x06000A7B RID: 2683 RVA: 0x0000813C File Offset: 0x0000633C
		public Credential(string username, string password, string target) : this(username, password, target, CredentialType.Generic)
		{
		}

		// Token: 0x06000A7C RID: 2684 RVA: 0x00008148 File Offset: 0x00006348
		public Credential(string username, string password, string target, CredentialType type)
		{
			this.Username = username;
			this.Password = password;
			this.Target = target;
			this.Type = type;
			this.PersistanceType = PersistanceType.Session;
			this._lastWriteTime = DateTime.MinValue;
		}

		// Token: 0x06000A7D RID: 2685 RVA: 0x0000817F File Offset: 0x0000637F
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000A7E RID: 2686 RVA: 0x00021C48 File Offset: 0x0001FE48
		~Credential()
		{
			this.Dispose(false);
		}

		// Token: 0x06000A7F RID: 2687 RVA: 0x0000818E File Offset: 0x0000638E
		private void Dispose(bool disposing)
		{
			if (!this._disposed && disposing)
			{
				this.SecurePassword.Clear();
				this.SecurePassword.Dispose();
			}
			this._disposed = true;
		}

		// Token: 0x06000A80 RID: 2688 RVA: 0x000081B8 File Offset: 0x000063B8
		private void CheckNotDisposed()
		{
			if (this._disposed)
			{
				throw new ObjectDisposedException("Credential object is already disposed.");
			}
		}

		// Token: 0x17000287 RID: 647
		// (get) Token: 0x06000A81 RID: 2689 RVA: 0x000081CD File Offset: 0x000063CD
		// (set) Token: 0x06000A82 RID: 2690 RVA: 0x000081DB File Offset: 0x000063DB
		public string Username
		{
			get
			{
				this.CheckNotDisposed();
				return this._username;
			}
			set
			{
				this.CheckNotDisposed();
				this._username = value;
			}
		}

		// Token: 0x17000288 RID: 648
		// (get) Token: 0x06000A83 RID: 2691 RVA: 0x000081EA File Offset: 0x000063EA
		// (set) Token: 0x06000A84 RID: 2692 RVA: 0x000081F7 File Offset: 0x000063F7
		public string Password
		{
			get
			{
				return SecureStringHelper.CreateString(this.SecurePassword);
			}
			set
			{
				this.CheckNotDisposed();
				this.SecurePassword = SecureStringHelper.CreateSecureString(string.IsNullOrEmpty(value) ? string.Empty : value);
			}
		}

		// Token: 0x17000289 RID: 649
		// (get) Token: 0x06000A85 RID: 2693 RVA: 0x0000821A File Offset: 0x0000641A
		// (set) Token: 0x06000A86 RID: 2694 RVA: 0x00008245 File Offset: 0x00006445
		public SecureString SecurePassword
		{
			get
			{
				this.CheckNotDisposed();
				Credential._unmanagedCodePermission.Demand();
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

		// Token: 0x1700028A RID: 650
		// (get) Token: 0x06000A87 RID: 2695 RVA: 0x00008281 File Offset: 0x00006481
		// (set) Token: 0x06000A88 RID: 2696 RVA: 0x0000828F File Offset: 0x0000648F
		public string Target
		{
			get
			{
				this.CheckNotDisposed();
				return this._target;
			}
			set
			{
				this.CheckNotDisposed();
				this._target = value;
			}
		}

		// Token: 0x1700028B RID: 651
		// (get) Token: 0x06000A89 RID: 2697 RVA: 0x0000829E File Offset: 0x0000649E
		// (set) Token: 0x06000A8A RID: 2698 RVA: 0x000082AC File Offset: 0x000064AC
		public string Description
		{
			get
			{
				this.CheckNotDisposed();
				return this._description;
			}
			set
			{
				this.CheckNotDisposed();
				this._description = value;
			}
		}

		// Token: 0x1700028C RID: 652
		// (get) Token: 0x06000A8B RID: 2699 RVA: 0x00021C78 File Offset: 0x0001FE78
		public DateTime LastWriteTime
		{
			get
			{
				return this.LastWriteTimeUtc.ToLocalTime();
			}
		}

		// Token: 0x1700028D RID: 653
		// (get) Token: 0x06000A8C RID: 2700 RVA: 0x000082BB File Offset: 0x000064BB
		// (set) Token: 0x06000A8D RID: 2701 RVA: 0x000082C9 File Offset: 0x000064C9
		public DateTime LastWriteTimeUtc
		{
			get
			{
				this.CheckNotDisposed();
				return this._lastWriteTime;
			}
			private set
			{
				this._lastWriteTime = value;
			}
		}

		// Token: 0x1700028E RID: 654
		// (get) Token: 0x06000A8E RID: 2702 RVA: 0x000082D2 File Offset: 0x000064D2
		// (set) Token: 0x06000A8F RID: 2703 RVA: 0x000082E0 File Offset: 0x000064E0
		public CredentialType Type
		{
			get
			{
				this.CheckNotDisposed();
				return this._type;
			}
			set
			{
				this.CheckNotDisposed();
				this._type = value;
			}
		}

		// Token: 0x1700028F RID: 655
		// (get) Token: 0x06000A90 RID: 2704 RVA: 0x000082EF File Offset: 0x000064EF
		// (set) Token: 0x06000A91 RID: 2705 RVA: 0x000082FD File Offset: 0x000064FD
		public PersistanceType PersistanceType
		{
			get
			{
				this.CheckNotDisposed();
				return this._persistanceType;
			}
			set
			{
				this.CheckNotDisposed();
				this._persistanceType = value;
			}
		}

		// Token: 0x06000A92 RID: 2706 RVA: 0x00021C94 File Offset: 0x0001FE94
		public bool Save()
		{
			this.CheckNotDisposed();
			Credential._unmanagedCodePermission.Demand();
			byte[] bytes = Encoding.Unicode.GetBytes(this.Password);
			if (this.Password.Length > 512)
			{
				throw new ArgumentOutOfRangeException("The password has exceeded 512 bytes.");
			}
			NativeMethods.CREDENTIAL credential = default(NativeMethods.CREDENTIAL);
			credential.TargetName = this.Target;
			credential.UserName = this.Username;
			credential.CredentialBlob = Marshal.StringToCoTaskMemUni(this.Password);
			credential.CredentialBlobSize = bytes.Length;
			credential.Comment = this.Description;
			credential.Type = (int)this.Type;
			credential.Persist = (int)this.PersistanceType;
			if (!NativeMethods.CredWrite(ref credential, 0u))
			{
				return false;
			}
			this.LastWriteTimeUtc = DateTime.UtcNow;
			return true;
		}

		// Token: 0x06000A93 RID: 2707 RVA: 0x00021D5C File Offset: 0x0001FF5C
		public bool Delete()
		{
			this.CheckNotDisposed();
			Credential._unmanagedCodePermission.Demand();
			if (string.IsNullOrEmpty(this.Target))
			{
				throw new InvalidOperationException("Target must be specified to delete a credential.");
			}
			return NativeMethods.CredDelete(string.IsNullOrEmpty(this.Target) ? new StringBuilder() : new StringBuilder(this.Target), this.Type, 0);
		}

		// Token: 0x06000A94 RID: 2708 RVA: 0x00021DBC File Offset: 0x0001FFBC
		public bool Load()
		{
			this.CheckNotDisposed();
			Credential._unmanagedCodePermission.Demand();
			IntPtr preexistingHandle;
			if (!NativeMethods.CredRead(this.Target, this.Type, 0, out preexistingHandle))
			{
				return false;
			}
			using (NativeMethods.CriticalCredentialHandle criticalCredentialHandle = new NativeMethods.CriticalCredentialHandle(preexistingHandle))
			{
				this.Loadpublic(criticalCredentialHandle.GetCredential());
			}
			return true;
		}

		// Token: 0x06000A95 RID: 2709 RVA: 0x00021E24 File Offset: 0x00020024
		public bool Exists()
		{
			this.CheckNotDisposed();
			Credential._unmanagedCodePermission.Demand();
			if (string.IsNullOrEmpty(this.Target))
			{
				throw new InvalidOperationException("Target must be specified to check existance of a credential.");
			}
			bool result;
			using (Credential credential = new Credential
			{
				Target = this.Target,
				Type = this.Type
			})
			{
				result = credential.Load();
			}
			return result;
		}

		// Token: 0x06000A96 RID: 2710 RVA: 0x00021E9C File Offset: 0x0002009C
		public void Loadpublic(NativeMethods.CREDENTIAL credential)
		{
			this.Username = credential.UserName;
			if (credential.CredentialBlobSize > 0)
			{
				this.Password = Marshal.PtrToStringUni(credential.CredentialBlob, credential.CredentialBlobSize / 2);
			}
			this.Target = credential.TargetName;
			this.Type = (CredentialType)credential.Type;
			this.PersistanceType = (PersistanceType)credential.Persist;
			this.Description = credential.Comment;
			this.LastWriteTimeUtc = DateTime.FromFileTimeUtc(credential.LastWritten);
		}

		// Token: 0x040003CD RID: 973
		private static object _lockObject = new object();

		// Token: 0x040003CE RID: 974
		private bool _disposed;

		// Token: 0x040003CF RID: 975
		private static SecurityPermission _unmanagedCodePermission;

		// Token: 0x040003D0 RID: 976
		private CredentialType _type;

		// Token: 0x040003D1 RID: 977
		private string _target;

		// Token: 0x040003D2 RID: 978
		private SecureString _password;

		// Token: 0x040003D3 RID: 979
		private string _username;

		// Token: 0x040003D4 RID: 980
		private string _description;

		// Token: 0x040003D5 RID: 981
		private DateTime _lastWriteTime;

		// Token: 0x040003D6 RID: 982
		private PersistanceType _persistanceType;
	}
}
