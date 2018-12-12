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

        private CostumerCatalog _costumerCatalog;


        public CreateProjectViewModel()
        {
            this._factory = new ProjectFactory();
            this._costumerCatalog = CostumerCatalog.Instance;

            this.AddCommand = new RelayCommand<Project>(new Action(this._factory.Create), 
                new Predicate<Project>(project => this._factory.CanCreate(this._factory.NewProject)));
        }

        public RelayCommand<Project> AddCommand { get; }
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

        public DateTimeOffset DeadLineOffset
        {
            get
            {
                if (this.NewProject.Deadline.HasValue)
                {
                    DateTimeOffset d = new DateTimeOffset(this.NewProject.Deadline.Value);
                    return d;
                }
                else
                {
                    NewProject.Deadline = DateTime.Today;
                    DateTimeOffset d = new DateTimeOffset(this.NewProject.Deadline.Value);
                    this.OnPropertyChanged(nameof(NewProject));
                    AddCommand.RaiseCanExecuteChanged();
                    return d;
                }
            }

            set
            {
                NewProject.Deadline = value.DateTime;
                AddCommand.RaiseCanExecuteChanged();
                this.OnPropertyChanged();
                this.OnPropertyChanged(nameof(NewProject));
            }
        }
        public string SelectedProjectCostumerName
        {
            get
            {
                if (this._costumerCatalog.CostumerList.Where(p => p.CostumerId == NewProject.CostumerId).ToList().Count != 1)
                {
                    return string.Empty;
                }
                return this._costumerCatalog.CostumerList.Find(
                                                               costumer => costumer.CostumerId == this.NewProject.CostumerId).Name;
            }
            set
            {
                if (this._costumerCatalog.CostumerList.Where(n => n.Name == value).ToList().Count == 1)
                {
                    this.NewProject.CostumerId = this._costumerCatalog.CostumerList.Find((costumer => costumer.Name == value)).CostumerId;
                    AddCommand.RaiseCanExecuteChanged();
                }
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
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
