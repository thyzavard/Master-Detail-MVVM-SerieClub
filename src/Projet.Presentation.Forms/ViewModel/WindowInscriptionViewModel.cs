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
    public class WindowInscriptionViewModel
    {
        private string _pseudo;
        private string _password1;
        private string _password2;
        private List<string> _sexe;


        public RelayCommand QuitCommand { get; set; }
        public RelayCommand InscriptionCommand { get; set; }

        public string Pseudo
        {
            get
            {
                return _pseudo;
            }

            set
            {
                _pseudo = value;
                InscriptionCommand.RaiseCanExecuteChanged();
            }
        }

        public string Password1
        {
            get
            {
                return _password1;
            }

            set
            {
                _password1 = value;
                InscriptionCommand.RaiseCanExecuteChanged();
            }
        }

        public string Password2
        {
            get
            {
                return _password2;
            }

            set
            {
                _password2 = value;
                InscriptionCommand.RaiseCanExecuteChanged();
            }
        }

        public List<string> Sexe
        {
            get
            {
                return _sexe;
            }

            set
            {
                _sexe = value;
            }
        }

        public WindowInscriptionViewModel()
        {
            Sexe = new List<string>();
            Sexe.Add("Pas spécifié...");
            Sexe.Add("Masculin");
            Sexe.Add("Féminin");
            QuitCommand = new RelayCommand(OnQuit, CanExecuteQuit);
            InscriptionCommand = new RelayCommand(OnInscription, CanExecuteInscription);
        }

        private bool CanExecuteInscription(object obj)
        {
            if (Pseudo != null)
            {
                if(Password1 != null && Password2 != null)
                {
                    if(Password1 == Password2)
                    {
                        return true;
                    }
                    return false;
                }
                return false;
            }
            return false;
        }

        private void OnInscription(object obj)
        {
            if (GestionBDD.verifLogin(Pseudo))
            {
                MessageBox.Show("Ce nom de compte est déjà utilisé", "Erreur", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                GestionBDD.inscription(Pseudo, Password1);
                /*if ( != "Pas spécifié...")
                {
                    GestionBDD.updateSexe(cmbSexe.Text, Pseudo);
                }*/
                MessageBox.Show("Inscription enregistrée", "Confirmation", MessageBoxButton.OK);
            }
        }

        private void OnQuit(object obj)
        {
            
        }

        private bool CanExecuteQuit(object obj)
        {
            return true;
        }
    }
}
