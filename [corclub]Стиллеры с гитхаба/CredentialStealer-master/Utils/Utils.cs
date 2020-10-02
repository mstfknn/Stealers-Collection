using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CredentialStealer.Utils
{
    public class Utils
    {
        public static byte[] ConcatenateBytes(byte[] a, byte[] b)
        {
            byte[] c = new byte[a.Length + b.Length];
            System.Buffer.BlockCopy(a, 0, c, 0, a.Length);
            System.Buffer.BlockCopy(b, 0, c, a.Length, b.Length);

            return c;
        }
    }
}
