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
        private ProjectsForEmployee _projectsForEmployee;



        public Employee Employee { get; set; }

        public Project Project { get; set; }

        public ProjectsForEmployee ProjectsForEmployee { get; set; }
        private Action _remove;
        private Predicate<Employee> _canRemove;
        private RelayCommand<Employee> _deleteCommand;


        public EmployeeViewModel()
        {
            this._projectCatalog = new ProjectCatalog();
            _projectCatalog.Load();
            this._employeeCatalog = new EmployeeCatalog();
            _employeeCatalog.Load();
            _projectsForEmployee = new ProjectsForEmployee();
            _remove = () =>
            {
                _projectCatalog.Remove(SelectedProject);
                _projectCatalog.ProjectList.Remove(SelectedProject);
                _selectedEmployee = null;
                OnPropertyChanged(nameof(EmployeeList));
            };
            _canRemove = (Employee selectedEmployee) => _employeeCatalog.EmployeeList.Contains(SelectedEmployee);
            _deleteCommand = new RelayCommand<Employee>(_remove, _canRemove);
        }


        public RelayCommand<Employee> DeleteCommand
        {
            get { return _deleteCommand; }
        }


        public string Name
        {
            get { return SelectedEmployee.Name; }
            set
            {
                Employee.Name = value;
                OnPropertyChanged();
            }
        }

        
        public int Phone
        {
            get { return SelectedEmployee.PhoneNr; }
            set
            {
                Employee.PhoneNr = value;
                OnPropertyChanged();
            }
        }

        public string Email
        {
            get { return SelectedEmployee.Email; }
            set
            {
                Employee.Email = value;
                OnPropertyChanged();
            }
        }
        public string HeaderText
        {
            get { return SelectedEmployee.Name; }
        }

        public string ContentText
        {
            get { return SelectedEmployee.PhoneNr + " " + SelectedEmployee.Email; }
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
                        if (_projectsForEmployee.EmployeeId == SelectedEmployee.EmployeeId)
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
            get
            {
                if (this._selectedEmployee != null)
                {
                    return this._selectedEmployee;
                }
                else
                {
                    this._selectedEmployee= EmployeeList[0];
                    return this._selectedEmployee;
                }
            }
            set
            {
                this._selectedEmployee = value;
                OnPropertyChanged();
            }
        }


        public List<Employee> EmployeeList
        {
            get
            {
                if (_selectedEmployee == null)
                {
                    if (this._employeeCatalog.EmployeeList != null)
                    {
                        var employeeList = from employee in _employeeCatalog.EmployeeList
                            orderby employee.Name
                            select employee;
                        SelectedEmployee = employeeList.First();
                        return employeeList.ToList();
                    }
                    else
                    {
                        return this._employeeCatalog.EmployeeList;
                    }
                }
                else
                {
                    var employeeList = from employee in _employeeCatalog.EmployeeList
                        where employee.Name == SelectedEmployee.Name

                        orderby employee.Name
                        select employee;
                    return employeeList.ToList();
                }

            }
        }

        public void Refresh()
        {
            OnPropertyChanged(nameof(EmployeeCatalog.Load));
            OnPropertyChanged(nameof(EmployeeList));
        }



        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

