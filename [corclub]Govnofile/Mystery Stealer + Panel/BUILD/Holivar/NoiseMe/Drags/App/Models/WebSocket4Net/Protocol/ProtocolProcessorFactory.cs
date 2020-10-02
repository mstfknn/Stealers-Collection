using System;
using NoiseMe.Drags.App.DTO.Linq;

namespace NoiseMe.Drags.App.Models.WebSocket4Net.Protocol
{
	// Token: 0x020000AB RID: 171
	internal class ProtocolProcessorFactory
	{
		// Token: 0x06000603 RID: 1539 RVA: 0x00005A19 File Offset: 0x00003C19
		public ProtocolProcessorFactory(params IProtocolProcessor[] processors)
		{
			this.m_OrderedProcessors = (from p in processors
			orderby (int)p.Version descending
			select p).ToArray<IProtocolProcessor>();
		}

		// Token: 0x06000604 RID: 1540 RVA: 0x00019D20 File Offset: 0x00017F20
		public IProtocolProcessor GetProcessorByVersion(WebSocketVersion version)
		{
			return this.m_OrderedProcessors.FirstOrDefault((IProtocolProcessor p) => p.Version == version);
		}

		// Token: 0x06000605 RID: 1541 RVA: 0x00019D54 File Offset: 0x00017F54
		public IProtocolProcessor GetPreferedProcessorFromAvialable(int[] versions)
		{
			foreach (int num in from i in versions
			orderby i descending
			select i)
			{
				foreach (IProtocolProcessor protocolProcessor in this.m_OrderedProcessors)
				{
					int version = (int)protocolProcessor.Version;
					if (version < num)
					{
						break;
					}
					if (version <= num)
					{
						return protocolProcessor;
					}
				}
			}
			return null;
		}

		// Token: 0x040002A4 RID: 676
		private IProtocolProcessor[] m_OrderedProcessors;
	}
}
