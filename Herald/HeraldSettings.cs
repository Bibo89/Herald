using Auxiliary.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Herald
{
    public class HeraldSettings : ISettings
    {
        [JsonPropertyName("announcements")]
        public List<Announcement> Announcements { get; set; } = new();
    }
}
