using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace University.Entities
{
    public class Group
    {
        public Group()
        {
            StudentGroups = new List<StudentGroup>();
        }

        public long Id { get; set; }
        [Required]
        [MaxLength(25)]
        public string Name { get; set; }
        public List<StudentGroup> StudentGroups { get; set; }
    }
}
