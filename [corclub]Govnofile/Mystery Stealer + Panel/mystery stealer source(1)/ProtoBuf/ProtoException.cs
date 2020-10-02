using System;

namespace ProtoBuf
{
	public class ProtoException : Exception
	{
		public ProtoException()
		{
		}

		public ProtoException(string message)
			: base(message)
		{
		}

		public ProtoException(string message, Exception innerException)
			: base(message, innerException)
		{
		}
	}
}
