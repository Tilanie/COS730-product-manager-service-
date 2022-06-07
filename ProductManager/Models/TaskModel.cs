using Newtonsoft.Json;

namespace ProductManager.Models
{
    public class TaskModel: BaseCosmosItem
    {
        [JsonProperty(PropertyName = "WarehouseNumber")]
        public string WarehouseNumber { get; set; }

        [JsonProperty(PropertyName = "CreatedOn")]
        public DateTime CreatedOn { get; set; }

        [JsonProperty(PropertyName = "CreatedBy")]
        public string CreatedBy { get; set; }

        [JsonProperty(PropertyName = "Status")]
        public string Status { get; set; }

        [JsonProperty(PropertyName = "AssignedTo")]
        public string AssignedTo { get; set; }

        [JsonProperty(PropertyName = "Type")]
        public string Type { get; set; }

        public override DataLayerType DataLayerType => DataLayerType.Task;
    }
}
