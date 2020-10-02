using System;

namespace NoiseMe.Drags.App.Models.WebSocket4Net.Command
{
	// Token: 0x020000CA RID: 202
	public class Text : WebSocketCommandBase
	{
		// Token: 0x06000698 RID: 1688 RVA: 0x00005EE9 File Offset: 0x000040E9
		public override void ExecuteCommand(WebSocket session, WebSocketCommandInfo commandInfo)
		{
			session.FireMessageReceived(commandInfo.Text);
		}

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x06000699 RID: 1689 RVA: 0x0001AC3C File Offset: 0x00018E3C
		public override string Name
		{
			get
			{
				return 1.ToString();
			}
		}
	}
}
