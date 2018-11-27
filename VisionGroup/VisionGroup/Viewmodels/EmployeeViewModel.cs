using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using VisionGroup.Annotations;
using VisionGroup.DomainClasses;

namespace VisionGroup.Viewmodels
{
    public class EmployeeViewModel : InotifyPropertyChanged
    {
        public Employee Employee { get; set; }


        public string Name
        {
            get { return Employee.Name; }
            set
            {
                Employee.Name = value;
                OnPropertyChanged();
            }
        }

        
        public int Phone
        {
            get { return Employee.PhoneNr; }
            set
            {
                Employee.PhoneNr = value;
                OnPropertyChanged();
            }
        }

        public string Email
        {
            get { return Employee.Email; }
            set
            {
                Employee.Email = value;
                OnPropertyChanged();
            }
        }

        public Project Aktivprojekt
        {
            get { return Employee.AktivProject; }
            set
            {
                Employee.AktivProject = value;
                OnPropertyChanged();
            }
        }

        public Project InAktivprojekt
        {
            get { return Employee.InaktivProject; }
            set
            {
                Employee.InaktivProject = value;
                OnPropertyChanged();
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
