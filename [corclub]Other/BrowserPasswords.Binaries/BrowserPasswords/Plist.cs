using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Xml;
using Microsoft.VisualBasic.CompilerServices;

namespace BrowserPasswords
{
	// Token: 0x02000048 RID: 72
	public class Plist
	{
		// Token: 0x060001D7 RID: 471 RVA: 0x00006C38 File Offset: 0x00004E38
		public static object readPlist(string path)
		{
			object result;
			using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read))
			{
				result = Plist.readPlist(fileStream, plistType.Auto);
			}
			return result;
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x00006C78 File Offset: 0x00004E78
		public static object readPlistSource(string source)
		{
			return Plist.readPlist(Encoding.UTF8.GetBytes(source));
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x00006C9C File Offset: 0x00004E9C
		public static object readPlist(byte[] data)
		{
			return Plist.readPlist(new MemoryStream(data), plistType.Auto);
		}

		// Token: 0x060001DA RID: 474 RVA: 0x00006CBC File Offset: 0x00004EBC
		public static plistType getPlistType(Stream stream)
		{
			byte[] array = new byte[8];
			stream.Read(array, 0, 8);
			plistType result;
			if (BitConverter.ToInt64(array, 0) == 3472403351741427810L)
			{
				result = plistType.Binary;
			}
			else
			{
				result = plistType.Xml;
			}
			return result;
		}

		// Token: 0x060001DB RID: 475 RVA: 0x00006CF8 File Offset: 0x00004EF8
		public static object readPlist(Stream stream, plistType type)
		{
			if (type == plistType.Auto)
			{
				type = Plist.getPlistType(stream);
				stream.Seek(0L, SeekOrigin.Begin);
			}
			if (type == plistType.Binary)
			{
				using (BinaryReader binaryReader = new BinaryReader(stream))
				{
					byte[] data = binaryReader.ReadBytes(checked((int)binaryReader.BaseStream.Length));
					return Plist.readBinary(data);
				}
			}
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.XmlResolver = null;
			xmlDocument.Load(stream);
			return Plist.readXml(xmlDocument);
		}

		// Token: 0x060001DC RID: 476 RVA: 0x00006D80 File Offset: 0x00004F80
		public static void writeXml(object value, string path)
		{
			using (StreamWriter streamWriter = new StreamWriter(path))
			{
				streamWriter.Write(Plist.writeXml(RuntimeHelpers.GetObjectValue(value)));
			}
		}

		// Token: 0x060001DD RID: 477 RVA: 0x00006DC4 File Offset: 0x00004FC4
		public static void writeXml(object value, Stream stream)
		{
			using (StreamWriter streamWriter = new StreamWriter(stream))
			{
				streamWriter.Write(Plist.writeXml(RuntimeHelpers.GetObjectValue(value)));
			}
		}

		// Token: 0x060001DE RID: 478 RVA: 0x00006E08 File Offset: 0x00005008
		public static string writeXml(object value)
		{
			string @string;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				using (XmlWriter xmlWriter = XmlWriter.Create(memoryStream, new XmlWriterSettings
				{
					Encoding = new UTF8Encoding(false),
					ConformanceLevel = ConformanceLevel.Document,
					Indent = true
				}))
				{
					xmlWriter.WriteStartDocument();
					xmlWriter.WriteDocType("plist", "-//Apple Computer//DTD PLIST 1.0//EN", "http://www.apple.com/DTDs/PropertyList-1.0.dtd", null);
					xmlWriter.WriteStartElement("plist");
					xmlWriter.WriteAttributeString("version", "1.0");
					Plist.compose(RuntimeHelpers.GetObjectValue(value), xmlWriter);
					xmlWriter.WriteEndElement();
					xmlWriter.WriteEndDocument();
					xmlWriter.Flush();
					xmlWriter.Close();
					@string = Encoding.UTF8.GetString(memoryStream.ToArray());
				}
			}
			return @string;
		}

