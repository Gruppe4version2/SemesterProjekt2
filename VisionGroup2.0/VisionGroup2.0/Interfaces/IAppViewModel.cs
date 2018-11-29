using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml.Controls;

namespace VisionGroup2._0.Interfaces
{
    public interface IAppViewModel
    {
        Frame AppFrame { get; }

        Dictionary<string, ICommand> NavigationCommands { get; }

        void SetAppFrame(Frame appFrame);

        void AddCommands();
    }
}
