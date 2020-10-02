using ProtoBuf.Meta;
using System;

namespace ProtoBuf.Serializers
{
	internal sealed class BlobSerializer : IProtoSerializer
	{
		private static readonly Type expectedType = typeof(byte[]);

		private readonly bool overwriteList;

		public Type ExpectedType => expectedType;

		bool IProtoSerializer.RequiresOldValue => !overwriteList;

		bool IProtoSerializer.ReturnsValue => true;

		public BlobSerializer(TypeModel model, bool overwriteList)
		{
			this.overwriteList = overwriteList;
		}

		public object Read(object value, ProtoReader source)
		{
			return ProtoReader.AppendBytes(overwriteList ? null : ((byte[])value), source);
		}

		public void Write(object value, ProtoWriter dest)
		{
			ProtoWriter.WriteBytes((byte[])value, dest);
		}
	}
}
