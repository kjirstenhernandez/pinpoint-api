using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class Event
    {
        
        public string id { get; set; } = Guid.NewGuid().ToString();
        public string title { get; set; } = string.Empty;
        public string description { get; set; } = string.Empty;
        public string address { get; set; } = string.Empty;
        public double lat { get; set; }
        public double lon { get; set; }
        public EventType type { get; set; }
        public string date { get; set; } = string.Empty;
        public string time { get; set; } = string.Empty;
        public string userId { get; set; } = string.Empty; // navigation property    
        public User? user { get; set; }
    }

    public enum EventType {
        convention,
        seminar,
        fundraiser,
        party,
        sportEvent,
        privateEvent,
        publicEvent,
        workshop,
        festival,
        corporateEvent,
        schoolEvent,
        performance


    }
}