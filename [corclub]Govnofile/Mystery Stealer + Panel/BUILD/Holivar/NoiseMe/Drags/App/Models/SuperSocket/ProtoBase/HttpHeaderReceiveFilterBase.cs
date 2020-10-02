using System;
using System.Text;

namespace NoiseMe.Drags.App.Models.SuperSocket.ProtoBase
{
	// Token: 0x020000DE RID: 222
	public abstract class HttpHeaderReceiveFilterBase<TPackageInfo> : TerminatorReceiveFilter<TPackageInfo> where TPackageInfo : IPackageInfo
	{
		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x06000720 RID: 1824 RVA: 0x000061F4 File Offset: 0x000043F4
		// (set) Token: 0x06000721 RID: 1825 RVA: 0x000061FC File Offset: 0x000043FC
		public Encoding HeaderEncoding { get; private set; }

		// Token: 0x06000722 RID: 1826 RVA: 0x00006205 File Offset: 0x00004405
		protected HttpHeaderReceiveFilterBase() : this(Encoding.UTF8)
		{
		}

		// Token: 0x06000723 RID: 1827 RVA: 0x00006212 File Offset: 0x00004412
		protected HttpHeaderReceiveFilterBase(Encoding headerEncoding) : this(headerEncoding, HttpHeaderReceiveFilterBase<TPackageInfo>.NewLine)
		{
		}

		// Token: 0x06000724 RID: 1828 RVA: 0x00006220 File Offset: 0x00004420
		protected HttpHeaderReceiveFilterBase(Encoding headerEncoding, byte[] terminator) : base(terminator)
		{
			if (headerEncoding == null)
			{
				throw new ArgumentNullException("headerEncoding");
			}
			this.HeaderEncoding = headerEncoding;
		}

		// Token: 0x06000725 RID: 1829
		protected abstract IReceiveFilter<TPackageInfo> GetBodyReceiveFilter(HttpHeaderInfo header, int headerSize);

		// Token: 0x06000726 RID: 1830
		protected abstract TPackageInfo ResolveHttpPackageWithoutBody(HttpHeaderInfo header);

		// Token: 0x06000727 RID: 1831 RVA: 0x0001B99C File Offset: 0x00019B9C
		public override TPackageInfo ResolvePackage(IBufferStream bufferStream)
		{
			int num = (int)bufferStream.Length;
			string headerData = bufferStream.ReadString(num, this.HeaderEncoding);
			HttpHeaderInfo header = new HttpHeaderInfo();
			MimeHeaderHelper.ParseHttpHeader(headerData, header);
			IReceiveFilter<TPackageInfo> bodyReceiveFilter = this.GetBodyReceiveFilter(header, num);
			if (bodyReceiveFilter != null)
			{
				base.NextReceiveFilter = bodyReceiveFilter;
				bufferStream.Clear();
				return default(TPackageInfo);
			}
			return this.ResolveHttpPackageWithoutBody(header);
		}

		// Token: 0x040002EF RID: 751
		private static readonly byte[] NewLine = new byte[]
		{
			13,
			10,
			13,
			10
		};
	}
}
