namespace xoxoxo
{
    public struct Credentials
    {
        public Credentials(string username, string password, string website)
        {
            Username = username;
            Password = password;
            Website = website;
        }

        public string Username { get; set; }

        public string Password { get; set; }

        public string Website { get; set; }
    }
}