using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProgrammingTechnologiesTask2.Presentation.ViewModel.Commands
{
    public class AsyncRelayCommand : ICommand
    {
        private readonly Func<Task> executeAsync;
        private readonly Func<bool> canExecute;
        private bool isExecuting;

        public AsyncRelayCommand(Func<Task> executeAsync, Func<bool> canExecute = null)
        {
            this.executeAsync = executeAsync;
            this.canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return !isExecuting && (canExecute == null || canExecute());
        }

        public async void Execute(object parameter)
        {
            if (!CanExecute(parameter))
            {
                return;
            }

            try
            {
                isExecuting = true;
                RaiseCanExecuteChanged();

                await executeAsync();
            }
            finally
            {
                isExecuting = false;
                RaiseCanExecuteChanged();
            }
        }

        public void RaiseCanExecuteChanged()
        {
            EventHandler handler = CanExecuteChanged;

            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }
    }
}