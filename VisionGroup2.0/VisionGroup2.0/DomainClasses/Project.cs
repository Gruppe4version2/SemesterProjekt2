using System;
using System.Collections.Generic;
using VisionGroup2._0.DomainClasses;

namespace VisionGroup2._0.DomainClasses
{
    public partial class Project
    {
        public int ProjectId { get; set; }
        public string Name { get; set; }
        public DateTime? Deadline { get; set; }
        public string ProjectLeader { get; set; }
        public int CostumerId { get; set; }
        public int EmployeeId { get; set; }

        public Costumer Costumer { get; set; }
        public Employee Employee { get; set; }
    }
}
