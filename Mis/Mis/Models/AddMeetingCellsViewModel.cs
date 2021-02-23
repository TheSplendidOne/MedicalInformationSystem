using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Mis.Models
{
    public class AddMeetingCellsViewModel
    {
        public String DoctorId { get; set; }

        public DateTime MeetingDate { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public Int32 MeetingTimeSpan { get; set; }
    }
}
