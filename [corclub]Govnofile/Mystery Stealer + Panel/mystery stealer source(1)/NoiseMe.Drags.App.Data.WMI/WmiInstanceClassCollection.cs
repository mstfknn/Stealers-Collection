using System.Collections;
using System.Collections.Generic;

namespace NoiseMe.Drags.App.Data.WMI
{
	public class WmiInstanceClassCollection : ICollection<WmiInstanceClass>, IEnumerable<WmiInstanceClass>, IEnumerable
	{
		private readonly List<WmiInstanceClass> _classes;

		public int Count => _classes.Count;

		public bool IsReadOnly => false;

		public WmiInstanceClass this[int classIndex] => _classes[classIndex];

		public WmiInstanceClassCollection()
		{
			_classes = new List<WmiInstanceClass>();
		}

		public WmiInstanceClassCollection(ICollection<WmiInstanceClass> collection)
		{
			_classes = new List<WmiInstanceClass>(collection);
		}

		public void Add(WmiInstanceClass item)
		{
			_classes.Add(item);
		}

		public void Clear()
		{
			_classes.Clear();
		}

		public bool Contains(WmiInstanceClass item)
		{
			return _classes.Contains(item);
		}

		public void CopyTo(WmiInstanceClass[] array, int arrayIndex)
		{
			_classes.CopyTo(array, arrayIndex);
		}

		public IEnumerator<WmiInstanceClass> GetEnumerator()
		{
			return _classes.GetEnumerator();
		}

		public bool Remove(WmiInstanceClass item)
		{
			return _classes.Remove(item);
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return _classes.GetEnumerator();
		}
	}
}
