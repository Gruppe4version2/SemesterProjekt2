namespace VisionGroup2._0.ViewModels
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.CompilerServices;

    using VisionGroup2._0.Annotations;
    using VisionGroup2._0.Catalogs;
    using VisionGroup2._0.Commands;
    using VisionGroup2._0.DomainClasses;

    public class CostumerViewModel : INotifyPropertyChanged
    {
        private bool _canEdit;

        private readonly CostumerCatalog _costumerCatalog;

        private readonly ProjectCatalog _projectCatalog;

        private Costumer _selectedCostumer;

        private Project _selectedProject;

        public CostumerViewModel()
        {
            this._projectCatalog = ProjectCatalog.Instance;
            this._costumerCatalog = CostumerCatalog.Instance;
            this.OnPropertyChanged(nameof(this.CostumerList));

            this.DeleteCustomerCommand = new RelayCommand<Costumer>(
                                                                    () =>
                                                                        {
                                                                            this._costumerCatalog.Remove(
                                                                                                         this
                                                                                                             ._selectedCostumer);
                                                                            this._selectedCostumer = null;
                                                                            this.OnPropertyChanged(
                                                                                                   nameof(
                                                                                                       this.CostumerList
                                                                                                   ));
                                                                        },
                                                                    costumer => this.ProjectsForCostumer.Count == 0);
            this.UpdateCustomerCommand = new RelayCommand<Costumer>(
                                                                    () =>
                                                                        {
                                                                            int id = this._selectedCostumer.CostumerId;
                                                                            this._costumerCatalog.Update(
                                                                                                         this
                                                                                                             ._selectedCostumer);
                                                                            this.OnPropertyChanged(
                                                                                                   nameof(
                                                                                                       this.CostumerList
                                                                                                   ));
                                                                            this.SelectedCostumer =
                                                                                this.CostumerList.Find(
                                                                                                       costumer =>
                                                                                                           costumer
                                                                                                               .CostumerId
                                                                                                           == id);
                                                                        },
                                                                    costumer => this.Edit);
            this.Edit = false;
            this.RefreshCommand = new RelayCommand<bool>(
                                                         () =>
                                                             {
                                                                 this._projectCatalog.Load();
                                                                 this._costumerCatalog.Load();
                                                                 this.OnPropertyChanged(nameof(this.CostumerList));
                                                                 this.OnPropertyChanged(nameof(this.SelectedCostumer));
                                                                 this.OnPropertyChanged(
                                                                                        nameof(this.ProjectsForCostumer
                                                                                        ));
                                                             });
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public List<Costumer> CostumerList
        {
            get
            {
                {
                    if (this._costumerCatalog.CostumerList != null)
                    {
                        var costumerList = from costumer in this._costumerCatalog.CostumerList
                                           orderby costumer.Name
                                           select costumer;
                        if (this._selectedCostumer == null)
                        {
                            this._selectedCostumer = costumerList.First();
                        }

                        return costumerList.ToList();
                    }

                    return this._costumerCatalog.CostumerList;
                }
            }
        }

        public RelayCommand<Costumer> DeleteCustomerCommand { get; }

        public bool Edit
        {
            get
            {
                return this._canEdit;
            }

            set
            {
                this._canEdit = value;
                this.OnPropertyChanged();
                this.OnPropertyChanged(nameof(this.ReadOnly));
                this.UpdateCustomerCommand.RaiseCanExecuteChanged();
            }
        }

        public List<Project> ProjectsForCostumer
        {
            get
            {
                var list = new List<Project>();

                if (this._projectCatalog.ProjectList != null)
                {
                    foreach (Project l in this._projectCatalog.ProjectList)
                    {
                        if (l.CostumerId == this.SelectedCostumer.CostumerId)
                        {
                            list.Add(l);
                        }
                    }
                }

                return list;
            }
        }

        public bool ReadOnly
        {
            get
            {
                return !this._canEdit;
            }
        }

        public RelayCommand<bool> RefreshCommand { get; }

        public string SearchCostumerName
        {
            get
            {
                return string.Empty;
            }

            set
            {
                if (this._costumerCatalog.CostumerList.Exists(costumer => costumer.Name == value))
                {
                    this.SelectedCostumer =
                        this._costumerCatalog.CostumerList[this._costumerCatalog.CostumerList.FindIndex(
                                                                                                        costumer =>
                                                                                                            costumer
                                                                                                                .Name
                                                                                                            == value)];
                }
            }
        }

        public Costumer SelectedCostumer
        {
            get
            {
                if (this._selectedCostumer == null)
                {
                    this._selectedCostumer = this.CostumerList.First();
                }

                return this._selectedCostumer;
            }

            set
            {
                this._selectedCostumer = value;
                this.OnPropertyChanged();
                this.OnPropertyChanged(nameof(this.ProjectsForCostumer));
                this.DeleteCustomerCommand.RaiseCanExecuteChanged();
            }
        }

        public Project SelectedProject
        {
            get
            {
                return this._selectedProject;
            }

            set
            {
                this._selectedProject = value;
                this.OnPropertyChanged();
            }
        }

        public RelayCommand<Costumer> UpdateCustomerCommand { get; }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}