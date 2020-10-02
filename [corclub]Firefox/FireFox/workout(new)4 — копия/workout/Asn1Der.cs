using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Security.Cryptography;

namespace workout
{
    public class MozillaPBE
    {
        private byte[] GlobalSalt { get; set; }

        private byte[] MasterPassword { get; set; }

        private byte[] EntrySalt { get; set; }

        public byte[] Key { get; private set; }

        public byte[] IV { get; private set; }

        public MozillaPBE(byte[] GlobalSalt, byte[] MasterPassword, byte[] EntrySalt)
        {
            this.GlobalSalt = GlobalSalt;
            this.MasterPassword = MasterPassword;
            this.EntrySalt = EntrySalt;
        }

        public void Compute()
        {
            SHA1CryptoServiceProvider cryptoServiceProvider = new SHA1CryptoServiceProvider();
            byte[] numArray1 = new byte[this.GlobalSalt.Length + this.MasterPassword.Length];
            Array.Copy((Array)this.GlobalSalt, 0, (Array)numArray1, 0, this.GlobalSalt.Length);
            Array.Copy((Array)this.MasterPassword, 0, (Array)numArray1, this.GlobalSalt.Length, this.MasterPassword.Length);
            byte[] buffer1 = numArray1;
            byte[] hash1 = cryptoServiceProvider.ComputeHash(buffer1);
            byte[] numArray2 = new byte[hash1.Length + this.EntrySalt.Length];
            Array.Copy((Array)hash1, 0, (Array)numArray2, 0, hash1.Length);
            Array.Copy((Array)this.EntrySalt, 0, (Array)numArray2, hash1.Length, this.EntrySalt.Length);
            byte[] buffer2 = numArray2;
            byte[] hash2 = cryptoServiceProvider.ComputeHash(buffer2);
            byte[] buffer3 = new byte[20];
            Array.Copy((Array)this.EntrySalt, 0, (Array)buffer3, 0, this.EntrySalt.Length);
            for (int length = this.EntrySalt.Length; length < 20; ++length)
                buffer3[length] = (byte)0;
            byte[] buffer4 = new byte[buffer3.Length + this.EntrySalt.Length];
            Array.Copy((Array)buffer3, 0, (Array)buffer4, 0, buffer3.Length);
            Array.Copy((Array)this.EntrySalt, 0, (Array)buffer4, buffer3.Length, this.EntrySalt.Length);
            byte[] hash3;
            byte[] hash4;
            using (HMACSHA1 hmacshA1 = new HMACSHA1(hash2))
            {
                hash3 = hmacshA1.ComputeHash(buffer4);
                byte[] hash5 = hmacshA1.ComputeHash(buffer3);
                byte[] buffer5 = new byte[hash5.Length + this.EntrySalt.Length];
                Array.Copy((Array)hash5, 0, (Array)buffer5, 0, hash5.Length);
                Array.Copy((Array)this.EntrySalt, 0, (Array)buffer5, hash5.Length, this.EntrySalt.Length);
                hash4 = hmacshA1.ComputeHash(buffer5);
            }
            byte[] numArray3 = new byte[hash3.Length + hash4.Length];
            Array.Copy((Array)hash3, 0, (Array)numArray3, 0, hash3.Length);
            Array.Copy((Array)hash4, 0, (Array)numArray3, hash3.Length, hash4.Length);
            this.Key = new byte[24];
            for (int index = 0; index < this.Key.Length; ++index)
                this.Key[index] = numArray3[index];
            this.IV = new byte[8];
            int index1 = this.IV.Length - 1;
            for (int index2 = numArray3.Length - 1; index2 >= numArray3.Length - this.IV.Length; --index2)
            {
                this.IV[index1] = numArray3[index2];
                --index1;
            }
        }
    }
    public class BerkeleyDB
    {
        public string Version { get; set; }

        public List<KeyValuePair<string, string>> Keys { get; private set; }

