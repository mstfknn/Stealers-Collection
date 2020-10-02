using System;
using System.Net.Sockets;

namespace NoiseMe.Drags.App.Models.SuperSocket.ClientEngine
{
	// Token: 0x0200010F RID: 271
	public class ProxyEventArgs : EventArgs
	{
		// Token: 0x0600082A RID: 2090 RVA: 0x0000692B File Offset: 0x00004B2B
		public ProxyEventArgs(Socket socket) : this(true, socket, null, null)
		{
		}

		// Token: 0x0600082B RID: 2091 RVA: 0x00006937 File Offset: 0x00004B37
		public ProxyEventArgs(Socket socket, string targetHostHame) : this(true, socket, targetHostHame, null)
		{
		}

		// Token: 0x0600082C RID: 2092 RVA: 0x00006943 File Offset: 0x00004B43
		public ProxyEventArgs(Exception exception) : this(false, null, null, exception)
		{
		}

		// Token: 0x0600082D RID: 2093 RVA: 0x0000694F File Offset: 0x00004B4F
		public ProxyEventArgs(bool connected, Socket socket, string targetHostName, Exception exception)
		{
			this.Connected = connected;
			this.Socket = socket;
			this.TargetHostName = targetHostName;
			this.Exception = exception;
		}

		// Token: 0x17000220 RID: 544
		// (get) Token: 0x0600082E RID: 2094 RVA: 0x00006974 File Offset: 0x00004B74
		// (set) Token: 0x0600082F RID: 2095 RVA: 0x0000697C File Offset: 0x00004B7C
		public bool Connected { get; private set; }

		// Token: 0x17000221 RID: 545
		// (get) Token: 0x06000830 RID: 2096 RVA: 0x00006985 File Offset: 0x00004B85
		// (set) Token: 0x06000831 RID: 2097 RVA: 0x0000698D File Offset: 0x00004B8D
		public Socket Socket { get; private set; }

		// Token: 0x17000222 RID: 546
		// (get) Token: 0x06000832 RID: 2098 RVA: 0x00006996 File Offset: 0x00004B96
		// (set) Token: 0x06000833 RID: 2099 RVA: 0x0000699E File Offset: 0x00004B9E
		public Exception Exception { get; private set; }

		// Token: 0x17000223 RID: 547
		// (get) Token: 0x06000834 RID: 2100 RVA: 0x000069A7 File Offset: 0x00004BA7
		// (set) Token: 0x06000835 RID: 2101 RVA: 0x000069AF File Offset: 0x00004BAF
		public string TargetHostName { get; private set; }
	}
}
