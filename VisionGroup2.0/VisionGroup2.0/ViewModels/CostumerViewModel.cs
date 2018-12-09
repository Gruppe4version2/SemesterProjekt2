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

namespace VisionGroup2._0.ViewModels
{
    public class CostumerViewModel : INotifyPropertyChanged 
    {
        private ProjectCatalog _projectCatalog;
        private CostumerCatalog _costumerCatalog;
        private Costumer _selectedCostumer;
        private Project _selectedProject;

        public CostumerViewModel()
        {
            this._projectCatalog = ProjectCatalog.Instance;
            this._costumerCatalog = CostumerCatalog.Instance;
            this.OnPropertyChanged(nameof(this.CostumerList));
            
            this.DeleteCustomerCommand = new RelayCommand<Costumer>(() => 
                {
                    this._costumerCatalog.Remove(this._selectedCostumer);
                    this._selectedCostumer = null;
                    this.OnPropertyChanged(nameof(this.CostumerList));
                },
                costumer => this.ProjectsForCostumer.Count == 0);
            this.UpdateCustomerCommand = new RelayCommand<Costumer>(() =>
                {
                    int id = this._selectedCostumer.CostumerId;
                    this._costumerCatalog.Update(this._selectedCostumer);
                    this.OnPropertyChanged(nameof(this.CostumerList));
                    SelectedCostumer = CostumerList.Find((costumer => costumer.CostumerId == id));
                });

        }

        public RelayCommand<Costumer> DeleteCustomerCommand { get; private set; }
        public RelayCommand<Costumer> UpdateCustomerCommand { get; private set; }


        public string ContentText
        {
            get { return this.SelectedCostumer.CvrNr + " " + this.SelectedCostumer.PhoneNr + " " + this.SelectedCostumer.Email; }
        }


        public List<Project> ProjectsForCostumer
        {
            get
            {
                var list = new List<Project>();

                if (this._projectCatalog.ProjectList != null)
                {
                    foreach (Project l in this._projectCatalog.ProjectList)
                        if (l.CostumerId == this.SelectedCostumer.CostumerId)
                        {
                            list.Add(l);
                        }
                }
                
                return list;
            }
        }

        public Project SelectedProject
        {
            get { return this._selectedProject; }
            set
            {
                this._selectedProject = value;
                this.OnPropertyChanged();
            }
        }

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
                    else
                    {
                        return this._costumerCatalog.CostumerList;
                    }
                }
            }
        }
        public Costumer SelectedCostumer
        {
            get
            {
                if (this._selectedCostumer == null)
                {
                    this._selectedCostumer = CostumerList.First();
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

        public void Refresh()
        {
            this.OnPropertyChanged(nameof(this.CostumerList));
        }


        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }






}
