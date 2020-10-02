namespace NoiseMe.Drags.App.Models.WebSocket4Net.Protocol.FramePartReader
{
	internal class MaskKeyReader : DataFramePartReader
	{
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
