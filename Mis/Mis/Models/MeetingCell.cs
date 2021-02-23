using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Mis.Models
{
    public class MeetingCell
    {
        [Key]
        public String Id { get; set; }

        public String DoctorId { get; set; }

        public String PatientId { get; set; }

        public DateTime MeetingDateTime { get; set; }

        public DoctorInfo Doctor { get; set; }

        public ApplicationUser Patient { get; set; }
    }
}
