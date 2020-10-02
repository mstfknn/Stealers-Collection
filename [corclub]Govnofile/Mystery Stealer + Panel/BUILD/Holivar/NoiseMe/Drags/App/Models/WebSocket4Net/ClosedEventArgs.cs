using System;

namespace NoiseMe.Drags.App.Models.WebSocket4Net
{
	// Token: 0x02000092 RID: 146
	public class ClosedEventArgs : EventArgs
	{
		// Token: 0x17000149 RID: 329
		// (get) Token: 0x060004D8 RID: 1240 RVA: 0x00004FF7 File Offset: 0x000031F7
		// (set) Token: 0x060004D9 RID: 1241 RVA: 0x00004FFF File Offset: 0x000031FF
		public short Code { get; private set; }

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x060004DA RID: 1242 RVA: 0x00005008 File Offset: 0x00003208
		// (set) Token: 0x060004DB RID: 1243 RVA: 0x00005010 File Offset: 0x00003210
		public string Reason { get; private set; }

		// Token: 0x060004DC RID: 1244 RVA: 0x00005019 File Offset: 0x00003219
		public ClosedEventArgs(short code, string reason)
		{
			this.Code = code;
			this.Reason = reason;
		}
	}
}
