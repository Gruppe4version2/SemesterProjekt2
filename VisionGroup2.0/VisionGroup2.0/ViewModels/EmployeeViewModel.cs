using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using VisionGroup2._0.Annotations;
using VisionGroup2._0.Catalogs;
using VisionGroup2._0.Commands;
using VisionGroup2._0.DomainClasses;

namespace VisionGroup2._0.ViewModels
{
    public class EmployeeViewModel : INotifyPropertyChanged
    {
        private ProjectCatalog _projectCatalog;
        private EmployeeCatalog _employeeCatalog;
        private Employee _selectedEmployee;
        private ProjectForEmployeesCatalog _projectsForEmployee;
        private Action _remove;
        private Predicate<Employee> _canRemove;
        private RelayCommand<Employee> _deleteCommand;
        private Project _selectedProject;


        public EmployeeViewModel()
        {
            this._projectCatalog = ProjectCatalog.Instance;
            this._employeeCatalog = EmployeeCatalog.Instance;
            this._projectsForEmployee = ProjectForEmployeesCatalog.Instance;
            this._remove = () =>
            {
                this._projectCatalog.Remove(this.SelectedProject);
                this._projectCatalog.ProjectList.Remove(this.SelectedProject);
                this._selectedEmployee = null;
                this.OnPropertyChanged(nameof(this.EmployeeList));
            };
            this._canRemove = (Employee selectedEmployee) => this._employeeCatalog.EmployeeList.Contains(this.SelectedEmployee);
            this._deleteCommand = new RelayCommand<Employee>(this._remove, this._canRemove);
        }
        
        public RelayCommand<Employee> DeleteCommand
        {
            get { return this._deleteCommand; }
        }

        public string ContentText
        {
            get { return this.SelectedEmployee.PhoneNr + " " + this.SelectedEmployee.Email; }
        }


        public List<Project> EmployeeProjects
        {
            get
            {
                List<Project> projectList = new List<Project>();
                foreach (var projectForEmployee in SelectedEmployee.ProjectsForEmployees)
                {
                    foreach (var project in this._projectCatalog.ProjectList)
                    {
                        if (project.ProjectId == projectForEmployee.ProjectId)
                        {
                            projectList.Add(project);
                        }
                    }
                }

                return projectList;
            }
        }

        public Project SelectedProject
        {
            get { return this._selectedProject; }
            set
            {
                this._selectedProject = value;
                this.OnPropertyChanged();
            }
        }


        public Employee SelectedEmployee
        {
            get
            {
                if (this._selectedEmployee != null)
                {
                    return this._selectedEmployee;
                }
                else
                {
                    this._selectedEmployee = this.EmployeeList[0];
                    return this._selectedEmployee;
                }
            }

            set
            {
                this._selectedEmployee = value;
                this.OnPropertyChanged();
            }
        }


        public List<Employee> EmployeeList
        {
            get
            {
                if (this._selectedEmployee == null)
                {
                    if (this._employeeCatalog.EmployeeList != null)
                    {
                        IOrderedEnumerable<Employee> employeeList = from employee in this._employeeCatalog.EmployeeList
                            orderby employee.Name
                            select employee;
                        this.SelectedEmployee = employeeList.First();
                        return employeeList.ToList();
                    }
                    else
                    {
                        return this._employeeCatalog.EmployeeList;
                    }
                }
                else
                {
                    IOrderedEnumerable<Employee> employeeList = from employee in this._employeeCatalog.EmployeeList
                        where employee.Name == this.SelectedEmployee.Name

                        orderby employee.Name
                        select employee;
                    return employeeList.ToList();
                }

            }
        }





        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

