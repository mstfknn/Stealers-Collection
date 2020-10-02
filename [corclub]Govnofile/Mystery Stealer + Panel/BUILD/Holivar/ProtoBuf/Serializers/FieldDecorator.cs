using System;
using System.Reflection;

namespace ProtoBuf.Serializers
{
	// Token: 0x02000040 RID: 64
	internal sealed class FieldDecorator : ProtoDecoratorBase
	{
		// Token: 0x1700005D RID: 93
		// (get) Token: 0x060001D8 RID: 472 RVA: 0x00003378 File Offset: 0x00001578
		public override Type ExpectedType
		{
			get
			{
				return this.forType;
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x060001D9 RID: 473 RVA: 0x00003147 File Offset: 0x00001347
		public override bool RequiresOldValue
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x060001DA RID: 474 RVA: 0x000031DF File Offset: 0x000013DF
		public override bool ReturnsValue
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060001DB RID: 475 RVA: 0x00003380 File Offset: 0x00001580
		public FieldDecorator(Type forType, FieldInfo field, IProtoSerializer tail) : base(tail)
		{
			this.forType = forType;
			this.field = field;
		}

		// Token: 0x060001DC RID: 476 RVA: 0x00003397 File Offset: 0x00001597
		public override void Write(object value, ProtoWriter dest)
		{
			value = this.field.GetValue(value);
			if (value != null)
			{
				this.Tail.Write(value, dest);
			}
		}

		// Token: 0x060001DD RID: 477 RVA: 0x0000E4F4 File Offset: 0x0000C6F4
		public override object Read(object value, ProtoReader source)
		{
			object obj = this.Tail.Read(this.Tail.RequiresOldValue ? this.field.GetValue(value) : null, source);
			if (obj != null)
			{
				this.field.SetValue(value, obj);
			}
			return null;
		}

		// Token: 0x040000EE RID: 238
		private readonly FieldInfo field;

		// Token: 0x040000EF RID: 239
		private readonly Type forType;
	}
}
