namespace NoiseMe.Drags.App.Models.WebSocket4Net.Common
{
	public class ArraySegmentEx<T>
	{
		public T[] Array
		{
			get;
			private set;
		}

		public int Count
		{
			get;
			private set;
		}

		public int Offset
		{
			get;
			private set;
		}

		public int From
		{
			get;
			set;
		}

		public int To
		{
			get;
			set;
		}

		public ArraySegmentEx(T[] array, int offset, int count)
		{
			Array = array;
			Offset = offset;
			Count = count;
		}
	}
}
