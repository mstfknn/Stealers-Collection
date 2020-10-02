namespace NoiseMe.Drags.App.Models.WebSocket4Net.Protocol.FramePartReader
{
	internal abstract class DataFramePartReader : IDataFramePartReader
	{
		public static IDataFramePartReader NewReader => FixPartReader;

		protected static IDataFramePartReader FixPartReader
		{
			get;
			private set;
		}

		protected static IDataFramePartReader ExtendedLenghtReader
		{
			get;
			private set;
		}

		protected static IDataFramePartReader MaskKeyReader
		{
			get;
			private set;
		}

		protected static IDataFramePartReader PayloadDataReader
		{
			get;
			private set;
		}

		static DataFramePartReader()
		{
			FixPartReader = new FixPartReader();
			ExtendedLenghtReader = new ExtendedLenghtReader();
			MaskKeyReader = new MaskKeyReader();
			PayloadDataReader = new PayloadDataReader();
		}

		public abstract int Process(int lastLength, WebSocketDataFrame frame, out IDataFramePartReader nextPartReader);
	}
}
