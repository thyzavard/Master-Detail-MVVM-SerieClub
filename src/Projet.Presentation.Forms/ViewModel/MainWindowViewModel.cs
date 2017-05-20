using Projet.Entite.Class;
using Projet.Presentation.Forms.Commands;
using Projet.Service.Fonctions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Projet.Presentation.Forms.ViewModel
{
    public class MainWindowViewModel
    {
        public RelayCommand InscrireCommand { get; private set; }
        public RelayCommand ConnexionCommand { get; private set; }
        public RelayCommand QuitCommand { get; set; }


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

        public string Password
        {
            get
            {
                return _password;
            }

            set
            {
                _password = value;
                ConnexionCommand.RaiseCanExecuteChanged();
            }
        }

        private string _password;
        private string _identifiant;

        public MainWindowViewModel()
        {
            InscrireCommand = new RelayCommand(OnInscription, CanExecuteInscription);
            ConnexionCommand = new RelayCommand(OnLogin, CanExecuteLogin);
            QuitCommand = new RelayCommand(OnQuit, CanExecuteQuit);
        }

        private void OnQuit(object obj)
        {
            Application.Current.Shutdown();
        }

        private bool CanExecuteQuit(object obj)
        {
            return true;
        }

        private bool CanExecuteLogin(object obj)
        {
            return true;
            //return Identifiant?.Length > 5;
        }

        private void OnLogin(object obj)
        {
            if(GestionBDD.verifLoginMdp(Identifiant, Password))
            {
                Utilisateur user = GestionBDD.remplirUser(Identifiant);
                WindowAcc w = new WindowAcc(user);
                w.Show();
            }
            else
            {
                MessageBox.Show("Les informations transmises n'ont pas permis de vous authentifier", "Erreur d'identification", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private bool CanExecuteInscription(object obj)
        {
            return true;
        }

        private void OnInscription(object obj)
        {
            WindowInscription wInsc = new WindowInscription();
            wInsc.Show();
        }
    }
}
