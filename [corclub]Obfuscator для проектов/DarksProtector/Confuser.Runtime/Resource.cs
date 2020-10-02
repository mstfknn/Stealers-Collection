using System;
using System.Reflection;

namespace Confuser.Runtime
{
    internal static class Resource {
		static Assembly c;

		// Hmm... Too lazy.
		static void Initialize() {
            uint l = (uint)Mutation.KeyI0;
			uint[] q = Mutation.Placeholder(new uint[Mutation.KeyI0]);

            uint[] k = new uint[0x10];
            uint n = (uint)Mutation.KeyI1;
			for (int i = 0; i < 0x10; i++) {
				n ^= n >> 13;
				n ^= n << 25;
				n ^= n >> 27;
				k[i] = n;
			}

			int s = 0, d = 0;
            uint[] w = new uint[0x10];
            byte[] o = new byte[l * 4];
			while (s < l) {
				for (int j = 0; j < 0x10; j++)
					w[j] = q[s + j];
				Mutation.Crypt(w, k);
				for (int j = 0; j < 0x10; j++) {
					uint e = w[j];
					o[d++] = (byte)e;
					o[d++] = (byte)(e >> 8);
					o[d++] = (byte)(e >> 16);
					o[d++] = (byte)(e >> 24);
					k[j] ^= e;
				}
				s += 0x10;
			}

			c = Assembly.Load(Lzma.Decompress(o));
			AppDomain.CurrentDomain.AssemblyResolve += Handler;
		}

        static Assembly Handler(object sender, ResolveEventArgs args) => c.FullName == args.Name ? c : null;
    }

	internal static class Resource_Packer {
		static Assembly c;

		// Hmm... Too lazy.
		static void Initialize() {
            uint l = (uint)Mutation.KeyI0;
			uint[] q = Mutation.Placeholder(new uint[Mutation.KeyI0]);

            uint[] k = new uint[0x10];
            uint n = (uint)Mutation.KeyI1;
			for (int i = 0; i < 0x10; i++) {
				n ^= n >> 13;
				n ^= n << 25;
				n ^= n >> 27;
				k[i] = n;
			}

			int s = 0, d = 0;
            uint[] w = new uint[0x10];
            byte[] o = new byte[l * 4];
			while (s < l) {
				for (int j = 0; j < 0x10; j++)
					w[j] = q[s + j];
				Mutation.Crypt(w, k);
				for (int j = 0; j < 0x10; j++) {
					uint e = w[j];
					o[d++] = (byte)e;
					o[d++] = (byte)(e >> 8);
					o[d++] = (byte)(e >> 16);
					o[d++] = (byte)(e >> 24);
					k[j] ^= e;
				}
				s += 0x10;
			}

			c = Assembly.Load(Lzma.Decompress(o));
			AppDomain.CurrentDomain.ResourceResolve += Handler;
		}

		static Assembly Handler(object sender, ResolveEventArgs args) {
            string[] n = c.GetManifestResourceNames();
            return Array.IndexOf(n, args.Name) != -1 ? c : null;
        }
    }
}