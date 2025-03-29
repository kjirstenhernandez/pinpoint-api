using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Event
{
    public struct BriefEventDto
    // Creates a brief version of the Event, making it more lightweight for an API call
    {
        public string id {get; init;}
        public string title {get; init;}
        public string date {get; init;}
        public string time {get; init;}

    }
}