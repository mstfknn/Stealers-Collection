using System;
using ProtoBuf.Meta;

namespace ProtoBuf.Serializers
{
	// Token: 0x02000056 RID: 86
	internal sealed class TagDecorator : ProtoDecoratorBase, IProtoTypeSerializer, IProtoSerializer
	{
		// Token: 0x06000275 RID: 629 RVA: 0x0000F434 File Offset: 0x0000D634
		public bool HasCallbacks(TypeModel.CallbackType callbackType)
		{
			IProtoTypeSerializer protoTypeSerializer = this.Tail as IProtoTypeSerializer;
			return protoTypeSerializer != null && protoTypeSerializer.HasCallbacks(callbackType);
		}

		// Token: 0x06000276 RID: 630 RVA: 0x0000F45C File Offset: 0x0000D65C
		public bool CanCreateInstance()
		{
			IProtoTypeSerializer protoTypeSerializer = this.Tail as IProtoTypeSerializer;
			return protoTypeSerializer != null && protoTypeSerializer.CanCreateInstance();
		}

		// Token: 0x06000277 RID: 631 RVA: 0x0000386E File Offset: 0x00001A6E
		public object CreateInstance(ProtoReader source)
		{
			return ((IProtoTypeSerializer)this.Tail).CreateInstance(source);
		}

		// Token: 0x06000278 RID: 632 RVA: 0x0000F480 File Offset: 0x0000D680
		public void Callback(object value, TypeModel.CallbackType callbackType, SerializationContext context)
		{
			IProtoTypeSerializer protoTypeSerializer = this.Tail as IProtoTypeSerializer;
			if (protoTypeSerializer != null)
			{
				protoTypeSerializer.Callback(value, callbackType, context);
			}
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x06000279 RID: 633 RVA: 0x000032C8 File Offset: 0x000014C8
		public override Type ExpectedType
		{
			get
			{
				return this.Tail.ExpectedType;
			}
		}

		// Token: 0x0600027A RID: 634 RVA: 0x00003881 File Offset: 0x00001A81
		public TagDecorator(int fieldNumber, WireType wireType, bool strict, IProtoSerializer tail) : base(tail)
		{
			this.fieldNumber = fieldNumber;
			this.wireType = wireType;
			this.strict = strict;
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x0600027B RID: 635 RVA: 0x000032D5 File Offset: 0x000014D5
		public override bool RequiresOldValue
		{
			get
			{
				return this.Tail.RequiresOldValue;
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x0600027C RID: 636 RVA: 0x000032E2 File Offset: 0x000014E2
		public override bool ReturnsValue
		{
			get
			{
				return this.Tail.ReturnsValue;
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x0600027D RID: 637 RVA: 0x000038A0 File Offset: 0x00001AA0
		private bool NeedsHint
		{
			get
			{
				return (this.wireType & (WireType)(-8)) > WireType.Variant;
			}
		}

		// Token: 0x0600027E RID: 638 RVA: 0x000038AE File Offset: 0x00001AAE
		public override object Read(object value, ProtoReader source)
		{
			if (this.strict)
			{
				source.Assert(this.wireType);
			}
			else if (this.NeedsHint)
			{
				source.Hint(this.wireType);
			}
			return this.Tail.Read(value, source);
		}

		// Token: 0x0600027F RID: 639 RVA: 0x000038E7 File Offset: 0x00001AE7
		public override void Write(object value, ProtoWriter dest)
		{
			ProtoWriter.WriteFieldHeader(this.fieldNumber, this.wireType, dest);
			this.Tail.Write(value, dest);
		}

		// Token: 0x04000120 RID: 288
		private readonly bool strict;

		// Token: 0x04000121 RID: 289
		private readonly int fieldNumber;

		// Token: 0x04000122 RID: 290
		private readonly WireType wireType;
	}
}
