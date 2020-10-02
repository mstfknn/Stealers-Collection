using System;

namespace xoxoxo.Firefox
{
    public struct FirefoxPassword
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public Uri Host { get; set; }
    }
}