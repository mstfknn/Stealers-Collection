using ProtoBuf.Meta;
using System;
using System.Collections;

namespace ProtoBuf.Serializers
{
	internal sealed class ArrayDecorator : ProtoDecoratorBase
	{
		private readonly int fieldNumber;

		private const byte OPTIONS_WritePacked = 1;

		private const byte OPTIONS_OverwriteList = 2;

		private const byte OPTIONS_SupportNull = 4;

		private readonly byte options;

		private readonly WireType packedWireType;

		private readonly Type arrayType;

		private readonly Type itemType;

		public override Type ExpectedType => arrayType;

		public override bool RequiresOldValue => AppendToCollection;

		public override bool ReturnsValue => true;

		private bool AppendToCollection => (options & 2) == 0;

		private bool SupportNull => (options & 4) != 0;

		public ArrayDecorator(TypeModel model, IProtoSerializer tail, int fieldNumber, bool writePacked, WireType packedWireType, Type arrayType, bool overwriteList, bool supportNull)
			: base(tail)
		{
			itemType = arrayType.GetElementType();
			if (!supportNull)
			{
				if (Helpers.GetUnderlyingType(itemType) == null)
				{
					Type itemType2 = itemType;
				}
			}
			else
			{
				Type itemType3 = itemType;
			}
			if ((writePacked || packedWireType != WireType.None) && fieldNumber <= 0)
			{
				throw new ArgumentOutOfRangeException("fieldNumber");
			}
			if (!ListDecorator.CanPack(packedWireType))
			{
				if (writePacked)
				{
					throw new InvalidOperationException("Only simple data-types can use packed encoding");
				}
				packedWireType = WireType.None;
			}
			this.fieldNumber = fieldNumber;
			this.packedWireType = packedWireType;
			if (writePacked)
			{
				options |= 1;
			}
			if (overwriteList)
			{
				options |= 2;
			}
			if (supportNull)
			{
				options |= 4;
			}
			this.arrayType = arrayType;
		}

		public override void Write(object value, ProtoWriter dest)
		{
			IList list = (IList)value;
			int count = list.Count;
			bool flag = (options & 1) != 0;
			SubItemToken token;
			if (flag)
			{
				ProtoWriter.WriteFieldHeader(fieldNumber, WireType.String, dest);
				token = ProtoWriter.StartSubItem(value, dest);
				ProtoWriter.SetPackedField(fieldNumber, dest);
			}
			else
			{
				token = default(SubItemToken);
			}
			bool flag2 = !SupportNull;
			for (int i = 0; i < count; i++)
			{
				object obj = list[i];
				if (flag2 && obj == null)
				{
					throw new NullReferenceException();
				}
				Tail.Write(obj, dest);
			}
			if (flag)
			{
				ProtoWriter.EndSubItem(token, dest);
			}
		}

		public override object Read(object value, ProtoReader source)
		{
			int field = source.FieldNumber;
			BasicList basicList = new BasicList();
			if (packedWireType != WireType.None && source.WireType == WireType.String)
			{
				SubItemToken token = ProtoReader.StartSubItem(source);
				while (ProtoReader.HasSubValue(packedWireType, source))
				{
					basicList.Add(Tail.Read(null, source));
				}
				ProtoReader.EndSubItem(token, source);
			}
			else
			{
				do
				{
					basicList.Add(Tail.Read(null, source));
				}
				while (source.TryReadFieldHeader(field));
			}
			int num = AppendToCollection ? ((value != null) ? ((Array)value).Length : 0) : 0;
			Array array = Array.CreateInstance(itemType, num + basicList.Count);
			if (num != 0)
			{
				((Array)value).CopyTo(array, 0);
			}
			basicList.CopyTo(array, num);
			return array;
		}
	}
}
