using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;

namespace NoiseMe.Drags.App.Models.SuperSocket.ClientEngine
{
	// Token: 0x02000118 RID: 280
	public interface IClientSession
	{
		// Token: 0x17000237 RID: 567
		// (get) Token: 0x0600088C RID: 2188
		Socket Socket { get; }

		// Token: 0x17000238 RID: 568
		// (get) Token: 0x0600088D RID: 2189
		// (set) Token: 0x0600088E RID: 2190
		IProxyConnector Proxy { get; set; }

		// Token: 0x17000239 RID: 569
		// (get) Token: 0x0600088F RID: 2191
		// (set) Token: 0x06000890 RID: 2192
		int ReceiveBufferSize { get; set; }

		// Token: 0x1700023A RID: 570
		// (get) Token: 0x06000891 RID: 2193
		// (set) Token: 0x06000892 RID: 2194
		int SendingQueueSize { get; set; }

		// Token: 0x1700023B RID: 571
		// (get) Token: 0x06000893 RID: 2195
		bool IsConnected { get; }

		// Token: 0x06000894 RID: 2196
		void Connect(EndPoint remoteEndPoint);

		// Token: 0x06000895 RID: 2197
		void Send(ArraySegment<byte> segment);

		// Token: 0x06000896 RID: 2198
		void Send(IList<ArraySegment<byte>> segments);

		// Token: 0x06000897 RID: 2199
		void Send(byte[] data, int offset, int length);

		// Token: 0x06000898 RID: 2200
		bool TrySend(ArraySegment<byte> segment);

		// Token: 0x06000899 RID: 2201
		bool TrySend(IList<ArraySegment<byte>> segments);

		// Token: 0x0600089A RID: 2202
		void Close();

		// Token: 0x1400000E RID: 14
		// (add) Token: 0x0600089B RID: 2203
		// (remove) Token: 0x0600089C RID: 2204
		event EventHandler Connected;

		// Token: 0x1400000F RID: 15
		// (add) Token: 0x0600089D RID: 2205
		// (remove) Token: 0x0600089E RID: 2206
		event EventHandler Closed;

		// Token: 0x14000010 RID: 16
		// (add) Token: 0x0600089F RID: 2207
		// (remove) Token: 0x060008A0 RID: 2208
		event EventHandler<ErrorEventArgs> Error;

		// Token: 0x14000011 RID: 17
		// (add) Token: 0x060008A1 RID: 2209
		// (remove) Token: 0x060008A2 RID: 2210
		event EventHandler<DataEventArgs> DataReceived;
	}
}
