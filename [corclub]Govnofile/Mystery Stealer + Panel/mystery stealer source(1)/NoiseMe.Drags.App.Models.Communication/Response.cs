using ProtoBuf;
using System;

namespace NoiseMe.Drags.App.Models.Communication
{
	[ProtoContract(Name = "Response")]
	public class Response<T> : ResponseBase
	{
		private T _responseBody;

		[ProtoMember(1, Name = "Body")]
		public T Body
		{
			get
			{
				if (_responseBody == null)
				{
					if (typeof(T).IsClass && typeof(T).GetConstructor(Type.EmptyTypes) != null)
					{
						_responseBody = Activator.CreateInstance<T>();
					}
					else
					{
						_responseBody = default(T);
					}
					OnPropertyChanged("Body");
				}
				return _responseBody;
			}
			set
			{
				_responseBody = value;
				OnPropertyChanged("Body");
			}
		}

		public Response()
			: this(default(T))
		{
		}

		public Response(T _body)
		{
			if (_body == null)
			{
				_body = ((!typeof(T).IsClass || typeof(T).GetConstructor(Type.EmptyTypes) == null) ? default(T) : Activator.CreateInstance<T>());
			}
			Body = _body;
		}
	}
}
