namespace NoiseMe.Drags.App.Models.WebSocket4Net.Protocol
{
	internal class DraftHybi10HandshakeReader : HandshakeReader
	{
		public DraftHybi10HandshakeReader(WebSocket websocket)
			: base(websocket)
		{
		}

		public override WebSocketCommandInfo GetCommandInfo(byte[] readBuffer, int offset, int length, out int left)
		{
			WebSocketCommandInfo commandInfo = base.GetCommandInfo(readBuffer, offset, length, out left);
			if (commandInfo == null)
			{
				return null;
			}
			if (!HandshakeReader.BadRequestCode.Equals(commandInfo.Key))
			{
				base.NextCommandReader = new DraftHybi10DataReader();
			}
			return commandInfo;
		}
	}
}
