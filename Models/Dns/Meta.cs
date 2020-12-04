using System.Text.Json.Serialization; 

namespace CloudFlareDDNS.Models
{ 
    public class Meta    
    {
        [JsonPropertyName("auto_added")]
        public bool AutoAdded { get; set; } 

        [JsonPropertyName("source")]
        public string Source { get; set; } 
    }

}