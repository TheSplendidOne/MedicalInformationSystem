using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Mis.Models
{
    public class Comment
    {
        [Key]
        public String Id { get; set; }

        public String AuthorId { get; set; }

        public String OwnerId { get; set; }

        public String Content { get; set; }

        public DateTime CreationDate { get; set; }

        public ApplicationUser Author { get; set; }

        public ApplicationUser Owner { get; set; }
    }
}
