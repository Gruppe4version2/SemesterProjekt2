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
        private Project _selectedProject;
        private Employee _selectedEmployee;

        public ProjectViewModel()
        {
            this._projectCatalog = ProjectCatalog.Instance;
            this._employeeCatalog = EmployeeCatalog.Instance;
            this._costumerCatalog = CostumerCatalog.Instance;
            
            this.DeleteCommand = new RelayCommand<Project>(new Action(() =>
                                                                               {
                                                                                   this._projectCatalog.Remove(this.SelectedProject);
                                                                                   this._selectedProject = null;
                                                                                   this.OnPropertyChanged(nameof(this.ProjectList));
                                                                               }), 
                                                            new Predicate<Project>(project => this.SelectedProject != null));
            this.UpdateCommand = new RelayCommand<Project>(new Action(() =>
                                                                      {
                                                                          int id = this._selectedProject.ProjectId;
                                                                          this._projectCatalog.Update(this._selectedProject);
                                                                          this.OnPropertyChanged(nameof(this.ProjectList));
                                                                          this.SelectedProject = this.ProjectList.Find(project => project.ProjectId == id);
                                                                      }));
        }
        
        public RelayCommand<Project> DeleteCommand { get; }
        public RelayCommand<Project> UpdateCommand { get; }

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
                        if (employee.ProjectsForEmployees.Where(p => p.ProjectId == this.SelectedProject.ProjectId).ToList().Count > 0)
                        {
                            list.Add(employee);
                            //if (employee.ProjectsForEmployees.Where(p => p.ProjectId == this.SelectedProject.ProjectId && p.IsLeader).ToList().Count > 0)
                            //{
                            //    employee.ProjectLeader = true;
                            //}
                            //else
                            //{
                            //    employee.ProjectLeader = false;
                            //}
                        }
                }
                //list.Sort(((employee, employee1) => employee));
                return list;

            }
        }
        //public bool ProjectLeader 

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
                    this._selectedEmployee = this.EmployeesForProject[0];
                    return this._selectedEmployee;
                }
            }

            set
            {
                this._selectedEmployee = value;
                this.OnPropertyChanged();

            }
        }

        public string SelectedProjectCostumerName
        {
            get
            {
                return this._costumerCatalog.CostumerList.Find(
                    costumer => costumer.CostumerId == this.SelectedProject.CostumerId).Name;
            }
            set
            {
                if (this._costumerCatalog.CostumerList.Where(n => n.Name == value).ToList().Count == 1)
                {
                    SelectedProject.CostumerId = this._costumerCatalog.CostumerList.Find((costumer => costumer.Name == value)).CostumerId;
                }
                this.OnPropertyChanged();
            }
        }

        public void Refresh()
        {
            this.OnPropertyChanged(nameof(ProjectCatalog.Load));
            this.OnPropertyChanged(nameof(this.ProjectList));
        }


        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }
    }
}
