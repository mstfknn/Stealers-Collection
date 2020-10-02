using System;
using System.Collections.Generic;
using System.ComponentModel;
using ProtoBuf;

namespace NoiseMe.Drags.App.Models.Common
{
	// Token: 0x02000169 RID: 361
	[ProtoContract(Name = "Hardware")]
	public class Hardware : INotifyPropertyChanged, IEqualityComparer<Hardware>
	{
		// Token: 0x170002DF RID: 735
		// (get) Token: 0x06000B8F RID: 2959 RVA: 0x00008E90 File Offset: 0x00007090
		// (set) Token: 0x06000B90 RID: 2960 RVA: 0x00008E98 File Offset: 0x00007098
		[ProtoMember(1, Name = "Caption")]
		public string Caption
		{
			get
			{
				return this._caption;
			}
			set
			{
				this._caption = value;
				PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
				if (propertyChanged == null)
				{
					return;
				}
				propertyChanged(this, new PropertyChangedEventArgs("Caption"));
			}
		}

		// Token: 0x170002E0 RID: 736
		// (get) Token: 0x06000B91 RID: 2961 RVA: 0x00008EBC File Offset: 0x000070BC
		// (set) Token: 0x06000B92 RID: 2962 RVA: 0x00008EC4 File Offset: 0x000070C4
		[ProtoMember(2, Name = "Parameter")]
		public string Parameter
		{
			get
			{
				return this._parameter;
			}
			set
			{
				this._parameter = value;
				PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
				if (propertyChanged == null)
				{
					return;
				}
				propertyChanged(this, new PropertyChangedEventArgs("Parameter"));
			}
		}

		// Token: 0x170002E1 RID: 737
		// (get) Token: 0x06000B93 RID: 2963 RVA: 0x00008EE8 File Offset: 0x000070E8
		// (set) Token: 0x06000B94 RID: 2964 RVA: 0x00008EF0 File Offset: 0x000070F0
		[ProtoMember(3, Name = "HardType")]
		public HardwareType HardType
		{
			get
			{
				return this._hardType;
			}
			set
			{
				this._hardType = value;
				PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
				if (propertyChanged == null)
				{
					return;
				}
				propertyChanged(this, new PropertyChangedEventArgs("HardType"));
			}
		}

		// Token: 0x14000022 RID: 34
		// (add) Token: 0x06000B95 RID: 2965 RVA: 0x00023090 File Offset: 0x00021290
		// (remove) Token: 0x06000B96 RID: 2966 RVA: 0x000230C8 File Offset: 0x000212C8
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x06000B97 RID: 2967 RVA: 0x00023100 File Offset: 0x00021300
		public override string ToString()
		{
			return "Name: " + this.Caption + "," + ((this.HardType == HardwareType.Processor) ? (" " + this.Parameter + " Cores") : (" " + this.Parameter + " bytes"));
		}

		// Token: 0x06000B98 RID: 2968 RVA: 0x00008F14 File Offset: 0x00007114
		public bool Equals(Hardware x, Hardware y)
		{
			return x.Caption == y.Caption && x.Parameter == y.Parameter;
		}

		// Token: 0x06000B99 RID: 2969 RVA: 0x00023158 File Offset: 0x00021358
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

		// Token: 0x04000472 RID: 1138
		private string _parameter;

		// Token: 0x04000473 RID: 1139
		private string _caption;

		// Token: 0x04000474 RID: 1140
		private HardwareType _hardType;
	}
}
