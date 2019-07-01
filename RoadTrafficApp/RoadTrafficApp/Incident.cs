using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace RoadTrafficApp
{
    public class Incident
    {
        [JsonProperty("lat")]
        public string Latitude { get; set; }
        [JsonProperty("lng")]
        public string Longitude { get; set; }
        [JsonProperty("shortdesc")]
        public string ShortDescription { get; set; }
        public ParameterizedDescription ParameterizedDescription { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public int Type { get; set; }
        public int Severity { get; set; }
        public bool Impacting { get; set; }
        public double Distance { get; set; }
    }
}
