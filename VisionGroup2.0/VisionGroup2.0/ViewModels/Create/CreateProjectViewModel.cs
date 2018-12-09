using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using VisionGroup2._0.Annotations;
using VisionGroup2._0.Catalogs;
using VisionGroup2._0.Commands;
using VisionGroup2._0.DomainClasses;
using VisionGroup2._0.Factories;

namespace VisionGroup2._0.ViewModels.Create
{
    public class CreateProjectViewModel : INotifyPropertyChanged
    {
        private ProjectFactory _factory;


        public CreateProjectViewModel()
        {
            this._factory = new ProjectFactory();

            this.AddCommand = new RelayCommand<Costumer>(new Action(this._factory.Create), new Predicate<Costumer>(project => this._factory.CanCreate(this._factory.NewProject)));
        }

        public RelayCommand<Costumer> AddCommand { get; }

        public void Update()
        {
            AddCommand.RaiseCanExecuteChanged();
        }

        public Project NewProject
        {
            get
            {
                return this._factory.NewProject;
            }

            set
            {

                this._factory.NewProject = value;
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
