using System.Collections.Generic;

namespace CloudFlareDDNS.Models
{

    public class CloudflareSettings
    {
        public string ApiKey { get; set; }
        public string UserEmail { get; set; }
        public List<RecordSetting> Records { get; set; }
    }
}