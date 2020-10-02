using System;
using ProtoBuf.Meta;

namespace ProtoBuf.Serializers
{
	// Token: 0x0200003C RID: 60
	internal sealed class DefaultValueDecorator : ProtoDecoratorBase
	{
		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060001C1 RID: 449 RVA: 0x000032C8 File Offset: 0x000014C8
		public override Type ExpectedType
		{
			get
			{
				return this.Tail.ExpectedType;
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060001C2 RID: 450 RVA: 0x000032D5 File Offset: 0x000014D5
		public override bool RequiresOldValue
		{
			get
			{
				return this.Tail.RequiresOldValue;
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060001C3 RID: 451 RVA: 0x000032E2 File Offset: 0x000014E2
		public override bool ReturnsValue
		{
			get
			{
				return this.Tail.ReturnsValue;
			}
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x0000E158 File Offset: 0x0000C358
		public DefaultValueDecorator(TypeModel model, object defaultValue, IProtoSerializer tail) : base(tail)
		{
			if (defaultValue == null)
			{
				throw new ArgumentNullException("defaultValue");
			}
			if (model.MapType(defaultValue.GetType()) != tail.ExpectedType)
			{
				throw new ArgumentException("Default value is of incorrect type", "defaultValue");
			}
			this.defaultValue = defaultValue;
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x000032EF File Offset: 0x000014EF
		public override void Write(object value, ProtoWriter dest)
		{
			if (!object.Equals(value, this.defaultValue))
			{
				this.Tail.Write(value, dest);
			}
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x0000330C File Offset: 0x0000150C
		public override object Read(object value, ProtoReader source)
		{
			return this.Tail.Read(value, source);
		}

		// Token: 0x040000E7 RID: 231
		private readonly object defaultValue;
	}
}
