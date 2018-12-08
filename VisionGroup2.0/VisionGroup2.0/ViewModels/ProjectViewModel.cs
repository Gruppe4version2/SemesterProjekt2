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
        private ProjectsForEmployee _projectsForEmployee;
        private ProjectForEmployeesCatalog _projectForEmployeesCatalog;
        private EmployeeCatalog _employeeCatalog;
        private Action _remove;
        private Predicate<Project> _canRemove;
        private RelayCommand<Project> _deleteCommand;



        private Project _selectedProject;
        private Employee _selectedEmployee;
        public ProjectViewModel()
        {
            this._projectCatalog = new ProjectCatalog();
            this._projectCatalog.Load();
            this._projectsForEmployee = new ProjectsForEmployee();
            this._projectForEmployeesCatalog = new ProjectForEmployeesCatalog();
            _projectForEmployeesCatalog.Load();
            _employeeCatalog = new EmployeeCatalog();
            _employeeCatalog.Load();
            _remove = () =>
            {
                _projectCatalog.Remove(SelectedProject);
                _projectCatalog.ProjectList.Remove(SelectedProject);
                _selectedProject = null;
                OnPropertyChanged(nameof(ProjectList));
            };
            _canRemove = (Project selectedProject) => _projectCatalog.ProjectList.Contains(SelectedProject);
            _deleteCommand = new RelayCommand<Project>(_remove, _canRemove);
        }

        public RelayCommand<Project> DeleteCommand
        {
            get
            {
                return _deleteCommand;

            }

        }
        public string Name
        {
            get { return SelectedProject.Name; }
            set
            {
                SelectedProject.Name = value;
                OnPropertyChanged();
            }
        }

        public string CostumerName
        {
            get { return SelectedProject.Costumer.Name; }
            set
            {
                SelectedProject.Costumer.Name = value;
                OnPropertyChanged();
            }
        }

        public DateTime? Deadline
        {
            get { return SelectedProject.Deadline; }
            set
            {
                SelectedProject.Deadline = value;
                OnPropertyChanged();
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
                    this._selectedProject = ProjectList[0];
                    return this._selectedProject;
                }
            }
            set
            {
                this._selectedProject = value;
                OnPropertyChanged();

            }
        }

        public List<Project> ProjectList
        {
            get
            {
                if (_selectedProject == null)
                {
                    if (this._projectCatalog.ProjectList != null)
                    {
                        var projectList = from project in _projectCatalog.ProjectList
                                           orderby project.Name
                            select project;
                        SelectedProject = projectList.First();
                        return projectList.ToList();
                    }
                    else
                    {
                        return this._projectCatalog.ProjectList;
                    }
                }
                else
                {
                    var projectList = from project in _projectCatalog.ProjectList
                                       where project.Name == SelectedProject.Name

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
                List<Employee> list = new List<Employee>();

                if (_projectCatalog.ProjectList != null)
                {
                    foreach (var employee in _employeeCatalog.EmployeeList)
                    {
                        if (employee.ProjectsForEmployees.Where(p => p.ProjectId == SelectedProject.ProjectId).ToList().Count > 0)
                        {
                            list.Add(employee);
                        }
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
                    this._selectedEmployee = EmployeesForProject[0];
                    return this._selectedEmployee;
                }
            }
            set
            {
                this._selectedEmployee = value;
                OnPropertyChanged();

            }
        }
        public void Refresh()
        {
            OnPropertyChanged(nameof(ProjectCatalog.Load));
            OnPropertyChanged(nameof(ProjectList));
        }


        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }
    }
}
