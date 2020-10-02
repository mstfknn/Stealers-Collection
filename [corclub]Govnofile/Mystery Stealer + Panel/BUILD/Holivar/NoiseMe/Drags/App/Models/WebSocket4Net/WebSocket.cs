using System;
using System.Collections.Generic;
using System.Net;
using System.Security.Authentication;
using System.Threading;
using MailRy.Net;
using NoiseMe.Drags.App.Models.SuperSocket.ClientEngine;
using NoiseMe.Drags.App.Models.WebSocket4Net.Command;
using NoiseMe.Drags.App.Models.WebSocket4Net.Common;
using NoiseMe.Drags.App.Models.WebSocket4Net.Protocol;

namespace NoiseMe.Drags.App.Models.WebSocket4Net
{
	// Token: 0x02000097 RID: 151
	public class WebSocket : IDisposable
	{
		// Token: 0x1700014D RID: 333
		// (get) Token: 0x060004F0 RID: 1264 RVA: 0x00005120 File Offset: 0x00003320
		// (set) Token: 0x060004F1 RID: 1265 RVA: 0x00005128 File Offset: 0x00003328
		public TcpClientSession Client { get; private set; }

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x060004F2 RID: 1266 RVA: 0x00005131 File Offset: 0x00003331
		// (set) Token: 0x060004F3 RID: 1267 RVA: 0x00005139 File Offset: 0x00003339
		public WebSocketVersion Version { get; private set; }

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x060004F4 RID: 1268 RVA: 0x00005142 File Offset: 0x00003342
		// (set) Token: 0x060004F5 RID: 1269 RVA: 0x0000514A File Offset: 0x0000334A
		public DateTime LastActiveTime { get; set; }

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x060004F6 RID: 1270 RVA: 0x00005153 File Offset: 0x00003353
		// (set) Token: 0x060004F7 RID: 1271 RVA: 0x0000515B File Offset: 0x0000335B
		public bool EnableAutoSendPing { get; set; }

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x060004F8 RID: 1272 RVA: 0x00005164 File Offset: 0x00003364
		// (set) Token: 0x060004F9 RID: 1273 RVA: 0x0000516C File Offset: 0x0000336C
		public int AutoSendPingInterval { get; set; }

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x060004FA RID: 1274 RVA: 0x00005175 File Offset: 0x00003375
		// (set) Token: 0x060004FB RID: 1275 RVA: 0x0000517D File Offset: 0x0000337D
		public IProtocolProcessor ProtocolProcessor { get; private set; }

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x060004FC RID: 1276 RVA: 0x00005186 File Offset: 0x00003386
		public bool SupportBinary
		{
			get
			{
				return this.ProtocolProcessor.SupportBinary;
			}
		}

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x060004FD RID: 1277 RVA: 0x00005193 File Offset: 0x00003393
		// (set) Token: 0x060004FE RID: 1278 RVA: 0x0000519B File Offset: 0x0000339B
		public Uri TargetUri { get; private set; }

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x060004FF RID: 1279 RVA: 0x000051A4 File Offset: 0x000033A4
		// (set) Token: 0x06000500 RID: 1280 RVA: 0x000051AC File Offset: 0x000033AC
		public string SubProtocol { get; private set; }

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x06000501 RID: 1281 RVA: 0x000051B5 File Offset: 0x000033B5
		// (set) Token: 0x06000502 RID: 1282 RVA: 0x000051BD File Offset: 0x000033BD
		public IDictionary<string, object> Items { get; private set; }

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x06000503 RID: 1283 RVA: 0x000051C6 File Offset: 0x000033C6
		// (set) Token: 0x06000504 RID: 1284 RVA: 0x000051CE File Offset: 0x000033CE
		public List<KeyValuePair<string, string>> Cookies { get; private set; }

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x06000505 RID: 1285 RVA: 0x000051D7 File Offset: 0x000033D7
		// (set) Token: 0x06000506 RID: 1286 RVA: 0x000051DF File Offset: 0x000033DF
		public List<KeyValuePair<string, string>> CustomHeaderItems { get; private set; }

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x06000507 RID: 1287 RVA: 0x000051E8 File Offset: 0x000033E8
		public int StateCode
		{
			get
			{
				return this.m_StateCode;
			}
		}

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x06000508 RID: 1288 RVA: 0x000051E8 File Offset: 0x000033E8
		public WebSocketState State
		{
			get
			{
				return (WebSocketState)this.m_StateCode;
			}
		}

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x06000509 RID: 1289 RVA: 0x000051F0 File Offset: 0x000033F0
		// (set) Token: 0x0600050A RID: 1290 RVA: 0x000051F8 File Offset: 0x000033F8
		public bool Handshaked { get; private set; }

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x0600050B RID: 1291 RVA: 0x00005201 File Offset: 0x00003401
		// (set) Token: 0x0600050C RID: 1292 RVA: 0x00005209 File Offset: 0x00003409
		public IProxyConnector Proxy { get; set; }

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x0600050D RID: 1293 RVA: 0x00005212 File Offset: 0x00003412
		public EndPoint HttpConnectProxy
		{
			get
			{
				return this.m_HttpConnectProxy;
			}
		}

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x0600050E RID: 1294 RVA: 0x0000521A File Offset: 0x0000341A
		// (set) Token: 0x0600050F RID: 1295 RVA: 0x00005222 File Offset: 0x00003422
		private protected IClientCommandReader<WebSocketCommandInfo> CommandReader { protected get; private set; }

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x06000510 RID: 1296 RVA: 0x0000522B File Offset: 0x0000342B
		// (set) Token: 0x06000511 RID: 1297 RVA: 0x00005233 File Offset: 0x00003433
		public bool NotSpecifiedVersion { get; private set; }

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x06000512 RID: 1298 RVA: 0x0000523C File Offset: 0x0000343C
		// (set) Token: 0x06000513 RID: 1299 RVA: 0x00005244 File Offset: 0x00003444
		public string LastPongResponse { get; set; }

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x06000514 RID: 1300 RVA: 0x0000524D File Offset: 0x0000344D
		// (set) Token: 0x06000515 RID: 1301 RVA: 0x00005255 File Offset: 0x00003455
		public string HandshakeHost { get; private set; }

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x06000516 RID: 1302 RVA: 0x0000525E File Offset: 0x0000345E
		// (set) Token: 0x06000517 RID: 1303 RVA: 0x00005266 File Offset: 0x00003466
		public string Origin { get; private set; }

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x06000518 RID: 1304 RVA: 0x0000526F File Offset: 0x0000346F
		// (set) Token: 0x06000519 RID: 1305 RVA: 0x00005277 File Offset: 0x00003477
		public bool NoDelay { get; set; }

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x0600051A RID: 1306 RVA: 0x00005280 File Offset: 0x00003480
		// (set) Token: 0x0600051B RID: 1307 RVA: 0x00005297 File Offset: 0x00003497
		public EndPoint LocalEndPoint
		{
			get
			{
				if (this.Client == null)
				{
					return null;
				}
				return this.Client.LocalEndPoint;
			}
			set
			{
				if (this.Client == null)
				{
					throw new Exception("Websocket client is not initilized.");
				}
				this.Client.LocalEndPoint = value;
			}
		}

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x0600051C RID: 1308 RVA: 0x00017AA0 File Offset: 0x00015CA0
		public SecurityOption Security
		{
			get
			{
				if (this.m_Security != null)
				{
					return this.m_Security;
				}
				SslStreamTcpSession sslStreamTcpSession = this.Client as SslStreamTcpSession;
				if (sslStreamTcpSession == null)
				{
					return this.m_Security = new SecurityOption();
				}
				return this.m_Security = sslStreamTcpSession.Security;
			}
		}

