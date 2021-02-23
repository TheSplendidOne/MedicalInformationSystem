using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Mis.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string About { get; set; }

        public DateTime Birthday { get; set; }

        public int UsernameChangeLimit { get; set; } = 10;

        public byte[] ProfilePicture { get; set; }

        public DoctorInfo DoctorInfo { get; set; }

        public ICollection<MeetingCell> MeetingCells { get; set; }

        public ICollection<Comment> AuthorsComments { get; set; }

        public ICollection<Comment> ProfileComments { get; set; }
    }
}
