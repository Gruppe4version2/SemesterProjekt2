using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisionGroup2._0.DomainClasses;
using VisionGroup2._0.Interfaces;

namespace VisionGroup2._0.Catalogs
{
    public class EmployeeCatalog : ICatalog<Employee>
    {
        public List<Employee> EmployeeList { get; set; }

        public void Add(Employee item)
        {
            EmployeeList.Add(item);
        }

        public void Load(Employee item)
        {
            throw new NotImplementedException();
        }

        public void Remove(Employee item)
        {
            EmployeeList.Remove(item);
        }

        public void Update(Employee item)
        {
            EmployeeList[EmployeeList
                .FindIndex(a => a.EmployeeId == item.EmployeeId)] = item;
        }
    }
}
