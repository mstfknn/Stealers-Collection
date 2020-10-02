namespace NoiseMe.Drags.App.Models.SuperSocket.ClientEngine.Proxy
{
	internal class ReceiveState
	{
		public byte[] Buffer
		{
			get;
			private set;
		}

		public int Length
		{
			get;
			set;
		}

		public ReceiveState(byte[] buffer)
		{
			Buffer = buffer;
		}
	}
}
