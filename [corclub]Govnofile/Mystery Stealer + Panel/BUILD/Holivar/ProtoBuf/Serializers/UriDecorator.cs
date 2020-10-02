using System;
using ProtoBuf.Meta;

namespace ProtoBuf.Serializers
{
	// Token: 0x0200005D RID: 93
	internal sealed class UriDecorator : ProtoDecoratorBase
	{
		// Token: 0x060002B7 RID: 695 RVA: 0x00003A48 File Offset: 0x00001C48
		public UriDecorator(TypeModel model, IProtoSerializer tail) : base(tail)
		{
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x060002B8 RID: 696 RVA: 0x00003A51 File Offset: 0x00001C51
		public override Type ExpectedType
		{
			get
			{
				return UriDecorator.expectedType;
			}
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x060002B9 RID: 697 RVA: 0x000031DF File Offset: 0x000013DF
		public override bool RequiresOldValue
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x060002BA RID: 698 RVA: 0x00003147 File Offset: 0x00001347
		public override bool ReturnsValue
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060002BB RID: 699 RVA: 0x00003A58 File Offset: 0x00001C58
		public override void Write(object value, ProtoWriter dest)
		{
			this.Tail.Write(((Uri)value).AbsoluteUri, dest);
		}

		// Token: 0x060002BC RID: 700 RVA: 0x0000FD40 File Offset: 0x0000DF40
		public override object Read(object value, ProtoReader source)
		{
			string text = (string)this.Tail.Read(null, source);
			if (text.Length != 0)
			{
				return new Uri(text);
			}
			return null;
		}

		// Token: 0x04000136 RID: 310
		private static readonly Type expectedType = typeof(Uri);
	}
}
