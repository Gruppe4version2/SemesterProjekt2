namespace VisionGroup2._0.DomainClasses
{
    public class ProjectsForEmployee
    {
        public Employee Employee { get; set; }

        public int EmployeeId { get; set; }

        public bool IsLeader { get; set; }

        public Project Project { get; set; }

        public int ProjectId { get; set; }
    }
}