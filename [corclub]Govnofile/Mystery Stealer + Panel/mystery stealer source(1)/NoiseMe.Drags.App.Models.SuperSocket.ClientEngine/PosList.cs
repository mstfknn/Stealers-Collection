using System.Collections;
using System.Collections.Generic;

namespace NoiseMe.Drags.App.Models.SuperSocket.ClientEngine
{
	public class PosList<T> : List<T>, IPosList<T>, IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable
	{
		public int Position
		{
			get;
			set;
		}
	}
}
