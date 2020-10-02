using System;

namespace NoiseMe.Drags.App.Models.WebSocket4Net.Protocol
{
	// Token: 0x020000A5 RID: 165
	internal class DraftHybi10HandshakeReader : HandshakeReader
	{
		// Token: 0x060005BC RID: 1468 RVA: 0x00005905 File Offset: 0x00003B05
		public DraftHybi10HandshakeReader(WebSocket websocket) : base(websocket)
		{
		}

		// Token: 0x060005BD RID: 1469 RVA: 0x00019544 File Offset: 0x00017744
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
