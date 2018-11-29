using System.Windows.Input;

namespace VisionGroup2._0.Interfaces
{
    public interface ICommandBase : ICommand
    {
        void RaiseCanExecuteChanged();
    }
}