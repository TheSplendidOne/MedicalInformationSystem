using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Mis.Models
{
    public class DoctorInfo
    {
        [Key]
        public String UserId { get; set; }

        public String Position { get; set; }

        public String HospitalId { get; set; }

        public Hospital Hospital { get; set; }

        public ApplicationUser User { get; set; }

        public ICollection<MeetingCell> MeetingCells { get; set; }
    }
}
