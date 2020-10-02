using System;

namespace NoiseMe.Drags.App.Models.WebSocket4Net.Protocol.FramePartReader
{
	// Token: 0x020000B2 RID: 178
	internal class ExtendedLenghtReader : DataFramePartReader
	{
		// Token: 0x06000636 RID: 1590 RVA: 0x00019EAC File Offset: 0x000180AC
		public override int Process(int lastLength, WebSocketDataFrame frame, out IDataFramePartReader nextPartReader)
		{
			int num = 2;
			if (frame.PayloadLenght == 126)
			{
				num += 2;
			}
			else
			{
				num += 8;
			}
			if (frame.Length < num)
			{
				nextPartReader = this;
				return -1;
			}
			if (frame.HasMask)
			{
				nextPartReader = DataFramePartReader.MaskKeyReader;
			}
			else
			{
				if (frame.ActualPayloadLength == 0L)
				{
					nextPartReader = null;
					return (int)((long)frame.Length - (long)num);
				}
				nextPartReader = DataFramePartReader.PayloadDataReader;
			}
			if (frame.Length > num)
			{
				return nextPartReader.Process(num, frame, out nextPartReader);
			}
			return 0;
		}
	}
}
