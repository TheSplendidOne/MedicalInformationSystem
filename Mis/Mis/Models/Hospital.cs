using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Mis.Models
{
    public class Hospital
    {
        [Key]
        public String Id { get; set; }

        public String Name { get; set; }

        public String Description { get; set; }

        public String Address { get; set; }

        public ICollection<DoctorInfo> Doctors { get; set; }
    }
}
