using System.Text.Json.Serialization; 
using System.Collections.Generic;
using System;

namespace CloudFlareDDNS.Models
{ 
    public class ZoneResult    
    {
        [JsonPropertyName("id")]
        public string Id { get; set; } 

        [JsonPropertyName("name")]
        public string Name { get; set; } 

        [JsonPropertyName("development_mode")]
        public int DevelopmentMode { get; set; } 

        [JsonPropertyName("original_name_servers")]
        public List<string> OriginalNameServers { get; set; } 

        [JsonPropertyName("original_registrar")]
        public string OriginalRegistrar { get; set; } 

        [JsonPropertyName("original_dnshost")]
        public string OriginalDnshost { get; set; } 

        [JsonPropertyName("created_on")]
        public DateTime CreatedOn { get; set; } 

        [JsonPropertyName("modified_on")]
        public DateTime ModifiedOn { get; set; } 

        [JsonPropertyName("activated_on")]
        public DateTime ActivatedOn { get; set; } 

        [JsonPropertyName("owner")]
        public object Owner { get; set; } 

        [JsonPropertyName("account")]
        public Account Account { get; set; } 

        [JsonPropertyName("permissions")]
        public List<string> Permissions { get; set; } 

        [JsonPropertyName("plan")]
        public Plan Plan { get; set; } 

        [JsonPropertyName("plan_pending")]
        public PlanPending PlanPending { get; set; } 

        [JsonPropertyName("status")]
        public string Status { get; set; } 

        [JsonPropertyName("paused")]
        public bool Paused { get; set; } 

        [JsonPropertyName("type")]
        public string Type { get; set; } 

        [JsonPropertyName("name_servers")]
        public List<string> NameServers { get; set; } 
    }

}