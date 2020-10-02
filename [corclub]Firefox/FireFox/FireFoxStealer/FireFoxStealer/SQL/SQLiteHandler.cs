using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

public class SQLiteHandler
{
    #region bytes
    private byte[] byTes;
    private ulong Encouding;
    private string[] Fld_n;
    private sqlite_master_entry[] mte;
    private ushort ps;
    private byte[] SQLDTS = new byte[] { 0, 1, 2, 3, 4, 6, 8, 8, 0, 0 };
    private table_entry[] te;
    private SchemaFormat format;
    #endregion bytes
    #region ReadTable
    public SQLiteHandler(string bn)
    {
        if (File.Exists(bn))
        {
            FileSystem.FileOpen(1, bn, OpenMode.Binary, OpenAccess.Read, OpenShare.Shared, -1);
            string str = Strings.Space((int)FileSystem.LOF(1));
            FileSystem.FileGet(1, ref str, -1, false);
            FileSystem.FileClose(new int[] { 1 });
            this.byTes = Encoding.Default.GetBytes(str);
            if (Encoding.Default.GetString(this.byTes, 0, 15).CompareTo("SQLite format 3") != 0)
            {
                throw new Exception("Not a valid SQLite 3 Database File");
            }
            if (this.byTes[0x34] != 0) // 52 bytes
            {
                throw new Exception("Auto-vacuum capable database is not supported");
            }
            decimal checker = new decimal(this.ConvertToInteger(44, 4));
            format = (SchemaFormat)checker;
            /*if (decimal.Compare(checker, 4M) >= 0)
            {
                throw new Exception("No supported Schema layer file-format");
             } */
            this.ps = (ushort)this.ConvertToInteger(0x10, 2); // 16
            this.Encouding = this.ConvertToInteger(0x38, 4); // 56
            if (decimal.Compare(new decimal(this.Encouding), decimal.Zero) == 0)
            {
                this.Encouding = 1;
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
        ulong num = 0;
        int num2 = Size - 1;
        for (int i = 0; i <= num2; i++)
        {
            num = (num << 8) | this.byTes[startIndex + i];
        }
        return num;
    }
    private long CVL(int startIndex, int endIndex)
    {
        endIndex++;
        byte[] numArray = new byte[8];
        int num = endIndex - startIndex;
        bool flag = false;
        if ((num == 0) | (num > 9))
        {
            return 0;
        }
        if (num == 1)
        {
            numArray[0] = (byte)(this.byTes[startIndex] & (int) 0x7f);
            return BitConverter.ToInt64(numArray, 0);
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
            numArray[0] = this.byTes[endIndex - 1];
            endIndex--;
            index = 1;
        }
        int num5 = startIndex;
        for (int i = endIndex - 1; i >= num5; i += -1)
        {
            if ((i - 1) >= startIndex)
            {
                numArray[index] = (byte)((((byte)(this.byTes[i] >> ((num2 - 1) & 7))) & (((int)0xff) >> num2)) | ((byte)(this.byTes[i - 1] << (num3 & 7))));
                num2++;
                index++;
                num3--;
            }
            else if (!flag)
            {
                numArray[index] = (byte)(((byte)(this.byTes[i] >> ((num2 - 1) & 7))) & (((int)0xff) >> num2));
            }
        }
        return BitConverter.ToInt64(numArray, 0);
    }
    public int GetRowCount()
    {
        return this.te.Length;
    }
    public string[] GetTableNames()
    {
        string[] arySrc = null;
        int index = 0;
        int num2 = this.mte.Length - 1;
        for (int i = 0; i <= num2; i++)
        {
            if (this.mte[i].item_type == "table")
            {
                arySrc = (string[])Utils.CopyArray(arySrc, new string[index + 1]);
                arySrc[index] = this.mte[i].item_name;
                index++;
            }
        }
        return arySrc;
    }
    public string GetValue(int row_num, int field)
    {
        if (row_num >= this.te.Length)
        {
            return null;
        }
        if (field >= this.te[row_num].content.Length)
        {
            return null;
        }
        return this.te[row_num].content[field];
    }
    public string GetValue(int row_num, string field)
    {
        int num = -1;
        int length = this.Fld_n.Length;
        for (int i = 0; i <= length; i++)
        {
            if (this.Fld_n[i].ToLower().CompareTo(field.ToLower()) == 0)
            {
                num = i;
                break;
            }
        }
        if (num == -1)
        {
            return null;
        }
        return this.GetValue(row_num, num);
    }
    private int GVL(int startIndex)
    {
        if (startIndex > this.byTes.Length)
        {
            return 0;
        }
        int num = startIndex + 8;
        for (int i = startIndex; i <= num; i++)
        {
            if (i > (this.byTes.Length - 1))
            {
                return 0;
            }
            if ((this.byTes[i] & 0x80) != 0x80) // 128 bytes
            {
                return i;
            }
        }
        return (startIndex + 8);
    }
    private bool IsOdd(long value)
    {
        return ((value & 1) == 1);
    }
    private void ReadMasterTable(ulong Offset)
    {
        if (this.byTes[(int)Offset] == 13)
        {
            ushort num = Convert.ToUInt16(decimal.Subtract(new decimal(this.ConvertToInteger(Convert.ToInt32(decimal.Add(new decimal(Offset), 3)), 2)), 1));
            int length = 0;
            if (this.mte != null)
            {
                length = this.mte.Length;
                this.mte = (sqlite_master_entry[])Utils.CopyArray(this.mte, new sqlite_master_entry[(this.mte.Length + num) + 1]);
            }
            else
            {
                this.mte = new sqlite_master_entry[num + 1];
            }
            int num3 = num;
            for (int i = 0; i <= num3; i++)
            {
                ulong num5 = this.ConvertToInteger(Convert.ToInt32(decimal.Add(decimal.Add(new decimal(Offset), 8), new decimal(i * 2))), 2);
                if (decimal.Compare(new decimal(Offset), 100) != 0)
                {
                    num5 += Offset;
                }
                int endIndex = this.GVL((int)num5);
                long num7 = this.CVL((int)num5, endIndex);
                int num8 = this.GVL(Convert.ToInt32(decimal.Add(decimal.Add(new decimal(num5), decimal.Subtract(new decimal(endIndex), new decimal(num5))), 1)));
                this.mte[length + i].row_id = this.CVL(Convert.ToInt32(decimal.Add(decimal.Add(new decimal(num5), decimal.Subtract(new decimal(endIndex), new decimal(num5))), 1)), num8);
                num5 = Convert.ToUInt64(decimal.Add(decimal.Add(new decimal(num5), decimal.Subtract(new decimal(num8), new decimal(num5))), 1));
                endIndex = this.GVL((int)num5);
                num8 = endIndex;
                long num9 = this.CVL((int)num5, endIndex);
                long[] numArray = new long[5];
                int index = 0;
                do
                {
                    endIndex = num8 + 1;
                    num8 = this.GVL(endIndex);
                    numArray[index] = this.CVL(endIndex, num8);
                    if (numArray[index] > 9)
                    {
                        if (this.IsOdd(numArray[index]))
                        {
                            numArray[index] = (long)Math.Round((double)(((double)(numArray[index] - 13)) / 2.0));
                        }
                        else
                        {
                            numArray[index] = (long)Math.Round((double)(((double)(numArray[index] - 12)) / 2.0));
                        }
                    }
                    else
                    {
                        numArray[index] = this.SQLDTS[(int)numArray[index]];
                    }
                    index++;
                }
                while (index <= 4);
                if (decimal.Compare(new decimal(this.Encouding), 1) == 0)
                {
                    this.mte[length + i].item_type = Encoding.Default.GetString(this.byTes, Convert.ToInt32(decimal.Add(new decimal(num5), new decimal(num9))), (int)numArray[0]);
                }
                else if (decimal.Compare(new decimal(this.Encouding), 2) == 0)
                {
                    this.mte[length + i].item_type = Encoding.Unicode.GetString(this.byTes, Convert.ToInt32(decimal.Add(new decimal(num5), new decimal(num9))), (int)numArray[0]);
                }
                else if (decimal.Compare(new decimal(this.Encouding), 3) == 0)
                {
                    this.mte[length + i].item_type = Encoding.BigEndianUnicode.GetString(this.byTes, Convert.ToInt32(decimal.Add(new decimal(num5), new decimal(num9))), (int)numArray[0]);
                }
                if (decimal.Compare(new decimal(this.Encouding), 1) == 0)
                {
                    this.mte[length + i].item_name = Encoding.Default.GetString(this.byTes, Convert.ToInt32(decimal.Add(decimal.Add(new decimal(num5), new decimal(num9)), new decimal(numArray[0]))), (int)numArray[1]);
                }
                else if (decimal.Compare(new decimal(this.Encouding), 2) == 0)
                {
                    this.mte[length + i].item_name = Encoding.Unicode.GetString(this.byTes, Convert.ToInt32(decimal.Add(decimal.Add(new decimal(num5), new decimal(num9)), new decimal(numArray[0]))), (int)numArray[1]);
                }
                else if (decimal.Compare(new decimal(this.Encouding), 3) == 0)
                {
                    this.mte[length + i].item_name = Encoding.BigEndianUnicode.GetString(this.byTes, Convert.ToInt32(decimal.Add(decimal.Add(new decimal(num5), new decimal(num9)), new decimal(numArray[0]))), (int)numArray[1]);
                }
                this.mte[length + i].root_num = (long)this.ConvertToInteger(Convert.ToInt32(decimal.Add(decimal.Add(decimal.Add(decimal.Add(new decimal(num5), new decimal(num9)), new decimal(numArray[0])), new decimal(numArray[1])), new decimal(numArray[2]))), (int)numArray[3]);
                if (decimal.Compare(new decimal(this.Encouding), 1) == 0)
                {
                    this.mte[length + i].sql_statement = Encoding.Default.GetString(this.byTes, Convert.ToInt32(decimal.Add(decimal.Add(decimal.Add(decimal.Add(decimal.Add(new decimal(num5), new decimal(num9)), new decimal(numArray[0])), new decimal(numArray[1])), new decimal(numArray[2])), new decimal(numArray[3]))), (int)numArray[4]);
                }
                else if (decimal.Compare(new decimal(this.Encouding), 2) == 0)
                {
                    this.mte[length + i].sql_statement = Encoding.Unicode.GetString(this.byTes, Convert.ToInt32(decimal.Add(decimal.Add(decimal.Add(decimal.Add(decimal.Add(new decimal(num5), new decimal(num9)), new decimal(numArray[0])), new decimal(numArray[1])), new decimal(numArray[2])), new decimal(numArray[3]))), (int)numArray[4]);
                }
                else if (decimal.Compare(new decimal(this.Encouding), 3) == 0)
                {
                    this.mte[length + i].sql_statement = Encoding.BigEndianUnicode.GetString(this.byTes, Convert.ToInt32(decimal.Add(decimal.Add(decimal.Add(decimal.Add(decimal.Add(new decimal(num5), new decimal(num9)), new decimal(numArray[0])), new decimal(numArray[1])), new decimal(numArray[2])), new decimal(numArray[3]))), (int)numArray[4]);
                }
            }
        }
        else if (this.byTes[(int)Offset] == 5)
        {
            int num12 = Convert.ToUInt16(decimal.Subtract(new decimal(this.ConvertToInteger(Convert.ToInt32(decimal.Add(new decimal(Offset), 3)), 2)), 1));
            for (int j = 0; j <= num12; j++)
            {
                ushort startIndex = (ushort)this.ConvertToInteger(Convert.ToInt32(decimal.Add(decimal.Add(new decimal(Offset), 12), new decimal(j * 2))), 2);
                if (decimal.Compare(new decimal(Offset), 100) == 0)
                {
                    this.ReadMasterTable(Convert.ToUInt64(decimal.Multiply(decimal.Subtract(new decimal(this.ConvertToInteger(startIndex, 4)), 1), new decimal(this.ps))));
                }
                else
                {
                    this.ReadMasterTable(Convert.ToUInt64(decimal.Multiply(decimal.Subtract(new decimal(this.ConvertToInteger((int)(Offset + startIndex), 4)), 1), new decimal(this.ps))));
                }
            }
            this.ReadMasterTable(Convert.ToUInt64(decimal.Multiply(decimal.Subtract(new decimal(this.ConvertToInteger(Convert.ToInt32(decimal.Add(new decimal(Offset), 8)), 4)), 1), new decimal(this.ps))));
        }
    }
    public bool ReadTable(string TableName)
    {
        int index = -1;
        int length = this.mte.Length;
        for (int i = 0; i <= length; i++)
        {
            if (this.mte[i].item_name.ToLower().CompareTo(TableName.ToLower()) == 0)
            {
                index = i;
                break;
            }
        }
        if (index == -1)
        {
            return false;
        }
        string[] strArray = this.mte[index].sql_statement.Substring(this.mte[index].sql_statement.IndexOf("(") + 1).Split(new char[] { ',' });
        int num4 = strArray.Length - 1;
        for (int j = 0; j <= num4; j++)
        {
            strArray[j] = Strings.Trim(strArray[j]);
            int num6 = strArray[j].IndexOf(" ");
            if (num6 > 0)
            {
                strArray[j] = strArray[j].Substring(0, num6);
            }
            if (strArray[j].IndexOf("UNIQUE") == 0)
            {
                break;
            }
            this.Fld_n = (string[])Utils.CopyArray(this.Fld_n, new string[j + 1]);
            this.Fld_n[j] = strArray[j];
        }
        return this.ReadTableFromOffset((ulong)((this.mte[index].root_num - 1) * this.ps));
    }
    private bool ReadTableFromOffset(ulong Offset)
    {
        if (this.byTes[(int)Offset] == 13)
        {
            ushort num = Convert.ToUInt16(decimal.Subtract(new decimal(this.ConvertToInteger(Convert.ToInt32(decimal.Add(new decimal(Offset), 3)), 2)), 1));
            int length = 0;
            if (this.te != null)
            {
                length = this.te.Length;
                this.te = (table_entry[])Utils.CopyArray(this.te, new table_entry[(this.te.Length + num) + 1]);
            }
            else
            {
                this.te = new table_entry[num + 1];
            }
            int num3 = num;
            for (int i = 0; i <= num3; i++)
            {
                record_header_field[] arySrc = null;
                ulong num5 = this.ConvertToInteger(Convert.ToInt32(decimal.Add(decimal.Add(new decimal(Offset), 8), new decimal(i * 2))), 2);
                if (decimal.Compare(new decimal(Offset), 100) != 0)
                {
                    num5 += Offset;
                }
                int endIndex = this.GVL((int)num5);
                long num7 = this.CVL((int)num5, endIndex);
                int num8 = this.GVL(Convert.ToInt32(decimal.Add(decimal.Add(new decimal(num5), decimal.Subtract(new decimal(endIndex), new decimal(num5))), 1)));
                this.te[length + i].row_id = this.CVL(Convert.ToInt32(decimal.Add(decimal.Add(new decimal(num5), decimal.Subtract(new decimal(endIndex), new decimal(num5))), 1)), num8);
                num5 = Convert.ToUInt64(decimal.Add(decimal.Add(new decimal(num5), decimal.Subtract(new decimal(num8), new decimal(num5))), 1));
                endIndex = this.GVL((int)num5);
                num8 = endIndex;
                long num9 = this.CVL((int)num5, endIndex);
                long num10 = Convert.ToInt64(decimal.Add(decimal.Subtract(new decimal(num5), new decimal(endIndex)), 1));
                for (int j = 0; num10 < num9; j++)
                {
                    arySrc = (record_header_field[])Utils.CopyArray(arySrc, new record_header_field[j + 1]);
                    endIndex = num8 + 1;
                    num8 = this.GVL(endIndex);
                    arySrc[j].type = this.CVL(endIndex, num8);
                    if (arySrc[j].type > 9)
                    {
                        if (this.IsOdd(arySrc[j].type))
                        {
                            arySrc[j].size = (long)Math.Round((double)(((double)(arySrc[j].type - 13)) / 2.0));
                        }
                        else
                        {
                            arySrc[j].size = (long)Math.Round((double)(((double)(arySrc[j].type - 12)) / 2.0));
                        }
                    }
                    else
                    {
                        arySrc[j].size = this.SQLDTS[(int)arySrc[j].type];
                    }
                    num10 = (num10 + (num8 - endIndex)) + 1L;
                }
                this.te[length + i].content = new string[(arySrc.Length - 1) + 1];
                int num12 = 0;
                int num13 = arySrc.Length - 1;
                for (int k = 0; k <= num13; k++)
                {
                    if (arySrc[k].type > 9)
                    {
                        if (!this.IsOdd(arySrc[k].type))
                        {
                            if (decimal.Compare(new decimal(this.Encouding), 1) == 0)
                            {
                                this.te[length + i].content[k] = Encoding.Default.GetString(this.byTes, Convert.ToInt32(decimal.Add(decimal.Add(new decimal(num5), new decimal(num9)), new decimal(num12))), (int)arySrc[k].size);
                            }
                            else if (decimal.Compare(new decimal(this.Encouding), 2) == 0)
                            {
                                this.te[length + i].content[k] = Encoding.Unicode.GetString(this.byTes, Convert.ToInt32(decimal.Add(decimal.Add(new decimal(num5), new decimal(num9)), new decimal(num12))), (int)arySrc[k].size);
                            }
                            else if (decimal.Compare(new decimal(this.Encouding), 3) == 0)
                            {
                                this.te[length + i].content[k] = Encoding.BigEndianUnicode.GetString(this.byTes, Convert.ToInt32(decimal.Add(decimal.Add(new decimal(num5), new decimal(num9)), new decimal(num12))), (int)arySrc[k].size);
                            }
                        }
                        else
                        {
                            this.te[length + i].content[k] = Encoding.Default.GetString(this.byTes, Convert.ToInt32(decimal.Add(decimal.Add(new decimal(num5), new decimal(num9)), new decimal(num12))), (int)arySrc[k].size);
                        }
                    }
                    else
                    {
                        this.te[length + i].content[k] = Conversions.ToString(this.ConvertToInteger(Convert.ToInt32(decimal.Add(decimal.Add(new decimal(num5), new decimal(num9)), new decimal(num12))), (int)arySrc[k].size));
                    }
                    num12 += (int)arySrc[k].size;
                }
            }
        }
        else if (this.byTes[(int)Offset] == 5)
        {
            int num16 = Convert.ToUInt16(decimal.Subtract(new decimal(this.ConvertToInteger(Convert.ToInt32(decimal.Add(new decimal(Offset), 3)), 2)), 1));
            for (int m = 0; m <= num16; m++)
            {
                ushort num18 = (ushort)this.ConvertToInteger(Convert.ToInt32(decimal.Add(decimal.Add(new decimal(Offset), 12), new decimal(m * 2))), 2);
                this.ReadTableFromOffset(Convert.ToUInt64(decimal.Multiply(decimal.Subtract(new decimal(this.ConvertToInteger((int)(Offset + num18), 4)), 1), new decimal(this.ps))));
            }
            this.ReadTableFromOffset(Convert.ToUInt64(decimal.Multiply(decimal.Subtract(new decimal(this.ConvertToInteger(Convert.ToInt32(decimal.Add(new decimal(Offset), 8)), 4)), 1), new decimal(this.ps))));
        }
        return true;
    }
    #endregion ReadTable
    #region Structure
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
    #endregion
    #region Enums
    enum SchemaFormat : int
    {
        /// <summary>
        /// Format 1 is understood by all versions of SQLite back to version 3.0.0.
        /// </summary>
        Format1 = 1,
        /// <summary>
        /// Format 2 adds the ability of rows within the same table to have a varying number of columns, in order to support the ALTER TABLE ... ADD COLUMN functionality. Support for reading and writing format 2 was added in SQLite version 3.1.3 on 2005-02-19.
        /// </summary>
        Format2 = 2,
        /// <summary>
        /// Format 3 adds the ability of extra columns added by ALTER TABLE ... ADD COLUMN to have non-NULL default values. This capability was added in SQLite version 3.1.4 on 2005-03-11.
        /// </summary>
        Format3 = 3,
        /// <summary>
        /// Format 4 causes SQLite to respect the DESC keyword on index declarations. (The DESC keyword is ignored in indexes for formats 1, 2, and 3.) Format 4 also adds two new boolean record type values (serial types 8 and 9). Support for format 4 was added in SQLite 3.3.0 on 2006-01-10.
        /// </summary>
        Format4 = 4
    }
    #endregion Enums
}