		// Token: 0x0600051E RID: 1310 RVA: 0x00017AEC File Offset: 0x00015CEC
		private EndPoint ResolveUri(string uri, int defaultPort, out int port)
		{
			this.TargetUri = new Uri(uri);
			if (string.IsNullOrEmpty(this.Origin))
			{
				this.Origin = this.TargetUri.GetOrigin();
			}
			port = this.TargetUri.Port;
			if (port <= 0)
			{
				port = defaultPort;
			}
			IPAddress address;
			EndPoint result;
			if (IPAddress.TryParse(this.TargetUri.Host, out address))
			{
				result = new IPEndPoint(address, port);
			}
			else
			{
				result = new DnsEndPoint(this.TargetUri.Host, port);
			}
			return result;
		}

		// Token: 0x0600051F RID: 1311 RVA: 0x00017B6C File Offset: 0x00015D6C
		private TcpClientSession CreateClient(string uri)
		{
			int num;
			this.m_RemoteEndPoint = this.ResolveUri(uri, 80, out num);
			if (num == 80)
			{
				this.HandshakeHost = this.TargetUri.Host;
			}
			else
			{
				this.HandshakeHost = this.TargetUri.Host + ":" + num;
			}
			return new AsyncTcpSession();
		}

		// Token: 0x06000520 RID: 1312 RVA: 0x00017BC8 File Offset: 0x00015DC8
		private TcpClientSession CreateSecureClient(string uri)
		{
			int num = uri.IndexOf('/', "wss://".Length);
			if (num < 0)
			{
				num = uri.IndexOf(':', "wss://".Length, uri.Length - "wss://".Length);
				if (num < 0)
				{
					uri = string.Concat(new object[]
					{
						uri,
						":",
						443,
						"/"
					});
				}
				else
				{
					uri += "/";
				}
			}
			else
			{
				if (num == "wss://".Length)
				{
					throw new ArgumentException("Invalid uri", "uri");
				}
				if (uri.IndexOf(':', "wss://".Length, num - "wss://".Length) < 0)
				{
					uri = string.Concat(new object[]
					{
						uri.Substring(0, num),
						":",
						443,
						uri.Substring(num)
					});
				}
			}
			int num2;
			this.m_RemoteEndPoint = this.ResolveUri(uri, 443, out num2);
			if (this.m_HttpConnectProxy != null)
			{
				this.m_RemoteEndPoint = this.m_HttpConnectProxy;
			}
			if (num2 == 443)
			{
				this.HandshakeHost = this.TargetUri.Host;
			}
			else
			{
				this.HandshakeHost = this.TargetUri.Host + ":" + num2;
			}
			return this.CreateSecureTcpSession();
		}

