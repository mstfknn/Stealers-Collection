using System;

namespace NoiseMe.Drags.App.Models.SuperSocket.ClientEngine.Proxy
{
	// Token: 0x02000126 RID: 294
	internal class ReceiveState
	{
		// Token: 0x0600092C RID: 2348 RVA: 0x0000715E File Offset: 0x0000535E
		public ReceiveState(byte[] buffer)
		{
			this.Buffer = buffer;
		}

		// Token: 0x17000253 RID: 595
		// (get) Token: 0x0600092D RID: 2349 RVA: 0x0000716D File Offset: 0x0000536D
		// (set) Token: 0x0600092E RID: 2350 RVA: 0x00007175 File Offset: 0x00005375
		public byte[] Buffer { get; private set; }

		// Token: 0x17000254 RID: 596
		// (get) Token: 0x0600092F RID: 2351 RVA: 0x0000717E File Offset: 0x0000537E
		// (set) Token: 0x06000930 RID: 2352 RVA: 0x00007186 File Offset: 0x00005386
		public int Length { get; set; }
	}
}
