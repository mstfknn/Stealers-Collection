using System;
using ProtoBuf;

namespace NoiseMe.Drags.App.Models.Communication
{
	// Token: 0x0200015A RID: 346
	[ProtoContract(Name = "RequestBase")]
	public class RequestBase : CommunicationObject
	{
		// Token: 0x06000B02 RID: 2818 RVA: 0x0000870D File Offset: 0x0000690D
		public RequestBase()
		{
			this.Randomizer = new Random();
			this.Letters = "qwertyuiopasdfghjklzxcvbnm1234567890QWERTYUIOPASDFGHJKLZXCVBNM";
		}

		// Token: 0x170002B0 RID: 688
		// (get) Token: 0x06000B03 RID: 2819 RVA: 0x0000872B File Offset: 0x0000692B
		// (set) Token: 0x06000B04 RID: 2820 RVA: 0x00008733 File Offset: 0x00006933
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
				base.OnPropertyChanged("Name");
			}
		}

		// Token: 0x170002B1 RID: 689
		// (get) Token: 0x06000B05 RID: 2821 RVA: 0x00008747 File Offset: 0x00006947
		// (set) Token: 0x06000B06 RID: 2822 RVA: 0x00008775 File Offset: 0x00006975
		[ProtoMember(2, Name = "ID")]
		public string ID
		{
			get
			{
				if (string.IsNullOrEmpty(this._iD))
				{
					this._iD = this.GenerateUniqueId(10);
					base.OnPropertyChanged("ID");
				}
				return this._iD;
			}
			set
			{
				this._iD = value;
				base.OnPropertyChanged("ID");
			}
		}

		// Token: 0x06000B07 RID: 2823 RVA: 0x00008789 File Offset: 0x00006989
		public Response<T> CreateResponse<T>(T _responseBody)
		{
			return new Response<T>(_responseBody)
			{
				ID = this.ID
			};
		}

		// Token: 0x06000B08 RID: 2824 RVA: 0x00022810 File Offset: 0x00020A10
		private string GenerateUniqueId(int length)
		{
			string text = string.Empty;
			for (int i = 0; i < length; i++)
			{
				text += this.Letters[this.Randomizer.Next(0, this.Letters.Length - 1)].ToString();
			}
			return text;
		}

		// Token: 0x04000438 RID: 1080
		private string _name;

		// Token: 0x04000439 RID: 1081
		private string _iD;

		// Token: 0x0400043A RID: 1082
		private readonly string Letters;

		// Token: 0x0400043B RID: 1083
		private readonly Random Randomizer;
	}
}
