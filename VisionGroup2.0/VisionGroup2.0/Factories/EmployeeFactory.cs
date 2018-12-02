using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisionGroup2._0.DomainClasses;
using VisionGroup2._0.Interfaces;

namespace VisionGroup2._0.Factories
{
    class EmployeeFactory : IEmployeeFactory
    {
        public Employee Create()
        {
            return new Employee();
        }

    }
}
