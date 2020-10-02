using System;

namespace NoiseMe.Drags.App.Models.WebSocket4Net.Protocol.FramePartReader
{
	// Token: 0x020000B6 RID: 182
	internal class PayloadDataReader : DataFramePartReader
	{
		// Token: 0x0600063D RID: 1597 RVA: 0x0001A000 File Offset: 0x00018200
		public override int Process(int lastLength, WebSocketDataFrame frame, out IDataFramePartReader nextPartReader)
		{
			long num = (long)lastLength + frame.ActualPayloadLength;
			if ((long)frame.Length < num)
			{
				nextPartReader = this;
				return -1;
			}
			nextPartReader = null;
			return (int)((long)frame.Length - num);
		}
	}
}
