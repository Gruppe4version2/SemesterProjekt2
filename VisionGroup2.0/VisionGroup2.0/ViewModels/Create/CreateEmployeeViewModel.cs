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
using VisionGroup2._0.Factories;

namespace VisionGroup2._0.ViewModels.Create
{
    class CreateEmployeeViewModel : INotifyPropertyChanged
    {


        private EmployeeFactory _employeeFactory;

        private EmployeeCatalog _employeeCatalog;

        public RelayCommand<Employee> AddCommand { get; set; }

        public CreateEmployeeViewModel()
        {
            this._employeeFactory = new EmployeeFactory();

            this._employeeCatalog = EmployeeCatalog.Instance;

            this.AddCommand = new RelayCommand<Employee>(new Action(this._employeeFactory.Create),
                new Predicate<Employee>(employee => this._employeeFactory.CanCreate(this._employeeFactory.NewEmployee)));
        }

        public Employee NewEmployee
        {
            get
            {
                return this._employeeFactory.NewEmployee;
            }

            set
            {
                this._employeeFactory.NewEmployee = value;
                this.OnPropertyChanged();
                this.AddCommand.RaiseCanExecuteChanged();
                this.OnPropertyChanged(nameof(this.AddCommand));
            }
        }

        public void Update()
        {
            AddCommand.RaiseCanExecuteChanged();
        }


        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }
    }
}
