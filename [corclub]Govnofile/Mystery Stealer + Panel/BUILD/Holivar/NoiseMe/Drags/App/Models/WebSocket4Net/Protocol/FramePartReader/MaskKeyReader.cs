using System;

namespace NoiseMe.Drags.App.Models.WebSocket4Net.Protocol.FramePartReader
{
	// Token: 0x020000B5 RID: 181
	internal class MaskKeyReader : DataFramePartReader
	{
		// Token: 0x0600063B RID: 1595 RVA: 0x00019F98 File Offset: 0x00018198
		public override int Process(int lastLength, WebSocketDataFrame frame, out IDataFramePartReader nextPartReader)
		{
			int num = lastLength + 4;
			if (frame.Length < num)
			{
				nextPartReader = this;
				return -1;
			}
			frame.MaskKey = frame.InnerData.ToArrayData(lastLength, 4);
			if (frame.ActualPayloadLength == 0L)
			{
				nextPartReader = null;
				return (int)((long)frame.Length - (long)num);
			}
			nextPartReader = new PayloadDataReader();
			if (frame.Length > num)
			{
				return nextPartReader.Process(num, frame, out nextPartReader);
			}
			return 0;
		}
	}
}
