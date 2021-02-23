using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mis.Models
{
    public class ShowMeetingCellsViewModel
    {
        public String DoctorId { get; set; }

        public List<MeetingCell> Cells { get; set; }
    }
}
