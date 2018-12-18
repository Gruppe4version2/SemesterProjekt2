namespace VisionGroup2._0.ViewModels.Create
{
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    using VisionGroup2._0.Annotations;
    using VisionGroup2._0.Commands;
    using VisionGroup2._0.DomainClasses;
    using VisionGroup2._0.Factories;

    internal class CreateCostumerViewModel : INotifyPropertyChanged
    {
        private readonly CostumerFactory costumerFactory;

        public CreateCostumerViewModel()
        {
            this.costumerFactory = new CostumerFactory();

            this.AddCommand = new RelayCommand<Costumer>(
                                                         this.costumerFactory.Create,
                                                         costumer =>
                                                             this.costumerFactory.CanCreate(
                                                                                            this.costumerFactory
                                                                                                .NewCostumer));
            this.UpdateCommand = new RelayCommand<Costumer>(
                                                            () =>
                                                                {
                                                                    this.OnPropertyChanged(nameof(this.NewCostumer));
                                                                    this.AddCommand.RaiseCanExecuteChanged();
                                                                });
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public RelayCommand<Costumer> AddCommand { get; }

        public Costumer NewCostumer
        {
            get
            {
                return this.costumerFactory.NewCostumer;
            }

            set
            {
                this.costumerFactory.NewCostumer = value;
                this.OnPropertyChanged();
                this.AddCommand.RaiseCanExecuteChanged();
                this.OnPropertyChanged(nameof(this.AddCommand));
            }
        }

        public RelayCommand<Costumer> UpdateCommand { get; }

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