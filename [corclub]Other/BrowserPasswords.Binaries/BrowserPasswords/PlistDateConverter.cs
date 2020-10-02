using System;

namespace BrowserPasswords
{
	// Token: 0x0200004A RID: 74
	public class PlistDateConverter
	{
		// Token: 0x06000205 RID: 517 RVA: 0x0000882C File Offset: 0x00006A2C
		public static long GetAppleTime(long unixTime)
		{
			return checked(unixTime - PlistDateConverter.timeDifference);
		}

		// Token: 0x06000206 RID: 518 RVA: 0x00008844 File Offset: 0x00006A44
		public static long GetUnixTime(long appleTime)
		{
			return checked(appleTime + PlistDateConverter.timeDifference);
		}

		// Token: 0x06000207 RID: 519 RVA: 0x0000885C File Offset: 0x00006A5C
		public static DateTime ConvertFromAppleTimeStamp(double timestamp)
		{
			DateTime dateTime = new DateTime(2001, 1, 1, 0, 0, 0, 0);
			return dateTime.AddSeconds(timestamp);
		}

		// Token: 0x06000208 RID: 520 RVA: 0x00008888 File Offset: 0x00006A88
		public static double ConvertToAppleTimeStamp(DateTime date)
		{
			DateTime d = new DateTime(2001, 1, 1, 0, 0, 0, 0);
			return Math.Floor((date - d).TotalSeconds);
		}

		// Token: 0x040001B4 RID: 436
		public static long timeDifference = 978307200L;
	}
}
