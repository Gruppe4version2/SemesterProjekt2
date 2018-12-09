namespace VisionGroup2._0.ViewModels.Create
{
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    using Windows.UI.Xaml.Controls;

    using VisionGroup2._0.Annotations;
    using VisionGroup2._0.Catalogs;
    using VisionGroup2._0.Commands;
    using VisionGroup2._0.DomainClasses;
    using VisionGroup2._0.Factories;
    using VisionGroup2._0.Views.New_objects;

    internal class CreateCostumerViewModel : INotifyPropertyChanged
    {
        private CostumerCatalog _costumerCatalog;

        private Factory _factory;

        private Costumer _newCostumer;

        public CreateCostumerViewModel()
        {
            this._costumerCatalog = CostumerCatalog.Instance;
            this._factory = new Factory();
            this._newCostumer = new Costumer();

            this.AddCommand = new RelayCommand<Costumer>(new Action(this._factory.Create), new Predicate<Costumer>(costumer => this._factory.CanCreate(this._factory.NewCostumer)));
            UpdateCommand = new RelayCommand<Costumer>(new Action(() =>
                                                                       {
                                                                           this.OnPropertyChanged(nameof(NewCostumer));
                                                                           AddCommand.RaiseCanExecuteChanged();
                                                                       }));
        }

        public RelayCommand<Costumer> AddCommand { get; }
        public RelayCommand<Costumer> UpdateCommand { get; }

        public void Update()
        {
            AddCommand.RaiseCanExecuteChanged();
        }

        public Costumer NewCostumer
        {
            get
            {
                return this._factory.NewCostumer;
            }

            set
            {
                
                    this._factory.NewCostumer = value;
                    this.OnPropertyChanged();
                    this.AddCommand.RaiseCanExecuteChanged();
                    this.OnPropertyChanged(nameof(this.AddCommand));
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
