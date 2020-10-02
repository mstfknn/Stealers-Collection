using NoiseMe.Drags.Dots;
using System;

namespace MailRy
{
	public static class Topolm
	{
		[STAThread]
		private static void Main(string[] args)
		{
			new DreamWork(args[0], args[1]).Crown();
			Console.ReadLine();
		}
	}
}
