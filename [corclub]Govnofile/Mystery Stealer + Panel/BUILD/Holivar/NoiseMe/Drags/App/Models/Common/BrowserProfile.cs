using System;
using System.Collections.Generic;
using System.ComponentModel;
using ProtoBuf;

namespace NoiseMe.Drags.App.Models.Common
{
	// Token: 0x02000162 RID: 354
	[ProtoContract(Name = "BrowserProfile")]
	public class BrowserProfile : INotifyPropertyChanged, IEqualityComparer<BrowserProfile>
	{
		// Token: 0x170002C8 RID: 712
		// (get) Token: 0x06000B50 RID: 2896 RVA: 0x00008B87 File Offset: 0x00006D87
		// (set) Token: 0x06000B51 RID: 2897 RVA: 0x00008B8F File Offset: 0x00006D8F
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

		// Token: 0x170002C9 RID: 713
		// (get) Token: 0x06000B52 RID: 2898 RVA: 0x00008BB3 File Offset: 0x00006DB3
		// (set) Token: 0x06000B53 RID: 2899 RVA: 0x00008BBB File Offset: 0x00006DBB
		[ProtoMember(2, Name = "Profile")]
		public string Profile
		{
			get
			{
				return this._profile;
			}
			set
			{
				this._profile = value;
				PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
				if (propertyChanged == null)
				{
					return;
				}
				propertyChanged(this, new PropertyChangedEventArgs("Profile"));
			}
		}

		// Token: 0x170002CA RID: 714
		// (get) Token: 0x06000B54 RID: 2900 RVA: 0x00022DE0 File Offset: 0x00020FE0
		// (set) Token: 0x06000B55 RID: 2901 RVA: 0x00008BDF File Offset: 0x00006DDF
		[ProtoMember(3, Name = "BrowserCredendtials")]
		public List<BrowserCredendtial> BrowserCredendtials
		{
			get
			{
				List<BrowserCredendtial> result;
				if ((result = this._browserCredendtials) == null)
				{
					result = (this._browserCredendtials = new List<BrowserCredendtial>());
				}
				return result;
			}
			set
			{
				this._browserCredendtials = value;
				PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
				if (propertyChanged == null)
				{
					return;
				}
				propertyChanged(this, new PropertyChangedEventArgs("BrowserCredendtials"));
			}
		}

		// Token: 0x170002CB RID: 715
		// (get) Token: 0x06000B56 RID: 2902 RVA: 0x00022E08 File Offset: 0x00021008
		// (set) Token: 0x06000B57 RID: 2903 RVA: 0x00008C03 File Offset: 0x00006E03
		[ProtoMember(4, Name = "BrowserCookies")]
		public List<BrowserCookie> BrowserCookies
		{
			get
			{
				List<BrowserCookie> result;
				if ((result = this._browserCookies) == null)
				{
					result = (this._browserCookies = new List<BrowserCookie>());
				}
				return result;
			}
			set
			{
				this._browserCookies = value;
				PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
				if (propertyChanged == null)
				{
					return;
				}
				propertyChanged(this, new PropertyChangedEventArgs("BrowserCookies"));
			}
		}

		// Token: 0x170002CC RID: 716
		// (get) Token: 0x06000B58 RID: 2904 RVA: 0x00022E30 File Offset: 0x00021030
		// (set) Token: 0x06000B59 RID: 2905 RVA: 0x00008C27 File Offset: 0x00006E27
		[ProtoMember(5, Name = "BrowserAutofill")]
		public List<BrowserAutofill> BrowserAutofills
		{
			get
			{
				List<BrowserAutofill> result;
				if ((result = this._browserAutofills) == null)
				{
					result = (this._browserAutofills = new List<BrowserAutofill>());
				}
				return result;
			}
			set
			{
				this._browserAutofills = value;
				PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
				if (propertyChanged == null)
				{
					return;
				}
				propertyChanged(this, new PropertyChangedEventArgs("BrowserAutofills"));
			}
		}

		// Token: 0x170002CD RID: 717
		// (get) Token: 0x06000B5A RID: 2906 RVA: 0x00022E58 File Offset: 0x00021058
		// (set) Token: 0x06000B5B RID: 2907 RVA: 0x00008C4B File Offset: 0x00006E4B
		[ProtoMember(6, Name = "BrowserCreditCard")]
		public List<BrowserCreditCard> BrowserCreditCards
		{
			get
			{
				List<BrowserCreditCard> result;
				if ((result = this._browserCreditCards) == null)
				{
					result = (this._browserCreditCards = new List<BrowserCreditCard>());
				}
				return result;
			}
			set
			{
				this._browserCreditCards = value;
				PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
				if (propertyChanged == null)
				{
					return;
				}
				propertyChanged(this, new PropertyChangedEventArgs("BrowserCreditCards"));
			}
		}

		// Token: 0x1400001E RID: 30
		// (add) Token: 0x06000B5C RID: 2908 RVA: 0x00022E80 File Offset: 0x00021080
		// (remove) Token: 0x06000B5D RID: 2909 RVA: 0x00022EB8 File Offset: 0x000210B8
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x06000B5E RID: 2910 RVA: 0x00008C6F File Offset: 0x00006E6F
		public bool Equals(BrowserProfile x, BrowserProfile y)
		{
			return x.Name == y.Name && x.Profile == y.Profile;
		}

		// Token: 0x06000B5F RID: 2911 RVA: 0x00022EF0 File Offset: 0x000210F0
		public int GetHashCode(BrowserProfile obj)
		{
			int num = 37;
			num *= 397;
			if (!string.IsNullOrEmpty(obj.Name))
			{
				num += obj.Name.GetHashCode();
			}
			if (!string.IsNullOrEmpty(obj.Profile))
			{
				num += obj.Profile.GetHashCode();
			}
			return num;
		}

		// Token: 0x04000457 RID: 1111
		private string _name;

		// Token: 0x04000458 RID: 1112
		private string _profile;

		// Token: 0x04000459 RID: 1113
		private List<BrowserCredendtial> _browserCredendtials;

		// Token: 0x0400045A RID: 1114
		private List<BrowserCookie> _browserCookies;

		// Token: 0x0400045B RID: 1115
		private List<BrowserAutofill> _browserAutofills;

		// Token: 0x0400045C RID: 1116
		private List<BrowserCreditCard> _browserCreditCards;
	}
}
