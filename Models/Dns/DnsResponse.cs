using System.Text.Json.Serialization; 
using System.Collections.Generic; 

namespace CloudFlareDDNS.Models
{ 
    public class DnsResponse    
    {
        [JsonPropertyName("success")]
        public bool Success { get; set; } 

        [JsonPropertyName("errors")]
        public List<object> Errors { get; set; } 

        [JsonPropertyName("messages")]
        public List<object> Messages { get; set; } 

        [JsonPropertyName("result")]
        public List<DnsRecordResult> Result { get; set; } 
    }

}