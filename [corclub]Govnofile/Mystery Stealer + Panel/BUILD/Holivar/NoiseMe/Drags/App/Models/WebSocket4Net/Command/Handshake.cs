using System;

namespace NoiseMe.Drags.App.Models.WebSocket4Net.Command
{
	// Token: 0x020000C7 RID: 199
	public class Handshake : WebSocketCommandBase
	{
		// Token: 0x0600068F RID: 1679 RVA: 0x0001ABAC File Offset: 0x00018DAC
		public override void ExecuteCommand(WebSocket session, WebSocketCommandInfo commandInfo)
		{
			string text;
			if (!session.ProtocolProcessor.VerifyHandshake(session, commandInfo, out text))
			{
				session.FireError(new Exception(text));
				session.Close((int)session.ProtocolProcessor.CloseStatusCode.ProtocolError, text);
				return;
			}
			session.OnHandshaked();
		}

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x06000690 RID: 1680 RVA: 0x0001ABF4 File Offset: 0x00018DF4
		public override string Name
		{
			get
			{
				return -1.ToString();
			}
		}
	}
}
