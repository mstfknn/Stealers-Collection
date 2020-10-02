using System;

namespace NoiseMe.Drags.App.Models.WebSocket4Net
{
	// Token: 0x02000093 RID: 147
	public class DataReceivedEventArgs : EventArgs
	{
		// Token: 0x060004DD RID: 1245 RVA: 0x0000502F File Offset: 0x0000322F
		public DataReceivedEventArgs(byte[] data)
		{
			this.Data = data;
		}

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x060004DE RID: 1246 RVA: 0x0000503E File Offset: 0x0000323E
		// (set) Token: 0x060004DF RID: 1247 RVA: 0x00005046 File Offset: 0x00003246
		public byte[] Data { get; private set; }
	}
}
