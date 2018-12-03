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
    public class ProjectViewModel : INotifyPropertyChanged
    {

        private ProjectCatalog _projectCatalog;
        private ProjectsForEmployee _projectsForEmployee;

        private Project _selectedProject;
        public ProjectViewModel()
        {
            this._projectCatalog = new ProjectCatalog();
            this._projectCatalog.Load();
            this._projectsForEmployee = new ProjectsForEmployee();
            EmployeeCatalog = new EmployeeCatalog();
            EmployeeCatalog.Load();
        }
        public EmployeeCatalog EmployeeCatalog { get; set; }
        public Employee Employee { get; set; }


        public string Name
        {
            get { return SelectedProject.Name; }
            set
            {
                SelectedProject.Name = value;
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

        public string CostumerName
        {
            get { return SelectedProject.Costumer.Name; }
            set
            {
                SelectedProject.Costumer.Name = value;
                OnPropertyChanged();
            }

        }


        public Costumer Costumer
        {
            get { return SelectedProject.Costumer; }
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

        public Project SelectedProject
        {
            get {
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

        public List<Employee> EmployeesForProject
        {
            get
            {
                List<Employee> list = new List<Employee>();
                if (_projectCatalog.ProjectList !=null)
                {


                    foreach (var employee in EmployeeCatalog.EmployeeList)
                    {
                        if (_projectsForEmployee.ProjectId == SelectedProject.ProjectId)
                        {
                            list.Add(employee);
                        }
                    }
                }

                return list;
            }
           
        }

        public Employee SelectedEmployee
        {
            get { return Employee; }
            set
            {
                Employee = value;
                OnPropertyChanged(nameof(EmployeesForProject));
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
