namespace VisionGroup2._0.ViewModels
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.CompilerServices;

    using VisionGroup2._0.Annotations;
    using VisionGroup2._0.Catalogs;
    using VisionGroup2._0.Commands;
    using VisionGroup2._0.DomainClasses;

    public class EmployeeViewModel : INotifyPropertyChanged
    {
        private readonly EmployeeCatalog _employeeCatalog;

        private readonly ProjectCatalog _projectCatalog;

        private bool _canEdit;

        private ProjectForEmployeesCatalog _projectsForEmployee;

        private Employee _selectedEmployee;

        private Project _selectedProject;

        public EmployeeViewModel()
        {
            this._projectCatalog = ProjectCatalog.Instance;
            this._employeeCatalog = EmployeeCatalog.Instance;
            this._projectsForEmployee = ProjectForEmployeesCatalog.Instance;
            this._canEdit = false;
            this.DeleteCommand = new RelayCommand<Employee>(
                                                            () =>
                                                                {
                                                                    this._employeeCatalog.Remove(
                                                                                                 this
                                                                                                     ._selectedEmployee);
                                                                    this._selectedEmployee = null;
                                                                    this.OnPropertyChanged(nameof(this.EmployeeList));
                                                                },
                                                            employee =>
                                                                this._employeeCatalog.EmployeeList.Contains(
                                                                                                            this
                                                                                                                .SelectedEmployee));
            this.UpdateCommand = new RelayCommand<Employee>(
                                                            () =>
                                                                {
                                                                    int id = this._selectedEmployee.EmployeeId;
                                                                    this._employeeCatalog.Update(
                                                                                                 this
                                                                                                     ._selectedEmployee);
                                                                    this.OnPropertyChanged(nameof(this.EmployeeList));
                                                                    this.SelectedEmployee =
                                                                        this.EmployeeList.Find(
                                                                                               employee =>
                                                                                                   employee.EmployeeId
                                                                                                   == id);
                                                                },
                                                            employee => this.Edit);
            this.RefreshCommand = new RelayCommand<bool>(
                                                         () =>
                                                             {
                                                                 this._projectCatalog.Load();
                                                                 this._employeeCatalog.Load();
                                                                 this._selectedEmployee = null;
                                                                 this.OnPropertyChanged(nameof(this.EmployeeList));
                                                             });
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public string ContentText
        {
            get
            {
                return this.SelectedEmployee.PhoneNr + " " + this.SelectedEmployee.Email;
            }
        }

        public RelayCommand<Employee> DeleteCommand { get; }

        public bool Edit
        {
            get
            {
                return this._canEdit;
            }

            set
            {
                this._canEdit = value;
                this.OnPropertyChanged(nameof(this.ReadOnly));
                this.OnPropertyChanged();
                this.UpdateCommand.RaiseCanExecuteChanged();
            }
        }

        public List<Employee> EmployeeList
        {
            get
            {
                if (this._selectedEmployee == null)
                {
                    var employeeList = from employee in this._employeeCatalog.EmployeeList
                                       orderby employee.Name
                                       select employee;
                    this.SelectedEmployee = employeeList.First();
                    return employeeList.ToList();
                }
                else
                {
                    var employeeList = from employee in this._employeeCatalog.EmployeeList
                                       orderby employee.Name
                                       select employee;
                    return employeeList.ToList();
                }
            }
        }

        public List<Project> EmployeeProjects
        {
            get
            {
                var projectList = new List<Project>();
                foreach (ProjectsForEmployee projectForEmployee in this.SelectedEmployee.ProjectsForEmployees)
                {
                    foreach (Project project in this._projectCatalog.ProjectList)
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

        public bool ReadOnly
        {
            get
            {
                return !this._canEdit;
            }
        }

        public RelayCommand<bool> RefreshCommand { get; }

        public string SearchEmployeeName
        {
            get
            {
                return string.Empty;
            }

            set
            {
                if (this._employeeCatalog.EmployeeList.Exists(employee => employee.Name == value))
                {
                    this.SelectedEmployee =
                        this._employeeCatalog.EmployeeList[this._employeeCatalog.EmployeeList.FindIndex(
                                                                                                        employee =>
                                                                                                            employee
                                                                                                                .Name
                                                                                                            == value)];
                }
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

                this._selectedEmployee = this.EmployeeList[0];
                return this._selectedEmployee;
            }

            set
            {
                this._selectedEmployee = value;
                this.OnPropertyChanged(nameof(this.EmployeeProjects));
                this.OnPropertyChanged();
            }
        }

        public Project SelectedProject
        {
            get
            {
                return this._selectedProject;
            }

            set
            {
                this._selectedProject = value;
                this.OnPropertyChanged();
            }
        }

        public RelayCommand<Employee> UpdateCommand { get; set; }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}