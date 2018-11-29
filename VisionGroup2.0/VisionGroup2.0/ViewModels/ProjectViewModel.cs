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
    public class ProjectViewModel : INotifyPropertyChanged
    {

        private ProjectCatalog _projectCatalog;

        public Project Project { get; set; }

        public string Name
        {
            get { return Project.Name; }
            set
            {
                Project.Name = value;
                OnPropertyChanged();
            }
        }

        public DateTime? Deadline
        {
            get { return Project.Deadline; }
            set
            {
                Project.Deadline = value;
                OnPropertyChanged();
            }
        }

        public string ProjectLeader
        {
            get { return Project.ProjectLeader; }
            set
            {
                Project.ProjectLeader = value;
                OnPropertyChanged();
            }
        }

        public Costumer Costumer
        {
            get { return Project.Costumer; }
        }

        public List<Project> ListOfProjects
        {
            get { return _projectCatalog.ProjectList; }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }
    }
}
