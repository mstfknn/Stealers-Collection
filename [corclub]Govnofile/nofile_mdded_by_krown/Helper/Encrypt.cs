namespace who.Helper
{
    using System;

    class Encrypt
    {
        private const int key = 666;

        public static byte[] Encrypt_RC4(byte[] pwd, byte[] data)
        {
            int a, i, j, k, tmp;
            int[] key, box;
            byte[] cipher;

            key = new int[256];
            box = new int[256];
            cipher = new byte[data.Length];

            for (i = 0; i < 256; i++)
            {
                key[i] = pwd[i % pwd.Length];
                box[i] = i;
            }
            for (j = i = 0; i < 256; i++)
            {
                j = (j + box[i] + key[i]) % 256;
                tmp = box[i];
                box[i] = box[j];
                box[j] = tmp;
            }
            for (a = j = i = 0; i < data.Length; i++)
            {
                a++;
                a %= 256;
                j += box[a];
                j %= 256;
                tmp = box[a];
                box[a] = box[j];
                box[j] = tmp;
                k = box[((box[a] + box[j]) % 256)];
                cipher[i] = (byte)(data[i] ^ k);
            }
            return cipher;
        }

        public static byte[] Decrypt_RC4(byte[] pwd, byte[] data)
        {
            return Encrypt_RC4(pwd, data);
        }


        public static string XOR(string target)
        {
            string result = "";

            for (int i = 0; i < target.Length; i++)
            {
                char ch = (char)(target[i] ^ key);
                result += ch;
            }

            Console.WriteLine("XOR Encoded string: " + result);
            return result;
        }
    }
}
