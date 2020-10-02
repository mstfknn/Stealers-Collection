using System;
using ProtoBuf.Meta;

namespace ProtoBuf.Serializers
{
	// Token: 0x0200004B RID: 75
	internal sealed class NetObjectSerializer : IProtoSerializer
	{
		// Token: 0x06000220 RID: 544 RVA: 0x0000EEFC File Offset: 0x0000D0FC
		public NetObjectSerializer(TypeModel model, Type type, int key, BclHelpers.NetObjectOptions options)
		{
			bool flag = (options & BclHelpers.NetObjectOptions.DynamicType) > BclHelpers.NetObjectOptions.None;
			this.key = (flag ? -1 : key);
			this.type = (flag ? model.MapType(typeof(object)) : type);
			this.options = options;
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x06000221 RID: 545 RVA: 0x0000356A File Offset: 0x0000176A
		public Type ExpectedType
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x06000222 RID: 546 RVA: 0x00003147 File Offset: 0x00001347
		public bool ReturnsValue
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x06000223 RID: 547 RVA: 0x00003147 File Offset: 0x00001347
		public bool RequiresOldValue
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000224 RID: 548 RVA: 0x00003572 File Offset: 0x00001772
		public object Read(object value, ProtoReader source)
		{
			return BclHelpers.ReadNetObject(value, source, this.key, (this.type == typeof(object)) ? null : this.type, this.options);
		}

		// Token: 0x06000225 RID: 549 RVA: 0x000035A2 File Offset: 0x000017A2
		public void Write(object value, ProtoWriter dest)
		{
			BclHelpers.WriteNetObject(value, dest, this.key, this.options);
		}

		// Token: 0x04000108 RID: 264
		private readonly int key;

		// Token: 0x04000109 RID: 265
		private readonly Type type;

		// Token: 0x0400010A RID: 266
		private readonly BclHelpers.NetObjectOptions options;
	}
}
