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
        private Project _project;
        private Action _remove;
        private Predicate<Costumer> _canRemove;
        public Costumer Costumer { get; set; }
        private RelayCommand<Costumer> _deleteCommand;

        public CostumerViewModel()
        {
            this._projectCatalog = new ProjectCatalog();
            _projectCatalog.Load();
            this._costumerCatalog = new CostumerCatalog();
            _costumerCatalog.Load();
            this._project = new Project();
            _remove = () =>
            {
                _costumerCatalog.Remove(SelectedCostumer);
                Refresh();
            };
            _canRemove = (Costumer selectedCostumer) => _costumerCatalog.CostumerList.Contains(SelectedCostumer);
            _deleteCommand = new RelayCommand<Costumer>(_remove, _canRemove);
        }

        public RelayCommand<Costumer> DeleteCommand
        {
            get
            {
                return _deleteCommand;
               
            }
            
        }


        public string Name
        {
            get { return SelectedCostumer.Name; }
            set
            {
                SelectedCostumer.Name = value;
                OnPropertyChanged();
            }
        }

        

        public int CVR
        {
            get { return SelectedCostumer.CvrNr; }
        }

        public int Phone
        {
            get { return SelectedCostumer.PhoneNr; }
            set
            {
                SelectedCostumer.PhoneNr = value;
                OnPropertyChanged();
            }
        }

        public string Email
        {
            get { return SelectedCostumer.Email; }
            set
            {
                SelectedCostumer.Email = value;
                OnPropertyChanged();
            }
        }


        public string HeaderText
        {
            get { return SelectedCostumer.Name; }
        }

        public string ContentText
        {
            get { return SelectedCostumer.CvrNr + " " + SelectedCostumer.PhoneNr + " " + SelectedCostumer.Email; }
        }


        public List<Project> ProjectsForCostumer
        {
            get
            {
                List<Project> list = new List<Project>();

                if (this._projectCatalog.ProjectList != null)
                {
                    foreach (var l in _projectCatalog.ProjectList)
                    {
                        if (_project.CostumerId == SelectedCostumer.CostumerId)
                        {
                            list.Add(l);
                        }
                    }
                }
                
                return list;
            }
        }

        public Project SelectedProject
        {
            get { return _project; }
            set
            {
                _project = value;
                OnPropertyChanged(nameof(ProjectsForCostumer));
            }
        }

        public List<Costumer> CostumerList
        {
            get
            {
                if (_selectedCostumer == null)
                {
                    if (this._costumerCatalog.CostumerList != null)
                    {
                        var costumerList = from costumer in _costumerCatalog.CostumerList
                            orderby costumer.Name
                            select costumer;
                        SelectedCostumer = costumerList.First();
                        return costumerList.ToList();
                    }
                    else
                    {
                        return this._costumerCatalog.CostumerList;
                    }
                }
                else
                {
                    var costumerList = from costumer in _costumerCatalog.CostumerList
                        where costumer.Name == SelectedCostumer.Name

                        orderby costumer.Name
                        select costumer;
                    return costumerList.ToList();
                }

            }
        }
        public Costumer SelectedCostumer
        {
            get
            {
                if (this._selectedCostumer != null)
                {
                    return this._selectedCostumer;
                }
                else
                {
                    this._selectedCostumer = CostumerList[0];
                    return this._selectedCostumer;
                }
            }
            set
            {
                this._selectedCostumer = value;
                OnPropertyChanged();
            }
        }

        public void Refresh()
        {
            OnPropertyChanged(nameof(CostumerCatalog.Load));
        }


        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }






}
