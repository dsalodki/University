using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace University.ViewModels
{
    public class FilterStudentsViewModel
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string UniqueIdentifier { get; set; }
        public string GroupNames { get; set; }
    }
}
