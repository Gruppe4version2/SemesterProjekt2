﻿using System;
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

        private bool _canEdit;

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
                                                                              foreach (var projectsForEmployee in EmployeesForProject)
                                                                              {
                                                                                  this._projectForEmployeesCatalog.Update(projectsForEmployee);
                                                                                  this._employeeCatalog.Load();
                                                                              }
                                                                          }), project => Edit);
            this.AddEmployeeCommand =
                new RelayCommand<Employee>(new Action(
                                                    () =>
                                                        {
                                                            this._projectForEmployee.ProjectId = this._selectedProject.ProjectId;
                                                            this._projectForEmployee.EmployeeId = this._addEmployee.EmployeeId;
                                                            this._projectForEmployee.IsLeader = false;
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

                                                                                            if (this._employeeCatalog.EmployeeList.Exists(employee => employee.Name == this._addEmployee.Name) && !this.EmployeesForProject.Exists(employee => employee.Employee.Name == this._addEmployee.Name))
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
            this._canEdit = false;
            RefreshCommand = new RelayCommand<bool>(
                                                    () =>
                                                        {
                                                            this._projectCatalog.Load();
                                                            this._employeeCatalog.Load();
                                                            this._costumerCatalog.Load();
                                                            this.OnPropertyChanged(nameof(ProjectList));
                                                            this.OnPropertyChanged(nameof(SelectedProject));
                                                            this.OnPropertyChanged(nameof(EmployeesForProject));
                                                        });
        }
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
                this.OnPropertyChanged(nameof(ReadOnly));
                UpdateCommand.RaiseCanExecuteChanged();
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

        public List<ProjectsForEmployee> EmployeesForProject
        {
            get
            {
                var list = new List<ProjectsForEmployee>();

                if (this._selectedProject != null)
                {
                    foreach (Employee employee in this._employeeCatalog.EmployeeList)
                    {
                        foreach (var projectForEmployee in employee.ProjectsForEmployees)
                        {
                            if (projectForEmployee.ProjectId == this._selectedProject.ProjectId)
                            {
                                projectForEmployee.Employee = employee;
                                list.Add(projectForEmployee);
                            }
                        }
                        //if (employee.ProjectsForEmployees.Where(p => p.ProjectId == this.SelectedProject.ProjectId)
                        //            .ToList().Count > 0)
                        //{
                        //    list.Add(employee);

                        //    // if (employee.ProjectsForEmployees.Where(p => p.ProjectId == this.SelectedProject.ProjectId && p.IsLeader).ToList().Count > 0)
                        //    // {
                        //    // employee.ProjectLeader = true;
                        //    // }
                        //    // else
                        //    // {
                        //    // employee.ProjectLeader = false;
                        //    // }
                        //}
                    }
                }

                // list.Sort(((employee, employee1) => employee));
                return list;
            }
            set
            {
                foreach (var projectsForEmployee in value)
                {
                    this._projectForEmployeesCatalog.Update(projectsForEmployee);
                    this._employeeCatalog.Load();
                }
            }
        }

        // public bool ProjectLeader 
        public ProjectsForEmployee SelectedEmployee
        {
            get
            {
                if (this._selectedEmployee != null)
                {
                    return this._selectedEmployee.ProjectsForEmployees.First((employee => employee.ProjectId == this._selectedProject.ProjectId));
                }
                else
                {
                    return null;
                }
            }

            set
            {
                if (value == null)
                {
                    if (EmployeesForProject.Count < 1)
                    {
                        this._selectedEmployee = null;
                    }
                    else if (EmployeesForProject[0].Employee == null)
                    {
                        this._selectedEmployee = null;
                    }
                    else
                    {
                        this._selectedEmployee = EmployeesForProject[0].Employee;
                    }
                }
                else
                {
                    this._selectedEmployee = value.Employee;
                }
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

        public string SearchProjectName
        {
            get
            {
                return string.Empty;
            }

            set
            {
                if (this._projectCatalog.ProjectList.Exists((project => project.Name == value)))
                {
                    SelectedProject =
                        this._projectCatalog.ProjectList[this._projectCatalog.ProjectList.FindIndex(
                                                                                                    (project =>
                                                                                                         project.Name
                                                                                                         == value))];
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
