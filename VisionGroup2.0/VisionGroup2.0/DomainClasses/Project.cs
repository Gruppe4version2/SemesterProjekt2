namespace VisionGroup2._0.DomainClasses
{
    using System;
    using System.Collections.Generic;

    public class Project
    {
        public Project()
        {
            this.ProjectsForEmployees = new HashSet<ProjectsForEmployee>();
        }

        public Costumer Costumer { get; set; }

        public int CostumerId { get; set; }

        public DateTime? Deadline { get; set; }

        public string Name { get; set; }

        public int ProjectId { get; set; }

        public ICollection<ProjectsForEmployee> ProjectsForEmployees { get; set; }
    }
}