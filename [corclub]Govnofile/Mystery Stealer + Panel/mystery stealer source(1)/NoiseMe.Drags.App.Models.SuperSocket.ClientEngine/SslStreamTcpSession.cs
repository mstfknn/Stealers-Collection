using System;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;

namespace NoiseMe.Drags.App.Models.SuperSocket.ClientEngine
{
	public class SslStreamTcpSession : AuthenticatedStreamTcpSession
	{
		protected override void StartAuthenticatedStream(Socket client)
		{
			SecurityOption security = base.Security;
			if (security == null)
			{
				throw new Exception("securityOption was not configured");
			}
			SslStream sslStream = new SslStream(new NetworkStream(client), leaveInnerStreamOpen: false, ValidateRemoteCertificate);
			sslStream.BeginAuthenticateAsClient(base.HostName, security.Certificates, security.EnabledSslProtocols, checkCertificateRevocation: false, OnAuthenticated, sslStream);
		}

		private void OnAuthenticated(IAsyncResult result)
		{
			SslStream sslStream = result.AsyncState as SslStream;
			if (sslStream == null)
			{
				EnsureSocketClosed();
				OnError(new NullReferenceException("Ssl Stream is null OnAuthenticated"));
			}
			else
			{
				try
				{
					sslStream.EndAuthenticateAsClient(result);
				}
				catch (Exception e)
				{
					EnsureSocketClosed();
					OnError(e);
					return;
				}
				OnAuthenticatedStreamConnected(sslStream);
			}
		}

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
				OnError(new Exception(sslPolicyErrors.ToString()));
				return false;
			}
			if (sslPolicyErrors != 0 && sslPolicyErrors != SslPolicyErrors.RemoteCertificateChainErrors)
			{
				OnError(new Exception(sslPolicyErrors.ToString()));
				return false;
			}
			if (chain != null && chain.ChainStatus != null)
			{
				X509ChainStatus[] chainStatus = chain.ChainStatus;
				for (int i = 0; i < chainStatus.Length; i++)
				{
					X509ChainStatus x509ChainStatus = chainStatus[i];
					if ((!(certificate.Subject == certificate.Issuer) || x509ChainStatus.Status != X509ChainStatusFlags.UntrustedRoot) && x509ChainStatus.Status != 0)
					{
						OnError(new Exception(sslPolicyErrors.ToString()));
						return false;
					}
				}
			}
			return true;
		}
	}
}
