using System;

namespace NoiseMe.Drags.App.Models.WebSocket4Net
{
	// Token: 0x02000095 RID: 149
	public class MessageReceivedEventArgs : EventArgs
	{
		// Token: 0x060004EC RID: 1260 RVA: 0x00005100 File Offset: 0x00003300
		public MessageReceivedEventArgs(string message)
		{
			this.Message = message;
		}

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x060004ED RID: 1261 RVA: 0x0000510F File Offset: 0x0000330F
		// (set) Token: 0x060004EE RID: 1262 RVA: 0x00005117 File Offset: 0x00003317
		public string Message { get; private set; }
	}
}
