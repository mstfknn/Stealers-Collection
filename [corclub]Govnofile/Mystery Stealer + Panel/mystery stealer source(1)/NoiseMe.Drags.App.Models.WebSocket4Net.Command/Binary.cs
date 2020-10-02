namespace NoiseMe.Drags.App.Models.WebSocket4Net.Command
{
	public class Binary : WebSocketCommandBase
	{
		public override string Name => 2.ToString();

		public override void ExecuteCommand(WebSocket session, WebSocketCommandInfo commandInfo)
		{
			session.FireDataReceived(commandInfo.Data);
		}
	}
}
