using System;
using System.Collections.Generic;

namespace VisionGroup
{
    public partial class Project
    {
        public int ProjectId { get; set; }
        public string Name { get; set; }
        public DateTime? Deadline { get; set; }
        public string ProjectLeader { get; set; }

        public Costumer ProjectNavigation { get; set; }
    }
}
