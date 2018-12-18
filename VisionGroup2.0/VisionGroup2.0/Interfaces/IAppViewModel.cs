namespace VisionGroup2._0.Interfaces
{
    using System.Collections.Generic;
    using System.Windows.Input;

    using Windows.UI.Xaml.Controls;

    public interface IAppViewModel
    {
        Frame AppFrame { get; }

        Dictionary<string, ICommand> NavigationCommands { get; }

        void AddCommands();

        void SetAppFrame(Frame appFrame);
    }
}