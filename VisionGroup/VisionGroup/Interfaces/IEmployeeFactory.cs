﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisionGroup.DomainClasses;

namespace VisionGroup.Interfaces
{
    public interface IEmployeeFactory
    {
        Employee Create();
    }
}
