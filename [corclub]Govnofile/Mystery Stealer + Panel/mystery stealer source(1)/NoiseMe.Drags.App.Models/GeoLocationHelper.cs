using NoiseMe.Drags.App.Models.JSON;
using System;
using System.IO;
using System.Net;

namespace NoiseMe.Drags.App.Models
{
	public static class GeoLocationHelper
	{
		public static readonly string[] ImageList;

		public static int ImageIndex
		{
			get;
			set;
		}

		public static GeoInformation GeoInfo
		{
			get;
			private set;
		}

		public static DateTime LastLocated
		{
			get;
			private set;
		}

		public static bool LocationCompleted
		{
			get;
			private set;
		}

		static GeoLocationHelper()
		{
			ImageList = new string[247]
			{
				"ad",
				"ae",
				"af",
				"ag",
				"ai",
				"al",
				"am",
				"an",
				"ao",
				"ar",
				"as",
				"at",
				"au",
				"aw",
				"ax",
				"az",
				"ba",
				"bb",
				"bd",
				"be",
				"bf",
				"bg",
				"bh",
				"bi",
				"bj",
				"bm",
				"bn",
				"bo",
				"br",
				"bs",
				"bt",
				"bv",
				"bw",
				"by",
				"bz",
				"ca",
				"catalonia",
				"cc",
				"cd",
				"cf",
				"cg",
				"ch",
				"ci",
				"ck",
				"cl",
				"cm",
				"cn",
				"co",
				"cr",
				"cs",
				"cu",
				"cv",
				"cx",
				"cy",
				"cz",
				"de",
				"dj",
				"dk",
				"dm",
				"do",
				"dz",
				"ec",
				"ee",
				"eg",
				"eh",
				"england",
				"er",
				"es",
				"et",
				"europeanunion",
				"fam",
				"fi",
				"fj",
				"fk",
				"fm",
				"fo",
				"fr",
				"ga",
				"gb",
				"gd",
				"ge",
				"gf",
				"gh",
				"gi",
				"gl",
				"gm",
				"gn",
				"gp",
				"gq",
				"gr",
				"gs",
				"gt",
				"gu",
				"gw",
				"gy",
				"hk",
				"hm",
				"hn",
				"hr",
				"ht",
				"hu",
				"id",
				"ie",
				"il",
				"in",
				"io",
				"iq",
				"ir",
				"is",
				"it",
				"jm",
				"jo",
				"jp",
				"ke",
				"kg",
				"kh",
				"ki",
				"km",
				"kn",
				"kp",
				"kr",
				"kw",
				"ky",
				"kz",
				"la",
				"lb",
				"lc",
				"li",
				"lk",
				"lr",
				"ls",
				"lt",
				"lu",
				"lv",
				"ly",
				"ma",
				"mc",
				"md",
				"me",
				"mg",
				"mh",
				"mk",
				"ml",
				"mm",
				"mn",
				"mo",
				"mp",
				"mq",
				"mr",
				"ms",
				"mt",
				"mu",
				"mv",
				"mw",
				"mx",
				"my",
				"mz",
				"na",
				"nc",
				"ne",
				"nf",
				"ng",
				"ni",
				"nl",
				"no",
				"np",
				"nr",
				"nu",
				"nz",
				"om",
				"pa",
				"pe",
				"pf",
				"pg",
				"ph",
				"pk",
				"pl",
				"pm",
				"pn",
				"pr",
				"ps",
				"pt",
				"pw",
				"py",
				"qa",
				"re",
				"ro",
				"rs",
				"ru",
				"rw",
				"sa",
				"sb",
				"sc",
				"scotland",
				"sd",
				"se",
				"sg",
				"sh",
				"si",
				"sj",
				"sk",
				"sl",
				"sm",
				"sn",
				"so",
				"sr",
				"st",
				"sv",
				"sy",
				"sz",
				"tc",
				"td",
				"tf",
				"tg",
				"th",
				"tj",
				"tk",
				"tl",
				"tm",
				"tn",
				"to",
				"tr",
				"tt",
				"tv",
				"tw",
				"tz",
				"ua",
				"ug",
				"um",
				"us",
				"uy",
				"uz",
				"va",
				"vc",
				"ve",
				"vg",
				"vi",
				"vn",
				"vu",
				"wales",
				"wf",
				"ws",
				"ye",
				"yt",
				"za",
				"zm",
				"zw"
			};
			GeoInfo = new GeoInformation();
			LastLocated = new DateTime(1, 1, 1, 0, 0, 0, DateTimeKind.Utc);
		}

