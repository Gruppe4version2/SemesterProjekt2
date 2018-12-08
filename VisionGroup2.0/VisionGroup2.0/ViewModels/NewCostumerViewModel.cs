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
        private Costumer _costumer;

        private Action _add;
        private Predicate<Costumer> _canAdd;
        private RelayCommand<Costumer> _addCommand;
        public NewCostumerViewModel()
        {
            _costumer = new Costumer();
            _costumerCatalog = new CostumerCatalog();
            _costumerCatalog.Load();
            _costumerFactory = new CostumerFactory();

            _add = () =>
            {
                _costumerCatalog.Add(_costumerFactory.Create());
                _costumerCatalog.CostumerList.Add(_costumerFactory.Create());

            };
            _canAdd = (Costumer selectedCostumer) => _costumerCatalog.CostumerList.Contains(_costumer);
            _addCommand = new RelayCommand<Costumer>(_add, _canAdd);
        }

        public RelayCommand<Costumer> AddCommand
        {
            get
            {
                return _addCommand;
            }
        }

        public string Name
        {
            set
            {
                _costumerFactory.Create().Name = value;
                OnPropertyChanged();
            }
        }

        public string Email
        {

            set
            {
                _costumerFactory.Create().Email = value;
                OnPropertyChanged();
            }
        }

        public int CVR
        {
            set
            {
                _costumerFactory.Create().CvrNr = value;
                OnPropertyChanged();
            }
        }

        public int PhoneNr
        {

            set
            {
                _costumerFactory.Create().PhoneNr= value;
                OnPropertyChanged();
            }
        }

        public int GivenId
        {
            get { return _costumerCatalog.CostumerList.Count + 1; }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
