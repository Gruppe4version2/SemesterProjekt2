using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Windows.UI.Xaml.Controls;
using Commands.Implementation;
using Utilis.UI.Navigation;
using VisionGroup.Annotations;


namespace VisionGroup.Viewmodels
{
    public class AppViewModel : INotifyPropertyChanged
    {
        private NavigationViewItem _navigationViewItem;
        private Dictionary<string, ICommand> _navigationCommands;
        private Frame _appFrame;
        private static Frame _appFrameInstance;


        public AppViewModel()
        {
            _navigationViewItem = null;
        }

        public static Frame AppFrameInstance
        {
            get
            {
                return _appFrameInstance;
            }
            set
            {
                _appFrameInstance = value;
            }
        }

        public Dictionary<string, ICommand> NavigationCommands
        {
            get { return this._navigationCommands; }
        }
        public NavigationViewItem SelectedNavigationViewItem
        {
            get { return _navigationViewItem;}
            set
            {
                _navigationViewItem = value;
                if (_navigationViewItem == null) return;

                string tag = _navigationViewItem.Tag.ToString();
                if (!NavigationCommands.ContainsKey(tag))
                {
                    throw new ArgumentException($"Siden {tag} findes ikke");
                }
                NavigationCommands[tag].Execute(null);
               
            }
        }

        public void AddCommands()
        {
            NavigationCommands.Add("ÅbenProjektView", new NavigationCommand(_appFrame, typeof(ProjectViewModel)));
            NavigationCommands.Add("ÅbenCostumerView", new NavigationCommand(_appFrame, typeof(CostumerViewModel)));

        }

        public void SetAppFrame(Frame appFrame)
        {
            _appFrame = appFrame;
            AppFrameInstance = _appFrame;
            AddCommands();
            OnPropertyChanged(nameof(NavigationCommands));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}