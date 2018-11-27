using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisionGroup.DomainClasses;
using VisionGroup.Interfaces;

namespace VisionGroup.Factories
{
    class EmployeeFactory : IEmployeeFactory
    {
        public Employee Create()
        {
            return new Employee();
        }
    }
}
