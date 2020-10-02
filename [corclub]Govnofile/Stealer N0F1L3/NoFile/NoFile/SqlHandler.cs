namespace NoFile
{
    using System;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Text;

    internal class SqlHandler
    {
        private readonly ulong _dbEncoding;
        private string[] _fieldNames;
        private readonly byte[] _fileBytes;
        private SqliteMasterEntry[] _masterTableEntries;
        private readonly ulong _pageSize;
        private readonly byte[] _sqlDataTypeSize = new byte[] { 0, 1, 2, 3, 4, 6, 8, 8, 0, 0 };
        private TableEntry[] _tableEntries;

        public SqlHandler(string fileName)
        {
            this._fileBytes = File.ReadAllBytes(fileName);
            this._pageSize = this.ConvertToULong(0x10, 2);
            this._dbEncoding = this.ConvertToULong(0x38, 4);
            this.ReadMasterTable(100L);
        }

        private ulong ConvertToULong(int startIndex, int size)
        {
            try
            {
                if ((size > 8) | (size == 0))
                {
                    return 0L;
                }
                ulong num = 0L;
                for (int i = 0; i <= (size - 1); i++)
                {
                    num = (num << 8) | this._fileBytes[startIndex + i];
                }
                return num;
            }
            catch
            {
                return 0L;
            }
        }

        private long Cvl(int startIdx, int endIdx)
        {
            try
            {
                endIdx++;
                byte[] buffer = new byte[8];
                int num = endIdx - startIdx;
                bool flag = false;
                if ((num == 0) | (num > 9))
                {
                    return 0L;
                }
                if (num == 1)
                {
                    buffer[0] = (byte) (this._fileBytes[startIdx] & 0x7f);
                    return BitConverter.ToInt64(buffer, 0);
                }
                if (num == 9)
                {
                    flag = true;
                }
                int num2 = 1;
                int num3 = 7;
                int index = 0;
                if (flag)
                {
                    buffer[0] = this._fileBytes[endIdx - 1];
                    endIdx--;
                    index = 1;
                }
                for (int i = endIdx - 1; i >= startIdx; i += -1)
                {
                    if ((i - 1) >= startIdx)
                    {
                        buffer[index] = (byte) (((this._fileBytes[i] >> (num2 - 1)) & (((int) 0xff) >> num2)) | (this._fileBytes[i - 1] << num3));
                        num2++;
                        index++;
                        num3--;
                    }
                    else if (!flag)
                    {
                        buffer[index] = (byte) ((this._fileBytes[i] >> (num2 - 1)) & (((int) 0xff) >> num2));
                    }
                }
                return BitConverter.ToInt64(buffer, 0);
            }
            catch
            {
                return 0L;
            }
        }

        public int GetRowCount()
        {
            return this._tableEntries.Length;
        }

        public string GetValue(int rowNum, int field)
        {
            try
            {
                if (rowNum >= this._tableEntries.Length)
                {
                    return null;
                }
                return ((field >= this._tableEntries[rowNum].Content.Length) ? null : this._tableEntries[rowNum].Content[field]);
            }
            catch
            {
                return null;
            }
        }

        private int Gvl(int startIdx)
        {
            int num;
            try
            {
                if (startIdx > this._fileBytes.Length)
                {
                    return 0;
                }
                int index = startIdx;
                while (index <= (startIdx + 8))
                {
                    if (index > (this._fileBytes.Length - 1))
                    {
                        return 0;
                    }
                    if ((this._fileBytes[index] & 0x80) != 0x80)
                    {
                        goto Label_004B;
                    }
                    index++;
                }
                return (startIdx + 8);
            Label_004B:
                num = index;
            }
            catch
            {
                num = 0;
            }
            return num;
        }

        private static bool IsOdd(long value)
        {
            return ((value & 1L) == 1L);
        }

        private void ReadMasterTable(long offset)
        {
            try
            {
                switch (this._fileBytes[(int) ((IntPtr) offset)])
                {
                    case 5:
                    {
                        ushort num2 = (ushort) (this.ConvertToULong(((int) offset) + 3, 2) - ((ulong) 1L));
                        for (int i = 0; i <= num2; i++)
                        {
                            ushort startIndex = (ushort) this.ConvertToULong((((int) offset) + 12) + (i * 2), 2);
                            if (offset == 100L)
                            {
                                this.ReadMasterTable((long) ((this.ConvertToULong(startIndex, 4) - ((ulong) 1L)) * this._pageSize));
                            }
                            else
                            {
                                this.ReadMasterTable((long) ((this.ConvertToULong((int) (offset + startIndex), 4) - ((ulong) 1L)) * this._pageSize));
                            }
                        }
                        this.ReadMasterTable((long) ((this.ConvertToULong(((int) offset) + 8, 4) - ((ulong) 1L)) * this._pageSize));
                        break;
                    }
                    case 13:
                    {
                        ulong num3 = this.ConvertToULong(((int) offset) + 3, 2) - ((ulong) 1L);
                        int length = 0;
                        if (this._masterTableEntries != null)
                        {
                            length = this._masterTableEntries.Length;
                            Array.Resize<SqliteMasterEntry>(ref this._masterTableEntries, (this._masterTableEntries.Length + ((int) num3)) + 1);
                        }
                        else
                        {
                            this._masterTableEntries = new SqliteMasterEntry[num3 + ((ulong) 1L)];
                        }
                        for (ulong j = 0L; j <= num3; j += (ulong) 1L)
                        {
                            ulong num8 = this.ConvertToULong((((int) offset) + 8) + (((int) j) * 2), 2);
                            if (offset != 100L)
                            {
                                num8 += (ulong) offset;
                            }
                            int endIdx = this.Gvl((int) num8);
                            this.Cvl((int) num8, endIdx);
                            int num10 = this.Gvl((int) ((num8 + ((double)endIdx - num8)) + ((ulong) 1L)));
                            this.Cvl((int) ((num8 + ((double)endIdx - num8)) + ((ulong) 1L)), num10);
                            ulong num11 = (ulong)(num8 + (((double)num10 - num8) + ((ulong) 1L)));
                            int num12 = this.Gvl((int) num11);
                            int num13 = num12;
                            long num14 = this.Cvl((int) num11, num12);
                            long[] numArray = new long[5];
                            for (int k = 0; k <= 4; k++)
                            {
                                int startIdx = num13 + 1;
                                num13 = this.Gvl(startIdx);
                                numArray[k] = this.Cvl(startIdx, num13);
                                numArray[k] = (numArray[k] <= 9L) ? ((long) this._sqlDataTypeSize[(int) ((IntPtr) numArray[k])]) : (!IsOdd(numArray[k]) ? ((numArray[k] - 12L) / 2L) : ((numArray[k] - 13L) / 2L));
                            }
                            if (this._dbEncoding != 1L)
                            {
                            }
                            if (this._dbEncoding == 1L)
                            {
                                this._masterTableEntries[length + ((int) j)].ItemName = Encoding.Default.GetString(this._fileBytes, (int) ((num11 + ((ulong) num14)) + ((ulong) numArray[0])), (int) numArray[1]);
                            }
                            else if (this._dbEncoding == 2L)
                            {
                                this._masterTableEntries[length + ((int) j)].ItemName = Encoding.Unicode.GetString(this._fileBytes, (int) ((num11 + ((ulong) num14)) + ((ulong) numArray[0])), (int) numArray[1]);
                            }
                            else if (this._dbEncoding == 3L)
                            {
                                this._masterTableEntries[length + ((int) j)].ItemName = Encoding.BigEndianUnicode.GetString(this._fileBytes, (int) ((num11 + ((ulong) num14)) + ((ulong) numArray[0])), (int) numArray[1]);
                            }
                            this._masterTableEntries[length + ((int) j)].RootNum = (long) this.ConvertToULong((int) ((((num11 + ((ulong) num14)) + ((ulong) numArray[0])) + ((ulong) numArray[1])) + ((ulong) numArray[2])), (int) numArray[3]);
                            if (this._dbEncoding == 1L)
                            {
                                this._masterTableEntries[length + ((int) j)].SqlStatement = Encoding.Default.GetString(this._fileBytes, (int) (((((num11 + ((ulong) num14)) + ((ulong) numArray[0])) + ((ulong) numArray[1])) + ((ulong) numArray[2])) + ((ulong) numArray[3])), (int) numArray[4]);
                            }
                            else if (this._dbEncoding == 2L)
                            {
                                this._masterTableEntries[length + ((int) j)].SqlStatement = Encoding.Unicode.GetString(this._fileBytes, (int) (((((num11 + ((ulong) num14)) + ((ulong) numArray[0])) + ((ulong) numArray[1])) + ((ulong) numArray[2])) + ((ulong) numArray[3])), (int) numArray[4]);
                            }
                            else if (this._dbEncoding == 3L)
                            {
                                this._masterTableEntries[length + ((int) j)].SqlStatement = Encoding.BigEndianUnicode.GetString(this._fileBytes, (int) (((((num11 + ((ulong) num14)) + ((ulong) numArray[0])) + ((ulong) numArray[1])) + ((ulong) numArray[2])) + ((ulong) numArray[3])), (int) numArray[4]);
                            }
                        }
                        break;
                    }
                }
            }
            catch
            {
            }
        }

        public bool ReadTable(string tableName)
        {
            try
            {
                int index = -1;
                int num2 = 0;
                while (num2 <= this._masterTableEntries.Length)
                {
                    if (string.Compare(this._masterTableEntries[num2].ItemName.ToLower(), tableName.ToLower(), StringComparison.Ordinal) == 0)
                    {
                        goto Label_003B;
                    }
                    num2++;
                }
                goto Label_003D;
            Label_003B:
                index = num2;
            Label_003D:
                if (index == -1)
                {
                    return false;
                }
                char[] separator = new char[] { ',' };
                string[] strArray = this._masterTableEntries[index].SqlStatement.Substring(this._masterTableEntries[index].SqlStatement.IndexOf("(", StringComparison.Ordinal) + 1).Split(separator);
                for (int i = 0; i <= (strArray.Length - 1); i++)
                {
                    strArray[i] = strArray[i].TrimStart(new char[0]);
                    int length = strArray[i].IndexOf(' ');
                    if (length > 0)
                    {
                        strArray[i] = strArray[i].Substring(0, length);
                    }
                    if (strArray[i].IndexOf("UNIQUE", StringComparison.Ordinal) != 0)
                    {
                        Array.Resize<string>(ref this._fieldNames, i + 1);
                        this._fieldNames[i] = strArray[i];
                    }
                }
                return this.ReadTableFromOffset(((ulong) (this._masterTableEntries[index].RootNum - 1L)) * this._pageSize);
            }
            catch
            {
                return false;
            }
        }

        private bool ReadTableFromOffset(ulong offset)
        {
            try
            {
                if (this._fileBytes[(int) ((IntPtr) offset)] == 13)
                {
                    ushort num = (ushort) (this.ConvertToULong(((int) offset) + 3, 2) - ((ulong) 1L));
                    int length = 0;
                    if (this._tableEntries != null)
                    {
                        length = this._tableEntries.Length;
                        Array.Resize<TableEntry>(ref this._tableEntries, (this._tableEntries.Length + num) + 1);
                    }
                    else
                    {
                        this._tableEntries = new TableEntry[num + 1];
                    }
                    for (ushort i = 0; i <= num; i = (ushort) (i + 1))
                    {
                        ulong num4 = this.ConvertToULong((((int) offset) + 8) + (i * 2), 2);
                        if (offset != 100L)
                        {
                            num4 += offset;
                        }
                        int endIdx = this.Gvl((int) num4);
                        this.Cvl((int) num4, endIdx);
                        int num6 = this.Gvl((int) ((num4 + ((double)endIdx - num4)) + ((ulong) 1L)));
                        this.Cvl((int) ((num4 + ((double)endIdx - num4)) + ((ulong) 1L)), num6);
                        ulong num7 = (ulong)(num4 + (((double)num6 - num4) + ((ulong) 1L)));
                        int num8 = this.Gvl((int) num7);
                        int num9 = num8;
                        long num10 = this.Cvl((int) num7, num8);
                        RecordHeaderField[] array = null;
                        long num11 = (((long) num7) - num8) + 1L;
                        for (int j = 0; num11 < num10; j++)
                        {
                            Array.Resize<RecordHeaderField>(ref array, j + 1);
                            int startIdx = num9 + 1;
                            num9 = this.Gvl(startIdx);
                            array[j].Type = this.Cvl(startIdx, num9);
                            array[j].Size = (array[j].Type <= 9L) ? ((long) this._sqlDataTypeSize[(int) ((IntPtr) array[j].Type)]) : (!IsOdd(array[j].Type) ? ((array[j].Type - 12L) / 2L) : ((array[j].Type - 13L) / 2L));
                            num11 = (num11 + (num9 - startIdx)) + 1L;
                        }
                        if (array != null)
                        {
                            this._tableEntries[length + i].Content = new string[array.Length];
                            int num14 = 0;
                            for (int k = 0; k <= (array.Length - 1); k++)
                            {
                                if (array[k].Type > 9L)
                                {
                                    if (!IsOdd(array[k].Type))
                                    {
                                        if (this._dbEncoding == 1L)
                                        {
                                            this._tableEntries[length + i].Content[k] = Encoding.Default.GetString(this._fileBytes, ((int) (num7 + ((ulong) num10))) + num14, (int) array[k].Size);
                                        }
                                        else if (this._dbEncoding == 2L)
                                        {
                                            this._tableEntries[length + i].Content[k] = Encoding.Unicode.GetString(this._fileBytes, ((int) (num7 + ((ulong) num10))) + num14, (int) array[k].Size);
                                        }
                                        else if (this._dbEncoding == 3L)
                                        {
                                            this._tableEntries[length + i].Content[k] = Encoding.BigEndianUnicode.GetString(this._fileBytes, ((int) (num7 + ((ulong) num10))) + num14, (int) array[k].Size);
                                        }
                                    }
                                    else
                                    {
                                        this._tableEntries[length + i].Content[k] = Encoding.Default.GetString(this._fileBytes, ((int) (num7 + ((ulong) num10))) + num14, (int) array[k].Size);
                                    }
                                }
                                else
                                {
                                    this._tableEntries[length + i].Content[k] = Convert.ToString(this.ConvertToULong(((int) (num7 + ((ulong) num10))) + num14, (int) array[k].Size));
                                }
                                num14 += (int) array[k].Size;
                            }
                        }
                    }
                }
                else if (this._fileBytes[(int) ((IntPtr) offset)] == 5)
                {
                    ushort num16 = (ushort) (this.ConvertToULong((int) (offset + ((ulong) 3L)), 2) - ((ulong) 1L));
                    for (ushort m = 0; m <= num16; m = (ushort) (m + 1))
                    {
                        ushort num18 = (ushort) this.ConvertToULong((((int) offset) + 12) + (m * 2), 2);
                        this.ReadTableFromOffset((this.ConvertToULong((int) (offset + num18), 4) - ((ulong) 1L)) * this._pageSize);
                    }
                    this.ReadTableFromOffset((this.ConvertToULong((int) (offset + ((ulong) 8L)), 4) - ((ulong) 1L)) * this._pageSize);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct RecordHeaderField
        {
            public long Size;
            public long Type;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct SqliteMasterEntry
        {
            public string ItemName;
            public long RootNum;
            public string SqlStatement;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct TableEntry
        {
            public string[] Content;
        }
    }
}

