using ProtoBuf.Meta;
using System;

namespace ProtoBuf.Serializers
{
	internal sealed class NetObjectSerializer : IProtoSerializer
	{
		private readonly int key;

		private readonly Type type;

		private readonly BclHelpers.NetObjectOptions options;

		public Type ExpectedType => type;

		public bool ReturnsValue => true;

		public bool RequiresOldValue => true;

		public NetObjectSerializer(TypeModel model, Type type, int key, BclHelpers.NetObjectOptions options)
		{
			bool flag = (options & BclHelpers.NetObjectOptions.DynamicType) != BclHelpers.NetObjectOptions.None;
			this.key = (flag ? (-1) : key);
			this.type = (flag ? model.MapType(typeof(object)) : type);
			this.options = options;
		}

		public object Read(object value, ProtoReader source)
		{
			return BclHelpers.ReadNetObject(value, source, key, (type == typeof(object)) ? null : type, options);
		}

		public void Write(object value, ProtoWriter dest)
		{
			BclHelpers.WriteNetObject(value, dest, key, options);
		}
	}
}
