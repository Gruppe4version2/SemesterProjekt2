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
        private List<Employee> _employeeList;
        public List<Employee> EmployeeList
        {
            get
            {
                if (this._employeeList != null)
                {
                    return this._employeeList;
                }
                else
                {
                    Load();
                    return this._employeeList;

                }
            }
            set
            {
                this._employeeList = value;
            }
        }


        public void Add(Employee item)
        {
            using (var db = new DbContextVisionGroup())
            {
                db.Employees.Add(item);
                db.SaveChanges();
            }
        }

        public void Remove(Employee item)
        {
            using (var db = new DbContextVisionGroup())
            {
                db.Employees.Remove(item);
                db.SaveChanges();
            }
        }




        public void Update(Employee item)
        {
            using (var db = new DbContextVisionGroup())
            {
                db.Employees.Update(item);
                db.SaveChanges();
            }
        }

        public void Load()
        {
            using (var db = new DbContextVisionGroup())
            {
                EmployeeList = db.Employees.ToList();
            }
        }
    }
}
