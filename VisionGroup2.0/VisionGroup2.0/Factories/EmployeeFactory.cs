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
            foreach (Employee employee in this._employeeCatalog.EmployeeList)
            {
                if (this.NewEmployee.Name.Length < 1 || this.NewEmployee.Email.Length < 1 || employee.Email == this.NewEmployee.Email || employee.PhoneNr == this.NewEmployee.PhoneNr)
                {
                    return false;
                }
            }

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
