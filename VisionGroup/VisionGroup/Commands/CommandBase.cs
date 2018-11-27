using System;
using System.Windows.Input;

namespace VisionGroup.Commands
{
    public abstract class CommandBase : ICommand
    {
        protected abstract bool CanExecute();

        protected abstract void Execute();


        public bool CanExecute(object parameter)
        {
            return CanExecute();
        }

        public void Execute(object parameter)
        {
            Execute();
        }

        public event EventHandler CanExecuteChanged;
    }
}