using System;

namespace NoiseMe.Drags.App.Models.SuperSocket.ProtoBase
{
	public class SearchMarkState<T> where T : IEquatable<T>
	{
		public T[] Mark
		{
			get;
			private set;
		}

		public int Matched
		{
			get;
			set;
		}

		public SearchMarkState(T[] mark)
		{
			Mark = mark;
		}

		public void Change(T[] mark)
		{
			Mark = mark;
			Matched = 0;
		}
	}
}
