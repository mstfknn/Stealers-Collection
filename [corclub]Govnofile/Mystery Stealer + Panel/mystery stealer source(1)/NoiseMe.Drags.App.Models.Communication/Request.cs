using ProtoBuf;
using System;

namespace NoiseMe.Drags.App.Models.Communication
{
	[ProtoContract(Name = "Request")]
	public class Request<T> : RequestBase
	{
		private T _body;

		[ProtoMember(2, Name = "Body")]
		public T Body
		{
			get
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
					OnPropertyChanged("Body");
				}
				return _body;
			}
			set
			{
				_body = value;
				OnPropertyChanged("Body");
			}
		}

		public Request()
			: this(default(T), string.Empty)
		{
		}

		public Request(T _body, string name)
		{
			if (_body == null)
			{
				_body = ((!typeof(T).IsClass || typeof(T).GetConstructor(Type.EmptyTypes) == null) ? default(T) : Activator.CreateInstance<T>());
			}
			Body = _body;
			base.Name = (string.IsNullOrEmpty(name) ? string.Empty : name);
		}
	}
}
