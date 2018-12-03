using System;
using System.Collections.Generic;

namespace VisionGroup2._0.DomainClasses
{
    public partial class ProjectsForEmployee
    {
        public int ProjectId { get; set; }
        public int EmployeeId { get; set; }
        public bool IsLeader { get; set; }

        public Employee Employee { get; set; }
        public Project Project { get; set; }
    }
}
