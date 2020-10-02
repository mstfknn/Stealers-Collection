using System;

namespace NoiseMe.Drags.App.Models.WebSocket4Net.Command
{
	// Token: 0x020000C9 RID: 201
	public class Pong : WebSocketCommandBase
	{
		// Token: 0x06000695 RID: 1685 RVA: 0x00005ED0 File Offset: 0x000040D0
		public override void ExecuteCommand(WebSocket session, WebSocketCommandInfo commandInfo)
		{
			session.LastActiveTime = DateTime.Now;
			session.LastPongResponse = commandInfo.Text;
		}

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x06000696 RID: 1686 RVA: 0x0001AC24 File Offset: 0x00018E24
		public override string Name
		{
			get
			{
				return 10.ToString();
			}
		}
	}
}
