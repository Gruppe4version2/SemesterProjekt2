using System;
using System.Collections.Generic;

namespace VisionGroup
{
    public partial class Costumer
    {
        public int CostumerId { get; set; }
        public string Name { get; set; }
        public int CvrNr { get; set; }
        public int PhonNr { get; set; }
        public string Email { get; set; }

        public Project Project { get; set; }
    }
}
