using System;
using System.Threading;

namespace NoiseMe.Drags.App.Models
{
	// Token: 0x02000081 RID: 129
	public class AsyncTask
	{
		// Token: 0x06000460 RID: 1120 RVA: 0x0001655C File Offset: 0x0001475C
		public static TaskAction<R> StartNew<R>(TaskAction<R> function)
		{
			R retv = default(R);
			bool completed = false;
			object sync = new object();
			function.BeginInvoke(delegate(IAsyncResult iAsyncResult)
			{
				object sync = sync;
				lock (sync)
				{
					completed = true;
					retv = function.EndInvoke(iAsyncResult);
					Monitor.Pulse(sync);
				}
			}, null);
			return delegate()
			{
				object sync = sync;
				R retv;
				lock (sync)
				{
					if (!completed)
					{
						Monitor.Wait(sync);
					}
					retv = retv;
				}
				return retv;
			};
		}

		// Token: 0x06000461 RID: 1121 RVA: 0x000165BC File Offset: 0x000147BC
		public static AsyncAction StartNew(AsyncAction function)
		{
			bool completed = false;
			object sync = new object();
			function.BeginInvoke(delegate(IAsyncResult iAsyncResult)
			{
				object sync = sync;
				lock (sync)
				{
					completed = true;
					function.EndInvoke(iAsyncResult);
					Monitor.Pulse(sync);
				}
			}, null);
			return delegate()
			{
				object sync = sync;
				lock (sync)
				{
					if (!completed)
					{
						Monitor.Wait(sync);
					}
				}
			};
		}
	}
}
