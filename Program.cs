using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using CloudFlareDDNS.Models;
using Microsoft.Extensions.Configuration;

namespace CloudFlareDDNS
{
    class Program
    {
        private static CloudflareSettings _settings;
        private static List<DnsRecordDetails> records;
        private static CloudflareApi api;

        static async Task Main(string[] args)
        {
            // Grabs the appsetting config.
            IConfiguration configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables()
                .AddCommandLine(args)
                .Build();
            
            // Binds the dynamic object to the settings model
            _settings = new CloudflareSettings();
            configuration.Bind("Cloudflare", _settings);

            // Creates an instance of the CloudflareAPI object
            api = new CloudflareApi(_settings.ApiKey, _settings.UserEmail);
            
            // Creates a list of DNS Records
            records = new List<DnsRecordDetails>();

            /// Loops through the records wanting to be watched and updated
            foreach (var record in _settings.Records)
            {
                records.Add(await api.GetRecordInfo(record.ZoneId, record.RecordId));
            }

            // Starts a timer that goes off every 10 minutes to check for updates
            var timer = new System.Timers.Timer(10 * 1000);
            timer.Elapsed += new System.Timers.ElapsedEventHandler(timerPassed);
            timer.Start();

            // Check for an update now
            await checkForUpdates();

            Console.ReadLine();
        }
        
        private async static void timerPassed(object source, System.Timers.ElapsedEventArgs e)
        {
            await checkForUpdates();
        }

        /// <summary>
        /// Checks for an update using the list in the config
        /// </summary>
        /// <returns></returns>
        static async Task checkForUpdates()
        {
            // Grabs the machines current public IP Address
            string ip = await api.getPublicIpAddress();
            
            // Loops through the records and update if nessary
            foreach (DnsRecordDetails record in records)
            {
                if (record.Success && record.Result.Content != ip)
                {
                    Console.WriteLine($"Updated Record {record.Result.Name}. Old = {record.Result.Content} New = {ip}");

                    // Some LINQ Shit because of list
                    var obj = records.FirstOrDefault(rec => rec.Result.Id == record.Result.Id);
                    obj = await api.UpdateDnsRecord(_settings.Records.FirstOrDefault(rec => rec.RecordId == record.Result.Id).ZoneId, record.Result.Id, ip);
                }
            }
        }
    }
}
