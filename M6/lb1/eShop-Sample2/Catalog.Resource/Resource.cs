using System.Text.Json.Serialization;

namespace Catalog.Resource
{
    public class Resource
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        [JsonPropertyName("pantone_value")]
        public string PantoneValue { get; set; }
    }
}
