using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gzpr.Models
{
    public class WellModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public CompanyModel Company {get; set;}
        public WorkshopModel Workshop {get; set;}
        public FieldModel Field {get; set;}
    }
}
