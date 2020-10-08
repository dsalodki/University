using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace University.Entities
{
    public class Student
    {
        public Student()
        {
            StudentGroups = new List<StudentGroup>();
        }

        public long Id { get; set; }
        [Required]
        public bool Sex { get; set; }
        [MaxLength(40)]
        [Required]
        public string LastName { get; set; }
        [MaxLength(40)]
        [Required]
        public string FirstName { get; set; }
        [MaxLength(60)]
        public string MiddleName { get; set; }
        [MinLength(6)]
        [MaxLength(16)]
        public string UniqueIdentifier { get; set; }
        public List<StudentGroup> StudentGroups { get; set; }
    }
}
