using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class User
    {
        public string id {get; set; } = Guid.NewGuid().ToString();
        public string username { get; set; } = string.Empty;
        public string firstName {get; set; } = string.Empty;
        public string lastName {get; set; } = string.Empty;
        public int zipcode { get; set; }
        public string email { get; set; } = string.Empty;
        public string phone { get; set; } = string.Empty;

        public List<Event> Events {get; set; } = new List<Event>();

    }
}