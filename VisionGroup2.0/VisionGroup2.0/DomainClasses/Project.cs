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

        public int ProjectId { get; set; }
        public string Name { get; set; }
        public DateTime? Deadline { get; set; }
        public string ProjectLeader { get; set; }
        public int CostumerId { get; set; }

        public Costumer Costumer { get; set; }
        public ICollection<ProjectsForEmployee> ProjectsForEmployees { get; set; }
    }
}
