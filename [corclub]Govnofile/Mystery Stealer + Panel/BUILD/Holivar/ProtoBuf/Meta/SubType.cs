using System;
using System.Collections;
using System.Collections.Generic;
using ProtoBuf.Serializers;

namespace ProtoBuf.Meta
{
	// Token: 0x0200006F RID: 111
	public sealed class SubType
	{
		// Token: 0x170000EA RID: 234
		// (get) Token: 0x06000398 RID: 920 RVA: 0x000043D9 File Offset: 0x000025D9
		public int FieldNumber
		{
			get
			{
				return this.fieldNumber;
			}
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x06000399 RID: 921 RVA: 0x000043E1 File Offset: 0x000025E1
		public MetaType DerivedType
		{
			get
			{
				return this.derivedType;
			}
		}

		// Token: 0x0600039A RID: 922 RVA: 0x000043E9 File Offset: 0x000025E9
		public SubType(int fieldNumber, MetaType derivedType, DataFormat format)
		{
			if (derivedType == null)
			{
				throw new ArgumentNullException("derivedType");
			}
			if (fieldNumber <= 0)
			{
				throw new ArgumentOutOfRangeException("fieldNumber");
			}
			this.fieldNumber = fieldNumber;
			this.derivedType = derivedType;
			this.dataFormat = format;
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x0600039B RID: 923 RVA: 0x00004423 File Offset: 0x00002623
		internal IProtoSerializer Serializer
		{
			get
			{
				if (this.serializer == null)
				{
					this.serializer = this.BuildSerializer();
				}
				return this.serializer;
			}
		}

		// Token: 0x0600039C RID: 924 RVA: 0x00013694 File Offset: 0x00011894
		private IProtoSerializer BuildSerializer()
		{
			WireType wireType = WireType.String;
			if (this.dataFormat == DataFormat.Group)
			{
				wireType = WireType.StartGroup;
			}
			IProtoSerializer tail = new SubItemSerializer(this.derivedType.Type, this.derivedType.GetKey(false, false), this.derivedType, false);
			return new TagDecorator(this.fieldNumber, wireType, false, tail);
		}

		// Token: 0x04000175 RID: 373
		private readonly int fieldNumber;

		// Token: 0x04000176 RID: 374
		private readonly MetaType derivedType;

		// Token: 0x04000177 RID: 375
		private readonly DataFormat dataFormat;

		// Token: 0x04000178 RID: 376
		private IProtoSerializer serializer;

		// Token: 0x02000070 RID: 112
		internal sealed class Comparer : IComparer, IComparer<SubType>
		{
			// Token: 0x0600039D RID: 925 RVA: 0x0000443F File Offset: 0x0000263F
			public int Compare(object x, object y)
			{
				return this.Compare(x as SubType, y as SubType);
			}

			// Token: 0x0600039E RID: 926 RVA: 0x000136E4 File Offset: 0x000118E4
			public int Compare(SubType x, SubType y)
			{
				if (x == y)
				{
					return 0;
				}
				if (x == null)
				{
					return -1;
				}
				if (y == null)
				{
					return 1;
				}
				return x.FieldNumber.CompareTo(y.FieldNumber);
			}

			// Token: 0x04000179 RID: 377
			public static readonly SubType.Comparer Default = new SubType.Comparer();
		}
	}
}