		// Token: 0x06000521 RID: 1313 RVA: 0x00017D38 File Offset: 0x00015F38
		private void Initialize(string uri, string subProtocol, List<KeyValuePair<string, string>> cookies, List<KeyValuePair<string, string>> customHeaderItems, string userAgent, string origin, WebSocketVersion version, EndPoint httpConnectProxy, int receiveBufferSize)
		{
			if (version == WebSocketVersion.None)
			{
				this.NotSpecifiedVersion = true;
				version = WebSocketVersion.Rfc6455;
			}
			this.Version = version;
			this.ProtocolProcessor = WebSocket.GetProtocolProcessor(version);
			this.Cookies = cookies;
			this.Origin = origin;
			if (!string.IsNullOrEmpty(userAgent))
			{
				if (customHeaderItems == null)
				{
					customHeaderItems = new List<KeyValuePair<string, string>>();
				}
				customHeaderItems.Add(new KeyValuePair<string, string>("User-Agent", userAgent));
			}
			if (customHeaderItems != null && customHeaderItems.Count > 0)
			{
				this.CustomHeaderItems = customHeaderItems;
			}
			Handshake handshake = new Handshake();
			this.m_CommandDict.Add(handshake.Name, handshake);
			Text text = new Text();
			this.m_CommandDict.Add(text.Name, text);
			Binary binary = new Binary();
			this.m_CommandDict.Add(binary.Name, binary);
			Close close = new Close();
			this.m_CommandDict.Add(close.Name, close);
			Ping ping = new Ping();
			this.m_CommandDict.Add(ping.Name, ping);
			Pong pong = new Pong();
			this.m_CommandDict.Add(pong.Name, pong);
			BadRequest badRequest = new BadRequest();
			this.m_CommandDict.Add(badRequest.Name, badRequest);
			this.m_StateCode = -1;
			this.SubProtocol = subProtocol;
			this.Items = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
			this.m_HttpConnectProxy = httpConnectProxy;
			TcpClientSession tcpClientSession;
			if (uri.StartsWith("ws://", StringComparison.OrdinalIgnoreCase))
			{
				tcpClientSession = this.CreateClient(uri);
			}
			else
			{
				if (!uri.StartsWith("wss://", StringComparison.OrdinalIgnoreCase))
				{
					throw new ArgumentException("Invalid uri", "uri");
				}
				tcpClientSession = this.CreateSecureClient(uri);
			}
			tcpClientSession.ReceiveBufferSize = ((receiveBufferSize > 0) ? receiveBufferSize : 4096);
			tcpClientSession.Connected += this.client_Connected;
			tcpClientSession.Closed += this.client_Closed;
			tcpClientSession.Error += this.client_Error;
			tcpClientSession.DataReceived += this.client_DataReceived;
			this.Client = tcpClientSession;
			this.EnableAutoSendPing = true;
		}

		// Token: 0x06000522 RID: 1314 RVA: 0x000052E2 File Offset: 0x000034E2
		private void client_DataReceived(object sender, DataEventArgs e)
		{
			this.OnDataReceived(e.Data, e.Offset, e.Length);
		}

		// Token: 0x06000523 RID: 1315 RVA: 0x000052FC File Offset: 0x000034FC
		private void client_Error(object sender, ErrorEventArgs e)
		{
			this.OnError(e);
			this.OnClosed();
		}

		// Token: 0x06000524 RID: 1316 RVA: 0x0000530B File Offset: 0x0000350B
		private void client_Closed(object sender, EventArgs e)
		{
			this.OnClosed();
		}

		// Token: 0x06000525 RID: 1317 RVA: 0x00005313 File Offset: 0x00003513
		private void client_Connected(object sender, EventArgs e)
		{
			this.OnConnected();
		}

