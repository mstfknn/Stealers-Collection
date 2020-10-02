using System;
using ProtoBuf.Meta;

namespace ProtoBuf.Serializers
{
	// Token: 0x0200005A RID: 90
	internal class UInt16Serializer : IProtoSerializer
	{
		// Token: 0x060002A2 RID: 674 RVA: 0x000022E5 File Offset: 0x000004E5
		public UInt16Serializer(TypeModel model)
		{
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x060002A3 RID: 675 RVA: 0x000039AF File Offset: 0x00001BAF
		public virtual Type ExpectedType
		{
			get
			{
				return UInt16Serializer.expectedType;
			}
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x060002A4 RID: 676 RVA: 0x000031DF File Offset: 0x000013DF
		bool IProtoSerializer.RequiresOldValue
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x060002A5 RID: 677 RVA: 0x00003147 File Offset: 0x00001347
		bool IProtoSerializer.ReturnsValue
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060002A6 RID: 678 RVA: 0x000039B6 File Offset: 0x00001BB6
		public virtual object Read(object value, ProtoReader source)
		{
			return source.ReadUInt16();
		}

		// Token: 0x060002A7 RID: 679 RVA: 0x000039C3 File Offset: 0x00001BC3
		public virtual void Write(object value, ProtoWriter dest)
		{
			ProtoWriter.WriteUInt16((ushort)value, dest);
		}

		// Token: 0x04000133 RID: 307
		private static readonly Type expectedType = typeof(ushort);
	}
}
