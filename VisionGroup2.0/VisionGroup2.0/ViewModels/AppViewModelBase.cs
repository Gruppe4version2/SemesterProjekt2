namespace VisionGroup2._0.ViewModels
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows.Input;

    using VisionGroup2._0.Interfaces;

    using Windows.UI.Xaml.Controls;

    public abstract class AppViewModelBase : INotifyPropertyChanged, IAppViewModel
    {
        protected AppViewModelBase()
        {
            this.AppFrame = AppFrameInstance;
            this.NavigationCommands = new Dictionary<string, ICommand>();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public static Frame AppFrameInstance { get; set; }

        public Frame AppFrame { get; private set; }

        public Dictionary<string, ICommand> NavigationCommands { get; }

        public abstract void AddCommands();

        public void SetAppFrame(Frame appFrame)
        {
            this.AppFrame = appFrame;
            AppFrameInstance = this.AppFrame;
            this.AddCommands();
            this.OnPropertyChanged("NavigationCommands");
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            // ISSUE: reference to a compiler-generated field
            PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if (propertyChanged == null)
            {
                return;
            }

            propertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}