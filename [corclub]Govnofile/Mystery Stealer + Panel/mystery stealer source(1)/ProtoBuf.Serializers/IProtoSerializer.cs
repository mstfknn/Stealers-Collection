using System;

namespace ProtoBuf.Serializers
{
	internal interface IProtoSerializer
	{
		Type ExpectedType
		{
			get;
		}

		bool RequiresOldValue
		{
			get;
		}

		bool ReturnsValue
		{
			get;
		}

		void Write(object value, ProtoWriter dest);

		object Read(object value, ProtoReader source);
	}
}
