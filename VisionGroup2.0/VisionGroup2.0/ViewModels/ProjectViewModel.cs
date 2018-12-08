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
        private ProjectForEmployeesCatalog _projectForEmployeesCatalog;
        private CostumerCatalog _costumerCatalog;
        private EmployeeCatalog _employeeCatalog;
        private Action _remove;
        private Predicate<Project> _canRemove;
        private RelayCommand<Project> _deleteCommand;
        private Project _selectedProject;
        private Employee _selectedEmployee;

        public ProjectViewModel()
        {
            this._projectCatalog = ProjectCatalog.Instance;
            this._employeeCatalog = EmployeeCatalog.Instance;
            this._costumerCatalog = CostumerCatalog.Instance;
            this._remove = () =>
            {
                this._projectCatalog.Remove(this.SelectedProject);
                this._projectCatalog.ProjectList.Remove(this.SelectedProject);
                this._selectedProject = null;
                this.OnPropertyChanged(nameof(this.ProjectList));
            };
            this._canRemove = (Project selectedProject) => this._projectCatalog.ProjectList.Contains(this.SelectedProject);
            this._deleteCommand = new RelayCommand<Project>(this._remove, this._canRemove);
        }

        public RelayCommand<Project> DeleteCommand
        {
            get
            {
                return this._deleteCommand;
            }
        }

        public DateTimeOffset DeadLineOffset
        {
            get
            {
                DateTimeOffset d = new DateTimeOffset(SelectedProject.Deadline.Value);
                return d;
            }
            set
            {
                SelectedProject.Deadline = value.DateTime;
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
                this.OnPropertyChanged(nameof(EmployeesForProject));
                this.OnPropertyChanged(nameof(DeadLineOffset));
                this.OnPropertyChanged(nameof(SelectedProjectCostumer));
            }
        }

        public List<Project> ProjectList
        {
            get
            {
                if (this._selectedProject == null)
                {
                    if (this._projectCatalog.ProjectList != null)
                    {
                        var projectList = from project in this._projectCatalog.ProjectList
                                           orderby project.Name
                            select project;
                        this.SelectedProject = projectList.First();
                        return projectList.ToList();
                    }
                    else
                    {
                        return this._projectCatalog.ProjectList;
                    }
                }
                else
                {
                    var projectList = from project in this._projectCatalog.ProjectList
                                       where project.Name == this.SelectedProject.Name

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
                        }
                }

                return list;

            }
        }

        public Employee ProjectLeader { get; private set; }

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

        public Costumer SelectedProjectCostumer
        {
            get
            {
                return this._costumerCatalog.CostumerList.Find(
                    (costumer => costumer.CostumerId == SelectedProject.CostumerId));
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
