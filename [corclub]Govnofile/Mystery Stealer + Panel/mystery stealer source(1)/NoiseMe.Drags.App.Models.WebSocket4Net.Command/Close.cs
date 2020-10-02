namespace NoiseMe.Drags.App.Models.WebSocket4Net.Command
{
	public class Close : WebSocketCommandBase
	{
		public override string Name => 8.ToString();

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
			session.Close(num, commandInfo.Text);
		}
	}
}
