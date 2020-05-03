using Newtonsoft.Json;
using System;

namespace InitData
{
    public class Invoice
    {
        public Invoice()
        {
        }

        [JsonProperty("id")]
        public string ID { get; set; }

        [JsonProperty("mailTo")]
        public string MailTo { get; set; }

        [JsonProperty("phoneTo")]
        public string PhoneTo { get; set; }
    }
}