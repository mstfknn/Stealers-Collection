using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using System;
using System.IO;
using System.Text;

namespace SimpleChromepwrec
{
    public partial class SQLiteHandler
    {
        private readonly byte[] db_bytes;
        private readonly ulong encoding;
        private string[] field_names;
        private Sqlite_master_entry[] master_table_entries;
        private readonly ushort page_size;
        private readonly byte[] SQLDataTypeSize = new byte[] { 0, 1, 2, 3, 4, 6, 8, 8, 0, 0 };
        private Table_entry[] table_entries;

        public SQLiteHandler(string baseName)
        {
            if (File.Exists(baseName))
            {
                FileSystem.FileOpen(1, baseName, OpenMode.Binary, OpenAccess.Read, OpenShare.Shared, -1);
                string str = Strings.Space((int)FileSystem.LOF(1));
                FileSystem.FileGet(1, ref str, -1, false);
                FileSystem.FileClose(new int[] { 1 });
                this.db_bytes = Encoding.Default.GetBytes(str);
                if (Encoding.Default.GetString(this.db_bytes, 0, 15).CompareTo("SQLite format 3") != 0)
                {
                    throw new Exception("Not a valid SQLite 3 Database File");
                }
                if (this.db_bytes[0x34] != 0)
                {
                    throw new Exception("Auto-vacuum capable database is not supported");
                }
                this.page_size = (ushort)this.ConvertToInteger(0x10, 2);
                this.encoding = this.ConvertToInteger(0x38, 4);
                if (decimal.Compare(new decimal(this.encoding), decimal.Zero) == 0)
                {
                    this.encoding = 1;
                }
                this.ReadMasterTable(100);
            }
        }

        private ulong ConvertToInteger(int startIndex, int Size)
        {
            if ((Size > 8) | (Size == 0))
            {
                return 0;
            }
            ulong num2 = 0;
            int num4 = Size - 1;
            for (int i = 0; i <= num4; i++)
            {
                num2 = (num2 << 8) | this.db_bytes[startIndex + i];
            }
            return num2;
        }

        private long CVL(int startIndex, int endIndex)
        {
            endIndex++;
            byte[] buffer = new byte[8];
            int num4 = endIndex - startIndex;
            bool flag = false;
            if ((num4 == 0) | (num4 > 9))
            {
                return 0L;
            }
            switch (num4)
            {
                case 1:
                    buffer[0] = (byte)(this.db_bytes[startIndex] & 0x7f);
                    return BitConverter.ToInt64(buffer, 0);
                case 9:
                    flag = true;
                    break;
                default:
                    break;
            }
            int num2 = 1;
            int num3 = 7;
            int index = 0;
            if (flag)
            {
                buffer[0] = this.db_bytes[endIndex - 1];
                endIndex--;
                index = 1;
            }
            for (int i = endIndex - 1; i >= startIndex; i += -1)
            {
                if ((i - 1) < startIndex)
                {
                    if (!flag)
                    {
                        buffer[index] = (byte)(((byte)(this.db_bytes[i] >> ((num2 - 1) & 7))) & (0xff >> num2));
                    }
                }
                else
                {
                    buffer[index] = (byte)((((byte)(this.db_bytes[i] >> ((num2 - 1) & 7))) & (0xff >> num2)) | ((byte)(this.db_bytes[i - 1] << (num3 & 7))));
                    num2++;
                    index++;
                    num3--;
                }
            }
            return BitConverter.ToInt64(buffer, 0);
        }

        public int GetRowCount() => this.table_entries.Length;

        public string[] GetTableNames()
        {
            string[] strArray2 = null;
            int index = 0;
            for (int i = 0; i <= this.master_table_entries.Length - 1; i++)
            {
                if (this.master_table_entries[i].item_type == "table")
                {
                    strArray2 = (string[])Utils.CopyArray(strArray2, new string[index + 1]);
                    strArray2[index] = this.master_table_entries[i].item_name;
                    index++;
                }
            }
            return strArray2;
        }

        public string GetValue(int row_num, int field) =>
        row_num >= this.table_entries.Length ? null : field >= this.table_entries[row_num].content.Length ? null : this.table_entries[row_num].content[field];

