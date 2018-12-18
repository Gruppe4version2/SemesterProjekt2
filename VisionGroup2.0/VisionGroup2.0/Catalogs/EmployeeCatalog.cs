namespace VisionGroup2._0.Catalogs
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using VisionGroup2._0.DomainClasses;
    using VisionGroup2._0.Interfaces;

    public class EmployeeCatalog : ICatalog<Employee>
    {
        private static EmployeeCatalog _instance;

        private List<Employee> _employeeList;

        public static EmployeeCatalog Instance
        {
            get
            {
                _instance = _instance ?? (_instance = new EmployeeCatalog());
                return _instance;
            }
        }

        public List<Employee> EmployeeList
        {
            get
            {
                if (this._employeeList != null)
                {
                    return this._employeeList;
                }

                this.Load();
                return this._employeeList;
            }

            set
            {
                this._employeeList = value;
            }
        }

        public void Add(Employee item)
        {
            using (DbContextVisionGroup db = new DbContextVisionGroup())
            {
                db.Employees.Add(item);
                this.EmployeeList.Add(item);
                db.SaveChanges();
            }
        }

        public void Load()
        {
            using (DbContextVisionGroup db = new DbContextVisionGroup())
            {
                Task a = Task.Run(new Action(ProjectForEmployeesCatalog.Instance.Load));
                this.EmployeeList = db.Employees.ToList();
                a.Wait();
                foreach (Employee employee in this._employeeList)
                {
                    foreach (ProjectsForEmployee projectForEmployee in ProjectForEmployeesCatalog
                                                                       .Instance.ProjectsForEmployeesList)
                    {
                        if (employee.EmployeeId == projectForEmployee.EmployeeId)
                        {
                            employee.ProjectsForEmployees.Add(projectForEmployee);
                        }
                    }
                }
            }
        }

        public void Remove(Employee item)
        {
            using (DbContextVisionGroup db = new DbContextVisionGroup())
            {
                db.Employees.Remove(item);
                this.EmployeeList.Remove(item);
                foreach (ProjectsForEmployee project in item.ProjectsForEmployees)
                {
                    db.ProjectsForEmployees.Remove(project);
                    ProjectForEmployeesCatalog.Instance.ProjectsForEmployeesList.Remove(project);
                }

                db.SaveChanges();
            }
        }

        public void Update(Employee item)
        {
            using (DbContextVisionGroup db = new DbContextVisionGroup())
            {
                db.Employees.Update(item);
                this.EmployeeList[this.EmployeeList.FindIndex(employee => employee.EmployeeId == item.EmployeeId)] =
                    item;
                db.SaveChanges();
            }
        }
    }
}