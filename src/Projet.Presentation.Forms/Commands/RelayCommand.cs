using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Projet.Presentation.Forms.Commands
{
    public class RelayCommand : ICommand
    {

        private Action executeInternal;

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;

        }

        public void Execute(object parameter)
        {
            executeInternal();
        }


        public RelayCommand(Action execute)
        {
            executeInternal=execute;

     


        }
    }
}
