using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mis.Models
{
    public class FindDoctorViewModel
    {
        public String FirstName { get; set; }

        public String LastName { get; set; }

        public String Position { get; set; }

        public String HospitalName { get; set; }

        public List<DoctorInfo> Doctors { get; set; }
    }
}
