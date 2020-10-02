namespace ISteal.Cookies
{
    internal class Cookie
    {
        public Cookie() { }

        public string Domain { get; set; }
        public string ExpirationDate { get; set; }
        public string HostOnly { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public string Secure { get; set; }
        public string Value { get; set; }
    }
}