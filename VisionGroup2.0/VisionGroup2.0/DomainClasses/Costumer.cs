using System;
using System.Collections.Generic;

namespace VisionGroup2._0.DomainClasses
{
    public partial class Costumer
    {
        public Costumer()
        {
            Projects = new HashSet<Project>();
        }

        public int CostumerId { get; set; }
        public string Name { get; set; }
        public int CvrNr { get; set; }
        public int PhoneNr { get; set; }
        public string Email { get; set; }

        public ICollection<Project> Projects { get; set; }
    }
}
