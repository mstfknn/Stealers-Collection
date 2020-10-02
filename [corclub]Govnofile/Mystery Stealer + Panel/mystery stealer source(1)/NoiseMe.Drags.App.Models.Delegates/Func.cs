namespace NoiseMe.Drags.App.Models.Delegates
{
	public delegate TResult Func<out TResult>();
	public delegate TResult Func<in T, out TResult>(T arg);
	public delegate TResult Func<in T1, in T2, out TResult>(T1 arg1, T2 arg2);
	public delegate TResult Func<in T1, in T2, in T3, in T4, out TResult>(T1 t1, T2 t2, T3 t3, T4 t4);
}
