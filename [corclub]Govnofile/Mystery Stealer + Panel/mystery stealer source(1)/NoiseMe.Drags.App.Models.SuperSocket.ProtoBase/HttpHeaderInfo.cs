using System;
using System.Collections.Generic;

namespace NoiseMe.Drags.App.Models.SuperSocket.ProtoBase
{
	public class HttpHeaderInfo : Dictionary<string, string>
	{
		public string Method
		{
			get;
			set;
		}

		public string Path
		{
			get;
			set;
		}

		public string Version
		{
			get;
			set;
		}

		public HttpHeaderInfo()
			: base((IEqualityComparer<string>)StringComparer.OrdinalIgnoreCase)
		{
		}

		public string Get(string key)
		{
			string value = string.Empty;
			TryGetValue(key, out value);
			return value;
		}
	}
}
