using Projet.Entite.Class;
using Projet.Presentation.Forms.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet.Presentation.Forms.ViewModel
{
    public class WindowAccViewModel
    {
        private Utilisateur _user;
        private string _pseudo;
        public RelayCommand PersoProfilCommand { get; set; }
        public RelayCommand AdministrationCommand { get; set; }

        public string Pseudo
        {
            get
            {
                return _pseudo;
            }

            set
            {
                _pseudo = value;
            }
        }

        public WindowAccViewModel(Utilisateur user)
        {
            _user = user;
            Pseudo = _user.pseudo;
            PersoProfilCommand = new RelayCommand(OnPersoProfil, CanExecutePersoProfil);
            AdministrationCommand = new RelayCommand(OnAdministration, CanExecuteAdministration);
        }

        private void OnAdministration(object obj)
        {
            WindowAdd w = new WindowAdd();
            w.Show();
        }

        private bool CanExecuteAdministration(object obj)
        {
            return true;
        }

        private void OnPersoProfil(object obj)
        {
            WindowPersoProfil w = new WindowPersoProfil(_user);
            w.Show();
        }

        private bool CanExecutePersoProfil(object obj)
        {
            return true;
        }
    }
}
