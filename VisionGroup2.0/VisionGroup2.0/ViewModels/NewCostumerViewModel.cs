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
using VisionGroup2._0.Factories;

namespace VisionGroup2._0.ViewModels
{
    class NewCostumerViewModel : INotifyPropertyChanged
    {
        private CostumerCatalog _costumerCatalog;

        private CostumerFactory _costumerFactory;

        private RelayCommand<Costumer> _addCommand;
        public NewCostumerViewModel()
        {
            _costumerCatalog = CostumerCatalog.Instance;
            _costumerFactory = new CostumerFactory();

            _addCommand = new RelayCommand<Costumer>(new Action(this._costumerFactory.Create), new Predicate<Costumer>(this._costumerFactory.CanCreate));
        }

        public RelayCommand<Costumer> AddCommand
        {
            get
            {
                return _addCommand;
            }
        }

        public Costumer NewCostumer
        {
            get
            {
                return this._costumerFactory.NewCostumer;
            }
            set
            {
                this._costumerFactory.NewCostumer = value;
                this.OnPropertyChanged();
                AddCommand.RaiseCanExecuteChanged();
                this.OnPropertyChanged(nameof(AddCommand));
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
