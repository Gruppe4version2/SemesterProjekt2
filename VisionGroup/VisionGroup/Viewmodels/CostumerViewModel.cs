using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using VisionGroup.Annotations;

namespace VisionGroup.Viewmodels
{
    public class CostumerViewModel : INotifyPropertyChanged
    {
        private ProjectCatalog _projectCatalog;
        private CostumerCatalog _costumerCatalog;


        public Costumer Costumer { get; set; }
        public Project Project { get; set; }


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
            get { return Costumer.PhonNr; }
            set
            {
                Costumer.PhonNr = value;
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

        public List<Project> ProjectsForCostumer
        {
            get
            {
                List<Project> list = new List<Project>();

                foreach (var l in _projectCatalog.ProjectList)
                {
                    if (Project.ProjectNavigation.CostumerId == Costumer.CostumerId)
                    {
                        list.Add(l);
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
            get { return _costumerCatalog.CostumerList; }
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

        public string HeaderText
        {
            get { return Costumer.Name; }
        }

        public string ContentText
        {
            get { return Costumer.CvrNr + " " + Costumer.PhonNr + " " + Costumer.Email; }
        }





        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}