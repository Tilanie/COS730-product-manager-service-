using Newtonsoft.Json;

namespace ProductManager.Models
{
    public class TaskModel
    {
        [JsonProperty(PropertyName = "id")]
        public string id { get; set; }

        [JsonProperty(PropertyName = "warehouse_number")]
        public string WarehouseNumber { get; set; }

        [JsonProperty(PropertyName = "created_on")]
        public DateTime CreatedOn { get; set; }

        [JsonProperty(PropertyName = "created_by")]
        public string CreatedBy { get; set; }

        [JsonProperty(PropertyName = "status")]
        public string Status { get; set; }

        [JsonProperty(PropertyName = "assigned_to")]
        public string AssignedTo { get; set; }

        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }

        public DataLayerType DataLayerType => DataLayerType.Task;
    }
}
