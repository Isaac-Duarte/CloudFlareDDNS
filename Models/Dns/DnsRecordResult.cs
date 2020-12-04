using System;
using System.Text.Json.Serialization; 

namespace CloudFlareDDNS.Models
{ 
    public class DnsRecordResult    
    {
        [JsonPropertyName("id")]
        public string Id { get; set; } 

        [JsonPropertyName("type")]
        public string Type { get; set; } 

        [JsonPropertyName("name")]
        public string Name { get; set; } 

        [JsonPropertyName("content")]
        public string Content { get; set; } 

        [JsonPropertyName("proxiable")]
        public bool Proxiable { get; set; } 

        [JsonPropertyName("proxied")]
        public bool Proxied { get; set; } 

        [JsonPropertyName("ttl")]
        public int Ttl { get; set; } 

        [JsonPropertyName("locked")]
        public bool Locked { get; set; } 

        [JsonPropertyName("zone_id")]
        public string ZoneId { get; set; } 

        [JsonPropertyName("zone_name")]
        public string ZoneName { get; set; } 

        [JsonPropertyName("created_on")]
        public DateTime CreatedOn { get; set; } 

        [JsonPropertyName("modified_on")]
        public DateTime ModifiedOn { get; set; } 

        [JsonPropertyName("data")]
        public object Data { get; set; } 

        [JsonPropertyName("meta")]
        public Meta Meta { get; set; } 
    }

}