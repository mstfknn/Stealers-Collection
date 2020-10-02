using System;
using ProtoBuf;

namespace NoiseMe.Drags.App.Models.Communication
{
	// Token: 0x0200015B RID: 347
	[ProtoContract(Name = "Response")]
	public class Response<T> : ResponseBase
	{
		// Token: 0x06000B09 RID: 2825 RVA: 0x00022864 File Offset: 0x00020A64
		public Response() : this(default(T))
		{
		}

		// Token: 0x06000B0A RID: 2826 RVA: 0x00022880 File Offset: 0x00020A80
		public Response(T _body)
		{
			if (_body == null)
			{
				if (typeof(T).IsClass && typeof(T).GetConstructor(Type.EmptyTypes) != null)
				{
					_body = Activator.CreateInstance<T>();
				}
				else
				{
					_body = default(T);
				}
			}
			this.Body = _body;
		}

		// Token: 0x170002B2 RID: 690
		// (get) Token: 0x06000B0B RID: 2827 RVA: 0x000228DC File Offset: 0x00020ADC
		// (set) Token: 0x06000B0C RID: 2828 RVA: 0x0000879D File Offset: 0x0000699D
		[ProtoMember(1, Name = "Body")]
		public T Body
		{
			get
			{
				if (this._responseBody == null)
				{
					if (typeof(T).IsClass && typeof(T).GetConstructor(Type.EmptyTypes) != null)
					{
						this._responseBody = Activator.CreateInstance<T>();
					}
					else
					{
						this._responseBody = default(T);
					}
					base.OnPropertyChanged("Body");
				}
				return this._responseBody;
			}
			set
			{
				this._responseBody = value;
				base.OnPropertyChanged("Body");
			}
		}

		// Token: 0x0400043C RID: 1084
		private T _responseBody;
	}
}