        public BerkeleyDB(string FileName)
        {
            List<byte> byteList = new List<byte>();
            this.Keys = new List<KeyValuePair<string, string>>();
            using (BinaryReader binaryReader = new BinaryReader((Stream)File.OpenRead(FileName)))
            {
                int num = 0;
                for (int length = (int)binaryReader.BaseStream.Length; num < length; ++num)
                    byteList.Add(binaryReader.ReadByte());
            }
            string str1 = BitConverter.ToString(this.Extract(byteList.ToArray(), 0, 4, false)).Replace("-", "");
            string str2 = BitConverter.ToString(this.Extract(byteList.ToArray(), 4, 4, false)).Replace("-", "");
            int int32 = BitConverter.ToInt32(this.Extract(byteList.ToArray(), 12, 4, true), 0);
            string str3 = "00061561";
            if (str1.Equals(str3))
            {
                this.Version = "Berkelet DB";
                if (str2.Equals("00000002"))
                    this.Version = this.Version + " 1.85 (Hash, version 2, native byte-order)";
                int num1 = int.Parse(BitConverter.ToString(this.Extract(byteList.ToArray(), 56, 4, false)).Replace("-", ""));
                int num2 = 1;
                while (this.Keys.Count < num1)
                {
                    string[] array = new string[(num1 - this.Keys.Count) * 2];
                    for (int index = 0; index < (num1 - this.Keys.Count) * 2; ++index)
                        array[index] = BitConverter.ToString(this.Extract(byteList.ToArray(), int32 * num2 + 2 + index * 2, 2, true)).Replace("-", "");
                    Array.Sort<string>(array);
                    int index1 = 0;
                    while (index1 < array.Length)
                    {
                        int start1 = Convert.ToInt32(array[index1], 16) + int32 * num2;
                        int start2 = Convert.ToInt32(array[index1 + 1], 16) + int32 * num2;
                        int num3 = index1 + 2 >= array.Length ? int32 + int32 * num2 : Convert.ToInt32(array[index1 + 2], 16) + int32 * num2;
                        string key = Encoding.ASCII.GetString(this.Extract(byteList.ToArray(), start2, num3 - start2, false));
                        string str4 = BitConverter.ToString(this.Extract(byteList.ToArray(), start1, start2 - start1, false));
                        if (!string.IsNullOrWhiteSpace(key))
                            this.Keys.Add(new KeyValuePair<string, string>(key, str4));
                        index1 += 2;
                    }
                    ++num2;
                }
            }
            else
                this.Version = "Unknow database format";
        }

        private byte[] Extract(byte[] source, int start, int length, bool littleEndian)
        {
            byte[] numArray = new byte[length];
            int index1 = 0;
            for (int index2 = start; index2 < start + length; ++index2)
            {
                numArray[index1] = source[index2];
                ++index1;
            }
            if (littleEndian)
                Array.Reverse((Array)numArray);
            return numArray;
        }

        private byte[] ConvertToLittleEndian(byte[] source)
        {
            Array.Reverse((Array)source);
            return source;
        }
    }
    public class Asn1DerObject
    {
        public Asn1Der.Type Type { get; set; }

        public int Lenght { get; set; }

        public List<Asn1DerObject> objects { get; set; }

        public byte[] Data { get; set; }

        public Asn1DerObject()
        {
            this.objects = new List<Asn1DerObject>();
        }

