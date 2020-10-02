using System;

namespace NoiseMe.Drags.App.Models.SuperSocket.ProtoBase
{
	// Token: 0x02000100 RID: 256
	public class StringPackageInfo : IPackageInfo<string>, IPackageInfo
	{
		// Token: 0x17000206 RID: 518
		// (get) Token: 0x060007B9 RID: 1977 RVA: 0x00006635 File Offset: 0x00004835
		// (set) Token: 0x060007BA RID: 1978 RVA: 0x0000663D File Offset: 0x0000483D
		public string Key { get; protected set; }

		// Token: 0x17000207 RID: 519
		// (get) Token: 0x060007BB RID: 1979 RVA: 0x00006646 File Offset: 0x00004846
		// (set) Token: 0x060007BC RID: 1980 RVA: 0x0000664E File Offset: 0x0000484E
		public string Body { get; protected set; }

		// Token: 0x17000208 RID: 520
		// (get) Token: 0x060007BD RID: 1981 RVA: 0x00006657 File Offset: 0x00004857
		// (set) Token: 0x060007BE RID: 1982 RVA: 0x0000665F File Offset: 0x0000485F
		public string[] Parameters { get; protected set; }

		// Token: 0x060007BF RID: 1983 RVA: 0x000022E5 File Offset: 0x000004E5
		protected StringPackageInfo()
		{
		}

		// Token: 0x060007C0 RID: 1984 RVA: 0x00006668 File Offset: 0x00004868
		public StringPackageInfo(string key, string body, string[] parameters)
		{
			this.Key = key;
			this.Body = body;
			this.Parameters = parameters;
		}

		// Token: 0x060007C1 RID: 1985 RVA: 0x00006685 File Offset: 0x00004885
		public StringPackageInfo(string source, IStringParser sourceParser)
		{
			this.InitializeData(source, sourceParser);
		}

		// Token: 0x060007C2 RID: 1986 RVA: 0x0001C29C File Offset: 0x0001A49C
		protected void InitializeData(string source, IStringParser sourceParser)
		{
			string key;
			string body;
			string[] parameters;
			sourceParser.Parse(source, out key, out body, out parameters);
			this.Key = key;
			this.Body = body;
			this.Parameters = parameters;
		}

		// Token: 0x060007C3 RID: 1987 RVA: 0x00006695 File Offset: 0x00004895
		public string GetFirstParam()
		{
			if (this.Parameters.Length != 0)
			{
				return this.Parameters[0];
			}
			return string.Empty;
		}

		// Token: 0x17000209 RID: 521
		public string this[int index]
		{
			get
			{
				return this.Parameters[index];
			}
		}
	}
}
