using System.Text.Json.Serialization; 

namespace CloudFlareDDNS.Models
{ 

    public class PlanPending    
    {
        [JsonPropertyName("id")]
        public string Id { get; set; } 

        [JsonPropertyName("name")]
        public string Name { get; set; } 

        [JsonPropertyName("price")]
        public int Price { get; set; } 

        [JsonPropertyName("currency")]
        public string Currency { get; set; } 

        [JsonPropertyName("frequency")]
        public string Frequency { get; set; } 

        [JsonPropertyName("legacy_id")]
        public string LegacyId { get; set; } 

        [JsonPropertyName("is_subscribed")]
        public bool IsSubscribed { get; set; } 

        [JsonPropertyName("can_subscribe")]
        public bool CanSubscribe { get; set; } 
    }

}