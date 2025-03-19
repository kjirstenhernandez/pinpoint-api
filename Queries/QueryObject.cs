using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Queries
{
    public class QueryObject
    {
        public string? Type {get; set;} = null;
        public string? Date {get; set;} = null;
        public string? Time {get; set;} = null;
    }

    // In progress, will integrate into the Web API later. Using it for filtering. 
}