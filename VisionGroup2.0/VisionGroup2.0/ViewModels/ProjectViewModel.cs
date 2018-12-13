using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using VisionGroup2._0.Annotations;
using VisionGroup2._0.Catalogs;
using VisionGroup2._0.Commands;
using VisionGroup2._0.DomainClasses;

namespace VisionGroup2._0.ViewModels
{
    public class ProjectViewModel : INotifyPropertyChanged
    {
        private ProjectCatalog _projectCatalog;

        private CostumerCatalog _costumerCatalog;

        private EmployeeCatalog _employeeCatalog;

        private ProjectForEmployeesCatalog _projectForEmployeesCatalog;

        private Project _selectedProject;

        private Employee _selectedEmployee;

        private Employee _addEmployee;

        private ProjectsForEmployee _projectForEmployee;

        public ProjectViewModel()
        {
            this._projectCatalog = ProjectCatalog.Instance;
            this._employeeCatalog = EmployeeCatalog.Instance;
            this._costumerCatalog = CostumerCatalog.Instance;
            this._projectForEmployeesCatalog = ProjectForEmployeesCatalog.Instance;
            this._addEmployee = new Employee();
            this._projectForEmployee = new ProjectsForEmployee();

            this.DeleteCommand = new RelayCommand<Project>(
                                                           new Action(
                                                                      () =>
                                                                          {
                                                                              this._projectCatalog.Remove(
                                                                                                          this
                                                                                                              .SelectedProject);
                                                                              this._selectedProject = null;
                                                                              this.OnPropertyChanged(
                                                                                                     nameof(
                                                                                                         this
                                                                                                             .ProjectList
                                                                                                     ));
                                                                          }),
                                                           new Predicate<Project>(
                                                                                  project =>
                                                                                      this.SelectedProject != null));

            this.UpdateCommand = new RelayCommand<Project>(
                                                           new Action(
                                                                      () =>
                                                                          {
                                                                              int id = this._selectedProject.ProjectId;
                                                                              this._projectCatalog.Update(
                                                                                                          this
                                                                                                              ._selectedProject);
                                                                              this.OnPropertyChanged(
                                                                                                     nameof(
                                                                                                         this
                                                                                                             .ProjectList
                                                                                                     ));
                                                                              this.SelectedProject =
                                                                                  this.ProjectList.Find(
                                                                                                        project =>
                                                                                                            project
                                                                                                                .ProjectId
                                                                                                            == id);
                                                                          }));
            this.AddEmployeeCommand =
                new RelayCommand<Employee>(new Action(
                                                    () =>
                                                        {
                                                            this._projectForEmployee.ProjectId = this._selectedProject.ProjectId;
                                                            this._projectForEmployee.EmployeeId = this._addEmployee.EmployeeId;
                                                            this._projectForEmployee.IsLeader = true;
                                                            this._projectForEmployeesCatalog.Add(this._projectForEmployee);
                                                            this._employeeCatalog.Load();
                                                            this._addEmployee = new Employee();
                                                            this.OnPropertyChanged(nameof(this.AddEmployeeName));
                                                            this.OnPropertyChanged(nameof(this.EmployeesForProject));
                                                            this.AddEmployeeCommand.RaiseCanExecuteChanged();
                                                        }), new Predicate<Employee>(
                                                                                    addEmployee =>
                                                                                        {
                                                                                            if (this._addEmployee.Name == null)
                                                                                            {
                                                                                                return false;
                                                                                            }

                                                                                            if (this._employeeCatalog.EmployeeList.Exists(employee => employee.Name == this._addEmployee.Name) && !this.EmployeesForProject.Exists(employee => employee.Name == this._addEmployee.Name))
                                                                                            {
                                                                                                return true;
                                                                                            }
                                                                                            else
                                                                                            {
                                                                                                return false;
                                                                                            }
                                                                                            }));
            this.DeleteEmployeeCommand = new RelayCommand<Employee>(new Action(() =>
                                                                              {
                                                                                  this._projectForEmployeesCatalog
                                                                                      .Remove(
                                                                                              this._selectedEmployee
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
                                                                                                         nameof(this.EmployeesForProject));
                                                                                  DeleteEmployeeCommand.RaiseCanExecuteChanged();
                                                                              }), new Predicate<Employee>(employee => this._selectedEmployee != null && this._selectedProject != null));
        }

        public RelayCommand<Project> DeleteCommand { get; }

        public RelayCommand<Project> UpdateCommand { get; }

        public RelayCommand<Employee> AddEmployeeCommand { get; }
        public RelayCommand<Employee> DeleteEmployeeCommand { get; }

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

        public Project SelectedProject
        {
            get
            {
                if (this._selectedProject != null)
                {
                    return this._selectedProject;
                }
                else
                {
                    this._selectedProject = this.ProjectList[0];
                    return this._selectedProject;
                }
            }

            set
            {
                this._selectedProject = value;
                SelectedEmployee = null;
                this.OnPropertyChanged();
                this.OnPropertyChanged(nameof(this.EmployeesForProject));
                this.OnPropertyChanged(nameof(this.DeadLineOffset));
                this.OnPropertyChanged(nameof(this.SelectedProjectCostumerName));
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

        public List<Employee> EmployeesForProject
        {
            get
            {
                var list = new List<Employee>();

                if (this._selectedProject != null)
                {
                    foreach (Employee employee in this._employeeCatalog.EmployeeList)
                    {
                        if (employee.ProjectsForEmployees.Where(p => p.ProjectId == this.SelectedProject.ProjectId)
                                    .ToList().Count > 0)
                        {
                            list.Add(employee);

                            // if (employee.ProjectsForEmployees.Where(p => p.ProjectId == this.SelectedProject.ProjectId && p.IsLeader).ToList().Count > 0)
                            // {
                            // employee.ProjectLeader = true;
                            // }
                            // else
                            // {
                            // employee.ProjectLeader = false;
                            // }
                        }
                    }
                }

                // list.Sort(((employee, employee1) => employee));
                return list;
            }
        }

        // public bool ProjectLeader 
        public Employee SelectedEmployee
        {
            get
            {
                //if (this._selectedEmployee != null)
                //{
                    return this._selectedEmployee;
                //}
                //else
                //{
                //    this._selectedEmployee = this.EmployeesForProject[0];
                //    return this._selectedEmployee;
                //}
            }

            set
            {
                this._selectedEmployee = value;
                this.OnPropertyChanged();
                DeleteEmployeeCommand.RaiseCanExecuteChanged();
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

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
