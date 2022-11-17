using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WPF
{
    public class CustomCommand : ICommand
    {
        private readonly Action<object?>? _execute;
        private readonly Func<object?, bool>? _canExecute;

        public CustomCommand(Action<object?>? execute, Func<object?, bool>? canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public bool CanExecute(object? param)
        {
            return _canExecute == null || _canExecute(param);
        }

        public void Execute(object? param)
        {
            _execute?.Invoke(param);
        }

        public event EventHandler? CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }
    }
}
