using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace FSE_API_MODEL
{
    public class EventDetails
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
        public string eventName { get; set; }
        public string eventDescription { get; set; }
        public string baseLocation { get; set; }
        public string beneficiaryName { get; set; }
        public string venueAddress { get; set; }
        public int totalNoVolunteers { get; set; }
        public int totalVolunteHours { get; set; }
        public int totalTravelHours { get; set; }
        public int livesImpacted { get; set; }


        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
