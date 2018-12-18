namespace VisionGroup2._0.ViewModels.Create
{
    using System;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.CompilerServices;

    using VisionGroup2._0.Annotations;
    using VisionGroup2._0.Catalogs;
    using VisionGroup2._0.Commands;
    using VisionGroup2._0.DomainClasses;
    using VisionGroup2._0.Factories;

    public class CreateProjectViewModel : INotifyPropertyChanged
    {
        private readonly CostumerCatalog _costumerCatalog;

        private readonly ProjectFactory _factory;

        public CreateProjectViewModel()
        {
            this._factory = new ProjectFactory();
            this._costumerCatalog = CostumerCatalog.Instance;

            this.AddCommand = new RelayCommand<Project>(
                                                        this._factory.Create,
                                                        project => this._factory.CanCreate(this._factory.NewProject));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public RelayCommand<Project> AddCommand { get; }

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
                    this.NewProject.Deadline = DateTime.Today;
                    DateTimeOffset d = new DateTimeOffset(this.NewProject.Deadline.Value);
                    this.OnPropertyChanged(nameof(this.NewProject));
                    this.AddCommand.RaiseCanExecuteChanged();
                    return d;
                }
            }

            set
            {
                this.NewProject.Deadline = value.DateTime;
                this.AddCommand.RaiseCanExecuteChanged();
                this.OnPropertyChanged();
                this.OnPropertyChanged(nameof(this.NewProject));
            }
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

        public string SelectedProjectCostumerName
        {
            get
            {
                if (this._costumerCatalog.CostumerList.Where(p => p.CostumerId == this.NewProject.CostumerId).ToList()
                        .Count != 1)
                {
                    return string.Empty;
                }

                return this._costumerCatalog.CostumerList
                           .Find(costumer => costumer.CostumerId == this.NewProject.CostumerId).Name;
            }

            set
            {
                if (this._costumerCatalog.CostumerList.Where(n => n.Name == value).ToList().Count == 1)
                {
                    this.NewProject.CostumerId = this._costumerCatalog.CostumerList
                                                     .Find(costumer => costumer.Name == value).CostumerId;
                    this.AddCommand.RaiseCanExecuteChanged();
                }
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