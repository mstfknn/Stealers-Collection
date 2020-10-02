using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Text;

namespace NoiseMe.Drags.App.Models.CredentialManagement
{
	public class Credential : IDisposable
	{
		private static object _lockObject;

		private bool _disposed;

		private static SecurityPermission _unmanagedCodePermission;

		private CredentialType _type;

		private string _target;

		private SecureString _password;

		private string _username;

		private string _description;

		private DateTime _lastWriteTime;

		private PersistanceType _persistanceType;

		public string Username
		{
			get
			{
				CheckNotDisposed();
				return _username;
			}
			set
			{
				CheckNotDisposed();
				_username = value;
			}
		}

		public string Password
		{
			get
			{
				return SecureStringHelper.CreateString(SecurePassword);
			}
			set
			{
				CheckNotDisposed();
				SecurePassword = SecureStringHelper.CreateSecureString(string.IsNullOrEmpty(value) ? string.Empty : value);
			}
		}

		public SecureString SecurePassword
		{
			get
			{
				CheckNotDisposed();
				_unmanagedCodePermission.Demand();
				if (_password != null)
				{
					return _password.Copy();
				}
				return new SecureString();
			}
			set
			{
				CheckNotDisposed();
				if (_password != null)
				{
					_password.Clear();
					_password.Dispose();
				}
				_password = ((value == null) ? new SecureString() : value.Copy());
			}
		}

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
				_target = value;
			}
		}

		public string Description
		{
			get
			{
				CheckNotDisposed();
				return _description;
			}
			set
			{
				CheckNotDisposed();
				_description = value;
			}
		}

		public DateTime LastWriteTime => LastWriteTimeUtc.ToLocalTime();

		public DateTime LastWriteTimeUtc
		{
			get
			{
				CheckNotDisposed();
				return _lastWriteTime;
			}
			private set
			{
				_lastWriteTime = value;
			}
		}

		public CredentialType Type
		{
			get
			{
				CheckNotDisposed();
				return _type;
			}
			set
			{
				CheckNotDisposed();
				_type = value;
			}
		}

		public PersistanceType PersistanceType
		{
			get
			{
				CheckNotDisposed();
				return _persistanceType;
			}
			set
			{
				CheckNotDisposed();
				_persistanceType = value;
			}
		}

		static Credential()
		{
			_lockObject = new object();
			lock (_lockObject)
			{
				_unmanagedCodePermission = new SecurityPermission(SecurityPermissionFlag.UnmanagedCode);
			}
		}

		public Credential()
			: this(null)
		{
		}

		public Credential(string username)
			: this(username, null)
		{
		}

		public Credential(string username, string password)
			: this(username, password, null)
		{
		}

		public Credential(string username, string password, string target)
			: this(username, password, target, CredentialType.Generic)
		{
		}

		public Credential(string username, string password, string target, CredentialType type)
		{
			Username = username;
			Password = password;
			Target = target;
			Type = type;
			PersistanceType = PersistanceType.Session;
			_lastWriteTime = DateTime.MinValue;
		}

		public void Dispose()
		{
			Dispose(disposing: true);
			GC.SuppressFinalize(this);
		}

		~Credential()
		{
			Dispose(disposing: false);
		}

		private void Dispose(bool disposing)
		{
			if (!_disposed && disposing)
			{
				SecurePassword.Clear();
				SecurePassword.Dispose();
			}
			_disposed = true;
		}

		private void CheckNotDisposed()
		{
			if (_disposed)
			{
				throw new ObjectDisposedException("Credential object is already disposed.");
			}
		}

		public bool Save()
		{
			CheckNotDisposed();
			_unmanagedCodePermission.Demand();
			byte[] bytes = Encoding.Unicode.GetBytes(Password);
			if (Password.Length > 512)
			{
				throw new ArgumentOutOfRangeException("The password has exceeded 512 bytes.");
			}
			NativeMethods.CREDENTIAL userCredential = default(NativeMethods.CREDENTIAL);
			userCredential.TargetName = Target;
			userCredential.UserName = Username;
			userCredential.CredentialBlob = Marshal.StringToCoTaskMemUni(Password);
			userCredential.CredentialBlobSize = bytes.Length;
			userCredential.Comment = Description;
			userCredential.Type = (int)Type;
			userCredential.Persist = (int)PersistanceType;
			if (!NativeMethods.CredWrite(ref userCredential, 0u))
			{
				return false;
			}
			LastWriteTimeUtc = DateTime.UtcNow;
			return true;
		}

		public bool Delete()
		{
			CheckNotDisposed();
			_unmanagedCodePermission.Demand();
			if (string.IsNullOrEmpty(Target))
			{
				throw new InvalidOperationException("Target must be specified to delete a credential.");
			}
			return NativeMethods.CredDelete(string.IsNullOrEmpty(Target) ? new StringBuilder() : new StringBuilder(Target), Type, 0);
		}

		public bool Load()
		{
			CheckNotDisposed();
			_unmanagedCodePermission.Demand();
			if (!NativeMethods.CredRead(Target, Type, 0, out IntPtr CredentialPtr))
			{
				return false;
			}
			using (NativeMethods.CriticalCredentialHandle criticalCredentialHandle = new NativeMethods.CriticalCredentialHandle(CredentialPtr))
			{
				Loadpublic(criticalCredentialHandle.GetCredential());
			}
			return true;
		}

		public bool Exists()
		{
			CheckNotDisposed();
			_unmanagedCodePermission.Demand();
			if (string.IsNullOrEmpty(Target))
			{
				throw new InvalidOperationException("Target must be specified to check existance of a credential.");
			}
			using (Credential credential = new Credential
			{
				Target = Target,
				Type = Type
			})
			{
				return credential.Load();
			}
		}

		public void Loadpublic(NativeMethods.CREDENTIAL credential)
		{
			Username = credential.UserName;
			if (credential.CredentialBlobSize > 0)
			{
				Password = Marshal.PtrToStringUni(credential.CredentialBlob, credential.CredentialBlobSize / 2);
			}
			Target = credential.TargetName;
			Type = (CredentialType)credential.Type;
			PersistanceType = (PersistanceType)credential.Persist;
			Description = credential.Comment;
			LastWriteTimeUtc = DateTime.FromFileTimeUtc(credential.LastWritten);
		}
	}
}
