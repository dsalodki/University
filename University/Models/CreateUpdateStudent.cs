﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace University.Models
{
    public class CreateUpdateStudent
    {
        public bool Sex { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string UniqueIdentifier { get; set; }
    }
}
