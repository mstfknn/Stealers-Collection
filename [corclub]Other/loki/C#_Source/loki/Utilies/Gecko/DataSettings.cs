namespace Loki.Gecko
{
    public class DataSettings
    {
        public int Id { get; set; }
        public string Hostname { get; set; }
        public object HttpRealm { get; set; }
        public string FormSubmitURL { get; set; }
        public string UsernameField { get; set; }
        public string PasswordField { get; set; }
        public string EncryptedUsername { get; set; }
        public string EncryptedPassword { get; set; }
        public string Guid { get; set; }
        public int EncType { get; set; }
        public long TimeCreated { get; set; }
        public long TimeLastUsed { get; set; }
        public long TimePasswordChanged { get; set; }
        public int TimesUsed { get; set; }
    }
}