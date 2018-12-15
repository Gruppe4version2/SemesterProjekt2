using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml.Controls;
using VisionGroup2._0.Interfaces;

namespace VisionGroup2._0.Commands
{
    public class NavigationCommand : ICommand
    {
        private Frame _frame;
        private Type _pageType;
        private Func<bool> _canNavigateFunc;

        public NavigationCommand(Frame frame, Type pageType, Func<bool> canNavigateFunc)
        {
            this._frame = frame;
            this._pageType = pageType;
            this._canNavigateFunc = canNavigateFunc;
        }

        public NavigationCommand(Frame frame, Type pageType)
            : this(frame, pageType, (Func<bool>) (() => true))
        {
        }

        public bool CanExecute(object parameter)
        {
            return this._canNavigateFunc();
        }

        public void Execute(object parameter)
        {
            this._frame.Navigate(this._pageType);
        }

        public event EventHandler CanExecuteChanged;
        public void RaiseCanExecuteChanged()
        {
            EventHandler canExecuteChanged = this.CanExecuteChanged;
            if (canExecuteChanged == null)
                return;
            canExecuteChanged((object)this, EventArgs.Empty);
        }
    }
}
