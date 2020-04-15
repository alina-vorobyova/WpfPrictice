using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace MovieSearchWpf.Commands
{
    public class Command<T> : CommandBase, ICommand
    {
        private Action<T> action;

        public Command(Action<T> action, Func<bool> canExecute = null)
        {
            this.action = action;
            this.canExecute = canExecute;
        }

        public void Execute(object parameter)
        {
            action?.Invoke((T)parameter);
        }
    }

    public class Command : CommandBase, ICommand
    {
        private Action action;

        public Command(Action action, Func<bool> canExecute = null)
        {
            this.action = action;
            this.canExecute = canExecute;
        }

        public void Execute(object parameter)
        {
            action?.Invoke();
        }
    }
}
