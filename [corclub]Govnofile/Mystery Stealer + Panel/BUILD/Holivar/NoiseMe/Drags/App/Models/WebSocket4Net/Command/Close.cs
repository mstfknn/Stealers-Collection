using System;

namespace NoiseMe.Drags.App.Models.WebSocket4Net.Command
{
	// Token: 0x020000C6 RID: 198
	public class Close : WebSocketCommandBase
	{
		// Token: 0x0600068C RID: 1676 RVA: 0x0001AB4C File Offset: 0x00018D4C
		public override void ExecuteCommand(WebSocket session, WebSocketCommandInfo commandInfo)
		{
			if (session.StateCode == 2)
			{
				session.CloseWithoutHandshake();
				return;
			}
			short num = commandInfo.CloseStatusCode;
			if (num <= 0)
			{
				num = session.ProtocolProcessor.CloseStatusCode.NoStatusCode;
			}
			session.Close((int)num, commandInfo.Text);
		}

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x0600068D RID: 1677 RVA: 0x0001AB94 File Offset: 0x00018D94
		public override string Name
		{
			get
			{
				return 8.ToString();
			}
		}
	}
}
