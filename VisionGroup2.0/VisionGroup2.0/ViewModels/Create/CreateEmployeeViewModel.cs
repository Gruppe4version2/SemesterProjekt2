namespace VisionGroup2._0.ViewModels.Create
{
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    using VisionGroup2._0.Annotations;
    using VisionGroup2._0.Catalogs;
    using VisionGroup2._0.Commands;
    using VisionGroup2._0.DomainClasses;
    using VisionGroup2._0.Factories;

    internal class CreateEmployeeViewModel : INotifyPropertyChanged
    {
        private EmployeeCatalog _employeeCatalog;

        private readonly EmployeeFactory _employeeFactory;

        public CreateEmployeeViewModel()
        {
            this._employeeFactory = new EmployeeFactory();

            this._employeeCatalog = EmployeeCatalog.Instance;

            this.AddCommand = new RelayCommand<Employee>(
                                                         this._employeeFactory.Create,
                                                         employee =>
                                                             this._employeeFactory.CanCreate(
                                                                                             this._employeeFactory
                                                                                                 .NewEmployee));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public RelayCommand<Employee> AddCommand { get; set; }

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
            this.AddCommand.RaiseCanExecuteChanged();
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}