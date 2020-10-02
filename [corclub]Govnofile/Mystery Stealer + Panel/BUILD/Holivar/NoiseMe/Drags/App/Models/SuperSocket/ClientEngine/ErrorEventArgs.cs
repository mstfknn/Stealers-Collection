using System;

namespace NoiseMe.Drags.App.Models.SuperSocket.ClientEngine
{
	// Token: 0x02000116 RID: 278
	public class ErrorEventArgs : EventArgs
	{
		// Token: 0x17000236 RID: 566
		// (get) Token: 0x06000888 RID: 2184 RVA: 0x00006CFC File Offset: 0x00004EFC
		// (set) Token: 0x06000889 RID: 2185 RVA: 0x00006D04 File Offset: 0x00004F04
		public Exception Exception { get; private set; }

		// Token: 0x0600088A RID: 2186 RVA: 0x00006D0D File Offset: 0x00004F0D
		public ErrorEventArgs(Exception exception)
		{
			this.Exception = exception;
		}
	}
}
