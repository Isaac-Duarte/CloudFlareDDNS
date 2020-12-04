using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Threading.Tasks;
using CloudFlareDDNS.Models;
using Flurl;
using Flurl.Http;

namespace CloudFlareDDNS
{
    public class CloudflareApi
    {
        private readonly string _apiKey;
        private readonly string _userEmail;
        private readonly string _baseUrl = "https://api.cloudflare.com/client/v4/";
        private readonly Dictionary<string, string> defaultHeaders;
        
        /// <summary>
        /// Consturctor for CloudFlareAPI
        /// </summary>
        /// <param name="apiKey">Api Key for cloudflare account</param>
        /// <param name="userEmail">User email for cloudflare account</param>
        public CloudflareApi(string apiKey, string userEmail)
        {
            _apiKey = apiKey;
            _userEmail = userEmail;

            // Creates a dictionary of default headers for cloudflare
            defaultHeaders = new Dictionary<string, string>()
            {
                ["X-Auth-Email"] = _userEmail,
                ["X-Auth-Key"] = _apiKey,
                ["Content-Type"] = "application/json"
            };
        }
        
        /// <summary>
        /// Grabs the local IPAddress of the machine
        /// </summary>
        /// <returns></returns>
        public async Task<string> getLocalIPAddress()
        {
            IPHostEntry host = await Dns.GetHostEntryAsync(Dns.GetHostName());

            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }

            throw new Exception("No network adapters with an IPv4 address in the system!");
        }
         
        public async Task<string> getPublicIpAddress()
        {
            return await "http://ip.42.pl/raw"
                .GetStringAsync();
        }
        
        /// <summary>
        /// Returns wether the network is available
        /// </summary>
        /// <returns></returns>
        public bool NetworkAvailable()
        {
            return NetworkInterface.GetIsNetworkAvailable();
        }

        /// <summary>
        /// Returns all the zones of an account
        /// </summary>
        /// <returns></returns>
        public async Task<ZoneResponse> GetZonesAsync()
        {
            return await _baseUrl
                .AppendPathSegment("zones")
                .WithHeaders(defaultHeaders)
                .GetAsync()
                .ReceiveJson<ZoneResponse>();
        }
        
        /// <summary>
        /// Returns a DNS Record list of a zone
        /// </summary>
        /// <param name="zoneId">Which zone to scan</param>
        /// <returns></returns>
        public async Task<DnsResponse> GetDnsList(string zoneId)
        {
            return await _baseUrl
                .AppendPathSegments("zones", zoneId, "dns_records")
                .WithHeaders(defaultHeaders)
                .GetAsync()
                .ReceiveJson<DnsResponse>();
        }

        /// <summary>
        /// Get's information about a record
        /// </summary>
        /// <param name="zoneId">Zone Id of the record</param>
        /// <param name="recordId">Record Id</param>
        /// <returns></returns>
        public async Task<DnsRecordDetails> GetRecordInfo(string zoneId, string recordId)
        {
            return await _baseUrl
                .AppendPathSegments("zones", zoneId, "dns_records", recordId)
                .WithHeaders(defaultHeaders)
                .GetAsync()
                .ReceiveJson<DnsRecordDetails>();
        }

        /// <summary>
        /// Updates a DNS Record by the Id of the DNS Record
        /// </summary>
        /// <param name="zoneId">Zone Id of the record</param>
        /// <param name="recordId">Record Id</param>
        /// <param name="content">What to update with (IP Address)</param>
        /// <returns>The result from the request</returns>
        public async Task<DnsRecordDetails> UpdateDnsRecord(string zoneId, string recordId, string content)
        {
            DnsRecordDetails recordInfo = await GetRecordInfo(zoneId, recordId);

            return await _baseUrl
                .AppendPathSegments("zones", zoneId, "dns_records", recordId)
                .WithHeaders(defaultHeaders)
                .PutJsonAsync(new
                {
                    type = recordInfo.Result.Type,
                    name = recordInfo.Result.Name,
                    content = content,             
                    ttl = recordInfo.Result.Ttl       
                })
                .ReceiveJson<DnsRecordDetails>();
        }
    }
}