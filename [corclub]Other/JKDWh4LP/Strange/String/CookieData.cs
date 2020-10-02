using System;

namespace Strange.String
{
	public class CookieData
	{
		public string host_key { get; set; }
		public string name { get; set; }
		public string value { get; set; }
		public string path { get; set; }
		public string expires_utc { get; set; }
		public string secure { get; set; }

		public override string ToString()
		{
			return string.Format("{0}\tFALSE\t{1}\t{2}\t{3}\t{4}\t{5}\r\n", new object[]
			{
				this.host_key,
				this.path,
				this.secure.ToUpper(),
				this.expires_utc,
				this.name,
				this.value
			});
		}
	}
}
