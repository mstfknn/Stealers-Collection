namespace NoiseMe.Drags.App.Models.SuperSocket.ProtoBase
{
	public enum CloseReason
	{
		Unknown,
		ServerShutdown,
		ClientClosing,
		ServerClosing,
		ApplicationError,
		SocketError,
		TimeOut,
		ProtocolError,
		publicError
	}
}
