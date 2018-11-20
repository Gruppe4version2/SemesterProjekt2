using System.ComponentModel;
using System.Runtime.CompilerServices;
using VisionGroup.Annotations;

namespace VisionGroup.Viewmodels
{
    public class CostumerViewModel : INotifyPropertyChanged
    {

        public Costumer Costumer { get; set; }


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





        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}