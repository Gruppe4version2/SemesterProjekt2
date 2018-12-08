using System;
using System.Collections.Generic;

namespace VisionGroup2._0.DomainClasses
{
    public partial class Employee
    {
        public Employee()
        {
            ProjectsForEmployees = new HashSet<ProjectsForEmployee>();
        }

        public int EmployeeId { get; }
        public string Name { get; set; }
        public int PhoneNr { get; set; }
        public string Email { get; set; }

        public ICollection<ProjectsForEmployee> ProjectsForEmployees { get; set; }
    }
}
