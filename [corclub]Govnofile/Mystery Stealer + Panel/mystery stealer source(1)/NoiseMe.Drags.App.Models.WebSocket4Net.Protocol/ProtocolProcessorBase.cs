using System;
using System.Collections.Generic;

namespace NoiseMe.Drags.App.Models.WebSocket4Net.Protocol
{
	internal abstract class ProtocolProcessorBase : IProtocolProcessor
	{
		protected const string HeaderItemFormat = "{0}: {1}";

		private static char[] s_SpaceSpliter = new char[1]
		{
			' '
		};

		public abstract bool SupportBinary
		{
			get;
		}

		public abstract bool SupportPingPong
		{
			get;
		}

		public ICloseStatusCode CloseStatusCode
		{
			get;
			private set;
		}

		public WebSocketVersion Version
		{
			get;
			private set;
		}

		protected string VersionTag
		{
			get;
			private set;
		}

		public ProtocolProcessorBase(WebSocketVersion version, ICloseStatusCode closeStatusCode)
		{
			CloseStatusCode = closeStatusCode;
			Version = version;
			int num = (int)version;
			VersionTag = num.ToString();
		}

		public abstract void SendHandshake(WebSocket websocket);

		public abstract ReaderBase CreateHandshakeReader(WebSocket websocket);

		public abstract bool VerifyHandshake(WebSocket websocket, WebSocketCommandInfo handshakeInfo, out string description);

		public abstract void SendMessage(WebSocket websocket, string message);

		public abstract void SendCloseHandshake(WebSocket websocket, int statusCode, string closeReason);

		public abstract void SendPing(WebSocket websocket, string ping);

		public abstract void SendPong(WebSocket websocket, string pong);

		public abstract void SendData(WebSocket websocket, byte[] data, int offset, int length);

		public abstract void SendData(WebSocket websocket, IList<ArraySegment<byte>> segments);

		protected virtual bool ValidateVerbLine(string verbLine)
		{
			string[] array = verbLine.Split(s_SpaceSpliter, 3, StringSplitOptions.RemoveEmptyEntries);
			if (array.Length < 2)
			{
				return false;
			}
			if (!array[0].StartsWith("HTTP/"))
			{
				return false;
			}
			int result = 0;
			if (!int.TryParse(array[1], out result))
			{
				return false;
			}
			return result == 101;
		}
	}
}
