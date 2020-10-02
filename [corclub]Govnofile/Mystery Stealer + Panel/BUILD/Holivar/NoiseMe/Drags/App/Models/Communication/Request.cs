using System;
using ProtoBuf;

namespace NoiseMe.Drags.App.Models.Communication
{
	// Token: 0x02000159 RID: 345
	[ProtoContract(Name = "Request")]
	public class Request<T> : RequestBase
	{
		// Token: 0x06000AFE RID: 2814 RVA: 0x00022710 File Offset: 0x00020910
		public Request() : this(default(T), string.Empty)
		{
		}

		// Token: 0x06000AFF RID: 2815 RVA: 0x00022734 File Offset: 0x00020934
		public Request(T _body, string name)
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
			base.Name = (string.IsNullOrEmpty(name) ? string.Empty : name);
		}

		// Token: 0x170002AF RID: 687
		// (get) Token: 0x06000B00 RID: 2816 RVA: 0x000227A4 File Offset: 0x000209A4
		// (set) Token: 0x06000B01 RID: 2817 RVA: 0x000086F9 File Offset: 0x000068F9
		[ProtoMember(2, Name = "Body")]
		public T Body
		{
			get
			{
				if (this._body == null)
				{
					if (typeof(T).IsClass && typeof(T).GetConstructor(Type.EmptyTypes) != null)
					{
						this._body = Activator.CreateInstance<T>();
					}
					else
					{
						this._body = default(T);
					}
					base.OnPropertyChanged("Body");
				}
				return this._body;
			}
			set
			{
				this._body = value;
				base.OnPropertyChanged("Body");
			}
		}

		// Token: 0x04000437 RID: 1079
		private T _body;
	}
}
