using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using VisionGroup2._0.Annotations;
using VisionGroup2._0.DomainClasses;
using VisionGroup2._0.Interfaces;

namespace VisionGroup2._0.Catalogs
{
    public class EmployeeCatalog : ICatalog<Employee>, INotifyPropertyChanged
    {
        #region Singleton
        private static EmployeeCatalog _instance;
        public static EmployeeCatalog Instance
        {
            get
            {
                _instance = _instance ?? (_instance = new EmployeeCatalog());
                return _instance;
            }
        }
        #endregion
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
                OnPropertyChanged();
            }
        }

        public void Remove(Employee item)
        {
            using (var db = new DbContextVisionGroup())
            {
                db.Employees.Remove(item);
                var projectEmployees = db.ProjectsForEmployees.Where(p => p.EmployeeId == item.EmployeeId).ToList();
                foreach (var project in projectEmployees)
                {
                    db.ProjectsForEmployees.Remove(project);
                }
                db.SaveChanges();
                OnPropertyChanged();
                
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
                Task a = Task.Run(new Action(ProjectForEmployeesCatalog.Instance.Load));
                EmployeeList = db.Employees.ToList();
                a.Wait();
                foreach (var employee in this._employeeList)
                {
                    foreach (var projectForEmployee in ProjectForEmployeesCatalog.Instance.ProjectsForEmployeesList)
                    {
                        if (employee.EmployeeId == projectForEmployee.EmployeeId)
                        {
                            employee.ProjectsForEmployees.Add(projectForEmployee);
                        }
                    }
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
