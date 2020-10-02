using System;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;

namespace NoiseMe.Drags.App.Models.SuperSocket.ClientEngine
{
	// Token: 0x0200011A RID: 282
	public class SslStreamTcpSession : AuthenticatedStreamTcpSession
	{
		// Token: 0x060008B5 RID: 2229 RVA: 0x0001DA6C File Offset: 0x0001BC6C
		protected override void StartAuthenticatedStream(Socket client)
		{
			SecurityOption security = base.Security;
			if (security == null)
			{
				throw new Exception("securityOption was not configured");
			}
			SslStream sslStream = new SslStream(new NetworkStream(client), false, new RemoteCertificateValidationCallback(this.ValidateRemoteCertificate));
			sslStream.BeginAuthenticateAsClient(base.HostName, security.Certificates, security.EnabledSslProtocols, false, new AsyncCallback(this.OnAuthenticated), sslStream);
		}

		// Token: 0x060008B6 RID: 2230 RVA: 0x0001DAD0 File Offset: 0x0001BCD0
		private void OnAuthenticated(IAsyncResult result)
		{
			SslStream sslStream = result.AsyncState as SslStream;
			if (sslStream == null)
			{
				base.EnsureSocketClosed();
				this.OnError(new NullReferenceException("Ssl Stream is null OnAuthenticated"));
				return;
			}
			try
			{
				sslStream.EndAuthenticateAsClient(result);
			}
			catch (Exception e)
			{
				base.EnsureSocketClosed();
				this.OnError(e);
				return;
			}
			base.OnAuthenticatedStreamConnected(sslStream);
		}

		// Token: 0x060008B7 RID: 2231 RVA: 0x0001DB38 File Offset: 0x0001BD38
		private bool ValidateRemoteCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
		{
			RemoteCertificateValidationCallback serverCertificateValidationCallback = ServicePointManager.ServerCertificateValidationCallback;
			if (serverCertificateValidationCallback != null)
			{
				return serverCertificateValidationCallback(sender, certificate, chain, sslPolicyErrors);
			}
			if (sslPolicyErrors == SslPolicyErrors.None)
			{
				return true;
			}
			if (base.Security.AllowNameMismatchCertificate)
			{
				sslPolicyErrors &= ~SslPolicyErrors.RemoteCertificateNameMismatch;
			}
			if (base.Security.AllowCertificateChainErrors)
			{
				sslPolicyErrors &= ~SslPolicyErrors.RemoteCertificateChainErrors;
			}
			if (sslPolicyErrors == SslPolicyErrors.None)
			{
				return true;
			}
			if (!base.Security.AllowUnstrustedCertificate)
			{
				this.OnError(new Exception(sslPolicyErrors.ToString()));
				return false;
			}
			if (sslPolicyErrors != SslPolicyErrors.None && sslPolicyErrors != SslPolicyErrors.RemoteCertificateChainErrors)
			{
				this.OnError(new Exception(sslPolicyErrors.ToString()));
				return false;
			}
			if (chain != null && chain.ChainStatus != null)
			{
				foreach (X509ChainStatus x509ChainStatus in chain.ChainStatus)
				{
					if ((!(certificate.Subject == certificate.Issuer) || x509ChainStatus.Status != X509ChainStatusFlags.UntrustedRoot) && x509ChainStatus.Status != X509ChainStatusFlags.NoError)
					{
						this.OnError(new Exception(sslPolicyErrors.ToString()));
						return false;
					}
				}
			}
			return true;
		}
	}
}
