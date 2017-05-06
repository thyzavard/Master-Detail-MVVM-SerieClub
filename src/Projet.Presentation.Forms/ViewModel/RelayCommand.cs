using System;
using System.Windows.Input;

namespace Example.Navigation.Presentation.App.Commands
{
    public class RelayCommand : ICommand
    {
        private Action<object> execute;
        private Predicate<object> canExecute;
        private event EventHandler CanExecuteChangedInternal;

        public RelayCommand(Action<object> execute) : this(execute, (parameter) => true) { }

        public RelayCommand(Action<object> execute, Predicate<object> canExecute)
        {
            if (execute == null)
                throw new ArgumentException("the parameter cannot be null", nameof(execute));

            if (canExecute == null)
                throw new ArgumentException("the parameter cannot be null", nameof(canExecute));

            this.execute = execute;
            this.canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
                CanExecuteChangedInternal += value;
            }
            remove
            {
                CommandManager.RequerySuggested -= value;
                CanExecuteChangedInternal -= value;
            }
        }

        public bool CanExecute(object parameter) => canExecute != null && canExecute(parameter);

        public void Execute(object parameter) => execute(parameter);

        public void OnCanExecuteChanged() => CanExecuteChangedInternal?.Invoke(this, EventArgs.Empty);

        public void Destroy()
        {
            canExecute = _ => false;
            execute = _ => { return; };
        }
    }
}
