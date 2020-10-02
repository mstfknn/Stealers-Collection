using System;
using System.Threading;
using NoiseMe.Drags.App.Models;
using NoiseMe.Drags.App.Models.Communication;
using NoiseMe.Drags.App.Models.LocalModels.Extensions;
using NoiseMe.Drags.App.Models.WebSocket4Net;

namespace NoiseMe.Drags.App.Data.Srv
{
	// Token: 0x0200017F RID: 383
	public static class JJo
	{
		// Token: 0x06000C62 RID: 3170 RVA: 0x000256A0 File Offset: 0x000238A0
		public static bool? OnResponse(this ResponseBase response)
		{
			hd onResponseRecieved = JJo.OnResponseRecieved;
			if (onResponseRecieved == null)
			{
				return null;
			}
			return new bool?(onResponseRecieved(response));
		}

		// Token: 0x06000C63 RID: 3171 RVA: 0x000097C2 File Offset: 0x000079C2
		public static AsyncAction ProcessResponse<TInput>(this RequestBase request, WebSocket session, TInput objectToSend)
		{
			return AsyncTask.StartNew(delegate()
			{
				byte[] array = request.CreateResponse<TInput>(objectToSend).SerializeProto<Response<TInput>>();
				session.Send(array, 0, array.Length);
			});
		}

		// Token: 0x06000C64 RID: 3172 RVA: 0x000097EE File Offset: 0x000079EE
		public static AsyncAction ProcessRequest<TInput>(this WebSocket session, TInput objectToSend, string Name)
		{
			return AsyncTask.StartNew(delegate()
			{
				byte[] array = new Request<TInput>(objectToSend, Name).SerializeProto<Request<TInput>>();
				session.Send(array, 0, array.Length);
			});
		}

		// Token: 0x06000C65 RID: 3173 RVA: 0x0000981A File Offset: 0x00007A1A
		public static TaskAction<TResult> ProcessRequest<TInput, TResult>(this WebSocket session, TInput objectToSend, string Name, int timeoutMilliseconds = 60000)
		{
			return AsyncTask.StartNew<TResult>(delegate()
			{
				TResult result;
				try
				{
					JJo.<>c__DisplayClass4_1<TInput, TResult> CS$<>8__locals2 = new JJo.<>c__DisplayClass4_1<TInput, TResult>();
					DateTime t = DateTime.Now.AddMilliseconds((double)timeoutMilliseconds);
					CS$<>8__locals2.localRequest = new Request<TInput>(objectToSend, Name);
					CS$<>8__locals2.handled = false;
					CS$<>8__locals2.result = default(TResult);
					JJo.OnResponseRecieved = (hd)Delegate.Combine(JJo.OnResponseRecieved, new hd(CS$<>8__locals2.<ProcessRequest>g__OnResponse|1));
					byte[] array = CS$<>8__locals2.localRequest.SerializeProto<Request<TInput>>();
					session.Send(array, 0, array.Length);
					while (t >= DateTime.Now && !CS$<>8__locals2.handled)
					{
						Thread.Sleep(100);
					}
					JJo.OnResponseRecieved = (hd)Delegate.Remove(JJo.OnResponseRecieved, new hd(CS$<>8__locals2.<ProcessRequest>g__OnResponse|1));
					result = CS$<>8__locals2.result;
				}
				catch (Exception value)
				{
					Console.WriteLine(value);
					result = default(TResult);
				}
				return result;
			});
		}

		// Token: 0x040004C4 RID: 1220
		public static hd OnResponseRecieved;
	}
}