		// Token: 0x060001DF RID: 479 RVA: 0x00006EE8 File Offset: 0x000050E8
		public static void writeBinary(object value, string path)
		{
			using (BinaryWriter binaryWriter = new BinaryWriter(new FileStream(path, FileMode.Create)))
			{
				binaryWriter.Write(Plist.writeBinary(RuntimeHelpers.GetObjectValue(value)));
			}
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x00006F30 File Offset: 0x00005130
		public static void writeBinary(object value, Stream stream)
		{
			using (BinaryWriter binaryWriter = new BinaryWriter(stream))
			{
				binaryWriter.Write(Plist.writeBinary(RuntimeHelpers.GetObjectValue(value)));
			}
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x00006F74 File Offset: 0x00005174
		public static byte[] writeBinary(object value)
		{
			Plist.offsetTable.Clear();
			Plist.objectTable.Clear();
			Plist.refCount = 0;
			Plist.objRefSize = 0;
			Plist.offsetByteSize = 0;
			Plist.offsetTableOffset = 0L;
			int num = checked(Plist.countObject(RuntimeHelpers.GetObjectValue(value)) - 1);
			Plist.refCount = num;
			Plist.objRefSize = Plist.RegulateNullBytes(BitConverter.GetBytes(Plist.refCount)).Length;
			Plist.composeBinary(RuntimeHelpers.GetObjectValue(value));
			Plist.writeBinaryString("bplist00", false);
			Plist.offsetTableOffset = (long)Plist.objectTable.Count;
			checked
			{
				Plist.offsetTable.Add(Plist.objectTable.Count - 8);
				Plist.offsetByteSize = Plist.RegulateNullBytes(BitConverter.GetBytes(Plist.offsetTable[Plist.offsetTable.Count - 1])).Length;
				List<byte> list = new List<byte>();
				Plist.offsetTable.Reverse();
				int i = 0;
				while (i < Plist.offsetTable.Count)
				{
					Plist.offsetTable[i] = Plist.objectTable.Count - Plist.offsetTable[i];
					byte[] array = Plist.RegulateNullBytes(BitConverter.GetBytes(Plist.offsetTable[i]), Plist.offsetByteSize);
					Array.Reverse(array);
					list.AddRange(array);
					Math.Max(Interlocked.Increment(ref i), i - 1);
				}
				Plist.objectTable.AddRange(list);
				Plist.objectTable.AddRange(new byte[6]);
				Plist.objectTable.Add(Convert.ToByte(Plist.offsetByteSize));
				Plist.objectTable.Add(Convert.ToByte(Plist.objRefSize));
				byte[] bytes = BitConverter.GetBytes(unchecked((long)num) + 1L);
				Array.Reverse(bytes);
				Plist.objectTable.AddRange(bytes);
				Plist.objectTable.AddRange(BitConverter.GetBytes(0));
				bytes = BitConverter.GetBytes(Plist.offsetTableOffset);
				Array.Reverse(bytes);
				Plist.objectTable.AddRange(bytes);
				return Plist.objectTable.ToArray();
			}
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x00007158 File Offset: 0x00005358
		private static object readXml(XmlDocument xml)
		{
			XmlNode node = xml.DocumentElement.ChildNodes[0];
			return (Dictionary<string, object>)Plist.parse(node);
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x00007188 File Offset: 0x00005388
		private static object readBinary(byte[] data)
		{
			Plist.offsetTable.Clear();
			List<byte> offsetTableBytes = new List<byte>();
			Plist.objectTable.Clear();
			Plist.refCount = 0;
			Plist.objRefSize = 0;
			Plist.offsetByteSize = 0;
			Plist.offsetTableOffset = 0L;
			List<byte> list = new List<byte>(data);
			checked
			{
				List<byte> range = list.GetRange(list.Count - 32, 32);
				Plist.parseTrailer(range);
				Plist.objectTable = list.GetRange(0, (int)Plist.offsetTableOffset);
				offsetTableBytes = list.GetRange((int)Plist.offsetTableOffset, list.Count - (int)Plist.offsetTableOffset - 32);
				Plist.parseOffsetTable(offsetTableBytes);
				return Plist.parseBinary(0);
			}
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x00007228 File Offset: 0x00005428
		private static Dictionary<string, object> parseDictionary(XmlNode node)
		{
			XmlNodeList childNodes = node.ChildNodes;
			if (childNodes.Count % 2 != 0)
			{
				throw new DataMisalignedException("Dictionary elements must have an even number of child nodes");
			}
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			checked
			{
				for (int i = 0; i < childNodes.Count; i += 2)
				{
					XmlNode xmlNode = childNodes[i];
					XmlNode node2 = childNodes[i + 1];
					if (Operators.CompareString(xmlNode.Name, "key", false) != 0)
					{
						throw new ApplicationException("expected a key node");
					}
					object objectValue = RuntimeHelpers.GetObjectValue(Plist.parse(node2));
					if (objectValue != null)
					{
						dictionary.Add(xmlNode.InnerText, RuntimeHelpers.GetObjectValue(objectValue));
					}
				}
				return dictionary;
			}
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x000072C8 File Offset: 0x000054C8
		private static List<object> parseArray(XmlNode node)
		{
			List<object> list = new List<object>();
			try
			{
				foreach (object obj in node.ChildNodes)
				{
					XmlNode node2 = (XmlNode)obj;
					object objectValue = RuntimeHelpers.GetObjectValue(Plist.parse(node2));
					if (objectValue != null)
					{
						list.Add(RuntimeHelpers.GetObjectValue(objectValue));
					}
				}
			}
			finally
			{
				IEnumerator enumerator;
				if (enumerator is IDisposable)
				{
					(enumerator as IDisposable).Dispose();
				}
			}
			return list;
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x00007348 File Offset: 0x00005548
		private static void composeArray(List<object> value, XmlWriter writer)
		{
			writer.WriteStartElement("array");
			try
			{
				foreach (object obj in value)
				{
					object objectValue = RuntimeHelpers.GetObjectValue(obj);
					Plist.compose(RuntimeHelpers.GetObjectValue(objectValue), writer);
				}
			}
			finally
			{
				List<object>.Enumerator enumerator;
				((IDisposable)enumerator).Dispose();
			}
			writer.WriteEndElement();
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x000073B4 File Offset: 0x000055B4
		private static object parse(XmlNode node)
		{
			string name = node.Name;
			object result;
			if (Operators.CompareString(name, "dict", false) == 0)
			{
				result = Plist.parseDictionary(node);
			}
			else if (Operators.CompareString(name, "array", false) == 0)
			{
				result = Plist.parseArray(node);
			}
			else if (Operators.CompareString(name, "string", false) == 0)
			{
				result = node.InnerText;
			}
			else if (Operators.CompareString(name, "integer", false) == 0)
			{
				result = Convert.ToInt32(node.InnerText, NumberFormatInfo.InvariantInfo);
			}
			else if (Operators.CompareString(name, "real", false) == 0)
			{
				result = Convert.ToDouble(node.InnerText, NumberFormatInfo.InvariantInfo);
			}
			else if (Operators.CompareString(name, "false", false) == 0)
			{
				result = false;
			}
			else if (Operators.CompareString(name, "true", false) == 0)
			{
				result = true;
			}
			else if (Operators.CompareString(name, "null", false) == 0)
			{
				result = null;
			}
			else if (Operators.CompareString(name, "date", false) == 0)
			{
				result = XmlConvert.ToDateTime(node.InnerText, XmlDateTimeSerializationMode.Utc);
			}
			else
			{
				if (Operators.CompareString(name, "data", false) != 0)
				{
					throw new ApplicationException(string.Format("Plist Node `{0}' is not supported", node.Name));
				}
				result = Convert.FromBase64String(node.InnerText);
			}
			return result;
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x0000752C File Offset: 0x0000572C
		private static void compose(object value, XmlWriter writer)
		{
			if (value == null || value is string)
			{
				writer.WriteElementString("string", value as string);
			}
			else if (value is int || value is long)
			{
				string localName = "integer";
				int num = (int)value;
				writer.WriteElementString(localName, num.ToString(NumberFormatInfo.InvariantInfo));
			}
			else if (value is Dictionary<string, object> || value.GetType().ToString().StartsWith("System.Collections.Generic.Dictionary`2[System.String"))
			{
				Dictionary<string, object> dictionary = value as Dictionary<string, object>;
				if (dictionary == null)
				{
					dictionary = new Dictionary<string, object>();
					IDictionary dictionary2 = (IDictionary)value;
					try
					{
						foreach (object obj in dictionary2.Keys)
						{
							object objectValue = RuntimeHelpers.GetObjectValue(obj);
							dictionary.Add(objectValue.ToString(), RuntimeHelpers.GetObjectValue(dictionary2[RuntimeHelpers.GetObjectValue(objectValue)]));
						}
					}
					finally
					{
						IEnumerator enumerator;
						if (enumerator is IDisposable)
						{
							(enumerator as IDisposable).Dispose();
						}
					}
				}
				Plist.writeDictionaryValues(dictionary, writer);
			}
			else if (value is List<object>)
			{
				Plist.composeArray((List<object>)value, writer);
			}
			else if (value is byte[])
			{
				writer.WriteElementString("data", Convert.ToBase64String((byte[])value));
			}
			else if (value is float || value is double)
			{
				string localName2 = "real";
				double num2 = (double)value;
				writer.WriteElementString(localName2, num2.ToString(NumberFormatInfo.InvariantInfo));
			}
			else if (value is DateTime)
			{
				DateTime value2 = (DateTime)value;
				string value3 = XmlConvert.ToString(value2, XmlDateTimeSerializationMode.Utc);
				writer.WriteElementString("date", value3);
			}
			else
			{
				if (!(value is bool))
				{
					throw new Exception(string.Format("Value type '{0}' is unhandled", value.GetType().ToString()));
				}
				writer.WriteElementString(value.ToString().ToLower(), "");
			}
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x00007718 File Offset: 0x00005918
		private static void writeDictionaryValues(Dictionary<string, object> dictionary, XmlWriter writer)
		{
			writer.WriteStartElement("dict");
			try
			{
				foreach (string text in dictionary.Keys)
				{
					object objectValue = RuntimeHelpers.GetObjectValue(dictionary[text]);
					writer.WriteElementString("key", text);
					Plist.compose(RuntimeHelpers.GetObjectValue(objectValue), writer);
				}
			}
			finally
			{
				Dictionary<string, object>.KeyCollection.Enumerator enumerator;
				((IDisposable)enumerator).Dispose();
			}
			writer.WriteEndElement();
		}

		// Token: 0x060001EA RID: 490 RVA: 0x0000779C File Offset: 0x0000599C
		private static int countObject(object value)
		{
			int num = 0;
			string left = value.GetType().ToString();
			checked
			{
				if (Operators.CompareString(left, "System.Collections.Generic.Dictionary`2[System.String,System.Object]", false) == 0)
				{
					Dictionary<string, object> dictionary = (Dictionary<string, object>)value;
					try
					{
						foreach (string key in dictionary.Keys)
						{
							num += Plist.countObject(RuntimeHelpers.GetObjectValue(dictionary[key]));
						}
					}
					finally
					{
						Dictionary<string, object>.KeyCollection.Enumerator enumerator;
						((IDisposable)enumerator).Dispose();
					}
					num += dictionary.Keys.Count;
					Math.Max(Interlocked.Increment(ref num), num - 1);
				}
				else if (Operators.CompareString(left, "System.Collections.Generic.List`1[System.Object]", false) == 0)
				{
					List<object> list = (List<object>)value;
					try
					{
						foreach (object obj in list)
						{
							object objectValue = RuntimeHelpers.GetObjectValue(obj);
							num += Plist.countObject(RuntimeHelpers.GetObjectValue(objectValue));
						}
					}
					finally
					{
						List<object>.Enumerator enumerator2;
						((IDisposable)enumerator2).Dispose();
					}
					Math.Max(Interlocked.Increment(ref num), num - 1);
				}
				else
				{
					Math.Max(Interlocked.Increment(ref num), num - 1);
				}
				return num;
			}
		}

		// Token: 0x060001EB RID: 491 RVA: 0x000078D0 File Offset: 0x00005AD0
		private static byte[] writeBinaryDictionary(Dictionary<string, object> dictionary)
		{
			List<byte> list = new List<byte>();
			List<byte> list2 = new List<byte>();
			List<int> list3 = new List<int>();
			checked
			{
				int i = dictionary.Count - 1;
				while (i >= 0)
				{
					object[] array = new object[dictionary.Count + 1];
					dictionary.Values.CopyTo(array, 0);
					Plist.composeBinary(RuntimeHelpers.GetObjectValue(array[i]));
					Plist.offsetTable.Add(Plist.objectTable.Count);
					list3.Add(Plist.refCount);
					Math.Max(Interlocked.Decrement(ref Plist.refCount), Plist.refCount + 1);
					Math.Max(Interlocked.Decrement(ref i), i + 1);
				}
				i = dictionary.Count - 1;
				while (i >= 0)
				{
					string[] array2 = new string[dictionary.Count + 1];
					dictionary.Keys.CopyTo(array2, 0);
					Plist.composeBinary(array2[i]);
					Plist.offsetTable.Add(Plist.objectTable.Count);
					list3.Add(Plist.refCount);
					Math.Max(Interlocked.Decrement(ref Plist.refCount), Plist.refCount + 1);
					Math.Max(Interlocked.Decrement(ref i), i + 1);
				}
				if (dictionary.Count < 15)
				{
					list2.Add(Convert.ToByte((int)(208 | Convert.ToByte(dictionary.Count))));
				}
				else
				{
					list2.Add(223);
					list2.AddRange(Plist.writeBinaryInteger(dictionary.Count, false));
				}
				try
				{
					foreach (int value in list3)
					{
						byte[] array3 = Plist.RegulateNullBytes(BitConverter.GetBytes(value), Plist.objRefSize);
						Array.Reverse(array3);
						list.InsertRange(0, array3);
					}
				}
				finally
				{
					List<int>.Enumerator enumerator;
					((IDisposable)enumerator).Dispose();
				}
				list.InsertRange(0, list2);
				Plist.objectTable.InsertRange(0, list);
				return list.ToArray();
			}
		}

		// Token: 0x060001EC RID: 492 RVA: 0x00007AB0 File Offset: 0x00005CB0
		private static byte[] composeBinaryArray(List<object> objects)
		{
			List<byte> list = new List<byte>();
			List<byte> list2 = new List<byte>();
			List<int> list3 = new List<int>();
			checked
			{
				int i = objects.Count - 1;
				while (i >= 0)
				{
					Plist.composeBinary(RuntimeHelpers.GetObjectValue(objects[i]));
					Plist.offsetTable.Add(Plist.objectTable.Count);
					list3.Add(Plist.refCount);
					Math.Max(Interlocked.Decrement(ref Plist.refCount), Plist.refCount + 1);
					Math.Max(Interlocked.Decrement(ref i), i + 1);
				}
				if (objects.Count < 15)
				{
					list2.Add(Convert.ToByte((int)(160 | Convert.ToByte(objects.Count))));
				}
				else
				{
					list2.Add(175);
					list2.AddRange(Plist.writeBinaryInteger(objects.Count, false));
				}
				try
				{
					foreach (int value in list3)
					{
						byte[] array = Plist.RegulateNullBytes(BitConverter.GetBytes(value), Plist.objRefSize);
						Array.Reverse(array);
						list.InsertRange(0, array);
					}
				}
				finally
				{
					List<int>.Enumerator enumerator;
					((IDisposable)enumerator).Dispose();
				}
				list.InsertRange(0, list2);
				Plist.objectTable.InsertRange(0, list);
				return list.ToArray();
			}
		}

		// Token: 0x060001ED RID: 493 RVA: 0x00007BFC File Offset: 0x00005DFC
		private static byte[] composeBinary(object obj)
		{
			string left = obj.GetType().ToString();
			byte[] result;
			if (Operators.CompareString(left, "System.Collections.Generic.Dictionary`2[System.String,System.Object]", false) == 0)
			{
				byte[] array = Plist.writeBinaryDictionary((Dictionary<string, object>)obj);
				result = array;
			}
			else if (Operators.CompareString(left, "System.Collections.Generic.List`1[System.Object]", false) == 0)
			{
				byte[] array = Plist.composeBinaryArray((List<object>)obj);
				result = array;
			}
			else if (Operators.CompareString(left, "System.Byte[]", false) == 0)
			{
				byte[] array = Plist.writeBinaryByteArray((byte[])obj);
				result = array;
			}
			else if (Operators.CompareString(left, "System.Double", false) == 0)
			{
				byte[] array = Plist.writeBinaryDouble((double)obj);
				result = array;
			}
			else if (Operators.CompareString(left, "System.Int32", false) == 0)
			{
				byte[] array = Plist.writeBinaryInteger((int)obj, true);
				result = array;
			}
			else if (Operators.CompareString(left, "System.String", false) == 0)
			{
				byte[] array = Plist.writeBinaryString((string)obj, true);
				result = array;
			}
			else if (Operators.CompareString(left, "System.DateTime", false) == 0)
			{
				byte[] array = Plist.writeBinaryDate((DateTime)obj);
				result = array;
			}
			else if (Operators.CompareString(left, "System.Boolean", false) == 0)
			{
				byte[] array = Plist.writeBinaryBool((bool)obj);
				result = array;
			}
			else
			{
				result = new byte[0];
			}
			return result;
		}

		// Token: 0x060001EE RID: 494 RVA: 0x00007D58 File Offset: 0x00005F58
		public static byte[] writeBinaryDate(DateTime obj)
		{
			List<byte> list = new List<byte>(Plist.RegulateNullBytes(BitConverter.GetBytes(PlistDateConverter.ConvertToAppleTimeStamp(obj)), 8));
			list.Reverse();
			list.Insert(0, 51);
			Plist.objectTable.InsertRange(0, list);
			return list.ToArray();
		}

		// Token: 0x060001EF RID: 495 RVA: 0x00007DA4 File Offset: 0x00005FA4
		public static byte[] writeBinaryBool(bool obj)
		{
			List<byte> list = new List<byte>(new byte[]
			{
				obj ? 9 : 8
			});
			Plist.objectTable.InsertRange(0, list);
			return list.ToArray();
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x00007DE0 File Offset: 0x00005FE0
		private static byte[] writeBinaryInteger(int value, bool write)
		{
			List<byte> list = new List<byte>(BitConverter.GetBytes((long)value));
			list = new List<byte>(Plist.RegulateNullBytes(list.ToArray()));
			while ((double)list.Count != Math.Pow(2.0, Math.Log((double)list.Count) / Math.Log(2.0)))
			{
				list.Add(0);
			}
			int value2 = 16 | checked((int)Math.Round(Math.Log((double)list.Count) / Math.Log(2.0)));
			list.Reverse();
			list.Insert(0, Convert.ToByte(value2));
			if (write)
			{
				Plist.objectTable.InsertRange(0, list);
			}
			return list.ToArray();
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x00007E98 File Offset: 0x00006098
		private static byte[] writeBinaryDouble(double value)
		{
			List<byte> list = new List<byte>(Plist.RegulateNullBytes(BitConverter.GetBytes(value), 4));
			while ((double)list.Count != Math.Pow(2.0, Math.Log((double)list.Count) / Math.Log(2.0)))
			{
				list.Add(0);
			}
			int value2 = 32 | checked((int)Math.Round(Math.Log((double)list.Count) / Math.Log(2.0)));
			list.Reverse();
			list.Insert(0, Convert.ToByte(value2));
			Plist.objectTable.InsertRange(0, list);
			return list.ToArray();
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x00007F40 File Offset: 0x00006140
		private static byte[] writeBinaryByteArray(byte[] value)
		{
			List<byte> list = new List<byte>(value);
			List<byte> list2 = new List<byte>();
			if (value.Length < 15)
			{
				list2.Add(Convert.ToByte((int)(64 | Convert.ToByte(value.Length))));
			}
			else
			{
				list2.Add(79);
				list2.AddRange(Plist.writeBinaryInteger(list.Count, false));
			}
			list.InsertRange(0, list2);
			Plist.objectTable.InsertRange(0, list);
			return list.ToArray();
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x00007FB4 File Offset: 0x000061B4
		private static byte[] writeBinaryString(string value, bool head)
		{
			List<byte> list = new List<byte>();
			List<byte> list2 = new List<byte>();
			foreach (char value2 in value.ToCharArray())
			{
				list.Add(Convert.ToByte(value2));
			}
			if (head)
			{
				if (value.Length < 15)
				{
					list2.Add(Convert.ToByte((int)(80 | Convert.ToByte(value.Length))));
				}
				else
				{
					list2.Add(95);
					list2.AddRange(Plist.writeBinaryInteger(list.Count, false));
				}
			}
			list.InsertRange(0, list2);
			Plist.objectTable.InsertRange(0, list);
			return list.ToArray();
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x0000805C File Offset: 0x0000625C
		private static byte[] RegulateNullBytes(byte[] value)
		{
			return Plist.RegulateNullBytes(value, 1);
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x00008074 File Offset: 0x00006274
		private static byte[] RegulateNullBytes(byte[] value, int minBytes)
		{
			Array.Reverse(value);
			List<byte> list = new List<byte>(value);
			int i = 0;
			checked
			{
				while (i < list.Count)
				{
					if (list[i] != 0 || list.Count <= minBytes)
					{
						break;
					}
					list.Remove(list[i]);
					Math.Max(Interlocked.Decrement(ref i), i + 1);
					Math.Max(Interlocked.Increment(ref i), i - 1);
				}
				if (list.Count < minBytes)
				{
					int num = minBytes - list.Count;
					i = 0;
					while (i < num)
					{
						list.Insert(0, 0);
						Math.Max(Interlocked.Increment(ref i), i - 1);
					}
				}
				value = list.ToArray();
				Array.Reverse(value);
				return value;
			}
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x00008128 File Offset: 0x00006328
		private static void parseTrailer(List<byte> trailer)
		{
			Plist.offsetByteSize = BitConverter.ToInt32(Plist.RegulateNullBytes(trailer.GetRange(6, 1).ToArray(), 4), 0);
			Plist.objRefSize = BitConverter.ToInt32(Plist.RegulateNullBytes(trailer.GetRange(7, 1).ToArray(), 4), 0);
			byte[] array = trailer.GetRange(12, 4).ToArray();
			Array.Reverse(array);
			Plist.refCount = BitConverter.ToInt32(array, 0);
			byte[] array2 = trailer.GetRange(24, 8).ToArray();
			Array.Reverse(array2);
			Plist.offsetTableOffset = BitConverter.ToInt64(array2, 0);
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x000081B4 File Offset: 0x000063B4
		private static void parseOffsetTable(List<byte> offsetTableBytes)
		{
			checked
			{
				for (int i = 0; i < offsetTableBytes.Count; i += Plist.offsetByteSize)
				{
					byte[] array = offsetTableBytes.GetRange(i, Plist.offsetByteSize).ToArray();
					Array.Reverse(array);
					Plist.offsetTable.Add(BitConverter.ToInt32(Plist.RegulateNullBytes(array, 4), 0));
				}
			}
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x00008208 File Offset: 0x00006408
		private static object parseBinaryDictionary(int objRef)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			List<int> list = new List<int>();
			byte b = Plist.objectTable[Plist.offsetTable[objRef]];
			int num;
			int count = Plist.getCount(Plist.offsetTable[objRef], ref num);
			checked
			{
				if (count < 15)
				{
					num = Plist.offsetTable[objRef] + 1;
				}
				else
				{
					num = Plist.offsetTable[objRef] + 2 + Plist.RegulateNullBytes(BitConverter.GetBytes(count), 1).Length;
				}
				int i;
				for (i = num; i < num + count * 2 * Plist.objRefSize; i += Plist.objRefSize)
				{
					byte[] array = Plist.objectTable.GetRange(i, Plist.objRefSize).ToArray();
					Array.Reverse(array);
					list.Add(BitConverter.ToInt32(Plist.RegulateNullBytes(array, 4), 0));
				}
				i = 0;
				while (i < count)
				{
					dictionary.Add((string)Plist.parseBinary(list[i]), RuntimeHelpers.GetObjectValue(Plist.parseBinary(list[i + count])));
					Math.Max(Interlocked.Increment(ref i), i - 1);
				}
				return dictionary;
			}
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x00008320 File Offset: 0x00006520
		private static object parseBinaryArray(int objRef)
		{
			List<object> list = new List<object>();
			List<int> list2 = new List<int>();
			byte b = Plist.objectTable[Plist.offsetTable[objRef]];
			int num;
			int count = Plist.getCount(Plist.offsetTable[objRef], ref num);
			checked
			{
				if (count < 15)
				{
					num = Plist.offsetTable[objRef] + 1;
				}
				else
				{
					num = Plist.offsetTable[objRef] + 2 + Plist.RegulateNullBytes(BitConverter.GetBytes(count), 1).Length;
				}
				int i;
				for (i = num; i < num + count * Plist.objRefSize; i += Plist.objRefSize)
				{
					byte[] array = Plist.objectTable.GetRange(i, Plist.objRefSize).ToArray();
					Array.Reverse(array);
					list2.Add(BitConverter.ToInt32(Plist.RegulateNullBytes(array, 4), 0));
				}
				i = 0;
				while (i < count)
				{
					list.Add(RuntimeHelpers.GetObjectValue(Plist.parseBinary(list2[i])));
					Math.Max(Interlocked.Increment(ref i), i - 1);
				}
				return list;
			}
		}

		// Token: 0x060001FA RID: 506 RVA: 0x00008424 File Offset: 0x00006624
		private static int getCount(int bytePosition, ref int newBytePosition)
		{
			byte b = Plist.objectTable[bytePosition];
			byte b2 = Convert.ToByte((int)(b & 15));
			checked
			{
				int result;
				if (b2 < 15)
				{
					result = (int)b2;
					newBytePosition = bytePosition + 1;
				}
				else
				{
					result = (int)Plist.parseBinaryInt(bytePosition + 1, ref newBytePosition);
				}
				return result;
			}
		}

		// Token: 0x060001FB RID: 507 RVA: 0x00008470 File Offset: 0x00006670
		private static object parseBinary(int objRef)
		{
			byte b = Plist.objectTable[Plist.offsetTable[objRef]];
			int num = (int)(b & 240);
			if (num == 0)
			{
				if (true)
				{
					return Plist.objectTable[Plist.offsetTable[objRef]] != 0 && Plist.objectTable[Plist.offsetTable[objRef]] == 9;
				}
			}
			else if (num == 16)
			{
				if (true)
				{
					return Plist.parseBinaryInt(Plist.offsetTable[objRef]);
				}
			}
			else if (num == 32)
			{
				if (true)
				{
					return Plist.parseBinaryReal(Plist.offsetTable[objRef]);
				}
			}
			else if (num == 48)
			{
				if (true)
				{
					return Plist.parseBinaryDate(Plist.offsetTable[objRef]);
				}
			}
			else if (num == 64)
			{
				if (true)
				{
					return Plist.parseBinaryByteArray(Plist.offsetTable[objRef]);
				}
			}
			else if (num == 80)
			{
				if (true)
				{
					return Plist.parseBinaryAsciiString(Plist.offsetTable[objRef]);
				}
			}
			else if (num == 96)
			{
				if (true)
				{
					return Plist.parseBinaryUnicodeString(Plist.offsetTable[objRef]);
				}
			}
			else if (num == 208)
			{
				if (true)
				{
					return Plist.parseBinaryDictionary(objRef);
				}
			}
			else if (num == 160 && true)
			{
				return Plist.parseBinaryArray(objRef);
			}
			throw new Exception("This type is not supported");
		}

		// Token: 0x060001FC RID: 508 RVA: 0x000085E0 File Offset: 0x000067E0
		public static object parseBinaryDate(int headerPosition)
		{
			byte[] array = Plist.objectTable.GetRange(checked(headerPosition + 1), 8).ToArray();
			Array.Reverse(array);
			double timestamp = BitConverter.ToDouble(array, 0);
			DateTime dateTime = PlistDateConverter.ConvertFromAppleTimeStamp(timestamp);
			return dateTime;
		}

		// Token: 0x060001FD RID: 509 RVA: 0x00008620 File Offset: 0x00006820
		private static object parseBinaryInt(int headerPosition)
		{
			int num;
			return Plist.parseBinaryInt(headerPosition, ref num);
		}

		// Token: 0x060001FE RID: 510 RVA: 0x0000863C File Offset: 0x0000683C
		private static object parseBinaryInt(int headerPosition, ref int newHeaderPosition)
		{
			byte b = Plist.objectTable[headerPosition];
			checked
			{
				int num = (int)Math.Round(Math.Pow(2.0, (double)(b & 15)));
				byte[] array = Plist.objectTable.GetRange(headerPosition + 1, num).ToArray();
				Array.Reverse(array);
				newHeaderPosition = headerPosition + num + 1;
				return BitConverter.ToInt32(Plist.RegulateNullBytes(array, 4), 0);
			}
		}

		// Token: 0x060001FF RID: 511 RVA: 0x000086A8 File Offset: 0x000068A8
		private static object parseBinaryReal(int headerPosition)
		{
			byte b = Plist.objectTable[headerPosition];
			checked
			{
				int count = (int)Math.Round(Math.Pow(2.0, (double)(b & 15)));
				byte[] array = Plist.objectTable.GetRange(headerPosition + 1, count).ToArray();
				Array.Reverse(array);
				return BitConverter.ToDouble(Plist.RegulateNullBytes(array, 8), 0);
			}
		}

		// Token: 0x06000200 RID: 512 RVA: 0x0000870C File Offset: 0x0000690C
		private static object parseBinaryAsciiString(int headerPosition)
		{
			int index;
			int count = Plist.getCount(headerPosition, ref index);
			List<byte> range = Plist.objectTable.GetRange(index, count);
			return (range.Count > 0) ? Encoding.ASCII.GetString(range.ToArray()) : string.Empty;
		}

		// Token: 0x06000201 RID: 513 RVA: 0x00008754 File Offset: 0x00006954
		private static object parseBinaryUnicodeString(int headerPosition)
		{
			int num2;
			int num = Plist.getCount(headerPosition, ref num2);
			checked
			{
				num *= 2;
				byte[] array = new byte[num - 1 + 1];
				for (int i = 0; i < num; i += 2)
				{
					byte b = Plist.objectTable.GetRange(num2 + i, 1)[0];
					byte b2 = Plist.objectTable.GetRange(num2 + i + 1, 1)[0];
					if (BitConverter.IsLittleEndian)
					{
						array[i] = b2;
						array[i + 1] = b;
					}
					else
					{
						array[i] = b;
						array[i + 1] = b2;
					}
				}
				return Encoding.Unicode.GetString(array);
			}
		}

		// Token: 0x06000202 RID: 514 RVA: 0x000087E8 File Offset: 0x000069E8
		private static object parseBinaryByteArray(int headerPosition)
		{
			int index;
			int count = Plist.getCount(headerPosition, ref index);
			return Plist.objectTable.GetRange(index, count).ToArray();
		}

		// Token: 0x040001AA RID: 426
		private static List<int> offsetTable = new List<int>();

		// Token: 0x040001AB RID: 427
		private static List<byte> objectTable = new List<byte>();

		// Token: 0x040001AC RID: 428
		private static int refCount;

		// Token: 0x040001AD RID: 429
		private static int objRefSize;

		// Token: 0x040001AE RID: 430
		private static int offsetByteSize;

		// Token: 0x040001AF RID: 431
		private static long offsetTableOffset;
	}
}
