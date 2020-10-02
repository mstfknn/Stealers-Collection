using System;

namespace NoiseMe.Drags.App.Models.WebSocket4Net.Command
{
	// Token: 0x020000C8 RID: 200
	public class Ping : WebSocketCommandBase
	{
		// Token: 0x06000692 RID: 1682 RVA: 0x00005EB1 File Offset: 0x000040B1
		public override void ExecuteCommand(WebSocket session, WebSocketCommandInfo commandInfo)
		{
			session.LastActiveTime = DateTime.Now;
			session.ProtocolProcessor.SendPong(session, commandInfo.Text);
		}

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x06000693 RID: 1683 RVA: 0x0001AC0C File Offset: 0x00018E0C
		public override string Name
		{
			get
			{
				return 9.ToString();
			}
		}
	}
}
