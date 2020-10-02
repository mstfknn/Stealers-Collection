namespace NoiseMe.Drags.App.Models.WebSocket4Net.Protocol.FramePartReader
{
	internal class FixPartReader : DataFramePartReader
	{
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
