using System;
using System.Net;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;

namespace NoiseMe.Drags.App.Models.SuperSocket.ClientEngine
{
	// Token: 0x02000119 RID: 281
	public class SecurityOption
	{
		// Token: 0x1700023C RID: 572
		// (get) Token: 0x060008A3 RID: 2211 RVA: 0x00006D1C File Offset: 0x00004F1C
		// (set) Token: 0x060008A4 RID: 2212 RVA: 0x00006D24 File Offset: 0x00004F24
		public SslProtocols EnabledSslProtocols { get; set; }

		// Token: 0x1700023D RID: 573
		// (get) Token: 0x060008A5 RID: 2213 RVA: 0x00006D2D File Offset: 0x00004F2D
		// (set) Token: 0x060008A6 RID: 2214 RVA: 0x00006D35 File Offset: 0x00004F35
		public X509CertificateCollection Certificates { get; set; }

		// Token: 0x1700023E RID: 574
		// (get) Token: 0x060008A7 RID: 2215 RVA: 0x00006D3E File Offset: 0x00004F3E
		// (set) Token: 0x060008A8 RID: 2216 RVA: 0x00006D46 File Offset: 0x00004F46
		public bool AllowUnstrustedCertificate { get; set; }

		// Token: 0x1700023F RID: 575
		// (get) Token: 0x060008A9 RID: 2217 RVA: 0x00006D4F File Offset: 0x00004F4F
		// (set) Token: 0x060008AA RID: 2218 RVA: 0x00006D57 File Offset: 0x00004F57
		public bool AllowNameMismatchCertificate { get; set; }

		// Token: 0x17000240 RID: 576
		// (get) Token: 0x060008AB RID: 2219 RVA: 0x00006D60 File Offset: 0x00004F60
		// (set) Token: 0x060008AC RID: 2220 RVA: 0x00006D68 File Offset: 0x00004F68
		public bool AllowCertificateChainErrors { get; set; }

		// Token: 0x17000241 RID: 577
		// (get) Token: 0x060008AD RID: 2221 RVA: 0x00006D71 File Offset: 0x00004F71
		// (set) Token: 0x060008AE RID: 2222 RVA: 0x00006D79 File Offset: 0x00004F79
		public NetworkCredential Credential { get; set; }

		// Token: 0x060008AF RID: 2223 RVA: 0x00006D82 File Offset: 0x00004F82
		public SecurityOption() : this(SecurityOption.GetDefaultProtocol(), new X509CertificateCollection())
		{
		}

		// Token: 0x060008B0 RID: 2224 RVA: 0x00006D94 File Offset: 0x00004F94
		public SecurityOption(SslProtocols enabledSslProtocols) : this(enabledSslProtocols, new X509CertificateCollection())
		{
		}

		// Token: 0x060008B1 RID: 2225 RVA: 0x00006DA2 File Offset: 0x00004FA2
		public SecurityOption(SslProtocols enabledSslProtocols, X509Certificate certificate) : this(enabledSslProtocols, new X509CertificateCollection(new X509Certificate[]
		{
			certificate
		}))
		{
		}

		// Token: 0x060008B2 RID: 2226 RVA: 0x00006DBA File Offset: 0x00004FBA
		public SecurityOption(SslProtocols enabledSslProtocols, X509CertificateCollection certificates)
		{
			this.EnabledSslProtocols = enabledSslProtocols;
			this.Certificates = certificates;
		}

		// Token: 0x060008B3 RID: 2227 RVA: 0x00006DD0 File Offset: 0x00004FD0
		public SecurityOption(NetworkCredential credential)
		{
			this.Credential = credential;
		}

		// Token: 0x060008B4 RID: 2228 RVA: 0x00006DDF File Offset: 0x00004FDF
		private static SslProtocols GetDefaultProtocol()
		{
			return SslProtocols.Default;
		}
	}
}
