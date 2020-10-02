using System;
using System.Collections.Generic;
using  System.Text;

using System.Runtime.InteropServices;
using  System.Data;
using System.Collections;

namespace SQLiteWrapper
{
     //Firefox 3.5 and 3.6 Decryptor
    //Coded by DarkSel
     //Email and MSN: darksel.ltd@live.com
    //Websites:
     //http://www.hackforums.net
    //http://www.leetcoders.org
     //Made January 30, 2010

    //Credits to CodeProject for the  SQLiteWrapper class
    //Shouts to Steve, Son1cBl4st, Farhan878,  Fenimin, Aeonhack,
    //Albertino, JapaBrz, and all the other coders  I grew up with!

    //If you used this in your program, add me  in the credits!

    public class SQLiteBase
    {
         // imports system functions for work with pointers
         [DllImport("kernel32")]
        private extern static IntPtr  HeapAlloc(IntPtr heap, UInt32 flags, UInt32 bytes);

         [DllImport("kernel32")]
        private extern static IntPtr  GetProcessHeap();

        [DllImport("kernel32")]
         private extern static int lstrlen(IntPtr str);

        // imports  SQLite functions
        [DllImport("sqlite3")]
        private  static extern int sqlite3_open(IntPtr fileName, out IntPtr database);

         [DllImport("sqlite3")]
        private static extern int  sqlite3_close(IntPtr database);

        [DllImport("sqlite3")]
         private static extern int sqlite3_exec(IntPtr database, IntPtr  query, IntPtr callback, IntPtr arguments, out IntPtr error);

         [DllImport("sqlite3")]
        private static extern IntPtr  sqlite3_errmsg(IntPtr database);

        [DllImport("sqlite3")]
         private static extern int sqlite3_prepare_v2(IntPtr database,  IntPtr query, int length, out IntPtr statement, out IntPtr tail);

         [DllImport("sqlite3")]
        private static extern int  sqlite3_step(IntPtr statement);

        [DllImport("sqlite3")]
         private static extern int sqlite3_column_count(IntPtr statement);

         [DllImport("sqlite3")]
        private static extern IntPtr  sqlite3_column_name(IntPtr statement, int columnNumber);

         [DllImport("sqlite3")]
        private static extern int  sqlite3_column_type(IntPtr statement, int columnNumber);

         [DllImport("sqlite3")]
        private static extern int  sqlite3_column_int(IntPtr statement, int columnNumber);

         [DllImport("sqlite3")]
        private static extern double  sqlite3_column_double(IntPtr statement, int columnNumber);

         [DllImport("sqlite3")]
        private static extern IntPtr  sqlite3_column_text(IntPtr statement, int columnNumber);

         [DllImport("sqlite3")]
        private static extern IntPtr  sqlite3_column_blob(IntPtr statement, int columnNumber);

         [DllImport("sqlite3")]
        private static extern IntPtr  sqlite3_column_table_name(IntPtr statement, int columnNumber);

         [DllImport("sqlite3")]
        private static extern int  sqlite3_finalize(IntPtr handle);

        // SQLite constants 
         private const int SQL_OK = 0;
        private const int  SQL_ROW = 100;
        private const int SQL_DONE = 101;

         /// <summary>
        /// SQLite data types.
        ///  </summary>
        public enum SQLiteDataTypes
        {
             /// <summary>
            /// Integer numbers.
             /// </summary>
            INT = 1,
            ///  <summary>
            /// Decimal numbers.
            ///  </summary>
            FLOAT,
            ///  <summary>
            /// All kinds of texts.
             /// </summary>
            TEXT,
            ///  <summary>
            /// Blob objects - binary large objects.
             /// </summary>
            BLOB,
            ///  <summary>
            /// Nothing.
            ///  </summary>
            NULL
        };

        //  pointer to database
        private IntPtr database;

         /// <summary>
        /// Creates new instance of SQLiteBase  class with no database attached.
        /// </summary>
         public SQLiteBase()
        {
            database =  IntPtr.Zero;
        }

        /// <summary>
         /// Creates new instance of SQLiteBase class and opens database with  given name.
        /// </summary>
        /// <param  name="baseName">Name (and path) to SQLite database file</param>
         public SQLiteBase(String baseName)
        {
             OpenDatabase(baseName);
        }

