using System;

namespace NoiseMe.Drags.App.Models.WebSocket4Net.Command
{
	public class Handshake : WebSocketCommandBase
	{
		public override string Name => (-1).ToString();

		public override void ExecuteCommand(WebSocket session, WebSocketCommandInfo commandInfo)
		{
			if (!session.ProtocolProcessor.VerifyHandshake(session, commandInfo, out string description))
			{
				session.FireError(new Exception(description));
				session.Close(session.ProtocolProcessor.CloseStatusCode.ProtocolError, description);
			}
			else
			{
				session.OnHandshaked();
			}
		}
	}
}
