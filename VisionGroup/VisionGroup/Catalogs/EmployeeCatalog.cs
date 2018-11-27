using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisionGroup.DomainClasses;
using VisionGroup.Interfaces;

namespace VisionGroup.Catalogs
{
    class EmployeeCatalog : ICatalog<Employee>
    {
        public List<Employee> EmployeeList { get; set; }
        public void Add(Employee item)
        {
            EmployeeList.Add(item);
        }

        public void Remove(Employee item)
        {
            EmployeeList.Remove(item);
        }

        public void Update(Employee item)
        {
            EmployeeList[EmployeeList
                .FindIndex(a => a.EmployeeID == item.EmployeeID)] = item;
        }

        public void Load(Employee item)
        {
            throw new System.NotImplementedException();
        }
    }
}
