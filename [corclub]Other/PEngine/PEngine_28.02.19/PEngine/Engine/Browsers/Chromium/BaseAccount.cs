namespace PEngine.Engine.Browsers.Chromium
{
    using Newtonsoft.Json;

    public class BaseAccount
    {
        [JsonProperty("Browser")]
        public string BrowserName { get; set; }

        [JsonProperty("Link")]
        public string Url { get; set; }

        [JsonProperty("Login")]
        public string User { get; set; }

        [JsonProperty("Password")]
        public string Pass { get; set; }
    }
}