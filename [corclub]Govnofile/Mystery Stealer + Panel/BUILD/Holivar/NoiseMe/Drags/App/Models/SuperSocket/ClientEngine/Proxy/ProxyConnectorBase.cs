using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace NoiseMe.Drags.App.Models.SuperSocket.ClientEngine.Proxy
{
	// Token: 0x02000124 RID: 292
	public abstract class ProxyConnectorBase : IProxyConnector
	{
		// Token: 0x17000251 RID: 593
		// (get) Token: 0x06000913 RID: 2323 RVA: 0x00007075 File Offset: 0x00005275
		// (set) Token: 0x06000914 RID: 2324 RVA: 0x0000707D File Offset: 0x0000527D
		public EndPoint ProxyEndPoint { get; private set; }

		// Token: 0x17000252 RID: 594
		// (get) Token: 0x06000915 RID: 2325 RVA: 0x00007086 File Offset: 0x00005286
		// (set) Token: 0x06000916 RID: 2326 RVA: 0x0000708E File Offset: 0x0000528E
		public string TargetHostHame { get; private set; }

		// Token: 0x06000917 RID: 2327 RVA: 0x00007097 File Offset: 0x00005297
		public ProxyConnectorBase(EndPoint proxyEndPoint) : this(proxyEndPoint, null)
		{
		}

		// Token: 0x06000918 RID: 2328 RVA: 0x000070A1 File Offset: 0x000052A1
		public ProxyConnectorBase(EndPoint proxyEndPoint, string targetHostHame)
		{
			this.ProxyEndPoint = proxyEndPoint;
			this.TargetHostHame = targetHostHame;
		}

		// Token: 0x06000919 RID: 2329
		public abstract void Connect(EndPoint remoteEndPoint);

		// Token: 0x14000016 RID: 22
		// (add) Token: 0x0600091A RID: 2330 RVA: 0x000070B7 File Offset: 0x000052B7
		// (remove) Token: 0x0600091B RID: 2331 RVA: 0x000070D0 File Offset: 0x000052D0
		public event EventHandler<ProxyEventArgs> Completed
		{
			add
			{
				this.m_Completed = (EventHandler<ProxyEventArgs>)Delegate.Combine(this.m_Completed, value);
			}
			remove
			{
				this.m_Completed = (EventHandler<ProxyEventArgs>)Delegate.Remove(this.m_Completed, value);
			}
		}

		// Token: 0x0600091C RID: 2332 RVA: 0x000070E9 File Offset: 0x000052E9
		protected void OnCompleted(ProxyEventArgs args)
		{
			if (this.m_Completed == null)
			{
				return;
			}
			this.m_Completed(this, args);
		}

		// Token: 0x0600091D RID: 2333 RVA: 0x00007101 File Offset: 0x00005301
		protected void OnException(Exception exception)
		{
			this.OnCompleted(new ProxyEventArgs(exception));
		}

		// Token: 0x0600091E RID: 2334 RVA: 0x0000710F File Offset: 0x0000530F
		protected void OnException(string exception)
		{
			this.OnCompleted(new ProxyEventArgs(new Exception(exception)));
		}

		// Token: 0x0600091F RID: 2335 RVA: 0x0001EAF8 File Offset: 0x0001CCF8
		protected bool ValidateAsyncResult(SocketAsyncEventArgs e)
		{
			if (e.SocketError != SocketError.Success)
			{
				SocketException ex = new SocketException((int)e.SocketError);
				this.OnCompleted(new ProxyEventArgs(new Exception(ex.Message, ex)));
				return false;
			}
			return true;
		}

		// Token: 0x06000920 RID: 2336 RVA: 0x00007122 File Offset: 0x00005322
		protected void AsyncEventArgsCompleted(object sender, SocketAsyncEventArgs e)
		{
			if (e.LastOperation == SocketAsyncOperation.Send)
			{
				this.ProcessSend(e);
				return;
			}
			this.ProcessReceive(e);
		}

		// Token: 0x06000921 RID: 2337 RVA: 0x0001EB34 File Offset: 0x0001CD34
		protected void StartSend(Socket socket, SocketAsyncEventArgs e)
		{
			bool flag = false;
			try
			{
				flag = socket.SendAsync(e);
			}
			catch (Exception ex)
			{
				this.OnException(new Exception(ex.Message, ex));
				return;
			}
			if (!flag)
			{
				this.ProcessSend(e);
			}
		}

		// Token: 0x06000922 RID: 2338 RVA: 0x0001EB7C File Offset: 0x0001CD7C
		protected virtual void StartReceive(Socket socket, SocketAsyncEventArgs e)
		{
			bool flag = false;
			try
			{
				flag = socket.ReceiveAsync(e);
			}
			catch (Exception ex)
			{
				this.OnException(new Exception(ex.Message, ex));
				return;
			}
			if (!flag)
			{
				this.ProcessReceive(e);
			}
		}

		// Token: 0x06000923 RID: 2339
		protected abstract void ProcessConnect(Socket socket, object targetEndPoint, SocketAsyncEventArgs e, Exception exception);

		// Token: 0x06000924 RID: 2340
		protected abstract void ProcessSend(SocketAsyncEventArgs e);

		// Token: 0x06000925 RID: 2341
		protected abstract void ProcessReceive(SocketAsyncEventArgs e);

		// Token: 0x04000381 RID: 897
		protected static Encoding ASCIIEncoding = new ASCIIEncoding();

		// Token: 0x04000382 RID: 898
		private EventHandler<ProxyEventArgs> m_Completed;
	}
}
