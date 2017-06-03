using Projet.Presentation.Forms.Commands;
using Projet.Presentation.Forms.Events;
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
        #region private
        private string _pseudo;
        private string _password1;
        private string _password2;
        private List<string> _sexesource;
        private string _selectSexe;
        #endregion

        #region Command
        public RelayCommand QuitCommand { get; private set; }
        public RelayCommand InscriptionCommand { get; private set; }
        #endregion

        #region Public
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

        public List<string> SexeSource
        {
            get
            {
                return _sexesource;
            }

            set
            {
                _sexesource = value;
            }
        }

        public string SelectSexe
        {
            get
            {
                return _selectSexe;
            }
            set
            {
                _selectSexe = value;
                InscriptionCommand.RaiseCanExecuteChanged();
            }
        }
        #endregion

        public WindowInscriptionViewModel()
        {
            SexeSource = new List<string>();
            SexeSource.Add("Pas spécifié...");
            SexeSource.Add("Masculin");
            SexeSource.Add("Féminin");

            QuitCommand = new RelayCommand(OnQuit, CanExecuteQuit);
            InscriptionCommand = new RelayCommand(OnInscription, CanExecuteInscription);
        }

        private bool CanExecuteInscription(object obj)
        {
            if (Pseudo != null)
            {
                if (Password1 != null && Password2 != null)
                {
                    if (SelectSexe != null)
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
                if (Password1 == Password2)
                {
                    GestionBDD.inscription(Pseudo, Password1);
                    if (SelectSexe != "Pas spécifié...")
                    {
                        GestionBDD.updateSexe(SelectSexe, Pseudo);
                    }
                    MessageBox.Show("Inscription enregistrée", "Confirmation", MessageBoxButton.OK);
                }
                else
                {
                    MessageBox.Show("Veuillez rentrer le même mot de passe", "Mot de passe incorrect", MessageBoxButton.OK, MessageBoxImage.Warning);
                }

            }
        }

        private void OnQuit(object obj)
        {
            WindowClosedEvent.GetInstance().OnWindowClosedHandler(EventArgs.Empty);
        }

        private bool CanExecuteQuit(object obj)
        {
            return true;
        }
    }
}
