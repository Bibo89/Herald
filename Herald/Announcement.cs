using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Herald
{
    public class Announcement
    {
        [JsonPropertyName("startup-interval")]
        public int StartupInterval { get; set; }

        [JsonPropertyName("interval")]
        public int Interval { get; set; }

        [JsonPropertyName("message")]
        public string[] Message { get; set; } = Array.Empty<string>();

        [JsonPropertyName("actions")]
        public string[] Actions { get; set; } = Array.Empty<string>();
    }
}
