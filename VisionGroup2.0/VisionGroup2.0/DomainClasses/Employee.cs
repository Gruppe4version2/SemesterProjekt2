namespace VisionGroup2._0.DomainClasses
{
    using System.Collections.Generic;

    public class Employee
    {
        public Employee()
        {
            this.ProjectsForEmployees = new HashSet<ProjectsForEmployee>();
        }

        public string Email { get; set; }

        public int EmployeeId { get; set; }

        public string Name { get; set; }

        public int PhoneNr { get; set; }

        public ICollection<ProjectsForEmployee> ProjectsForEmployees { get; set; }
    }
}