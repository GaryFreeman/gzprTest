using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gzpr.Helpers
{
    public class WellRequest
    {
        public int id { get; set; }
        public string? name { get; set; }
        public int? company_id { get; set; }
        public int? workshop_id { get; set; }
        public int? field_id { get; set; }
    }
}