		public static void Initialize()
		{
			TryLocate();
			GeoInfo.Query = (string.IsNullOrEmpty(GeoInfo.Query) ? "UNKNOWN" : GeoInfo.Query);
			GeoInfo.Country = (string.IsNullOrEmpty(GeoInfo.Country) ? "UNKNOWN" : GeoInfo.Country);
			GeoInfo.CountryCode = (string.IsNullOrEmpty(GeoInfo.CountryCode) ? "UNKNOWN" : GeoInfo.CountryCode);
			GeoInfo.Region = (string.IsNullOrEmpty(GeoInfo.Region) ? "UNKNOWN" : GeoInfo.Region);
			GeoInfo.City = (string.IsNullOrEmpty(GeoInfo.City) ? "UNKNOWN" : GeoInfo.City);
			GeoInfo.Timezone = (string.IsNullOrEmpty(GeoInfo.Timezone) ? "UNKNOWN" : GeoInfo.Timezone);
			GeoInfo.Isp = (string.IsNullOrEmpty(GeoInfo.Isp) ? "UNKNOWN" : GeoInfo.Isp);
		}

		private static void TryLocate()
		{
			LocationCompleted = false;
			try
			{
				HttpWebRequest obj = (HttpWebRequest)WebRequest.Create("http://ip-api.com/json/");
				obj.UserAgent = "Mozilla/5.0 (Windows NT 6.3; rv:48.0) Gecko/20100101 Firefox/48.0";
				obj.Timeout = 10000;
				using (HttpWebResponse httpWebResponse = (HttpWebResponse)obj.GetResponse())
				{
					using (Stream stream = httpWebResponse.GetResponseStream())
					{
						using (StreamReader streamReader = new StreamReader(stream))
						{
							JsonValue jsonValue = streamReader.ReadToEnd().FromJSON();
							GeoInfo = new GeoInformation();
							GeoInfo.City = jsonValue["city"].ToString(saving: false);
							GeoInfo.Country = jsonValue["country"].ToString(saving: false);
							GeoInfo.CountryCode = jsonValue["countryCode"].ToString(saving: false);
							GeoInfo.Isp = jsonValue["isp"].ToString(saving: false);
							GeoInfo.Query = jsonValue["query"].ToString(saving: false);
							GeoInfo.Region = jsonValue["region"].ToString(saving: false);
							GeoInfo.Timezone = jsonValue["timezone"].ToString(saving: false);
						}
					}
				}
				LastLocated = DateTime.UtcNow;
				LocationCompleted = true;
			}
			catch
			{
				TryGetWanIp();
			}
		}

		private static void TryGetWanIp()
		{
			string query = "";
			try
			{
				HttpWebRequest obj = (HttpWebRequest)WebRequest.Create("http://api.ipify.org/");
				obj.Timeout = 5000;
				using (HttpWebResponse httpWebResponse = (HttpWebResponse)obj.GetResponse())
				{
					using (Stream stream = httpWebResponse.GetResponseStream())
					{
						using (StreamReader streamReader = new StreamReader(stream))
						{
							query = streamReader.ReadToEnd();
						}
					}
				}
			}
			catch
			{
			}
			GeoInfo.Query = query;
		}
	}
}
