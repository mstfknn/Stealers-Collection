namespace NoiseMe.Drags.App.Models.WebSocket4Net.Protocol.FramePartReader
{
	internal class PayloadDataReader : DataFramePartReader
	{
		public override int Process(int lastLength, WebSocketDataFrame frame, out IDataFramePartReader nextPartReader)
		{
			long num = lastLength + frame.ActualPayloadLength;
			if (frame.Length < num)
			{
				nextPartReader = this;
				return -1;
			}
			nextPartReader = null;
			return (int)(frame.Length - num);
		}
	}
}
