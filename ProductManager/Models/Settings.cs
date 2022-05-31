using Newtonsoft.Json;

namespace ProductManager.Models
{
    public class Settings
    {
        [JsonProperty(PropertyName = "Account")]
        public string Account { get; set; }

        [JsonProperty(PropertyName = "Key")]
        public string Key { get; set; }

        [JsonProperty(PropertyName = "DatabaseName")]
        public string DatabaseName { get; set; }

        [JsonProperty(PropertyName = "ContainerName")]
        public string ContainerName { get; set; }

        public Settings()
        {
            Account = String.Empty;
            Key = String.Empty;
            DatabaseName = String.Empty;
            ContainerName = String.Empty;
        }
    }
}
