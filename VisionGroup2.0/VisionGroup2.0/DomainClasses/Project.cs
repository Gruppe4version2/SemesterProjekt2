using System;
using System.Collections.Generic;

namespace VisionGroup2._0.DomainClasses
{
    public partial class Project
    {
        public Project()
        {
            ProjectsForEmployees = new HashSet<ProjectsForEmployee>();
        }

        public int ProjectId { get; }
        public string Name { get; set; }
        public DateTime? Deadline { get; set; }
        public int CostumerId { get; set; }

        public Costumer Costumer { get; set; }
        public ICollection<ProjectsForEmployee> ProjectsForEmployees { get; set; }
    }
}
