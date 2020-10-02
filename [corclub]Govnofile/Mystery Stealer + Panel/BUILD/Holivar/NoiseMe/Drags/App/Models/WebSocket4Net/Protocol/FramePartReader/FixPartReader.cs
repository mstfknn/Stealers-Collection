using System;

namespace NoiseMe.Drags.App.Models.WebSocket4Net.Protocol.FramePartReader
{
	// Token: 0x020000B3 RID: 179
	internal class FixPartReader : DataFramePartReader
	{
		// Token: 0x06000638 RID: 1592 RVA: 0x00019F24 File Offset: 0x00018124
		public override int Process(int lastLength, WebSocketDataFrame frame, out IDataFramePartReader nextPartReader)
		{
			if (frame.Length < 2)
			{
				nextPartReader = this;
				return -1;
			}
			if (frame.PayloadLenght < 126)
			{
				if (frame.HasMask)
				{
					nextPartReader = DataFramePartReader.MaskKeyReader;
				}
				else
				{
					if (frame.ActualPayloadLength == 0L)
					{
						nextPartReader = null;
						return (int)((long)frame.Length - 2L);
					}
					nextPartReader = DataFramePartReader.PayloadDataReader;
				}
			}
			else
			{
				nextPartReader = DataFramePartReader.ExtendedLenghtReader;
			}
			if (frame.Length > 2)
			{
				return nextPartReader.Process(2, frame, out nextPartReader);
			}
			return 0;
		}
	}
}
