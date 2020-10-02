using System;
using ProtoBuf.Meta;

namespace ProtoBuf.Serializers
{
	// Token: 0x02000055 RID: 85
	internal sealed class SystemTypeSerializer : IProtoSerializer
	{
		// Token: 0x0600026E RID: 622 RVA: 0x000022E5 File Offset: 0x000004E5
		public SystemTypeSerializer(TypeModel model)
		{
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x0600026F RID: 623 RVA: 0x00003840 File Offset: 0x00001A40
		public Type ExpectedType
		{
			get
			{
				return SystemTypeSerializer.expectedType;
			}
		}

		// Token: 0x06000270 RID: 624 RVA: 0x00003847 File Offset: 0x00001A47
		void IProtoSerializer.Write(object value, ProtoWriter dest)
		{
			ProtoWriter.WriteType((Type)value, dest);
		}

		// Token: 0x06000271 RID: 625 RVA: 0x00003855 File Offset: 0x00001A55
		object IProtoSerializer.Read(object value, ProtoReader source)
		{
			return source.ReadType();
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x06000272 RID: 626 RVA: 0x000031DF File Offset: 0x000013DF
		bool IProtoSerializer.RequiresOldValue
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x06000273 RID: 627 RVA: 0x00003147 File Offset: 0x00001347
		bool IProtoSerializer.ReturnsValue
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0400011F RID: 287
		private static readonly Type expectedType = typeof(Type);
	}
}
