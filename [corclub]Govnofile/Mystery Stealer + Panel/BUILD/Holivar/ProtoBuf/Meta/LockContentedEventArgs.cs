using System;

namespace ProtoBuf.Meta
{
	// Token: 0x0200006D RID: 109
	public sealed class LockContentedEventArgs : EventArgs
	{
		// Token: 0x06000392 RID: 914 RVA: 0x000043C2 File Offset: 0x000025C2
		internal LockContentedEventArgs(string ownerStackTrace)
		{
			this.ownerStackTrace = ownerStackTrace;
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x06000393 RID: 915 RVA: 0x000043D1 File Offset: 0x000025D1
		public string OwnerStackTrace
		{
			get
			{
				return this.ownerStackTrace;
			}
		}

		// Token: 0x04000174 RID: 372
		private readonly string ownerStackTrace;
	}
}
