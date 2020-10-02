using ProtoBuf;

namespace NoiseMe.Drags.App.Models.Communication
{
	[ProtoContract(Name = "ResponseBase")]
	public class ResponseBase : CommunicationObject
	{
		private string _responseID;

		[ProtoMember(1, Name = "ID")]
		public string ID
		{
			get
			{
				if (string.IsNullOrEmpty(_responseID))
				{
					_responseID = string.Empty;
					OnPropertyChanged("ID");
				}
				return _responseID;
			}
			set
			{
				_responseID = value;
				OnPropertyChanged("ID");
			}
		}
	}
}
