using ProtoBuf;
using System;

namespace NoiseMe.Drags.App.Models.Communication
{
	[ProtoContract(Name = "RequestBase")]
	public class RequestBase : CommunicationObject
	{
		private string _name;

		private string _iD;

		private readonly string Letters;

		private readonly Random Randomizer;

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
				OnPropertyChanged("Name");
			}
		}

		[ProtoMember(2, Name = "ID")]
		public string ID
		{
			get
			{
				if (string.IsNullOrEmpty(_iD))
				{
					_iD = GenerateUniqueId(10);
					OnPropertyChanged("ID");
				}
				return _iD;
			}
			set
			{
				_iD = value;
				OnPropertyChanged("ID");
			}
		}

		public RequestBase()
		{
			Randomizer = new Random();
			Letters = "qwertyuiopasdfghjklzxcvbnm1234567890QWERTYUIOPASDFGHJKLZXCVBNM";
		}

		public Response<T> CreateResponse<T>(T _responseBody)
		{
			return new Response<T>(_responseBody)
			{
				ID = ID
			};
		}

		private string GenerateUniqueId(int length)
		{
			string text = string.Empty;
			for (int i = 0; i < length; i++)
			{
				text += Letters[Randomizer.Next(0, Letters.Length - 1)].ToString();
			}
			return text;
		}
	}
}
