using System;
using System.Text;

namespace NoiseMe.Drags.App.Models.SuperSocket.ProtoBase
{
	public abstract class HttpHeaderReceiveFilterBase<TPackageInfo> : TerminatorReceiveFilter<TPackageInfo> where TPackageInfo : IPackageInfo
	{
		private static readonly byte[] NewLine = new byte[4]
		{
			13,
			10,
			13,
			10
		};

		public Encoding HeaderEncoding
		{
			get;
			private set;
		}

		protected HttpHeaderReceiveFilterBase()
			: this(Encoding.UTF8)
		{
		}

		protected HttpHeaderReceiveFilterBase(Encoding headerEncoding)
			: this(headerEncoding, NewLine)
		{
		}

		protected HttpHeaderReceiveFilterBase(Encoding headerEncoding, byte[] terminator)
			: base(terminator)
		{
			if (headerEncoding == null)
			{
				throw new ArgumentNullException("headerEncoding");
			}
			HeaderEncoding = headerEncoding;
		}

		protected abstract IReceiveFilter<TPackageInfo> GetBodyReceiveFilter(HttpHeaderInfo header, int headerSize);

		protected abstract TPackageInfo ResolveHttpPackageWithoutBody(HttpHeaderInfo header);

		public override TPackageInfo ResolvePackage(IBufferStream bufferStream)
		{
			int num = (int)bufferStream.Length;
			string headerData = bufferStream.ReadString(num, HeaderEncoding);
			HttpHeaderInfo header = new HttpHeaderInfo();
			MimeHeaderHelper.ParseHttpHeader(headerData, header);
			IReceiveFilter<TPackageInfo> bodyReceiveFilter = GetBodyReceiveFilter(header, num);
			if (bodyReceiveFilter != null)
			{
				base.NextReceiveFilter = bodyReceiveFilter;
				bufferStream.Clear();
				return default(TPackageInfo);
			}
			return ResolveHttpPackageWithoutBody(header);
		}
	}
}