        /// <summary>
         /// Opens database. 
        /// </summary>
         /// <param name="baseName">Name of database file</param>
         public void OpenDatabase(String baseName)
        {
             // opens database 
            if  (sqlite3_open(StringToPointer(baseName), out database) != SQL_OK)
             {
                // if there is some error, database  pointer is set to 0 and exception is throws
                database =  IntPtr.Zero;
                throw new Exception("Error with opening  database " + baseName + "!");
            }
        }

         /// <summary>
        /// Closes opened database.
         /// </summary>
        public void CloseDatabase()
        {
             // closes the database if there is one opened
             if (database != IntPtr.Zero)
            {
                 sqlite3_close(database);
            }
        }

         /// <summary>
        /// Returns the list of tables in opened  database.
        /// </summary>
        ///  <returns></returns>
        public ArrayList GetTables()
         {
            // executes query that select names of all  tables and views in master table of every database
            String  query = "SELECT name FROM sqlite_master " +
                                         "WHERE type IN ('table','view') AND name NOT LIKE  'sqlite_%'" +
                                        "UNION ALL " +
                                         "SELECT name FROM  sqlite_temp_master " +
                                        "WHERE  type IN ('table','view') " +
                                         "ORDER BY 1";
            DataTable table = ExecuteQuery(query);

             // when table is generater, it writes all table names in list  that is returned
            ArrayList list = new ArrayList();
             foreach (DataRow row in table.Rows)
            {
                 list.Add(row.ItemArray[0].ToString());
            }
             return list;
        }

        /// <summary>
         /// Executes query that does not return anything (e.g. UPDATE,  INSERT, DELETE).
        /// </summary>
        ///  <param name="query"></param>
        public void  ExecuteNonQuery(String query)
        {
            // calles  SQLite function that executes non-query
            IntPtr error;
             sqlite3_exec(database, StringToPointer(query), IntPtr.Zero,  IntPtr.Zero, out error);
            // if there is error, excetion  is thrown
            if (error != IntPtr.Zero)
                 throw new Exception("Error with executing non-query: \"" + query +  "\"!\n" + PointerToString(sqlite3_errmsg(error)));
        }

         /// <summary>
        /// Executes query that does  return something (e.g. SELECT).
        /// </summary>
         /// <param name="query"></param>
        ///  <returns></returns>
        public DataTable  ExecuteQuery(String query)
        {
            // processed  query
            IntPtr statement;

            // excess  data, it has no use
            IntPtr excessData;

             // process query and make statement
             sqlite3_prepare_v2(database, StringToPointer(query),  GetPointerLenght(StringToPointer(query)), out statement, out  excessData);

            // table for result of function
             DataTable table = new DataTable();

            // reads  first row - it is different from next rows because it also creates table  columns
            // result - returns SLQ_ROW while there is next  row
            int result = ReadFirstRow(statement, ref table);

             // reads rows
            while (result == SQL_ROW)
             {
                result = ReadNextRow(statement, ref  table);
            }

            // finalize executing this  query
            sqlite3_finalize(statement);

            //  returns table
            return table;
        }

