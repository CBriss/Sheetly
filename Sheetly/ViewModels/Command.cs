using System;
using System.Windows.Input;

namespace Sheetly.ViewModels
{
    public class Command : ICommand
    {
        readonly Action<object> action;

        public Command(Action<object> action) => this.action = action;
        public bool CanExecute(object parameter) => true;

        public void Execute(object parameter) => action(parameter);

        public event EventHandler CanExecuteChanged;
    }
}
