using System;

namespace NoiseMe.Drags.App.Models.WebSocket4Net.Command
{
	// Token: 0x020000C5 RID: 197
	public class Binary : WebSocketCommandBase
	{
		// Token: 0x06000689 RID: 1673 RVA: 0x00005EA3 File Offset: 0x000040A3
		public override void ExecuteCommand(WebSocket session, WebSocketCommandInfo commandInfo)
		{
			session.FireDataReceived(commandInfo.Data);
		}

		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x0600068A RID: 1674 RVA: 0x0001AB34 File Offset: 0x00018D34
		public override string Name
		{
			get
			{
				return 2.ToString();
			}
		}
	}
}
