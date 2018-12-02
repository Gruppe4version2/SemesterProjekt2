using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml.Controls;
using VisionGroup2._0.Interfaces;

namespace VisionGroup2._0.ViewModels
{
    public abstract class AppViewModelBase : INotifyPropertyChanged, IAppViewModel
    {
        private static Frame _appFrameInstance;
        private Frame _appFrame;
        private Dictionary<string, ICommand> _navigationCommands;

        protected AppViewModelBase()
        {
            _appFrame = _appFrameInstance;
            _navigationCommands = new Dictionary<string, ICommand>();
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
            get
            {
                return _navigationCommands;
            }
        }

        public Frame AppFrame
        {
            get
            {
                return _appFrame;
            }
        }

        public void SetAppFrame(Frame appFrame)
        {
            _appFrame = appFrame;
            AppFrameInstance = _appFrame;
            AddCommands();
            OnPropertyChanged("NavigationCommands");
        }

        public abstract void AddCommands();

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            // ISSUE: reference to a compiler-generated field
            PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if (propertyChanged == null)
                return;
            propertyChanged((object)this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
