namespace NoiseMe.Drags.App.Models.WebSocket4Net.Protocol.FramePartReader
{
	internal class ExtendedLenghtReader : DataFramePartReader
	{
		public override int Process(int lastLength, WebSocketDataFrame frame, out IDataFramePartReader nextPartReader)
		{
			int num = 2;
			num = ((frame.PayloadLenght != 126) ? (num + 8) : (num + 2));
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
