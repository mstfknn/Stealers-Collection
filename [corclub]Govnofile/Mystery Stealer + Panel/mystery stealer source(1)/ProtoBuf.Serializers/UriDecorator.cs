using ProtoBuf.Meta;
using System;

namespace ProtoBuf.Serializers
{
	internal sealed class UriDecorator : ProtoDecoratorBase
	{
		private static readonly Type expectedType = typeof(Uri);

		public override Type ExpectedType => expectedType;

		public override bool RequiresOldValue => false;

		public override bool ReturnsValue => true;

		public UriDecorator(TypeModel model, IProtoSerializer tail)
			: base(tail)
		{
		}

		public override void Write(object value, ProtoWriter dest)
		{
			Tail.Write(((Uri)value).AbsoluteUri, dest);
		}

		public override object Read(object value, ProtoReader source)
		{
			string text = (string)Tail.Read(null, source);
			if (text.Length != 0)
			{
				return new Uri(text);
			}
			return null;
		}
	}
}
