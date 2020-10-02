using System;

namespace NoiseMe.Drags.App.Models.WebSocket4Net.Protocol.FramePartReader
{
	// Token: 0x020000B4 RID: 180
	internal interface IDataFramePartReader
	{
		// Token: 0x0600063A RID: 1594
		int Process(int lastLength, WebSocketDataFrame frame, out IDataFramePartReader nextPartReader);
	}
}
