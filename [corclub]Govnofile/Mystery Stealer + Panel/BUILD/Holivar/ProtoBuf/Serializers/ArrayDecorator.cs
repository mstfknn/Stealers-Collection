using System;
using System.Collections;
using ProtoBuf.Meta;

namespace ProtoBuf.Serializers
{
	// Token: 0x02000035 RID: 53
	internal sealed class ArrayDecorator : ProtoDecoratorBase
	{
		// Token: 0x06000191 RID: 401 RVA: 0x0000DF24 File Offset: 0x0000C124
		public ArrayDecorator(TypeModel model, IProtoSerializer tail, int fieldNumber, bool writePacked, WireType packedWireType, Type arrayType, bool overwriteList, bool supportNull) : base(tail)
		{
			this.itemType = arrayType.GetElementType();
			if (!supportNull)
			{
				if (Helpers.GetUnderlyingType(this.itemType) == null)
				{
					Type type = this.itemType;
				}
			}
			else
			{
				Type type2 = this.itemType;
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
				this.options |= 1;
			}
			if (overwriteList)
			{
				this.options |= 2;
			}
			if (supportNull)
			{
				this.options |= 4;
			}
			this.arrayType = arrayType;
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000192 RID: 402 RVA: 0x00003137 File Offset: 0x00001337
		public override Type ExpectedType
		{
			get
			{
				return this.arrayType;
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000193 RID: 403 RVA: 0x0000313F File Offset: 0x0000133F
		public override bool RequiresOldValue
		{
			get
			{
				return this.AppendToCollection;
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000194 RID: 404 RVA: 0x00003147 File Offset: 0x00001347
		public override bool ReturnsValue
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000195 RID: 405 RVA: 0x0000314A File Offset: 0x0000134A
		private bool AppendToCollection
		{
			get
			{
				return (this.options & 2) == 0;
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000196 RID: 406 RVA: 0x00003157 File Offset: 0x00001357
		private bool SupportNull
		{
			get
			{
				return (this.options & 4) > 0;
			}
		}

		// Token: 0x06000197 RID: 407 RVA: 0x0000DFEC File Offset: 0x0000C1EC
		public override void Write(object value, ProtoWriter dest)
		{
			IList list = (IList)value;
			int count = list.Count;
			bool flag = (this.options & 1) > 0;
			SubItemToken token;
			if (flag)
			{
				ProtoWriter.WriteFieldHeader(this.fieldNumber, WireType.String, dest);
				token = ProtoWriter.StartSubItem(value, dest);
				ProtoWriter.SetPackedField(this.fieldNumber, dest);
			}
			else
			{
				token = default(SubItemToken);
			}
			bool flag2 = !this.SupportNull;
			for (int i = 0; i < count; i++)
			{
				object obj = list[i];
				if (flag2 && obj == null)
				{
					throw new NullReferenceException();
				}
				this.Tail.Write(obj, dest);
			}
			if (flag)
			{
				ProtoWriter.EndSubItem(token, dest);
			}
		}

		// Token: 0x06000198 RID: 408 RVA: 0x0000E08C File Offset: 0x0000C28C
		public override object Read(object value, ProtoReader source)
		{
			int field = source.FieldNumber;
			BasicList basicList = new BasicList();
			if (this.packedWireType != WireType.None && source.WireType == WireType.String)
			{
				SubItemToken token = ProtoReader.StartSubItem(source);
				while (ProtoReader.HasSubValue(this.packedWireType, source))
				{
					basicList.Add(this.Tail.Read(null, source));
				}
				ProtoReader.EndSubItem(token, source);
			}
			else
			{
				do
				{
					basicList.Add(this.Tail.Read(null, source));
				}
				while (source.TryReadFieldHeader(field));
			}
			int num = this.AppendToCollection ? ((value == null) ? 0 : ((Array)value).Length) : 0;
			Array array = Array.CreateInstance(this.itemType, num + basicList.Count);
			if (num != 0)
			{
				((Array)value).CopyTo(array, 0);
			}
			basicList.CopyTo(array, num);
			return array;
		}

		// Token: 0x040000D8 RID: 216
		private readonly int fieldNumber;

		// Token: 0x040000D9 RID: 217
		private const byte OPTIONS_WritePacked = 1;

		// Token: 0x040000DA RID: 218
		private const byte OPTIONS_OverwriteList = 2;

		// Token: 0x040000DB RID: 219
		private const byte OPTIONS_SupportNull = 4;

		// Token: 0x040000DC RID: 220
		private readonly byte options;

		// Token: 0x040000DD RID: 221
		private readonly WireType packedWireType;

		// Token: 0x040000DE RID: 222
		private readonly Type arrayType;

		// Token: 0x040000DF RID: 223
		private readonly Type itemType;
	}
}
