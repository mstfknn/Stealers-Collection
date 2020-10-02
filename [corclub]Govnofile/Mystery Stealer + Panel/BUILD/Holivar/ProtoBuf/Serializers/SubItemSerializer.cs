using System;
using ProtoBuf.Meta;

namespace ProtoBuf.Serializers
{
	// Token: 0x02000053 RID: 83
	internal sealed class SubItemSerializer : IProtoTypeSerializer, IProtoSerializer
	{
		// Token: 0x06000258 RID: 600 RVA: 0x0000373D File Offset: 0x0000193D
		bool IProtoTypeSerializer.HasCallbacks(TypeModel.CallbackType callbackType)
		{
			return ((IProtoTypeSerializer)this.proxy.Serializer).HasCallbacks(callbackType);
		}

		// Token: 0x06000259 RID: 601 RVA: 0x00003755 File Offset: 0x00001955
		bool IProtoTypeSerializer.CanCreateInstance()
		{
			return ((IProtoTypeSerializer)this.proxy.Serializer).CanCreateInstance();
		}

		// Token: 0x0600025A RID: 602 RVA: 0x0000376C File Offset: 0x0000196C
		void IProtoTypeSerializer.Callback(object value, TypeModel.CallbackType callbackType, SerializationContext context)
		{
			((IProtoTypeSerializer)this.proxy.Serializer).Callback(value, callbackType, context);
		}

		// Token: 0x0600025B RID: 603 RVA: 0x00003786 File Offset: 0x00001986
		object IProtoTypeSerializer.CreateInstance(ProtoReader source)
		{
			return ((IProtoTypeSerializer)this.proxy.Serializer).CreateInstance(source);
		}

		// Token: 0x0600025C RID: 604 RVA: 0x0000F238 File Offset: 0x0000D438
		public SubItemSerializer(Type type, int key, ISerializerProxy proxy, bool recursionCheck)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (proxy == null)
			{
				throw new ArgumentNullException("proxy");
			}
			this.type = type;
			this.proxy = proxy;
			this.key = key;
			this.recursionCheck = recursionCheck;
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x0600025D RID: 605 RVA: 0x0000379E File Offset: 0x0000199E
		Type IProtoSerializer.ExpectedType
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x0600025E RID: 606 RVA: 0x00003147 File Offset: 0x00001347
		bool IProtoSerializer.RequiresOldValue
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x0600025F RID: 607 RVA: 0x00003147 File Offset: 0x00001347
		bool IProtoSerializer.ReturnsValue
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000260 RID: 608 RVA: 0x000037A6 File Offset: 0x000019A6
		void IProtoSerializer.Write(object value, ProtoWriter dest)
		{
			if (this.recursionCheck)
			{
				ProtoWriter.WriteObject(value, this.key, dest);
				return;
			}
			ProtoWriter.WriteRecursionSafeObject(value, this.key, dest);
		}

		// Token: 0x06000261 RID: 609 RVA: 0x000037CB File Offset: 0x000019CB
		object IProtoSerializer.Read(object value, ProtoReader source)
		{
			return ProtoReader.ReadObject(value, this.key, source);
		}

		// Token: 0x04000116 RID: 278
		private readonly int key;

		// Token: 0x04000117 RID: 279
		private readonly Type type;

		// Token: 0x04000118 RID: 280
		private readonly ISerializerProxy proxy;

		// Token: 0x04000119 RID: 281
		private readonly bool recursionCheck;
	}
}