		// Token: 0x06000526 RID: 1318 RVA: 0x00017F40 File Offset: 0x00016140
		public bool GetAvailableProcessor(int[] availableVersions)
		{
			IProtocolProcessor preferedProcessorFromAvialable = WebSocket.m_ProtocolProcessorFactory.GetPreferedProcessorFromAvialable(availableVersions);
			if (preferedProcessorFromAvialable == null)
			{
				return false;
			}
			this.ProtocolProcessor = preferedProcessorFromAvialable;
			return true;
		}

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x06000527 RID: 1319 RVA: 0x0000531B File Offset: 0x0000351B
		// (set) Token: 0x06000528 RID: 1320 RVA: 0x00005328 File Offset: 0x00003528
		public int ReceiveBufferSize
		{
			get
			{
				return this.Client.ReceiveBufferSize;
			}
			set
			{
				this.Client.ReceiveBufferSize = value;
			}
		}

		// Token: 0x06000529 RID: 1321 RVA: 0x00017F68 File Offset: 0x00016168
		public void Open()
		{
			this.m_StateCode = 0;
			if (this.Proxy != null)
			{
				this.Client.Proxy = this.Proxy;
			}
			this.Client.NoDelay = this.NoDelay;
			this.Client.Connect(this.m_RemoteEndPoint);
		}

		// Token: 0x0600052A RID: 1322 RVA: 0x00005336 File Offset: 0x00003536
		private static IProtocolProcessor GetProtocolProcessor(WebSocketVersion version)
		{
			IProtocolProcessor processorByVersion = WebSocket.m_ProtocolProcessorFactory.GetProcessorByVersion(version);
			if (processorByVersion == null)
			{
				throw new ArgumentException("Invalid websocket version");
			}
			return processorByVersion;
		}

		// Token: 0x0600052B RID: 1323 RVA: 0x00005351 File Offset: 0x00003551
		private void OnConnected()
		{
			this.CommandReader = this.ProtocolProcessor.CreateHandshakeReader(this);
			if (this.Items.Count > 0)
			{
				this.Items.Clear();
			}
			this.ProtocolProcessor.SendHandshake(this);
		}

		// Token: 0x0600052C RID: 1324 RVA: 0x00017FB8 File Offset: 0x000161B8
		public virtual void OnHandshaked()
		{
			this.m_StateCode = 1;
			this.Handshaked = true;
			if (this.EnableAutoSendPing && this.ProtocolProcessor.SupportPingPong)
			{
				if (this.AutoSendPingInterval <= 0)
				{
					this.AutoSendPingInterval = 60;
				}
				this.m_WebSocketTimer = new Timer(new TimerCallback(this.OnPingTimerCallback), this.ProtocolProcessor, this.AutoSendPingInterval * 1000, this.AutoSendPingInterval * 1000);
			}
			EventHandler opened = this.m_Opened;
			if (opened != null)
			{
				opened(this, EventArgs.Empty);
			}
		}

		// Token: 0x0600052D RID: 1325 RVA: 0x00018044 File Offset: 0x00016244
		private void OnPingTimerCallback(object state)
		{
			IProtocolProcessor protocolProcessor = state as IProtocolProcessor;
			if (!string.IsNullOrEmpty(this.m_LastPingRequest) && !this.m_LastPingRequest.Equals(this.LastPongResponse))
			{
				try
				{
					protocolProcessor.SendPong(this, "");
				}
				catch (Exception e)
				{
					this.OnError(e);
					return;
				}
			}
			this.m_LastPingRequest = DateTime.Now.ToString();
			try
			{
				protocolProcessor.SendPing(this, this.m_LastPingRequest);
			}
			catch (Exception e2)
			{
				this.OnError(e2);
			}
		}

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x0600052E RID: 1326 RVA: 0x0000538A File Offset: 0x0000358A
		// (remove) Token: 0x0600052F RID: 1327 RVA: 0x000053A3 File Offset: 0x000035A3
		public event EventHandler Opened
		{
			add
			{
				this.m_Opened = (EventHandler)Delegate.Combine(this.m_Opened, value);
			}
			remove
			{
				this.m_Opened = (EventHandler)Delegate.Remove(this.m_Opened, value);
			}
		}

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x06000530 RID: 1328 RVA: 0x000053BC File Offset: 0x000035BC
		// (remove) Token: 0x06000531 RID: 1329 RVA: 0x000053D5 File Offset: 0x000035D5
		public event EventHandler<MessageReceivedEventArgs> MessageReceived
		{
			add
			{
				this.m_MessageReceived = (EventHandler<MessageReceivedEventArgs>)Delegate.Combine(this.m_MessageReceived, value);
			}
			remove
			{
				this.m_MessageReceived = (EventHandler<MessageReceivedEventArgs>)Delegate.Remove(this.m_MessageReceived, value);
			}
		}

