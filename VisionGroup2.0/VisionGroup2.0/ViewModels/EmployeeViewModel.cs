using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using VisionGroup2._0.Annotations;
using VisionGroup2._0.Catalogs;
using VisionGroup2._0.DomainClasses;

namespace VisionGroup2._0.ViewModels
{
    public class EmployeeViewModel : INotifyPropertyChanged
    {
        private ProjectCatalog _projectCatalog;

        private EmployeeCatalog _employeeCatalog;


        public Employee Employee { get; set; }

        public Project Project { get; set; }


        public EmployeeViewModel()
        {
            this._projectCatalog = new ProjectCatalog();
            this._employeeCatalog = new EmployeeCatalog();
        }


        public string Name
        {
            get { return Employee.Name; }
            set
            {
                Employee.Name = value;
                OnPropertyChanged();
            }
        }

        
        public int Phone
        {
            get { return Employee.PhoneNr; }
            set
            {
                Employee.PhoneNr = value;
                OnPropertyChanged();
            }
        }

        public string Email
        {
            get { return Employee.Email; }
            set
            {
                Employee.Email = value;
                OnPropertyChanged();
            }
        }
        public string HeaderText
        {
            get { return Employee.Name; }
        }

        public string ContentText
        {
            get { return Employee.PhoneNr + " " + Employee.Email; }
        }


        public List<Project> EmployeeProjects
        {
            get
            {
                List<Project> list = new List<Project>();

                if (this._projectCatalog.ProjectList != null)
                {
                    foreach (var l in _projectCatalog.ProjectList)
                    {
                        if (Project.EmployeeId == Employee.EmployeeId)
                        {
                            list.Add(l);
                        }
                    }
                }

                return list;
            }
        }

        public Project SelectedProject
        {
            get { return Project; }
            set
            {
                Project = value;
                OnPropertyChanged(nameof(EmployeeProjects));
            }
        }


        public Employee SelectedEmployee
        {
            get { return Employee; }
            set
            {
                Employee = value;
                OnPropertyChanged(nameof(EmployeeList));
            }
        }


        public List<Employee> EmployeeList
        {
            get
            {
                if (SelectedEmployee == null)
                {
                    if (this._employeeCatalog.EmployeeList != null)
                    {
                        var EmployeeList = from Employee in _employeeCatalog.EmployeeList
                                           orderby Employee.Name
                                           select Employee;
                        SelectedEmployee = EmployeeList.First();
                        return EmployeeList.ToList();
                    }
                    else
                    {
                        return this._employeeCatalog.EmployeeList;
                    }
                }
                else
                {
                    var EmployeeList = from Employee in _employeeCatalog.EmployeeList
                                       where Employee.Name == SelectedEmployee.Name

                                       orderby Employee.Name
                                       select Employee;
                    SelectedEmployee = EmployeeList.First();
                    return EmployeeList.ToList();
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
}
