using System;
using System.Linq;

namespace FoxGrabber.Cryptography
{
    public partial class Asn1Der : IAsn1Der
    {
        public Asn1Der(){ }

        public Asn1DerObject Parse(byte[] dataToParse)
        {
            var parsedData = new Asn1DerObject();

            for (int i = 0; i < dataToParse.Length; i++)
            {
                byte[] data;
                int len = 0;
                switch ((Type)dataToParse[i])
                {
                    case Type.Sequence:
                        if (parsedData.Lenght != 0)
                        {
                            parsedData.objects.Add(new Asn1DerObject()
                            {
                                Type = Type.Sequence,
                                Lenght = dataToParse[i + 1]
                            });
                            data = new byte[dataToParse[i + 1]];
                        }
                        else
                        {
                            parsedData.Type = Type.Sequence;
                            parsedData.Lenght = dataToParse.Length - (i + 2);
                            data = new byte[parsedData.Lenght];
                        }
                        len = (data.Length > dataToParse.Length - (i + 2)) ? dataToParse.Length - (i + 2) : data.Length;
                        Array.Copy(dataToParse, i + 2, data, 0, data.Length);
                        parsedData.objects.Add(this.Parse(data));
                        i = i + 1 + dataToParse[i + 1];
                        break;
                    case Type.Integer:
                        parsedData.objects.Add(new Asn1DerObject()
                        {
                            Type = Type.Integer,
                            Lenght = dataToParse[i + 1]
                        });
                        data = new byte[dataToParse[i + 1]];
                        len = ((i + 2) + dataToParse[i + 1] > dataToParse.Length) ? dataToParse.Length - (i + 2) : dataToParse[i + 1];
                        Array.Copy(dataToParse.ToArray(), i + 2, data, 0, len);
                        parsedData.objects.Last().Data = data;
                        i = i + 1 + parsedData.objects.Last().Lenght;
                        break;
                    case Type.OctetString:
                        parsedData.objects.Add(new Asn1DerObject()
                        {
                            Type = Type.OctetString,
                            Lenght = dataToParse[i + 1]
                        });
                        data = new byte[dataToParse[i + 1]];
                        len = ((i + 2) + dataToParse[i + 1] > dataToParse.Length) ? dataToParse.Length - (i + 2) : dataToParse[i + 1];
                        Array.Copy(dataToParse.ToArray(), i + 2, data, 0, len);
                        parsedData.objects.Last().Data = data;
                        i = i + 1 + parsedData.objects.Last().Lenght;
                        break;
                    case Type.ObjectIdentifier:
                        parsedData.objects.Add(new Asn1DerObject()
                        {
                            Type = Type.ObjectIdentifier,
                            Lenght = dataToParse[i + 1]
                        });
                        data = new byte[dataToParse[i + 1]];
                        len = ((i + 2) + dataToParse[i + 1] > dataToParse.Length) ? dataToParse.Length - (i + 2) : dataToParse[i + 1];
                        Array.Copy(dataToParse.ToArray(), i + 2, data, 0, len);
                        parsedData.objects.Last().Data = data;
                        i = i + 1 + parsedData.objects.Last().Lenght;
                        break;
                    default:
                        break;
                }
            }

            return parsedData;
        }
    }
}