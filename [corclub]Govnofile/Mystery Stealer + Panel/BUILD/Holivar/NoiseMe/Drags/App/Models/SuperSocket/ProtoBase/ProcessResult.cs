using System;
using System.Collections.Generic;

namespace NoiseMe.Drags.App.Models.SuperSocket.ProtoBase
{
	// Token: 0x020000F4 RID: 244
	public struct ProcessResult
	{
		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x06000754 RID: 1876 RVA: 0x00006305 File Offset: 0x00004505
		// (set) Token: 0x06000755 RID: 1877 RVA: 0x0000630D File Offset: 0x0000450D
		public ProcessState State { get; private set; }

		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x06000756 RID: 1878 RVA: 0x00006316 File Offset: 0x00004516
		// (set) Token: 0x06000757 RID: 1879 RVA: 0x0000631E File Offset: 0x0000451E
		public string Message { get; private set; }

		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x06000758 RID: 1880 RVA: 0x00006327 File Offset: 0x00004527
		// (set) Token: 0x06000759 RID: 1881 RVA: 0x0000632F File Offset: 0x0000452F
		public IList<IPackageInfo> Packages { get; private set; }

		// Token: 0x0600075A RID: 1882 RVA: 0x0001BB74 File Offset: 0x00019D74
		public static ProcessResult Create(ProcessState state)
		{
			return new ProcessResult
			{
				State = state
			};
		}

		// Token: 0x0600075B RID: 1883 RVA: 0x00006338 File Offset: 0x00004538
		public static ProcessResult Create(ProcessState state, string message)
		{
			return ProcessResult.Create(state, message, null);
		}

		// Token: 0x0600075C RID: 1884 RVA: 0x00006342 File Offset: 0x00004542
		public static ProcessResult Create(ProcessState state, IList<IPackageInfo> packages)
		{
			return ProcessResult.Create(state, string.Empty, packages);
		}

		// Token: 0x0600075D RID: 1885 RVA: 0x0001BB94 File Offset: 0x00019D94
		public static ProcessResult Create(ProcessState state, string message, IList<IPackageInfo> packages)
		{
			return new ProcessResult
			{
				State = state,
				Message = message,
				Packages = packages
			};
		}
	}
}
