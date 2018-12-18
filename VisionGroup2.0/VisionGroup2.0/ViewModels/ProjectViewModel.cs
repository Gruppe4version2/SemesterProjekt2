namespace VisionGroup2._0.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.CompilerServices;

    using VisionGroup2._0.Annotations;
    using VisionGroup2._0.Catalogs;
    using VisionGroup2._0.Commands;
    using VisionGroup2._0.DomainClasses;

    public class ProjectViewModel : INotifyPropertyChanged
    {
        private Employee _addEmployee;

        private bool _canEdit;

        private readonly CostumerCatalog _costumerCatalog;

        private readonly EmployeeCatalog _employeeCatalog;

        private readonly ProjectCatalog _projectCatalog;

        private readonly ProjectsForEmployee _projectForEmployee;

        private readonly ProjectForEmployeesCatalog _projectForEmployeesCatalog;

        private Employee _selectedEmployee;

        private Project _selectedProject;

        public ProjectViewModel()
        {
            this._projectCatalog = ProjectCatalog.Instance;
            this._employeeCatalog = EmployeeCatalog.Instance;
            this._costumerCatalog = CostumerCatalog.Instance;
            this._projectForEmployeesCatalog = ProjectForEmployeesCatalog.Instance;
            this._addEmployee = new Employee();
            this._projectForEmployee = new ProjectsForEmployee();

            this.DeleteCommand = new RelayCommand<Project>(
                                                           () =>
                                                               {
                                                                   this._projectCatalog.Remove(this.SelectedProject);
                                                                   this._selectedProject = null;
                                                                   this.OnPropertyChanged(nameof(this.ProjectList));
                                                               },
                                                           project => this.SelectedProject != null);

            this.UpdateCommand = new RelayCommand<Project>(
                                                           () =>
                                                               {
                                                                   int id = this._selectedProject.ProjectId;
                                                                   this._projectCatalog.Update(this._selectedProject);
                                                                   this.OnPropertyChanged(nameof(this.ProjectList));
                                                                   this.SelectedProject =
                                                                       this.ProjectList.Find(
                                                                                             project =>
                                                                                                 project.ProjectId
                                                                                                 == id);
                                                                   foreach (ProjectsForEmployee projectsForEmployee in
                                                                       this.EmployeesForProject)
                                                                   {
                                                                       this._projectForEmployeesCatalog.Update(
                                                                                                               projectsForEmployee);
                                                                       this._employeeCatalog.Load();
                                                                   }
                                                               },
                                                           project => this.Edit);
            this.AddEmployeeCommand = new RelayCommand<Employee>(
                                                                 () =>
                                                                     {
                                                                         this._projectForEmployee.ProjectId =
                                                                             this._selectedProject.ProjectId;
                                                                         this._projectForEmployee.EmployeeId =
                                                                             this._addEmployee.EmployeeId;
                                                                         this._projectForEmployee.IsLeader = false;
                                                                         this._projectForEmployeesCatalog.Add(
                                                                                                              this
                                                                                                                  ._projectForEmployee);
                                                                         this._employeeCatalog.Load();
                                                                         this._addEmployee = new Employee();
                                                                         this.OnPropertyChanged(
                                                                                                nameof(
                                                                                                    this.AddEmployeeName
                                                                                                ));
                                                                         this.OnPropertyChanged(
                                                                                                nameof(
                                                                                                    this
                                                                                                        .EmployeesForProject
                                                                                                ));
                                                                         this.AddEmployeeCommand
                                                                             .RaiseCanExecuteChanged();
                                                                     },
                                                                 addEmployee =>
                                                                     {
                                                                         if (this._addEmployee.Name == null
                                                                             || this.EmployeesForProject.Exists(
                                                                                                                employee =>
                                                                                                                    employee
                                                                                                                        .EmployeeId
                                                                                                                    == this
                                                                                                                       ._addEmployee
                                                                                                                       .EmployeeId)
                                                                         )
                                                                         {
                                                                             return false;
                                                                         }

                                                                         if (this._employeeCatalog.EmployeeList.Exists(
                                                                                                                       employee =>
                                                                                                                           employee
                                                                                                                               .Name
                                                                                                                           == this
                                                                                                                              ._addEmployee
                                                                                                                              .Name)
                                                                             && this.Edit)
                                                                         {
                                                                             return true;
                                                                         }

                                                                         return false;
                                                                     });
            this.DeleteEmployeeCommand = new RelayCommand<Employee>(
                                                                    () =>
                                                                        {
                                                                            this._projectForEmployeesCatalog.Remove(
                                                                                                                    this
                                                                                                                        ._selectedEmployee
                                                                                                                        .ProjectsForEmployees
                                                                                                                        .First(
                                                                                                                               employee =>
                                                                                                                                   employee
                                                                                                                                       .ProjectId
                                                                                                                                   == this
                                                                                                                                      ._selectedProject
                                                                                                                                      .ProjectId));
                                                                            this._employeeCatalog.Load();
                                                                            this._selectedEmployee = null;
                                                                            this.OnPropertyChanged(
                                                                                                   nameof(
                                                                                                       this
                                                                                                           .EmployeesForProject
                                                                                                   ));
                                                                            this.DeleteEmployeeCommand
                                                                                .RaiseCanExecuteChanged();
                                                                        },
                                                                    employee => this._selectedEmployee != null
                                                                                && this._selectedProject != null
                                                                                && this.Edit);
            this._canEdit = false;
            this.RefreshCommand = new RelayCommand<bool>(
                                                         () =>
                                                             {
                                                                 this._projectCatalog.Load();
                                                                 this._employeeCatalog.Load();
                                                                 this._costumerCatalog.Load();
                                                                 this.OnPropertyChanged(nameof(this.ProjectList));
                                                                 this.OnPropertyChanged(nameof(this.SelectedProject));
                                                                 this.OnPropertyChanged(
                                                                                        nameof(this.EmployeesForProject
                                                                                        ));
                                                             });
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public RelayCommand<Employee> AddEmployeeCommand { get; }

        public string AddEmployeeName
        {
            get
            {
                if (this._addEmployee.Name == null)
                {
                    return string.Empty;
                }

                return this._addEmployee.Name;
            }

            set
            {
                if (this._employeeCatalog.EmployeeList.Exists(employee => employee.Name == value))
                {
                    this._addEmployee =
                        this._employeeCatalog.EmployeeList[this._employeeCatalog.EmployeeList.FindIndex(
                                                                                                        employee =>
                                                                                                            employee
                                                                                                                .Name
                                                                                                            == value)];
                }
                else
                {
                    this._addEmployee.Name = value;
                }

                this.AddEmployeeCommand.RaiseCanExecuteChanged();
            }
        }

        public DateTimeOffset DeadLineOffset
        {
            get
            {
                DateTimeOffset d = new DateTimeOffset(this.SelectedProject.Deadline.Value);
                return d;
            }

            set
            {
                this.SelectedProject.Deadline = value.DateTime;
                this.OnPropertyChanged();
            }
        }

        public RelayCommand<Project> DeleteCommand { get; }

        public RelayCommand<Employee> DeleteEmployeeCommand { get; }

        public bool Edit
        {
            get
            {
                return this._canEdit;
            }

            set
            {
                this._canEdit = value;
                this.OnPropertyChanged();
                this.OnPropertyChanged(nameof(this.ReadOnly));
                this.UpdateCommand.RaiseCanExecuteChanged();
                this.AddEmployeeCommand.RaiseCanExecuteChanged();
                this.DeleteEmployeeCommand.RaiseCanExecuteChanged();
            }
        }

        public List<ProjectsForEmployee> EmployeesForProject
        {
            get
            {
                var list = new List<ProjectsForEmployee>();

                if (this._selectedProject != null)
                {
                    foreach (Employee employee in this._employeeCatalog.EmployeeList)
                    {
                        foreach (ProjectsForEmployee projectForEmployee in employee.ProjectsForEmployees)
                        {
                            if (projectForEmployee.ProjectId == this._selectedProject.ProjectId)
                            {
                                projectForEmployee.Employee = employee;
                                list.Add(projectForEmployee);
                            }
                        }
                    }
                }

                return list;
            }

            set
            {
                foreach (ProjectsForEmployee projectsForEmployee in value)
                {
                    this._projectForEmployeesCatalog.Update(projectsForEmployee);
                    this._employeeCatalog.Load();
                }
            }
        }

        public List<Project> ProjectList
        {
            get
            {
                if (this._selectedProject == null)
                {
                    var projectList = from project in this._projectCatalog.ProjectList
                                      orderby project.Name
                                      select project;
                    this.SelectedProject = projectList.First();
                    return projectList.ToList();
                }
                else
                {
                    var projectList = from project in this._projectCatalog.ProjectList
                                      orderby project.Name
                                      select project;
                    return projectList.ToList();
                }
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

        public string SearchProjectName
        {
            get
            {
                return string.Empty;
            }

            set
            {
                if (this._projectCatalog.ProjectList.Exists(project => project.Name == value))
                {
                    this.SelectedProject =
                        this._projectCatalog.ProjectList[this._projectCatalog.ProjectList.FindIndex(
                                                                                                    project =>
                                                                                                        project.Name
                                                                                                        == value)];
                }
            }
        }

        public ProjectsForEmployee SelectedEmployee
        {
            get
            {
                if (this._selectedEmployee != null)
                {
                    return this._selectedEmployee.ProjectsForEmployees.First(
                                                                             employee =>
                                                                                 employee.ProjectId
                                                                                 == this._selectedProject.ProjectId);
                }

                return null;
            }

            set
            {
                if (value == null)
                {
                    if (this.EmployeesForProject.Count < 1)
                    {
                        this._selectedEmployee = null;
                    }
                    else if (this.EmployeesForProject[0].Employee == null)
                    {
                        this._selectedEmployee = null;
                    }
                    else
                    {
                        this._selectedEmployee = this.EmployeesForProject[0].Employee;
                    }
                }
                else
                {
                    this._selectedEmployee = value.Employee;
                }

                this.OnPropertyChanged();
                this.DeleteEmployeeCommand.RaiseCanExecuteChanged();
            }
        }

        public Project SelectedProject
        {
            get
            {
                if (this._selectedProject != null)
                {
                    return this._selectedProject;
                }

                this._selectedProject = this.ProjectList[0];
                return this._selectedProject;
            }

            set
            {
                this._selectedProject = value;
                this.SelectedEmployee = null;
                this.OnPropertyChanged();
                this.OnPropertyChanged(nameof(this.EmployeesForProject));
                this.OnPropertyChanged(nameof(this.DeadLineOffset));
                this.OnPropertyChanged(nameof(this.SelectedProjectCostumerName));
            }
        }

        public string SelectedProjectCostumerName
        {
            get
            {
                return this._costumerCatalog.CostumerList
                           .Find(costumer => costumer.CostumerId == this.SelectedProject.CostumerId).Name;
            }

            set
            {
                if (this._costumerCatalog.CostumerList.Where(n => n.Name == value).ToList().Count == 1)
                {
                    this.SelectedProject.CostumerId =
                        this._costumerCatalog.CostumerList.Find(costumer => costumer.Name == value).CostumerId;
                }

                this.OnPropertyChanged();
            }
        }

        public RelayCommand<Project> UpdateCommand { get; }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}