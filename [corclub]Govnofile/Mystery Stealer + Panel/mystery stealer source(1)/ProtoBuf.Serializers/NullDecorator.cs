using ProtoBuf.Meta;
using System;

namespace ProtoBuf.Serializers
{
	internal sealed class NullDecorator : ProtoDecoratorBase
	{
		private readonly Type expectedType;

		public const int Tag = 1;

		public override Type ExpectedType => expectedType;

		public override bool ReturnsValue => true;

		public override bool RequiresOldValue => true;

		public NullDecorator(TypeModel model, IProtoSerializer tail)
			: base(tail)
		{
			if (!tail.ReturnsValue)
			{
				throw new NotSupportedException("NullDecorator only supports implementations that return values");
			}
			Type type = tail.ExpectedType;
			if (Helpers.IsValueType(type))
			{
				expectedType = model.MapType(typeof(Nullable<>)).MakeGenericType(type);
			}
			else
			{
				expectedType = type;
			}
		}

		public override object Read(object value, ProtoReader source)
		{
			SubItemToken token = ProtoReader.StartSubItem(source);
			int num;
			while ((num = source.ReadFieldHeader()) > 0)
			{
				if (num == 1)
				{
					value = Tail.Read(value, source);
				}
				else
				{
					source.SkipField();
				}
			}
			ProtoReader.EndSubItem(token, source);
			return value;
		}

		public override void Write(object value, ProtoWriter dest)
		{
			SubItemToken token = ProtoWriter.StartSubItem(null, dest);
			if (value != null)
			{
				Tail.Write(value, dest);
			}
			ProtoWriter.EndSubItem(token, dest);
		}
	}
}