		// Token: 0x06000532 RID: 1330 RVA: 0x000053EE File Offset: 0x000035EE
		public void FireMessageReceived(string message)
		{
			if (this.m_MessageReceived == null)
			{
				return;
			}
			this.m_MessageReceived(this, new MessageReceivedEventArgs(message));
		}

		// Token: 0x14000006 RID: 6
		// (add) Token: 0x06000533 RID: 1331 RVA: 0x0000540B File Offset: 0x0000360B
		// (remove) Token: 0x06000534 RID: 1332 RVA: 0x00005424 File Offset: 0x00003624
		public event EventHandler<DataReceivedEventArgs> DataReceived
		{
			add
			{
				this.m_DataReceived = (EventHandler<DataReceivedEventArgs>)Delegate.Combine(this.m_DataReceived, value);
			}
			remove
			{
				this.m_DataReceived = (EventHandler<DataReceivedEventArgs>)Delegate.Remove(this.m_DataReceived, value);
			}
		}

		// Token: 0x06000535 RID: 1333 RVA: 0x0000543D File Offset: 0x0000363D
		public void FireDataReceived(byte[] data)
		{
			if (this.m_DataReceived == null)
			{
				return;
			}
			this.m_DataReceived(this, new DataReceivedEventArgs(data));
		}

		// Token: 0x06000536 RID: 1334 RVA: 0x0000545A File Offset: 0x0000365A
		private bool EnsureWebSocketOpen()
		{
			if (!this.Handshaked)
			{
				this.OnError(new Exception("You must send data by websocket after websocket is opened!"));
				return false;
			}
			return true;
		}

		// Token: 0x06000537 RID: 1335 RVA: 0x00005477 File Offset: 0x00003677
		public void Send(string message)
		{
			if (!this.EnsureWebSocketOpen())
			{
				return;
			}
			this.ProtocolProcessor.SendMessage(this, message);
		}

		// Token: 0x06000538 RID: 1336 RVA: 0x0000548F File Offset: 0x0000368F
		public void Send(byte[] data, int offset, int length)
		{
			if (!this.EnsureWebSocketOpen())
			{
				return;
			}
			this.ProtocolProcessor.SendData(this, data, offset, length);
		}

		// Token: 0x06000539 RID: 1337 RVA: 0x000054A9 File Offset: 0x000036A9
		public void Send(IList<ArraySegment<byte>> segments)
		{
			if (!this.EnsureWebSocketOpen())
			{
				return;
			}
			this.ProtocolProcessor.SendData(this, segments);
		}

		// Token: 0x0600053A RID: 1338 RVA: 0x000180D8 File Offset: 0x000162D8
		private void OnClosed()
		{
			bool flag = false;
			if (this.m_StateCode == 2 || this.m_StateCode == 1 || this.m_StateCode == 0)
			{
				flag = true;
			}
			this.m_StateCode = 3;
			if (flag)
			{
				this.FireClosed();
			}
		}

		// Token: 0x0600053B RID: 1339 RVA: 0x000054C1 File Offset: 0x000036C1
		public void Close()
		{
			this.Close(string.Empty);
		}

		// Token: 0x0600053C RID: 1340 RVA: 0x000054CE File Offset: 0x000036CE
		public void Close(string reason)
		{
			this.Close((int)this.ProtocolProcessor.CloseStatusCode.NormalClosure, reason);
		}

		// Token: 0x0600053D RID: 1341 RVA: 0x00018114 File Offset: 0x00016314
		public void Close(int statusCode, string reason)
		{
			this.m_ClosedArgs = new ClosedEventArgs((short)statusCode, reason);
			if (Interlocked.CompareExchange(ref this.m_StateCode, 3, -1) == -1)
			{
				this.OnClosed();
				return;
			}
			if (Interlocked.CompareExchange(ref this.m_StateCode, 2, 0) != 0)
			{
				this.m_StateCode = 2;
				this.ClearTimer();
				this.m_WebSocketTimer = new Timer(new TimerCallback(this.CheckCloseHandshake), null, 5000, -1);
				try
				{
					this.ProtocolProcessor.SendCloseHandshake(this, statusCode, reason);
				}
				catch (Exception e)
				{
					if (this.Client != null)
					{
						this.OnError(e);
					}
				}
				return;
			}
			TcpClientSession client = this.Client;
			if (client != null && client.IsConnected)
			{
				client.Close();
				return;
			}
			this.OnClosed();
		}

		// Token: 0x0600053E RID: 1342 RVA: 0x000181D4 File Offset: 0x000163D4
		private void CheckCloseHandshake(object state)
		{
			if (this.m_StateCode == 3)
			{
				return;
			}
			try
			{
				this.CloseWithoutHandshake();
			}
			catch (Exception e)
			{
				this.OnError(e);
			}
		}

