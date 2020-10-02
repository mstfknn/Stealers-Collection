using System;

namespace ProtoBuf.Serializers
{
	internal abstract class ProtoDecoratorBase : IProtoSerializer
	{
		protected readonly IProtoSerializer Tail;

		public abstract Type ExpectedType
		{
			get;
		}

		public abstract bool ReturnsValue
		{
			get;
		}

		public abstract bool RequiresOldValue
		{
			get;
		}

		protected ProtoDecoratorBase(IProtoSerializer tail)
		{
			Tail = tail;
		}

		public abstract void Write(object value, ProtoWriter dest);

		public abstract object Read(object value, ProtoReader source);
	}
}
