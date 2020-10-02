using System;
using System.IO;
using System.Text;

namespace NoFile
{
    internal class SqlHandler
    {
        private readonly byte[] _sqlDataTypeSize = new byte[10]
        {
      (byte) 0,
      (byte) 1,
      (byte) 2,
      (byte) 3,
      (byte) 4,
      (byte) 6,
      (byte) 8,
      (byte) 8,
      (byte) 0,
      (byte) 0
        };
        private readonly ulong _dbEncoding;
        private readonly byte[] _fileBytes;
        private readonly ulong _pageSize;
        private string[] _fieldNames;
        private SqlHandler.SqliteMasterEntry[] _masterTableEntries;
        private SqlHandler.TableEntry[] _tableEntries;

        public SqlHandler(string fileName)
        {
            this._fileBytes = File.ReadAllBytes(fileName);
            this._pageSize = this.ConvertToULong(16, 2);
            this._dbEncoding = this.ConvertToULong(56, 4);
            this.ReadMasterTable(100L);
        }

        public string GetValue(int rowNum, int field)
        {
            try
            {
                if (rowNum >= this._tableEntries.Length)
                    return (string)null;
                return field >= this._tableEntries[rowNum].Content.Length ? (string)null : this._tableEntries[rowNum].Content[field];
            }
            catch
            {
                return (string)null;
            }
        }

        public int GetRowCount()
        {
            return this._tableEntries.Length;
        }

