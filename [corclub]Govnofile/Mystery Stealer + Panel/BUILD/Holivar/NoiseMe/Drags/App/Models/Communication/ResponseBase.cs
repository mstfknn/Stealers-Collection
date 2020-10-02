using System;
using ProtoBuf;

namespace NoiseMe.Drags.App.Models.Communication
{
	// Token: 0x0200015C RID: 348
	[ProtoContract(Name = "ResponseBase")]
	public class ResponseBase : CommunicationObject
	{
		// Token: 0x170002B3 RID: 691
		// (get) Token: 0x06000B0D RID: 2829 RVA: 0x000087B1 File Offset: 0x000069B1
		// (set) Token: 0x06000B0E RID: 2830 RVA: 0x000087DC File Offset: 0x000069DC
		[ProtoMember(1, Name = "ID")]
		public string ID
		{
			get
			{
				if (string.IsNullOrEmpty(this._responseID))
				{
					this._responseID = string.Empty;
					base.OnPropertyChanged("ID");
				}
				return this._responseID;
			}
			set
			{
				this._responseID = value;
				base.OnPropertyChanged("ID");
			}
		}

		// Token: 0x0400043D RID: 1085
		private string _responseID;
	}
}
