using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisionGroup2._0.DomainClasses;
using VisionGroup2._0.Interfaces;

namespace VisionGroup2._0.Factories
{
    using VisionGroup2._0.Catalogs;

    internal class EmployeeFactory : IFactory
    {
        private EmployeeCatalog _employeeCatalog;
        public EmployeeFactory()
        {
            this.NewEmployee = new Employee();
            this._employeeCatalog = EmployeeCatalog.Instance;
        }

        public Employee NewEmployee { get; set; }
        public bool CanCreate(Employee newEmployee)
        {
            if (newEmployee.Name == null || newEmployee.Email == null)
            {
                return false;
            }
            foreach (Employee employee in this._employeeCatalog.EmployeeList)
            {
                if (newEmployee.Name.Length < 1 || newEmployee.Email.Length < 1 || employee.Email == newEmployee.Email || employee.PhoneNr == newEmployee.PhoneNr)
                {
                    return false;
                }
            }

            NewEmployee = newEmployee;
            return true;
        }

        public void Create()
        {
            if (this.CanCreate(this.NewEmployee))
            {
                this._employeeCatalog.Add(this.NewEmployee);
            }
        }

    }
}