         // private function for reading firs row and creating DataTable
         private int ReadFirstRow(IntPtr statement, ref DataTable table)
         {
            // create new instance of DataTable with name  "resultTable"
            table = new DataTable("resultTable");

             // evaluates statement 
            int resultType =  sqlite3_step(statement);

            // if result of statement is  SQL_ROW, create new table and write row in it
            if  (resultType == SQL_ROW)
            {
                // returns  number of columns returned by statement
                int  columnCount = sqlite3_column_count(statement);

                //  declartaion of variables for reading first row
                 String columnName = "";
                int columnType = 0;
                 object[] columnValues = new object[columnCount];

                 // reads columns one by one
                for (int i = 0; i  < columnCount; i++)
                {
                    //  returns the name of current column
                    columnName =  PointerToString(sqlite3_column_name(statement, i));

                     // returns the type of current column
                     columnType = sqlite3_column_type(statement, i);

                     // checks type of columns - neccessary because different functions  are required for different types
                    switch  (columnType)
                    {
                        // in  case of integer column
                        case  (int)SQLiteDataTypes.INT:
                            {
                                 // adds new integer column to table
                                 table.Columns.Add(columnName,  Type.GetType("System.Int32"));

                                //  writes column value in object array
                                 columnValues[i] = sqlite3_column_int(statement, i);
                                 break;
                            }
                         // same as for integer, this one is for float
                         case (int)SQLiteDataTypes.FLOAT:
                             {
                                table.Columns.Add(columnName,  Type.GetType("System.Single"));
                                 columnValues[i] = sqlite3_column_double(statement, i);
                                 break;
                            }
                         // ... for text
                        case  (int)SQLiteDataTypes.TEXT:
                            {
                                 table.Columns.Add(columnName,  Type.GetType("System.String"));
                                 columnValues[i] = PointerToString(sqlite3_column_text(statement, i));
                                 break;
                            }
                         // ... for blob - blob are written in table as  strings!!
                        case (int)SQLiteDataTypes.BLOB:
                             {
                                 table.Columns.Add(columnName, Type.GetType("System.String"));
                                 columnValues[i] =  PointerToString(sqlite3_column_blob(statement, i));
                                 break;
                            }
                         // in case of something other, value is read as string
                         default:
                            {
                                 table.Columns.Add(columnName,  Type.GetType("System.String"));
                                 columnValues[i] = "";
                                break;
                             }
                    }
                }

                 // writes column values to table
                 table.Rows.Add(columnValues);
            }

            //  evalute statemnet for next results
            return  sqlite3_step(statement);
        }

        // private function  for reading rows other than first
        // it' same like first  row, only without creating table and columns
        private int  ReadNextRow(IntPtr statement, ref DataTable table)
        {
             int columnCount = sqlite3_column_count(statement);

             int columnType = 0;
            object[] columnValues = new  object[columnCount];

            for (int i = 0; i <  columnCount; i++)
            {
                columnType =  sqlite3_column_type(statement, i);

                switch  (columnType)
                {
                    case  (int)SQLiteDataTypes.INT:
                        {
                             columnValues[i] = sqlite3_column_int(statement, i);
                             break;
                        }
                     case (int)SQLiteDataTypes.FLOAT:
                        {
                             columnValues[i] =  sqlite3_column_double(statement, i);
                             break;
                        }
                    case  (int)SQLiteDataTypes.TEXT:
                        {
                             columnValues[i] =  PointerToString(sqlite3_column_text(statement, i));
                             break;
                        }
                     case (int)SQLiteDataTypes.BLOB:
                        {
                             columnValues[i] =  PointerToString(sqlite3_column_blob(statement, i));
                             break;
                        }
                     default:
                        {
                             columnValues[i] = "";
                            break;
                         }
                }
            }
             table.Rows.Add(columnValues);
            return  sqlite3_step(statement);
        }

        // converts string  to pointer
        private IntPtr StringToPointer(String str)
         {
            // if string is null, pointer is 0
            if  (str == null)
            {
                return IntPtr.Zero;
             }
            else
            {
                //  else, convert it to pointer
                Encoding encoding =  Encoding.UTF8;
                Byte[] bytes = encoding.GetBytes(str);
                 int length = bytes.Length + 1;
                IntPtr  pointer = HeapAlloc(GetProcessHeap(), 0, (UInt32)length);
                 Marshal.Copy(bytes, 0, pointer, bytes.Length);
                 Marshal.WriteByte(pointer, bytes.Length, 0);
                return  pointer;
            }
        }

        // convert pointer  to string
        private String PointerToString(IntPtr ptr)
         {
            if (ptr == IntPtr.Zero)
                return  null;

            Encoding encoding = Encoding.UTF8;

             int length = GetPointerLenght(ptr);
            Byte[] bytes =  new Byte[length];
            Marshal.Copy(ptr, bytes, 0, length);
             return encoding.GetString(bytes, 0, length);
        }

         // returns length of pointer
        private int  GetPointerLenght(IntPtr ptr)
        {
            if (ptr ==  IntPtr.Zero)
                return 0;
            return  lstrlen(ptr);
        }
    }
}