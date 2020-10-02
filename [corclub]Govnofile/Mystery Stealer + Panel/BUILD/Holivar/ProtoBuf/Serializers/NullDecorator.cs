using System;
using ProtoBuf.Meta;

namespace ProtoBuf.Serializers
{
	// Token: 0x0200004C RID: 76
	internal sealed class NullDecorator : ProtoDecoratorBase
	{
		// Token: 0x06000226 RID: 550 RVA: 0x0000EF48 File Offset: 0x0000D148
		public NullDecorator(TypeModel model, IProtoSerializer tail) : base(tail)
		{
			if (!tail.ReturnsValue)
			{
				throw new NotSupportedException("NullDecorator only supports implementations that return values");
			}
			Type type = tail.ExpectedType;
			if (Helpers.IsValueType(type))
			{
				this.expectedType = model.MapType(typeof(Nullable<>)).MakeGenericType(new Type[]
				{
					type
				});
				return;
			}
			this.expectedType = type;
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000227 RID: 551 RVA: 0x000035B7 File Offset: 0x000017B7
		public override Type ExpectedType
		{
			get
			{
				return this.expectedType;
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000228 RID: 552 RVA: 0x00003147 File Offset: 0x00001347
		public override bool ReturnsValue
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x06000229 RID: 553 RVA: 0x00003147 File Offset: 0x00001347
		public override bool RequiresOldValue
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600022A RID: 554 RVA: 0x0000EFAC File Offset: 0x0000D1AC
		public override object Read(object value, ProtoReader source)
		{
			SubItemToken token = ProtoReader.StartSubItem(source);
			int num;
			while ((num = source.ReadFieldHeader()) > 0)
			{
				if (num == 1)
				{
					value = this.Tail.Read(value, source);
				}
				else
				{
					source.SkipField();
				}
			}
			ProtoReader.EndSubItem(token, source);
			return value;
		}

		// Token: 0x0600022B RID: 555 RVA: 0x000035BF File Offset: 0x000017BF
		public override void Write(object value, ProtoWriter dest)
		{
			SubItemToken token = ProtoWriter.StartSubItem(null, dest);
			if (value != null)
			{
				this.Tail.Write(value, dest);
			}
			ProtoWriter.EndSubItem(token, dest);
		}

		// Token: 0x0400010B RID: 267
		private readonly Type expectedType;

		// Token: 0x0400010C RID: 268
		public const int Tag = 1;
	}
}