        private bool ReadTableFromOffset(ulong offset)
        {
            try
            {
                if ((int)this._fileBytes[offset] == 13)
                {
                    ushort num1 = (ushort)(this.ConvertToULong((int)offset + 3, 2) - 1UL);
                    int num2 = 0;
                    if (this._tableEntries != null)
                    {
                        num2 = this._tableEntries.Length;
                        Array.Resize<SqlHandler.TableEntry>(ref this._tableEntries, this._tableEntries.Length + (int)num1 + 1);
                    }
                    else
                        this._tableEntries = new SqlHandler.TableEntry[(int)num1 + 1];
                    for (ushort index1 = 0; (int)index1 <= (int)num1; ++index1)
                    {
                        ulong num3 = this.ConvertToULong((int)offset + 8 + (int)index1 * 2, 2);
                        if ((long)offset != 100L)
                            num3 += offset;
                        int endIdx1 = this.Gvl((int)num3);
                        this.Cvl((int)num3, endIdx1);
                        int endIdx2 = this.Gvl((int)((long)num3 + ((long)endIdx1 - (long)num3) + 1L));
                        this.Cvl((int)((long)num3 + ((long)endIdx1 - (long)num3) + 1L), endIdx2);
                        ulong num4 = num3 + (ulong)((long)endIdx2 - (long)num3 + 1L);
                        int endIdx3 = this.Gvl((int)num4);
                        int endIdx4 = endIdx3;
                        long num5 = this.Cvl((int)num4, endIdx3);
                        SqlHandler.RecordHeaderField[] array = (SqlHandler.RecordHeaderField[])null;
                        long num6 = (long)num4 - (long)endIdx3 + 1L;
                        int index2 = 0;
                        while (num6 < num5)
                        {
                            Array.Resize<SqlHandler.RecordHeaderField>(ref array, index2 + 1);
                            int startIdx = endIdx4 + 1;
                            endIdx4 = this.Gvl(startIdx);
                            array[index2].Type = this.Cvl(startIdx, endIdx4);
                            array[index2].Size = array[index2].Type <= 9L ? (long)this._sqlDataTypeSize[array[index2].Type] : (!SqlHandler.IsOdd(array[index2].Type) ? (array[index2].Type - 12L) / 2L : (array[index2].Type - 13L) / 2L);
                            num6 = num6 + (long)(endIdx4 - startIdx) + 1L;
                            ++index2;
                        }
                        if (array != null)
                        {
                            this._tableEntries[num2 + (int)index1].Content = new string[array.Length];
                            int num7 = 0;
                            for (int index3 = 0; index3 <= array.Length - 1; ++index3)
                            {
                                if (array[index3].Type > 9L)
                                {
                                    if (!SqlHandler.IsOdd(array[index3].Type))
                                    {
                                        if ((long)this._dbEncoding == 1L)
                                            this._tableEntries[num2 + (int)index1].Content[index3] = Encoding.Default.GetString(this._fileBytes, (int)((long)num4 + num5 + (long)num7), (int)array[index3].Size);
                                        else if ((long)this._dbEncoding == 2L)
                                            this._tableEntries[num2 + (int)index1].Content[index3] = Encoding.Unicode.GetString(this._fileBytes, (int)((long)num4 + num5 + (long)num7), (int)array[index3].Size);
                                        else if ((long)this._dbEncoding == 3L)
                                            this._tableEntries[num2 + (int)index1].Content[index3] = Encoding.BigEndianUnicode.GetString(this._fileBytes, (int)((long)num4 + num5 + (long)num7), (int)array[index3].Size);
                                    }
                                    else
                                        this._tableEntries[num2 + (int)index1].Content[index3] = Encoding.Default.GetString(this._fileBytes, (int)((long)num4 + num5 + (long)num7), (int)array[index3].Size);
                                }
                                else
                                    this._tableEntries[num2 + (int)index1].Content[index3] = Convert.ToString(this.ConvertToULong((int)((long)num4 + num5 + (long)num7), (int)array[index3].Size));
                                num7 += (int)array[index3].Size;
                            }
                        }
                    }
                }
                else if ((int)this._fileBytes[offset] == 5)
                {
                    ushort num1 = (ushort)(this.ConvertToULong((int)((long)offset + 3L), 2) - 1UL);
                    for (ushort index = 0; (int)index <= (int)num1; ++index)
                    {
                        ushort num2 = (ushort)this.ConvertToULong((int)offset + 12 + (int)index * 2, 2);
                        this.ReadTableFromOffset((this.ConvertToULong((int)((long)offset + (long)num2), 4) - 1UL) * this._pageSize);
                    }
                    this.ReadTableFromOffset((this.ConvertToULong((int)((long)offset + 8L), 4) - 1UL) * this._pageSize);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        private void ReadMasterTable(long offset)
        {
            try
            {
                switch (this._fileBytes[offset])
                {
                    case 5:
                        ushort num1 = (ushort)(this.ConvertToULong((int)offset + 3, 2) - 1UL);
                        for (int index = 0; index <= (int)num1; ++index)
                        {
                            ushort num2 = (ushort)this.ConvertToULong((int)offset + 12 + index * 2, 2);
                            if (offset == 100L)
                                this.ReadMasterTable(((long)this.ConvertToULong((int)num2, 4) - 1L) * (long)this._pageSize);
                            else
                                this.ReadMasterTable(((long)this.ConvertToULong((int)(offset + (long)num2), 4) - 1L) * (long)this._pageSize);
                        }
                        this.ReadMasterTable(((long)this.ConvertToULong((int)offset + 8, 4) - 1L) * (long)this._pageSize);
                        break;
                    case 13:
                        ulong num3 = this.ConvertToULong((int)offset + 3, 2) - 1UL;
                        int num4 = 0;
                        if (this._masterTableEntries != null)
                        {
                            num4 = this._masterTableEntries.Length;
                            Array.Resize<SqlHandler.SqliteMasterEntry>(ref this._masterTableEntries, this._masterTableEntries.Length + (int)num3 + 1);
                        }
                        else
                            this._masterTableEntries = new SqlHandler.SqliteMasterEntry[checked((ulong)unchecked((long)num3 + 1L))];
                        for (ulong index1 = 0; index1 <= num3; ++index1)
                        {
                            ulong num2 = this.ConvertToULong((int)offset + 8 + (int)index1 * 2, 2);
                            if (offset != 100L)
                                num2 += (ulong)offset;
                            int endIdx1 = this.Gvl((int)num2);
                            this.Cvl((int)num2, endIdx1);
                            int endIdx2 = this.Gvl((int)((long)num2 + ((long)endIdx1 - (long)num2) + 1L));
                            this.Cvl((int)((long)num2 + ((long)endIdx1 - (long)num2) + 1L), endIdx2);
                            ulong num5 = num2 + (ulong)((long)endIdx2 - (long)num2 + 1L);
                            int endIdx3 = this.Gvl((int)num5);
                            int endIdx4 = endIdx3;
                            long num6 = this.Cvl((int)num5, endIdx3);
                            long[] numArray = new long[5];
                            for (int index2 = 0; index2 <= 4; ++index2)
                            {
                                int startIdx = endIdx4 + 1;
                                endIdx4 = this.Gvl(startIdx);
                                numArray[index2] = this.Cvl(startIdx, endIdx4);
                                numArray[index2] = numArray[index2] <= 9L ? (long)this._sqlDataTypeSize[numArray[index2]] : (!SqlHandler.IsOdd(numArray[index2]) ? (numArray[index2] - 12L) / 2L : (numArray[index2] - 13L) / 2L);
                            }
                            if ((long)this._dbEncoding == 1L || (long)this._dbEncoding == 2L)
                                ;
                            if ((long)this._dbEncoding == 1L)
                                this._masterTableEntries[num4 + (int)index1].ItemName = Encoding.Default.GetString(this._fileBytes, (int)((long)num5 + num6 + numArray[0]), (int)numArray[1]);
                            else if ((long)this._dbEncoding == 2L)
                                this._masterTableEntries[num4 + (int)index1].ItemName = Encoding.Unicode.GetString(this._fileBytes, (int)((long)num5 + num6 + numArray[0]), (int)numArray[1]);
                            else if ((long)this._dbEncoding == 3L)
                                this._masterTableEntries[num4 + (int)index1].ItemName = Encoding.BigEndianUnicode.GetString(this._fileBytes, (int)((long)num5 + num6 + numArray[0]), (int)numArray[1]);
                            this._masterTableEntries[num4 + (int)index1].RootNum = (long)this.ConvertToULong((int)((long)num5 + num6 + numArray[0] + numArray[1] + numArray[2]), (int)numArray[3]);
                            if ((long)this._dbEncoding == 1L)
                                this._masterTableEntries[num4 + (int)index1].SqlStatement = Encoding.Default.GetString(this._fileBytes, (int)((long)num5 + num6 + numArray[0] + numArray[1] + numArray[2] + numArray[3]), (int)numArray[4]);
                            else if ((long)this._dbEncoding == 2L)
                                this._masterTableEntries[num4 + (int)index1].SqlStatement = Encoding.Unicode.GetString(this._fileBytes, (int)((long)num5 + num6 + numArray[0] + numArray[1] + numArray[2] + numArray[3]), (int)numArray[4]);
                            else if ((long)this._dbEncoding == 3L)
                                this._masterTableEntries[num4 + (int)index1].SqlStatement = Encoding.BigEndianUnicode.GetString(this._fileBytes, (int)((long)num5 + num6 + numArray[0] + numArray[1] + numArray[2] + numArray[3]), (int)numArray[4]);
                        }
                        break;
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
                int index1 = -1;
                for (int index2 = 0; index2 <= this._masterTableEntries.Length; ++index2)
                {
                    if (string.Compare(this._masterTableEntries[index2].ItemName.ToLower(), tableName.ToLower(), StringComparison.Ordinal) == 0)
                    {
                        index1 = index2;
                        break;
                    }
                }
                if (index1 == -1)
                    return false;
                string[] strArray = this._masterTableEntries[index1].SqlStatement.Substring(this._masterTableEntries[index1].SqlStatement.IndexOf("(", StringComparison.Ordinal) + 1).Split(',');
                for (int index2 = 0; index2 <= strArray.Length - 1; ++index2)
                {
                    strArray[index2] = strArray[index2].TrimStart();
                    int length = strArray[index2].IndexOf(' ');
                    if (length > 0)
                        strArray[index2] = strArray[index2].Substring(0, length);
                    if (strArray[index2].IndexOf("UNIQUE", StringComparison.Ordinal) != 0)
                    {
                        Array.Resize<string>(ref this._fieldNames, index2 + 1);
                        this._fieldNames[index2] = strArray[index2];
                    }
                }
                return this.ReadTableFromOffset((ulong)(this._masterTableEntries[index1].RootNum - 1L) * this._pageSize);
            }
            catch
            {
                return false;
            }
        }

        private ulong ConvertToULong(int startIndex, int size)
        {
            try
            {
                if (size > 8 | size == 0)
                    return 0;
                ulong num = 0;
                for (int index = 0; index <= size - 1; ++index)
                    num = num << 8 | (ulong)this._fileBytes[startIndex + index];
                return num;
            }
            catch
            {
                return 0;
            }
        }

        private int Gvl(int startIdx)
        {
            try
            {
                if (startIdx > this._fileBytes.Length)
                    return 0;
                for (int index = startIdx; index <= startIdx + 8; ++index)
                {
                    if (index > this._fileBytes.Length - 1)
                        return 0;
                    if (((int)this._fileBytes[index] & 128) != 128)
                        return index;
                }
                return startIdx + 8;
            }
            catch
            {
                return 0;
            }
        }

        private long Cvl(int startIdx, int endIdx)
        {
            try
            {
                ++endIdx;
                byte[] numArray = new byte[8];
                int num1 = endIdx - startIdx;
                bool flag = false;
                if (num1 == 0 | num1 > 9)
                    return 0;
                if (num1 == 1)
                {
                    numArray[0] = (byte)((uint)this._fileBytes[startIdx] & (uint)sbyte.MaxValue);
                    return BitConverter.ToInt64(numArray, 0);
                }
                if (num1 == 9)
                    flag = true;
                int num2 = 1;
                int num3 = 7;
                int index1 = 0;
                if (flag)
                {
                    numArray[0] = this._fileBytes[endIdx - 1];
                    --endIdx;
                    index1 = 1;
                }
                int index2 = endIdx - 1;
                while (index2 >= startIdx)
                {
                    if (index2 - 1 >= startIdx)
                    {
                        numArray[index1] = (byte)((int)this._fileBytes[index2] >> num2 - 1 & (int)byte.MaxValue >> num2 | (int)this._fileBytes[index2 - 1] << num3);
                        ++num2;
                        ++index1;
                        --num3;
                    }
                    else if (!flag)
                        numArray[index1] = (byte)((int)this._fileBytes[index2] >> num2 - 1 & (int)byte.MaxValue >> num2);
                    index2 += -1;
                }
                return BitConverter.ToInt64(numArray, 0);
            }
            catch
            {
                return 0;
            }
        }

        private static bool IsOdd(long value)
        {
            return (value & 1L) == 1L;
        }

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
