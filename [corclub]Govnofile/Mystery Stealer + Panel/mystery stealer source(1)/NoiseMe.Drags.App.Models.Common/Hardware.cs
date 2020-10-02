using ProtoBuf;
using System.Collections.Generic;
using System.ComponentModel;

namespace NoiseMe.Drags.App.Models.Common
{
	[ProtoContract(Name = "Hardware")]
	public class Hardware : INotifyPropertyChanged, IEqualityComparer<Hardware>
	{
		private string _parameter;

		private string _caption;

		private HardwareType _hardType;

		[ProtoMember(1, Name = "Caption")]
		public string Caption
		{
			get
			{
				return _caption;
			}
			set
			{
				_caption = value;
				this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Caption"));
			}
		}

		[ProtoMember(2, Name = "Parameter")]
		public string Parameter
		{
			get
			{
				return _parameter;
			}
			set
			{
				_parameter = value;
				this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Parameter"));
			}
		}

		[ProtoMember(3, Name = "HardType")]
		public HardwareType HardType
		{
			get
			{
				return _hardType;
			}
			set
			{
				_hardType = value;
				this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("HardType"));
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;

		public override string ToString()
		{
			return "Name: " + Caption + "," + ((HardType == HardwareType.Processor) ? (" " + Parameter + " Cores") : (" " + Parameter + " bytes"));
		}

		public bool Equals(Hardware x, Hardware y)
		{
			if (x.Caption == y.Caption)
			{
				return x.Parameter == y.Parameter;
			}
			return false;
		}

		public int GetHashCode(Hardware obj)
		{
			int num = 37;
			num *= 397;
			if (!string.IsNullOrEmpty(obj.Caption))
			{
				num += obj.Caption.GetHashCode();
			}
			if (!string.IsNullOrEmpty(obj.Parameter))
			{
				num += obj.Parameter.GetHashCode();
			}
			return num;
		}
	}
}
