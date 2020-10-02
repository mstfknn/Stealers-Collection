namespace PEngine.Engine.Browsers.Chromium.Cookies
{
    using Newtonsoft.Json;

    public class BaseCookies
    {
        [JsonProperty(PropertyName = "Application", Required = Required.Always)]
        public string Application { get; set; }

        [JsonProperty(PropertyName = "HostKey", Required = Required.Always)]
        public string HostKey { get; set; }

        [JsonProperty(PropertyName = "Name", Required = Required.Always)]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "Path", Required = Required.Always)]
        public string Path { get; set; }

        [JsonProperty(PropertyName = "Expires_utc", Required = Required.Always)]
        public long Expires_utc { get; set; }

        [JsonProperty(PropertyName = "Last_access_utc", Required = Required.Always)]
        public long Last_access_utc { get; set; }

        [JsonProperty(PropertyName = "EncryptedValue", Required = Required.Always)]
        public string EncryptedValue { get; set; }

        [JsonProperty(PropertyName = "Value", Required = Required.Always)]
        public int Value { get; set; }
    }
}