        public string GetValue(int row_num, string field)
        {
            int num = -1;
            for (int i = 0; i <= this.field_names.Length - 1; i++)
            {
                if (this.field_names[i].ToLower().CompareTo(field.ToLower()) == 0)
                {
                    num = i;
                    break;
                }
            }
            return num == -1 ? null : this.GetValue(row_num, num);
        }

        private int GVL(int startIndex)
        {
            if (startIndex > this.db_bytes.Length)
            {
                return 0;
            }
            for (int i = startIndex; i <= startIndex + 8; i++)
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

        private bool IsOdd(long value) => ((value & 1L) == 1L);

        private void ReadMasterTable(ulong Offset)
        {
            switch (this.db_bytes[(int)Offset])
            {
                case 13:
                    {
                        ushort num2 = Convert.ToUInt16(decimal.Subtract(new decimal(this.ConvertToInteger(Convert.ToInt32(decimal.Add(new decimal(Offset), 3M)), 2)), decimal.One));
                        int length = 0;
                        switch (this.master_table_entries)
                        {
                            case null:
                                this.master_table_entries = new Sqlite_master_entry[num2 + 1];
                                break;
                            default:
                                length = this.master_table_entries.Length;
                                this.master_table_entries = (Sqlite_master_entry[])Utils.CopyArray(this.master_table_entries, new Sqlite_master_entry[(this.master_table_entries.Length + num2) + 1]);
                                break;
                        }
                        for (int i = 0; i <= num2; i++)
                        {
                            ulong num = this.ConvertToInteger(Convert.ToInt32(decimal.Add(decimal.Add(new decimal(Offset), 8M), new decimal(i * 2))), 2);
                            if (decimal.Compare(new decimal(Offset), 100M) != 0)
                            {
                                num += Offset;
                            }
                            int endIndex = this.GVL((int)num);
                            long num7 = this.CVL((int)num, endIndex);
                            int num6 = this.GVL(Convert.ToInt32(decimal.Add(decimal.Add(new decimal(num), decimal.Subtract(new decimal(endIndex), new decimal(num))), decimal.One)));
                            this.master_table_entries[length + i].row_id = this.CVL(Convert.ToInt32(decimal.Add(decimal.Add(new decimal(num), decimal.Subtract(new decimal(endIndex), new decimal(num))), decimal.One)), num6);
                            num = Convert.ToUInt64(decimal.Add(decimal.Add(new decimal(num), decimal.Subtract(new decimal(num6), new decimal(num))), decimal.One));
                            endIndex = this.GVL((int)num);
                            num6 = endIndex;
                            long num5 = this.CVL((int)num, endIndex);
                            long[] numArray = new long[5];
                            int index = 0;
                            do
                            {
                                endIndex = num6 + 1;
                                num6 = this.GVL(endIndex);
                                numArray[index] = this.CVL(endIndex, num6);
                                numArray[index] = numArray[index] <= 9L
                                    ? this.SQLDataTypeSize[(int)numArray[index]]
                                    : this.IsOdd(numArray[index])
                                        ? (long)Math.Round((numArray[index] - 13L) / 2.0)
                                        : (long)Math.Round((numArray[index] - 12L) / 2.0);
                                index++;
                            }
                            while (index <= 4);
                            switch (decimal.Compare(new decimal(this.encoding), decimal.One))
                            {
                                case 0:
                                    this.master_table_entries[length + i].item_type = Encoding.Default.GetString(this.db_bytes, Convert.ToInt32(decimal.Add(new decimal(num), new decimal(num5))), (int)numArray[0]);
                                    break;
                                default:
                                    switch (decimal.Compare(new decimal(this.encoding), 2M))
                                    {
                                        case 0:
                                            this.master_table_entries[length + i].item_type = Encoding.Unicode.GetString(this.db_bytes, Convert.ToInt32(decimal.Add(new decimal(num), new decimal(num5))), (int)numArray[0]);
                                            break;
                                        default:
                                            if (decimal.Compare(new decimal(this.encoding), 3M) == 0)
                                            {
                                                this.master_table_entries[length + i].item_type = Encoding.BigEndianUnicode.GetString(this.db_bytes, Convert.ToInt32(decimal.Add(new decimal(num), new decimal(num5))), (int)numArray[0]);
                                            }

                                            break;
                                    }

                                    break;
                            }
                            if (decimal.Compare(new decimal(this.encoding), decimal.One) != 0)
                            {
                                if (decimal.Compare(new decimal(this.encoding), 2M) != 0)
                                {
                                    if (decimal.Compare(new decimal(this.encoding), 3M) == 0)
                                    {
                                        this.master_table_entries[length + i].item_name = Encoding.BigEndianUnicode.GetString(this.db_bytes, Convert.ToInt32(decimal.Add(decimal.Add(new decimal(num), new decimal(num5)), new decimal(numArray[0]))), (int)numArray[1]);
                                    }
                                }
                                else
                                {
                                    this.master_table_entries[length + i].item_name = Encoding.Unicode.GetString(this.db_bytes, Convert.ToInt32(decimal.Add(decimal.Add(new decimal(num), new decimal(num5)), new decimal(numArray[0]))), (int)numArray[1]);
                                }
                            }
                            else
                            {
                                this.master_table_entries[length + i].item_name = Encoding.Default.GetString(this.db_bytes, Convert.ToInt32(decimal.Add(decimal.Add(new decimal(num), new decimal(num5)), new decimal(numArray[0]))), (int)numArray[1]);
                            }
                            this.master_table_entries[length + i].root_num = (long)this.ConvertToInteger(Convert.ToInt32(decimal.Add(decimal.Add(decimal.Add(decimal.Add(new decimal(num), new decimal(num5)), new decimal(numArray[0])), new decimal(numArray[1])), new decimal(numArray[2]))), (int)numArray[3]);
                            if (decimal.Compare(new decimal(this.encoding), decimal.One) != 0)
                            {
                                if (decimal.Compare(new decimal(this.encoding), 2M) != 0)
                                {
                                    if (decimal.Compare(new decimal(this.encoding), 3M) == 0)
                                    {
                                        this.master_table_entries[length + i].sql_statement = Encoding.BigEndianUnicode.GetString(this.db_bytes, Convert.ToInt32(decimal.Add(decimal.Add(decimal.Add(decimal.Add(decimal.Add(new decimal(num), new decimal(num5)), new decimal(numArray[0])), new decimal(numArray[1])), new decimal(numArray[2])), new decimal(numArray[3]))), (int)numArray[4]);
                                    }
                                }
                                else
                                {
                                    this.master_table_entries[length + i].sql_statement = Encoding.Unicode.GetString(this.db_bytes, Convert.ToInt32(decimal.Add(decimal.Add(decimal.Add(decimal.Add(decimal.Add(new decimal(num), new decimal(num5)), new decimal(numArray[0])), new decimal(numArray[1])), new decimal(numArray[2])), new decimal(numArray[3]))), (int)numArray[4]);
                                }
                            }
                            else
                            {
                                this.master_table_entries[length + i].sql_statement = Encoding.Default.GetString(this.db_bytes, Convert.ToInt32(decimal.Add(decimal.Add(decimal.Add(decimal.Add(decimal.Add(new decimal(num), new decimal(num5)), new decimal(numArray[0])), new decimal(numArray[1])), new decimal(numArray[2])), new decimal(numArray[3]))), (int)numArray[4]);
                            }
                        }

                        break;
                    }

                case 5:
                    {
                        for (int j = 0; j <= Convert.ToUInt16(decimal.Subtract(new decimal(this.ConvertToInteger(Convert.ToInt32(decimal.Add(new decimal(Offset), 3M)), 2)), decimal.One)); j++)
                        {
                            ushort startIndex = (ushort)this.ConvertToInteger(Convert.ToInt32(decimal.Add(decimal.Add(new decimal(Offset), 12M), new decimal(j * 2))), 2);
                            switch (decimal.Compare(new decimal(Offset), 100M))
                            {
                                case 0:
                                    this.ReadMasterTable(Convert.ToUInt64(decimal.Multiply(decimal.Subtract(new decimal(this.ConvertToInteger(startIndex, 4)), decimal.One), new decimal(this.page_size))));
                                    break;
                                default:
                                    this.ReadMasterTable(Convert.ToUInt64(decimal.Multiply(decimal.Subtract(new decimal(this.ConvertToInteger((int)(Offset + startIndex), 4)), decimal.One), new decimal(this.page_size))));
                                    break;
                            }
                        }
                        this.ReadMasterTable(Convert.ToUInt64(decimal.Multiply(decimal.Subtract(new decimal(this.ConvertToInteger(Convert.ToInt32(decimal.Add(new decimal(Offset), 8M)), 4)), decimal.One), new decimal(this.page_size))));
                        break;
                    }
            }
        }

        public bool ReadTable(string TableName)
        {
            int index = -1;
            for (int i = 0; i <= this.master_table_entries.Length - 1; i++)
            {
                if (this.master_table_entries[i].item_name.ToLower().CompareTo(TableName.ToLower()) == 0)
                {
                    index = i;
                    break;
                }
            }
            if (index == -1)
            {
                return false;
            }
            string[] strArray = this.master_table_entries[index].sql_statement.Substring(this.master_table_entries[index].sql_statement.IndexOf("(") + 1).Split(new char[] { ',' });
            for (int j = 0; j <= strArray.Length - 1; j++)
            {
                strArray[j] = (strArray[j]).TrimStart();
                int num4 = strArray[j].IndexOf(" ");
                if (num4 > 0)
                {
                    strArray[j] = strArray[j].Substring(0, num4);
                }
                if (strArray[j].IndexOf("UNIQUE") == 0)
                {
                    break;
                }
                this.field_names = (string[])Utils.CopyArray(this.field_names, new string[j + 1]);
                this.field_names[j] = strArray[j];
            }
            return this.ReadTableFromOffset((ulong)((this.master_table_entries[index].root_num - 1L) * this.page_size));
        }

        private bool ReadTableFromOffset(ulong Offset)
        {
            if (this.db_bytes[(int)Offset] != 13)
            {
                if (this.db_bytes[(int)Offset] == 5)
                {
                    for (int m = 0; m <= Convert.ToUInt16(decimal.Subtract(new decimal(this.ConvertToInteger(Convert.ToInt32(decimal.Add(new decimal(Offset), 3M)), 2)), decimal.One)); m++)
                    {
                        ushort num13 = (ushort)this.ConvertToInteger(Convert.ToInt32(decimal.Add(decimal.Add(new decimal(Offset), 12M), new decimal(m * 2))), 2);
                        this.ReadTableFromOffset(Convert.ToUInt64(decimal.Multiply(decimal.Subtract(new decimal(this.ConvertToInteger((int)(Offset + num13), 4)), decimal.One), new decimal(this.page_size))));
                    }
                    this.ReadTableFromOffset(Convert.ToUInt64(decimal.Multiply(decimal.Subtract(new decimal(this.ConvertToInteger(Convert.ToInt32(decimal.Add(new decimal(Offset), 8M)), 4)), decimal.One), new decimal(this.page_size))));
                }
            }
            else
            {
                int num2 = Convert.ToInt32(decimal.Subtract(new decimal(this.ConvertToInteger(Convert.ToInt32(decimal.Add(new decimal(Offset), 3)), 2)), decimal.One));
                int length = 0;
                switch (this.table_entries)
                {
                    case null:
                        this.table_entries = new Table_entry[num2 + 1];
                        break;
                    default:
                        length = this.table_entries.Length;
                        this.table_entries = (Table_entry[])Utils.CopyArray(this.table_entries, new Table_entry[(this.table_entries.Length + num2) + 1]);
                        break;
                }
                for (int i = 0; i <= num2; i++)
                {
                    Record_header_field[] _fieldArray = null;
                    ulong num = this.ConvertToInteger(Convert.ToInt32(decimal.Add(decimal.Add(new decimal(Offset), 8M), new decimal(i * 2))), 2);
                    if (decimal.Compare(new decimal(Offset), 100) != 0)
                    {
                        num += Offset;
                    }
                    int endIndex = this.GVL((int)num);
                    long num9 = this.CVL((int)num, endIndex);
                    int num8 = this.GVL(Convert.ToInt32(decimal.Add(decimal.Add(new decimal(num), decimal.Subtract(new decimal(endIndex), new decimal(num))), decimal.One)));
                    this.table_entries[length + i].row_id = this.CVL(Convert.ToInt32(decimal.Add(decimal.Add(new decimal(num), decimal.Subtract(new decimal(endIndex), new decimal(num))), decimal.One)), num8);
                    num = Convert.ToUInt64(decimal.Add(decimal.Add(new decimal(num), decimal.Subtract(new decimal(num8), new decimal(num))), decimal.One));
                    endIndex = this.GVL((int)num);
                    num8 = endIndex;
                    long num7 = this.CVL((int)num, endIndex);
                    long num10 = Convert.ToInt64(decimal.Add(decimal.Subtract(new decimal(num), new decimal(endIndex)), decimal.One));
                    for (int j = 0; num10 < num7; j++)
                    {
                        _fieldArray = (Record_header_field[])Utils.CopyArray(_fieldArray, new Record_header_field[j + 1]);
                        endIndex = num8 + 1;
                        num8 = this.GVL(endIndex);
                        _fieldArray[j].type = this.CVL(endIndex, num8);
                        _fieldArray[j].size = _fieldArray[j].type <= 9L
                            ? this.SQLDataTypeSize[(int)_fieldArray[j].type]
                            : this.IsOdd(_fieldArray[j].type)
                                ? (long)Math.Round((_fieldArray[j].type - 13) / 2.0)
                                : (long)Math.Round((_fieldArray[j].type - 12) / 2.0);
                        num10 = (num10 + (num8 - endIndex)) + 1L;
                    }
                    this.table_entries[length + i].content = new string[(_fieldArray.Length - 1) + 1];
                    int num4 = 0;
                    for (int k = 0; k <= _fieldArray.Length - 1; k++)
                    {
                        if (_fieldArray[k].type > 9)
                        {
                            if (!this.IsOdd(_fieldArray[k].type))
                            {
                                if (decimal.Compare(new decimal(this.encoding), decimal.One) != 0)
                                {
                                    if (decimal.Compare(new decimal(this.encoding), 2) != 0)
                                    {
                                        if (decimal.Compare(new decimal(this.encoding), 3) == 0)
                                        {
                                            this.table_entries[length + i].content[k] = Encoding.BigEndianUnicode.GetString(this.db_bytes, Convert.ToInt32(decimal.Add(decimal.Add(new decimal(num), new decimal(num7)), new decimal(num4))), (int)_fieldArray[k].size);
                                        }
                                    }
                                    else
                                    {
                                        this.table_entries[length + i].content[k] = Encoding.Unicode.GetString(this.db_bytes, Convert.ToInt32(decimal.Add(decimal.Add(new decimal(num), new decimal(num7)), new decimal(num4))), (int)_fieldArray[k].size);
                                    }
                                }
                                else
                                {
                                    this.table_entries[length + i].content[k] = Encoding.Default.GetString(this.db_bytes, Convert.ToInt32(decimal.Add(decimal.Add(new decimal(num), new decimal(num7)), new decimal(num4))), (int)_fieldArray[k].size);
                                }
                            }
                            else
                            {
                                this.table_entries[length + i].content[k] = Encoding.Default.GetString(this.db_bytes, Convert.ToInt32(decimal.Add(decimal.Add(new decimal(num), new decimal(num7)), new decimal(num4))), (int)_fieldArray[k].size);
                            }
                        }
                        else
                        {
                            this.table_entries[length + i].content[k] = Conversions.ToString(this.ConvertToInteger(Convert.ToInt32(decimal.Add(decimal.Add(new decimal(num), new decimal(num7)), new decimal(num4))), (int)_fieldArray[k].size));
                        }
                        num4 += (int)_fieldArray[k].size;
                    }
                }
            }
            return true;
        }
    }
}
