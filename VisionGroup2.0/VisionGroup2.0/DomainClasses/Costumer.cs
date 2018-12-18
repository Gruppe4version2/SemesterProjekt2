namespace VisionGroup2._0.DomainClasses
{
    using System.Collections.Generic;

    public class Costumer
    {
        public Costumer()
        {
            this.Projects = new HashSet<Project>();
        }

        public int CostumerId { get; set; }

        public int CvrNr { get; set; }

        public string Email { get; set; }

        public string Name { get; set; }

        public int PhoneNr { get; set; }

        public ICollection<Project> Projects { get; set; }
    }
}