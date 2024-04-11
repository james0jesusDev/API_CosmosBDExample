using Newtonsoft.Json;

namespace API_CosmosBDExample.Models
{
    public class Elementos
    {
        [JsonProperty(PropertyName ="id")]
        public string Id { get; set; }
        [JsonProperty(PropertyName ="category")]
        public string Category { get; set; }
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }
        [JsonProperty(PropertyName = "iscomplete")]
        public string IsComplete { get; set; }

    }
}
