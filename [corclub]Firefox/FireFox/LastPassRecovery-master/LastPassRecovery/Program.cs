namespace LastPassRecovery
{
    class Program
    {
        static void Main()
        {
            var decryptor = new LastPassDecrypt();
            decryptor.DecryptPasswordStoredInFirefox();
        }
    }
}
