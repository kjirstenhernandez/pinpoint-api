using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Event
{
    public struct BriefEventDto
    {
        public string id {get; init;}
        public string title {get; init;}
        public string date {get; init;}
        public string time {get; init;}

    }
}