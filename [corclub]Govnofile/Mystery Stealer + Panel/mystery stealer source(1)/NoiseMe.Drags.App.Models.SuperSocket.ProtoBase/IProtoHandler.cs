namespace NoiseMe.Drags.App.Models.SuperSocket.ProtoBase
{
	public interface IProtoHandler
	{
		IProtoDataEncoder DataEncoder
		{
			get;
		}

		bool CanSend();

		void Close(ICommunicationChannel channel, CloseReason reason);
	}
}
