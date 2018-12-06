using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using VisionGroup2._0.Catalogs;
using VisionGroup2._0.DomainClasses;
using VisionGroup2._0.Interfaces;
using VisionGroup2._0.ViewModels;

namespace VisionGroup2._0.Commands
{
  public class RelayCommand<T> : ICommand
   {
      private readonly Predicate<T> _canExecute;
      private readonly Action _execute;
 
      public RelayCommand(Action execute)
         : this(execute, null)
      {
         _execute = execute;
      }
 
      public RelayCommand(Action execute, Predicate<T> canExecute)
      {
         if (execute == null)
         {
            throw new ArgumentNullException("execute");
         }
         _execute = execute;
         _canExecute = canExecute;
      }
 
      public bool CanExecute(object parameter)
      {
         return _canExecute == null || _canExecute((T) parameter);
      }
 
      public void Execute(object parameter)
      {
         _execute();
      }

       public event EventHandler CanExecuteChanged;

       public void RaiseCanExecuteChanged()
       {
           // ISSUE: reference to a compiler-generated field
           EventHandler canExecuteChanged = this.CanExecuteChanged;
           if (canExecuteChanged == null)
               return;
           canExecuteChanged((object)this, EventArgs.Empty);
       }
    }

}
