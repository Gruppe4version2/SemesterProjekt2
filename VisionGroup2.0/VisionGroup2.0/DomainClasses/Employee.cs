using System;
using System.Collections.Generic;

namespace VisionGroup2._0.DomainClasses
{
    public partial class Employee
    {
        public Employee()
        {
            Projects = new HashSet<Project>();
        }

        public int EmployeeId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int PhoneNr { get; set; }

        public ICollection<Project> Projects { get; set; }
    }
}
