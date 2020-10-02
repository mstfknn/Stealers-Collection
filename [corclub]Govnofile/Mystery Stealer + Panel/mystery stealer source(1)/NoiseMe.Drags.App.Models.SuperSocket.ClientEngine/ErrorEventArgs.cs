using System;

namespace NoiseMe.Drags.App.Models.SuperSocket.ClientEngine
{
	public class ErrorEventArgs : EventArgs
	{
		public Exception Exception
		{
			get;
			private set;
		}

		public ErrorEventArgs(Exception exception)
		{
			Exception = exception;
		}
	}
}
