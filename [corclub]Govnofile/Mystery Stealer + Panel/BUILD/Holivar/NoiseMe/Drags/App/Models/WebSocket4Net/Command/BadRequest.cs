using System;
using System.Collections.Generic;

namespace NoiseMe.Drags.App.Models.WebSocket4Net.Command
{
	// Token: 0x020000C4 RID: 196
	public class BadRequest : WebSocketCommandBase
	{
		// Token: 0x06000685 RID: 1669 RVA: 0x0001A9F8 File Offset: 0x00018BF8
		public override void ExecuteCommand(WebSocket session, WebSocketCommandInfo commandInfo)
		{
			Dictionary<string, object> valueContainer = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
			string empty = string.Empty;
			commandInfo.Text.ParseMimeHeader(valueContainer, out empty);
			string value = valueContainer.GetValue("Sec-WebSocket-Version", string.Empty);
			if (!session.NotSpecifiedVersion)
			{
				if (string.IsNullOrEmpty(value))
				{
					session.FireError(new Exception("the server doesn't support the websocket protocol version your client was using"));
				}
				else
				{
					session.FireError(new Exception(string.Format("the server(version: {0}) doesn't support the websocket protocol version your client was using", value)));
				}
				session.CloseWithoutHandshake();
				return;
			}
			if (string.IsNullOrEmpty(value))
			{
				session.FireError(new Exception("unknown server protocol version"));
				session.CloseWithoutHandshake();
				return;
			}
			string[] array = value.Split(BadRequest.m_ValueSeparator, StringSplitOptions.RemoveEmptyEntries);
			int[] array2 = new int[array.Length];
			for (int i = 0; i < array.Length; i++)
			{
				int num;
				if (!int.TryParse(array[i], out num))
				{
					session.FireError(new Exception("invalid websocket version"));
					session.CloseWithoutHandshake();
					return;
				}
				array2[i] = num;
			}
			if (!session.GetAvailableProcessor(array2))
			{
				session.FireError(new Exception("unknown server protocol version"));
				session.CloseWithoutHandshake();
				return;
			}
			session.ProtocolProcessor.SendHandshake(session);
		}

		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x06000686 RID: 1670 RVA: 0x0001AB18 File Offset: 0x00018D18
		public override string Name
		{
			get
			{
				return 400.ToString();
			}
		}

		// Token: 0x040002C3 RID: 707
		private const string m_WebSocketVersion = "Sec-WebSocket-Version";

		// Token: 0x040002C4 RID: 708
		private static readonly string[] m_ValueSeparator = new string[]
		{
			", "
		};
	}
}
