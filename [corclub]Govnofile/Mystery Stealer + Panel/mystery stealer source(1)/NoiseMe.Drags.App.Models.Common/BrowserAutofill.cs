using ProtoBuf;
using System.Collections.Generic;
using System.ComponentModel;

namespace NoiseMe.Drags.App.Models.Common
{
	[ProtoContract(Name = "BrowserAutofill")]
	public class BrowserAutofill : INotifyPropertyChanged, IEqualityComparer<BrowserAutofill>
	{
		private string _name;

		private string _value;

		[ProtoMember(1, Name = "Name")]
		public string Name
		{
			get
			{
				return _name;
			}
			set
			{
				_name = value;
				this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Name"));
			}
		}

		[ProtoMember(2, Name = "Value")]
		public string Value
		{
			get
			{
				return _value;
			}
			set
			{
				_value = value;
				this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Value"));
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;

		public bool Equals(BrowserAutofill x, BrowserAutofill y)
		{
			if (x.Name == y.Name)
			{
				return x.Value == y.Value;
			}
			return false;
		}

		public int GetHashCode(BrowserAutofill obj)
		{
			int num = 37;
			num *= 397;
			if (!string.IsNullOrEmpty(obj.Name))
			{
				num += obj.Name.GetHashCode();
			}
			if (!string.IsNullOrEmpty(obj.Value))
			{
				num += obj.Value.GetHashCode();
			}
			return num;
		}
	}
}
