namespace VisionGroup2._0.Commands
{
    using System;
    using System.Windows.Input;

    using Windows.UI.Xaml.Controls;

    public class NavigationCommand : ICommand
    {
        private readonly Func<bool> _canNavigateFunc;

        private readonly Frame _frame;

        private readonly Type _pageType;

        public NavigationCommand(Frame frame, Type pageType, Func<bool> canNavigateFunc)
        {
            this._frame = frame;
            this._pageType = pageType;
            this._canNavigateFunc = canNavigateFunc;
        }

        public NavigationCommand(Frame frame, Type pageType)
            : this(frame, pageType, () => true)
        {
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return this._canNavigateFunc();
        }

        public void Execute(object parameter)
        {
            this._frame.Navigate(this._pageType);
        }

        public void RaiseCanExecuteChanged()
        {
            EventHandler canExecuteChanged = this.CanExecuteChanged;
            if (canExecuteChanged == null)
            {
                return;
            }

            canExecuteChanged(this, EventArgs.Empty);
        }
    }
}