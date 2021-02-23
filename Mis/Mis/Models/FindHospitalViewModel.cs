using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mis.Models
{
    public class FindHospitalViewModel
    {
        public String Name { get; set; }

        public String Address { get; set; }

        public List<Hospital> Hospitals { get; set; }
    }
}
