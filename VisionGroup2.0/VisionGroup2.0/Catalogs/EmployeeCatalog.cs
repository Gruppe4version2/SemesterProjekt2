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
                EmployeeList = db.Employees.ToList();
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
