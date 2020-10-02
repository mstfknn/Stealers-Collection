namespace NoiseMe.Drags.App.Models.SuperSocket.ProtoBase
{
	public abstract class ProtoHandlerBase : IProtoHandler
	{
		public IProtoDataEncoder DataEncoder
		{
			get;
			set;
		}

		public abstract bool CanSend();

		public abstract void Close(ICommunicationChannel channel, CloseReason reason);
	}
}
