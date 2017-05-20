using Projet.Presentation.Forms.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet.Presentation.Forms.ViewModel
{
    public class MainWindowViewModel
    {
        public RelayCommand InscrireCommand { get; private set; }

        public string Identifiant
        {
            get
            {
                return _identifiant;
            }

            set
            {
                _identifiant = value;
                InscrireCommand.RaiseCanExecuteChanged();
            }
        }

        private string _identifiant;

        public MainWindowViewModel()
        {
            InscrireCommand = new RelayCommand(OnLogin, CanExecuteLogin);
        }

        private bool CanExecuteLogin(object obj)
        {
            return true;
            //return Identifiant?.Length > 5;
        }

        private void OnLogin(object obj)
        {

        }
    }
}

