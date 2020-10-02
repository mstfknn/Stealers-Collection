using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;

namespace NoiseMe.Drags.App.Models.CredentialManagement
{
	public abstract class BaseCredentialsPrompt : ICredentialsPrompt, IDisposable
	{
		private bool _disposed;

		private static SecurityPermission _unmanagedCodePermission;

		private static object _lockObject;

		private string _username;

		private SecureString _password;

		private bool _saveChecked;

		private string _message;

		private string _title;

		private int _errorCode;

		private int _dialogFlags;

		public bool SaveChecked
		{
			get
			{
				CheckNotDisposed();
				return _saveChecked;
			}
			set
			{
				CheckNotDisposed();
				_saveChecked = value;
			}
		}

		public string Message
		{
			get
			{
				CheckNotDisposed();
				return _message;
			}
			set
			{
				CheckNotDisposed();
				if (string.IsNullOrEmpty(value))
				{
					throw new ArgumentNullException("value");
				}
				if (value.Length > 32767)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				_message = value;
			}
		}

		public string Title
		{
			get
			{
				CheckNotDisposed();
				return _title;
			}
			set
			{
				CheckNotDisposed();
				if (string.IsNullOrEmpty(value))
				{
					throw new ArgumentNullException("value");
				}
				if (value.Length > 128)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				_title = value;
			}
		}

		public string Username
		{
			get
			{
				CheckNotDisposed();
				return _username ?? string.Empty;
			}
			set
			{
				CheckNotDisposed();
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (value.Length > 513)
				{
					throw new ArgumentOutOfRangeException("value");
				}
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
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (value.Length > 256)
				{
					throw new ArgumentOutOfRangeException("value");
				}
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

		public int ErrorCode
		{
			get
			{
				CheckNotDisposed();
				return _errorCode;
			}
			set
			{
				CheckNotDisposed();
				_errorCode = value;
			}
		}

		public abstract bool ShowSaveCheckBox
		{
			get;
			set;
		}

		public abstract bool GenericCredentials
		{
			get;
			set;
		}

		protected int DialogFlags => _dialogFlags;

		static BaseCredentialsPrompt()
		{
			_lockObject = new object();
			lock (_lockObject)
			{
				_unmanagedCodePermission = new SecurityPermission(SecurityPermissionFlag.UnmanagedCode);
			}
		}

		protected void AddFlag(bool add, int flag)
		{
			if (add)
			{
				_dialogFlags |= flag;
			}
			else
			{
				_dialogFlags &= ~flag;
			}
		}

		protected virtual NativeMethods.CREDUI_INFO CreateCREDUI_INFO(IntPtr owner)
		{
			NativeMethods.CREDUI_INFO cREDUI_INFO = default(NativeMethods.CREDUI_INFO);
			cREDUI_INFO.cbSize = Marshal.SizeOf(cREDUI_INFO);
			cREDUI_INFO.hwndParent = owner;
			cREDUI_INFO.pszCaptionText = Title;
			cREDUI_INFO.pszMessageText = Message;
			return cREDUI_INFO;
		}

		protected void CheckNotDisposed()
		{
			if (_disposed)
			{
				throw new ObjectDisposedException("CredentialsPrompt object is already disposed.");
			}
		}

		public void Dispose()
		{
			Dispose(disposing: true);
			GC.SuppressFinalize(this);
		}

		~BaseCredentialsPrompt()
		{
			Dispose(disposing: false);
		}

		private void Dispose(bool disposing)
		{
			if (_disposed)
			{
			}
			_disposed = true;
		}

		public virtual DialogResult ShowDialog()
		{
			return ShowDialog(IntPtr.Zero);
		}

		public abstract DialogResult ShowDialog(IntPtr owner);
	}
}
