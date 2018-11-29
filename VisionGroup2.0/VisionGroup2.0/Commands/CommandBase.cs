using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using VisionGroup2._0.Interfaces;

namespace VisionGroup2._0.Commands
{
    public abstract class CommandBase : ICommandBase, ICommand
    {
        public bool CanExecute(object parameter)
        {
            return this.CanExecute();
        }

        public void Execute(object parameter)
        {
            this.Execute();
        }

        protected virtual bool CanExecute()
        {
            return true;
        }

        protected abstract void Execute();

        public event EventHandler CanExecuteChanged;

        public void RaiseCanExecuteChanged()
        {
            // ISSUE: reference to a compiler-generated field
            EventHandler canExecuteChanged = this.CanExecuteChanged;
            if (canExecuteChanged == null)
                return;
            canExecuteChanged((object) this, EventArgs.Empty);
        }
    }
}