        public override string ToString()
        {
            StringBuilder stringBuilder1 = new StringBuilder();
            StringBuilder stringBuilder2 = new StringBuilder();
            switch (this.Type)
            {
                case Asn1Der.Type.Integer:
                    foreach (byte num in this.Data)
                        stringBuilder2.AppendFormat("{0:X2}", (object)num);
                    stringBuilder1.AppendLine("\tINTEGER " + (object)stringBuilder2);
                    stringBuilder2.Clear();
                    break;
                case Asn1Der.Type.OctetString:
                    foreach (byte num in this.Data)
                        stringBuilder2.AppendFormat("{0:X2}", (object)num);
                    stringBuilder1.AppendLine("\tOCTETSTRING " + stringBuilder2.ToString());
                    stringBuilder2.Clear();
                    break;
                case Asn1Der.Type.ObjectIdentifier:
                    foreach (byte num in this.Data)
                        stringBuilder2.AppendFormat("{0:X2}", (object)num);
                    stringBuilder1.AppendLine("\tOBJECTIDENTIFIER " + stringBuilder2.ToString());
                    stringBuilder2.Clear();
                    break;
                case Asn1Der.Type.Sequence:
                    stringBuilder1.AppendLine("SEQUENCE {");
                    break;
            }
            foreach (Asn1DerObject asn1DerObject in this.objects)
                stringBuilder1.Append(asn1DerObject.ToString());
            if (this.Type.Equals((object)Asn1Der.Type.Sequence))
                stringBuilder1.AppendLine("}");
            return stringBuilder1.ToString();
        }
    }
    public class Asn1Der
    {
        public Asn1DerObject Parse(byte[] dataToParse)
        {
            Asn1DerObject asn1DerObject1 = new Asn1DerObject();
            for (int index = 0; index < dataToParse.Length; ++index)
            {
                int num1 = 0;
                switch ((Asn1Der.Type)dataToParse[index])
                {
                    case Asn1Der.Type.Integer:
                        List<Asn1DerObject> objects1 = asn1DerObject1.objects;
                        Asn1DerObject asn1DerObject2 = new Asn1DerObject();
                        asn1DerObject2.Type = Asn1Der.Type.Integer;
                        int num2 = (int)dataToParse[index + 1];
                        asn1DerObject2.Lenght = num2;
                        objects1.Add(asn1DerObject2);
                        byte[] numArray1 = new byte[(int)dataToParse[index + 1]];
                        int length1 = index + 2 + (int)dataToParse[index + 1] > dataToParse.Length ? dataToParse.Length - (index + 2) : (int)dataToParse[index + 1];
                        Array.Copy((Array)((IEnumerable<byte>)dataToParse).ToArray<byte>(), index + 2, (Array)numArray1, 0, length1);
                        asn1DerObject1.objects.Last<Asn1DerObject>().Data = numArray1;
                        index = index + 1 + asn1DerObject1.objects.Last<Asn1DerObject>().Lenght;
                        break;
                    case Asn1Der.Type.OctetString:
                        List<Asn1DerObject> objects2 = asn1DerObject1.objects;
                        Asn1DerObject asn1DerObject3 = new Asn1DerObject();
                        asn1DerObject3.Type = Asn1Der.Type.OctetString;
                        int num3 = (int)dataToParse[index + 1];
                        asn1DerObject3.Lenght = num3;
                        objects2.Add(asn1DerObject3);
                        byte[] numArray2 = new byte[(int)dataToParse[index + 1]];
                        int length2 = index + 2 + (int)dataToParse[index + 1] > dataToParse.Length ? dataToParse.Length - (index + 2) : (int)dataToParse[index + 1];
                        Array.Copy((Array)((IEnumerable<byte>)dataToParse).ToArray<byte>(), index + 2, (Array)numArray2, 0, length2);
                        asn1DerObject1.objects.Last<Asn1DerObject>().Data = numArray2;
                        index = index + 1 + asn1DerObject1.objects.Last<Asn1DerObject>().Lenght;
                        break;
                    case Asn1Der.Type.ObjectIdentifier:
                        List<Asn1DerObject> objects3 = asn1DerObject1.objects;
                        Asn1DerObject asn1DerObject4 = new Asn1DerObject();
                        asn1DerObject4.Type = Asn1Der.Type.ObjectIdentifier;
                        int num4 = (int)dataToParse[index + 1];
                        asn1DerObject4.Lenght = num4;
                        objects3.Add(asn1DerObject4);
                        byte[] numArray3 = new byte[(int)dataToParse[index + 1]];
                        int length3 = index + 2 + (int)dataToParse[index + 1] > dataToParse.Length ? dataToParse.Length - (index + 2) : (int)dataToParse[index + 1];
                        Array.Copy((Array)((IEnumerable<byte>)dataToParse).ToArray<byte>(), index + 2, (Array)numArray3, 0, length3);
                        asn1DerObject1.objects.Last<Asn1DerObject>().Data = numArray3;
                        index = index + 1 + asn1DerObject1.objects.Last<Asn1DerObject>().Lenght;
                        break;
                    case Asn1Der.Type.Sequence:
                        byte[] dataToParse1;
                        if (asn1DerObject1.Lenght == 0)
                        {
                            asn1DerObject1.Type = Asn1Der.Type.Sequence;
                            asn1DerObject1.Lenght = dataToParse.Length - (index + 2);
                            dataToParse1 = new byte[asn1DerObject1.Lenght];
                        }
                        else
                        {
                            List<Asn1DerObject> objects4 = asn1DerObject1.objects;
                            Asn1DerObject asn1DerObject5 = new Asn1DerObject();
                            asn1DerObject5.Type = Asn1Der.Type.Sequence;
                            int num5 = (int)dataToParse[index + 1];
                            asn1DerObject5.Lenght = num5;
                            objects4.Add(asn1DerObject5);
                            dataToParse1 = new byte[(int)dataToParse[index + 1]];
                        }
                        num1 = dataToParse1.Length > dataToParse.Length - (index + 2) ? dataToParse.Length - (index + 2) : dataToParse1.Length;
                        Array.Copy((Array)dataToParse, index + 2, (Array)dataToParse1, 0, dataToParse1.Length);
                        asn1DerObject1.objects.Add(this.Parse(dataToParse1));
                        index = index + 1 + (int)dataToParse[index + 1];
                        break;
                }
            }
            return asn1DerObject1;
        }

        public enum Type
        {
            Integer = 2,
            BitString = 3,
            OctetString = 4,
            Null = 5,
            ObjectIdentifier = 6,
            Sequence = 48,
        }
    }
}