		// Token: 0x0600053F RID: 1343 RVA: 0x00018210 File Offset: 0x00016410
		public void CloseWithoutHandshake()
		{
			TcpClientSession client = this.Client;
			if (client != null)
			{
				client.Close();
			}
		}

		// Token: 0x06000540 RID: 1344 RVA: 0x00018230 File Offset: 0x00016430
		protected void ExecuteCommand(WebSocketCommandInfo commandInfo)
		{
			ICommand<WebSocket, WebSocketCommandInfo> command;
			if (this.m_CommandDict.TryGetValue(commandInfo.Key, out command))
			{
				command.ExecuteCommand(this, commandInfo);
			}
		}

		// Token: 0x06000541 RID: 1345 RVA: 0x0001825C File Offset: 0x0001645C
		private void OnDataReceived(byte[] data, int offset, int length)
		{
			for (;;)
			{
				int num;
				WebSocketCommandInfo commandInfo = this.CommandReader.GetCommandInfo(data, offset, length, out num);
				if (this.CommandReader.NextCommandReader != null)
				{
					this.CommandReader = this.CommandReader.NextCommandReader;
				}
				if (commandInfo != null)
				{
					this.ExecuteCommand(commandInfo);
				}
				if (num <= 0)
				{
					break;
				}
				offset = offset + length - num;
				length = num;
			}
		}

		// Token: 0x06000542 RID: 1346 RVA: 0x000054E7 File Offset: 0x000036E7
		public void FireError(Exception error)
		{
			this.OnError(error);
		}

		// Token: 0x14000007 RID: 7
		// (add) Token: 0x06000543 RID: 1347 RVA: 0x000054F0 File Offset: 0x000036F0
		// (remove) Token: 0x06000544 RID: 1348 RVA: 0x00005509 File Offset: 0x00003709
		public event EventHandler Closed
		{
			add
			{
				this.m_Closed = (EventHandler)Delegate.Combine(this.m_Closed, value);
			}
			remove
			{
				this.m_Closed = (EventHandler)Delegate.Remove(this.m_Closed, value);
			}
		}

		// Token: 0x06000545 RID: 1349 RVA: 0x000182B4 File Offset: 0x000164B4
		private void ClearTimer()
		{
			Timer webSocketTimer = this.m_WebSocketTimer;
			if (webSocketTimer == null)
			{
				return;
			}
			lock (this)
			{
				if (this.m_WebSocketTimer != null)
				{
					webSocketTimer.Change(-1, -1);
					webSocketTimer.Dispose();
					this.m_WebSocketTimer = null;
				}
			}
		}

		// Token: 0x06000546 RID: 1350 RVA: 0x00018310 File Offset: 0x00016510
		private void FireClosed()
		{
			this.ClearTimer();
			EventHandler closed = this.m_Closed;
			if (closed != null)
			{
				closed(this, this.m_ClosedArgs ?? EventArgs.Empty);
			}
		}

		// Token: 0x14000008 RID: 8
		// (add) Token: 0x06000547 RID: 1351 RVA: 0x00005522 File Offset: 0x00003722
		// (remove) Token: 0x06000548 RID: 1352 RVA: 0x0000553B File Offset: 0x0000373B
		public event EventHandler<ErrorEventArgs> Error
		{
			add
			{
				this.m_Error = (EventHandler<ErrorEventArgs>)Delegate.Combine(this.m_Error, value);
			}
			remove
			{
				this.m_Error = (EventHandler<ErrorEventArgs>)Delegate.Remove(this.m_Error, value);
			}
		}

		// Token: 0x06000549 RID: 1353 RVA: 0x00018344 File Offset: 0x00016544
		private void OnError(ErrorEventArgs e)
		{
			EventHandler<ErrorEventArgs> error = this.m_Error;
			if (error == null)
			{
				return;
			}
			error(this, e);
		}

		// Token: 0x0600054A RID: 1354 RVA: 0x00005554 File Offset: 0x00003754
		private void OnError(Exception e)
		{
			this.OnError(new ErrorEventArgs(e));
		}

		// Token: 0x0600054B RID: 1355 RVA: 0x00005562 File Offset: 0x00003762
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600054C RID: 1356 RVA: 0x00018364 File Offset: 0x00016564
		protected virtual void Dispose(bool disposing)
		{
			if (this.m_Disposed)
			{
				return;
			}
			if (disposing)
			{
				TcpClientSession client = this.Client;
				if (client != null)
				{
					client.Connected -= this.client_Connected;
					client.Closed -= this.client_Closed;
					client.Error -= this.client_Error;
					client.DataReceived -= this.client_DataReceived;
					if (client.IsConnected)
					{
						client.Close();
					}
					this.Client = null;
				}
				this.ClearTimer();
			}
			this.m_Disposed = true;
		}

		// Token: 0x0600054D RID: 1357 RVA: 0x000183F4 File Offset: 0x000165F4
		~WebSocket()
		{
			this.Dispose(false);
		}

