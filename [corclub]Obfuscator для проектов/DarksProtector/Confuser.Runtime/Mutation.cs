internal class Mutation
{
    public static readonly int KeyI0 = 0, KeyI1 = 1, KeyI2 = 2, KeyI3 = 3, KeyI4 = 4, KeyI5 = 5, KeyI6 = 6, KeyI7 = 7, KeyI8 = 8, KeyI9 = 9, KeyI10 = 10, 
	KeyI11 = 11, KeyI12 = 12, KeyI13 = 13, KeyI14 = 14, KeyI15 = 15;

    public static T Placeholder<T>(T val) => val;

    public static T Value<T>() => default(T);

    public static T Value<T, Arg0>(Arg0 arg0) => default(T);

    public static void Crypt(uint[] data, uint[] key) { }
}