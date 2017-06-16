using GalaSoft.MvvmLight;
using Projet.Entite.Class;
using Projet.Presentation.Forms.Commands;
using Projet.Presentation.Forms.Events;
using Projet.Service.Fonctions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Projet.Presentation.Forms.ViewModel
{
    public class MainWindowViewModel : ObservableObject
    {
        #region private
        private string _password;
        private string _identifiant;
        private WindowAcc _wAcceuil;
        private WindowInscription _wInscription;
        #endregion

        #region Command
        public RelayCommand InscrireCommand { get; private set; }
        public RelayCommand ConnexionCommand { get; private set; }
        public RelayCommand QuitCommand { get; set; }
        #endregion

        #region Public
        public string Identifiant
        {
            get
            {
                return _identifiant;
            }
            set { Set(() => Identifiant, ref _identifiant, value); }
        }

        public string Password
        {
            get
            {
                return _password;
            }
            set { Set(() => Password, ref _password, value); }
        }
        #endregion


       
        public MainWindowViewModel()
        {
            InscrireCommand = new RelayCommand(OnInscription, CanExecuteInscription);
            ConnexionCommand = new RelayCommand(OnLogin, CanExecuteLogin);
            QuitCommand = new RelayCommand(OnQuit, CanExecuteQuit);


            //Si les dossiers "Images" et "ImagesSerie" n'existent pas, ils sont créer et rempli avec les images de base
            if (!GestionFichierImage.creerFichierImages())
            {
                GestionFichierImage.triImageUser();
                GestionFichierImage.triImageSerie();
            }
            

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

        /// <summary>
        /// Vérifie les identifiants rentrés par l'utilisateur, si ils sont correct, la classe UserCourant est instancié et le fenêtre WindowAcc est ouverte
        /// </summary>
        /// <param name="obj"></param>
        private void OnLogin(object obj)
        {
            if(GestionBDD.verifLoginMdp(Identifiant, Password))
            {
                WindowAccClosedEvent.GetInstance().Handler += OnCloseWindowAcceuil;
                UserCourant.SetNull();
                GestionBDD.remplirUserCourant(Identifiant);

                _wAcceuil = new WindowAcc();
                Identifiant = null;
                Password = null;
                _wAcceuil.ShowDialog();
            }
            else
            {
                MessageBox.Show("Les informations transmises n'ont pas permis de vous authentifier", "Erreur d'identification", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        private void OnCloseWindowAcceuil(object sender, EventArgs e)
        {
            _wAcceuil.Close();
            WindowAccClosedEvent.GetInstance().Handler -= OnCloseWindowAcceuil;
        }

        private bool CanExecuteInscription(object obj)
        {
            return true;
        }

        /// <summary>
        /// Ouvre la fenêtre d'inscription, tout en s'abonnant à l'événement pour sa fermeture
        /// </summary>
        /// <param name="obj"></param>
        private void OnInscription(object obj)
        {
            WindowClosedEvent.GetInstance().Handler += OnCloseWInscription;
            _wInscription = new WindowInscription();
            _wInscription.ShowDialog();
            
        }
        private void OnCloseWInscription(object sender, EventArgs e)
        {
            _wInscription.Close();
            WindowClosedEvent.GetInstance().Handler -= OnCloseWInscription;
        }
    }
}