		// Token: 0x0600054E RID: 1358 RVA: 0x00005571 File Offset: 0x00003771
		public WebSocket(string uri, string subProtocol, WebSocketVersion version) : this(uri, subProtocol, WebSocket.EmptyCookies, null, string.Empty, string.Empty, version)
		{
		}

		// Token: 0x0600054F RID: 1359 RVA: 0x00018424 File Offset: 0x00016624
		public WebSocket(string uri, string subProtocol = "", List<KeyValuePair<string, string>> cookies = null, List<KeyValuePair<string, string>> customHeaderItems = null, string userAgent = "", string origin = "", WebSocketVersion version = WebSocketVersion.None, EndPoint httpConnectProxy = null, SslProtocols sslProtocols = SslProtocols.None, int receiveBufferSize = 0)
		{
			this.m_CommandDict = new Dictionary<string, ICommand<WebSocket, WebSocketCommandInfo>>(StringComparer.OrdinalIgnoreCase);
			this.m_SecureProtocols = SslProtocols.Default;
			base..ctor();
			if (sslProtocols != SslProtocols.None)
			{
				this.m_SecureProtocols = sslProtocols;
			}
			this.Initialize(uri, subProtocol, cookies, customHeaderItems, userAgent, origin, version, httpConnectProxy, receiveBufferSize);
		}

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x06000550 RID: 1360 RVA: 0x0000558C File Offset: 0x0000378C
		// (set) Token: 0x06000551 RID: 1361 RVA: 0x00018474 File Offset: 0x00016674
		public bool AllowUnstrustedCertificate
		{
			get
			{
				return this.m_AllowUnstrustedCertificate;
			}
			set
			{
				this.m_AllowUnstrustedCertificate = value;
				SslStreamTcpSession sslStreamTcpSession = this.Client as SslStreamTcpSession;
				if (sslStreamTcpSession != null)
				{
					sslStreamTcpSession.Security.AllowUnstrustedCertificate = this.m_AllowUnstrustedCertificate;
				}
			}
		}

		// Token: 0x06000552 RID: 1362 RVA: 0x00005594 File Offset: 0x00003794
		public WebSocket(string uri) : this(uri, string.Empty)
		{
		}

		// Token: 0x06000553 RID: 1363 RVA: 0x000055A2 File Offset: 0x000037A2
		public WebSocket(string uri, WebSocketVersion version) : this(uri, string.Empty, null, version)
		{
		}

		// Token: 0x06000554 RID: 1364 RVA: 0x000055B2 File Offset: 0x000037B2
		public WebSocket(string uri, string subProtocol) : this(uri, subProtocol, null, WebSocketVersion.None)
		{
		}

		// Token: 0x06000555 RID: 1365 RVA: 0x000055BE File Offset: 0x000037BE
		public WebSocket(string uri, List<KeyValuePair<string, string>> cookies) : this(uri, string.Empty, cookies, WebSocketVersion.None)
		{
		}

		// Token: 0x06000556 RID: 1366 RVA: 0x000055CE File Offset: 0x000037CE
		public WebSocket(string uri, string subProtocol, List<KeyValuePair<string, string>> cookies) : this(uri, subProtocol, cookies, WebSocketVersion.None)
		{
		}

		// Token: 0x06000557 RID: 1367 RVA: 0x000055DA File Offset: 0x000037DA
		public WebSocket(string uri, string subProtocol, List<KeyValuePair<string, string>> cookies, WebSocketVersion version) : this(uri, subProtocol, cookies, new List<KeyValuePair<string, string>>(), null, version)
		{
		}

		// Token: 0x06000558 RID: 1368 RVA: 0x000055ED File Offset: 0x000037ED
		public WebSocket(string uri, string subProtocol, List<KeyValuePair<string, string>> cookies, string userAgent, WebSocketVersion version) : this(uri, subProtocol, cookies, null, userAgent, version)
		{
		}

		// Token: 0x06000559 RID: 1369 RVA: 0x000055FD File Offset: 0x000037FD
		public WebSocket(string uri, string subProtocol, List<KeyValuePair<string, string>> cookies, List<KeyValuePair<string, string>> customHeaderItems, string userAgent, WebSocketVersion version) : this(uri, subProtocol, cookies, customHeaderItems, userAgent, string.Empty, version)
		{
		}

		// Token: 0x0600055A RID: 1370 RVA: 0x000184A8 File Offset: 0x000166A8
		public WebSocket(string uri, string subProtocol, List<KeyValuePair<string, string>> cookies, List<KeyValuePair<string, string>> customHeaderItems, string userAgent, string origin, WebSocketVersion version) : this(uri, subProtocol, cookies, customHeaderItems, userAgent, origin, version, null)
		{
		}

