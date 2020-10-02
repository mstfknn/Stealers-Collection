using System;

namespace NoiseMe.Drags.App.Models.WebSocket4Net.Command
{
	public class Pong : WebSocketCommandBase
	{
		public override string Name => 10.ToString();

		public override void ExecuteCommand(WebSocket session, WebSocketCommandInfo commandInfo)
		{
			session.LastActiveTime = DateTime.Now;
			session.LastPongResponse = commandInfo.Text;
		}
	}
}
