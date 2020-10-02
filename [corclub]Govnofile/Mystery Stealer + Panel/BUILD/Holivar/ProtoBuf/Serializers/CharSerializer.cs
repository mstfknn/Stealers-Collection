using System;
using ProtoBuf.Meta;

namespace ProtoBuf.Serializers
{
	// Token: 0x02000039 RID: 57
	internal sealed class CharSerializer : UInt16Serializer
	{
		// Token: 0x060001AE RID: 430 RVA: 0x00003226 File Offset: 0x00001426
		public CharSerializer(TypeModel model) : base(model)
		{
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060001AF RID: 431 RVA: 0x0000322F File Offset: 0x0000142F
		public override Type ExpectedType
		{
			get
			{
				return CharSerializer.expectedType;
			}
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x00003236 File Offset: 0x00001436
		public override void Write(object value, ProtoWriter dest)
		{
			ProtoWriter.WriteUInt16((ushort)((char)value), dest);
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x00003244 File Offset: 0x00001444
		public override object Read(object value, ProtoReader source)
		{
			return (char)source.ReadUInt16();
		}

		// Token: 0x040000E4 RID: 228
		private static readonly Type expectedType = typeof(char);
	}
}
