using Newtonsoft.Json;

namespace ProductManager.Models
{
    public class Product: BaseCosmosItem
    {
        [JsonProperty(PropertyName = "Created")]
        public DateTime Created { get; set; }

        [JsonProperty(PropertyName = "ProductID")]
        public Guid ProductID { get; set; }

        [JsonProperty(PropertyName = "VendorID")]
        public Guid VendorID { get; set; }

        [JsonProperty(PropertyName = "Size")]
        public int Size { get; set; }

        [JsonProperty(PropertyName = "UnitPrice")]
        public double UnitPrice { get; set; }

        [JsonProperty(PropertyName = "ProductName")]
        public string? ProductName { get; set; }

        [JsonProperty(PropertyName = "Colour")]
        public string? Colour { get; set; }

        [JsonProperty(PropertyName = "Category")]
        public string? Category { get; set; }

        [JsonProperty(PropertyName = "Brand")]
        public string? Brand { get; set; }

        [JsonProperty(PropertyName = "Description")]
        public string? Description { get; set; }

        [JsonProperty(PropertyName = "StockLevel")]
        public int? StockLevel { get; set; }

        public override DataLayerType DataLayerType => DataLayerType.Product;
    }
}