using System;
using ProtoBuf.Meta;

namespace ProtoBuf.Serializers
{
	// Token: 0x02000037 RID: 55
	internal sealed class BooleanSerializer : IProtoSerializer
	{
		// Token: 0x060001A0 RID: 416 RVA: 0x000022E5 File Offset: 0x000004E5
		public BooleanSerializer(TypeModel model)
		{
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060001A1 RID: 417 RVA: 0x000031BD File Offset: 0x000013BD
		public Type ExpectedType
		{
			get
			{
				return BooleanSerializer.expectedType;
			}
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x000031C4 File Offset: 0x000013C4
		public void Write(object value, ProtoWriter dest)
		{
			ProtoWriter.WriteBoolean((bool)value, dest);
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x000031D2 File Offset: 0x000013D2
		public object Read(object value, ProtoReader source)
		{
			return source.ReadBoolean();
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060001A4 RID: 420 RVA: 0x000031DF File Offset: 0x000013DF
		bool IProtoSerializer.RequiresOldValue
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060001A5 RID: 421 RVA: 0x00003147 File Offset: 0x00001347
		bool IProtoSerializer.ReturnsValue
		{
			get
			{
				return true;
			}
		}

		// Token: 0x040000E2 RID: 226
		private static readonly Type expectedType = typeof(bool);
	}
}
