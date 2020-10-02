namespace Loki.Gecko
{
    public class LoginSettings
    {
        public int NextId { get; set; }

        private DataSettings[] logins;

        public DataSettings[] GetLogins() => this.logins;

        public void SetLogins(DataSettings[] value) => this.logins = value;

        private object[] disabledHosts;

        public object[] GetDisabledHosts() => this.disabledHosts;

        public void SetDisabledHosts(object[] value) => this.disabledHosts = value;

        public int Version { get; set; }
    }
}