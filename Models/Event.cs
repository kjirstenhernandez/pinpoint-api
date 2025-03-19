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
        public double lat { get; set; } // latitude of address, will be used in next sprint for GIS mapping
        public double lon { get; set; } // longitude of address, will be used in next sprint for GIS mapping
        public EventType type { get; set; }
        public string date { get; set; } = string.Empty; // later validation specifies DD/MM/YYYY format.
        public string time { get; set; } = string.Empty; // HH:00, 24 hour format
        public string userId { get; set; } = string.Empty; // navigation property    
        public User? user { get; set; } // user that created the event; using as a foreign key for cascade.  
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