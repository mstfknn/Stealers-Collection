using System;
using System.Net;

namespace MailRy.Net
{
	// Token: 0x02000006 RID: 6
	public class DnsEndPoint : EndPoint
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000B RID: 11 RVA: 0x000021FE File Offset: 0x000003FE
		// (set) Token: 0x0600000C RID: 12 RVA: 0x00002206 File Offset: 0x00000406
		public string Host { get; private set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000D RID: 13 RVA: 0x0000220F File Offset: 0x0000040F
		// (set) Token: 0x0600000E RID: 14 RVA: 0x00002217 File Offset: 0x00000417
		public int Port { get; private set; }

		// Token: 0x0600000F RID: 15 RVA: 0x00002220 File Offset: 0x00000420
		public DnsEndPoint(string host, int port)
		{
			this.Host = host;
			this.Port = port;
		}
	}
}