		// Token: 0x0600055B RID: 1371 RVA: 0x000184C8 File Offset: 0x000166C8
		public WebSocket(string uri, string subProtocol, List<KeyValuePair<string, string>> cookies, List<KeyValuePair<string, string>> customHeaderItems, string userAgent, string origin, WebSocketVersion version, EndPoint httpConnectProxy)
		{
			this.m_CommandDict = new Dictionary<string, ICommand<WebSocket, WebSocketCommandInfo>>(StringComparer.OrdinalIgnoreCase);
			this.m_SecureProtocols = SslProtocols.Default;
			base..ctor();
			this.Initialize(uri, subProtocol, cookies, customHeaderItems, userAgent, origin, version, httpConnectProxy, 0);
		}

		// Token: 0x0600055C RID: 1372 RVA: 0x0001850C File Offset: 0x0001670C
		public WebSocket(string uri, string subProtocol, List<KeyValuePair<string, string>> cookies, List<KeyValuePair<string, string>> customHeaderItems, string userAgent, string origin, WebSocketVersion version, EndPoint httpConnectProxy, SslProtocols sslProtocols)
		{
			this.m_CommandDict = new Dictionary<string, ICommand<WebSocket, WebSocketCommandInfo>>(StringComparer.OrdinalIgnoreCase);
			this.m_SecureProtocols = SslProtocols.Default;
			base..ctor();
			this.m_SecureProtocols = sslProtocols;
			this.Initialize(uri, subProtocol, cookies, customHeaderItems, userAgent, origin, version, httpConnectProxy, 0);
		}

		// Token: 0x0600055D RID: 1373 RVA: 0x00018558 File Offset: 0x00016758
		private TcpClientSession CreateSecureTcpSession()
		{
			SslStreamTcpSession sslStreamTcpSession = new SslStreamTcpSession();
			(sslStreamTcpSession.Security = new SecurityOption()).EnabledSslProtocols = this.m_SecureProtocols;
			return sslStreamTcpSession;
		}

		// Token: 0x04000222 RID: 546
		private EndPoint m_RemoteEndPoint;

		// Token: 0x04000227 RID: 551
		protected const string UserAgentKey = "User-Agent";

		// Token: 0x0400022E RID: 558
		public const int DefaultReceiveBufferSize = 4096;

		// Token: 0x0400022F RID: 559
		private int m_StateCode;

		// Token: 0x04000232 RID: 562
		private EndPoint m_HttpConnectProxy;

		// Token: 0x04000234 RID: 564
		private Dictionary<string, ICommand<WebSocket, WebSocketCommandInfo>> m_CommandDict;

		// Token: 0x04000235 RID: 565
		private static ProtocolProcessorFactory m_ProtocolProcessorFactory = new ProtocolProcessorFactory(new IProtocolProcessor[]
		{
			new Rfc6455Processor(),
			new DraftHybi10Processor(),
			new DraftHybi00Processor()
		});

		// Token: 0x04000237 RID: 567
		private Timer m_WebSocketTimer;

		// Token: 0x04000239 RID: 569
		private string m_LastPingRequest;

		// Token: 0x0400023A RID: 570
		private const string m_UriScheme = "ws";

		// Token: 0x0400023B RID: 571
		private const string m_UriPrefix = "ws://";

		// Token: 0x0400023C RID: 572
		private const string m_SecureUriScheme = "wss";

		// Token: 0x0400023D RID: 573
		private const int m_SecurePort = 443;

		// Token: 0x0400023E RID: 574
		private const string m_SecureUriPrefix = "wss://";

		// Token: 0x04000242 RID: 578
		private SecurityOption m_Security;

		// Token: 0x04000243 RID: 579
		private bool m_Disposed;

		// Token: 0x04000244 RID: 580
		private EventHandler m_Opened;

		// Token: 0x04000245 RID: 581
		private EventHandler<MessageReceivedEventArgs> m_MessageReceived;

		// Token: 0x04000246 RID: 582
		private EventHandler<DataReceivedEventArgs> m_DataReceived;

		// Token: 0x04000247 RID: 583
		private const string m_NotOpenSendingMessage = "You must send data by websocket after websocket is opened!";

		// Token: 0x04000248 RID: 584
		private ClosedEventArgs m_ClosedArgs;

		// Token: 0x04000249 RID: 585
		private EventHandler m_Closed;

		// Token: 0x0400024A RID: 586
		private EventHandler<ErrorEventArgs> m_Error;

		// Token: 0x0400024B RID: 587
		private static List<KeyValuePair<string, string>> EmptyCookies;

		// Token: 0x0400024C RID: 588
		private bool m_AllowUnstrustedCertificate;

		// Token: 0x0400024D RID: 589
		private SslProtocols m_SecureProtocols;
	}
}
