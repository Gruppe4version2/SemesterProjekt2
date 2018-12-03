using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using VisionGroup2._0.Annotations;
using VisionGroup2._0.Catalogs;
using VisionGroup2._0.DomainClasses;

namespace VisionGroup2._0.ViewModels
{
    public class CostumerViewModel : INotifyPropertyChanged
    {
        private ProjectCatalog _projectCatalog;
        private CostumerCatalog _costumerCatalog;


        public Costumer Costumer { get; set; }
        public Project Project { get; set; }

        public CostumerViewModel()
        {
            this._projectCatalog = new ProjectCatalog();
            this._costumerCatalog = new CostumerCatalog();
        }


        public string Name
        {
            get { return Costumer.Name; }
            set
            {
                Costumer.Name = value;
                OnPropertyChanged();
            }
        }

        public int CVR
        {
            get { return Costumer.CvrNr; }
        }

        public int Phone
        {
            get { return Costumer.PhoneNr; }
            set
            {
                Costumer.PhoneNr = value;
                OnPropertyChanged();
            }
        }

        public string Email
        {
            get { return Costumer.Email; }
            set
            {
                Costumer.Email = value;
                OnPropertyChanged();
            }
        }
        public string HeaderText
        {
            get { return Costumer.Name; }
        }

        public string ContentText
        {
            get { return Costumer.CvrNr + " " + Costumer.PhoneNr + " " + Costumer.Email; }
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
                        if (Project.CostumerId == Costumer.CostumerId)
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
            get { return Project; }
            set
            {
                Project = value;
                OnPropertyChanged(nameof(ProjectsForCostumer));
            }
        }

        public List<Costumer> CostumerList
        {
            get {
                if (SelectedCostumer == null)
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
                        SelectedCostumer = costumerList.First();
                        return costumerList.ToList();
                    }

                }
            }
        public Costumer SelectedCostumer
        {
            get { return Costumer; }
            set
            {
                Costumer = value;
                OnPropertyChanged(nameof(CostumerList));
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
