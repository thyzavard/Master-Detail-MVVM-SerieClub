using Projet.Entite.Class;
using Projet.Presentation.Forms.Commands;
using Projet.Presentation.Forms.Events;
using Projet.Service.Fonctions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Projet.Presentation.Forms.ViewModel
{
    public class WindowAccViewModel : BaseViewModel
    {
        #region private
        private UserCourant _user = UserCourant.Instance();
        private string _pseudo;
        private object _selectedViewModel;
        private string _imageSelect;
        private bool _isVisible = true;
        private bool _isVisibleAdmin;
        private string _recherche;
        //Windows
        private WindowPersoProfil _wPersoProfil;
        private WindowAdd _wAdd;
        #endregion

        #region Command
        public RelayCommand PersoProfilCommand { get; private set; }
        public RelayCommand AdministrationCommand { get; private set; }
        public RelayCommand OuvrirAcceuilCommand { get; private set; }
        public RelayCommand OuvrirProfilCommand { get; private set; }
        public RelayCommand QuitCommand { get; private set; }
        public RelayCommand RechercherCommand { get; private set; }
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
            }
        }
        public object SelectedViewModel
        {
            get { return _selectedViewModel; }
            set
            {
                _selectedViewModel = value;
                OuvrirProfilCommand.RaiseCanExecuteChanged();
                OuvrirAcceuilCommand.RaiseCanExecuteChanged();
                NotifyPropertyChanged(nameof(SelectedViewModel));
            }
        }

        public string ImageSelect
        {
            get
            {
                return _imageSelect;
            }
            set { _imageSelect = value; }
        }

        public bool IsVisible
        {
            get
            {
                return _isVisible;
            }

            set
            {
                _isVisible = value;
                NotifyPropertyChanged(nameof(IsVisible));
            }
        }

        public bool IsVisibleAdmin
        {
            get
            {
                return _isVisibleAdmin;
            }

            set
            {
                _isVisibleAdmin = value;
                NotifyPropertyChanged(nameof(IsVisibleAdmin));
            }
        }

        public string Recherche
        {
            get
            {
                return _recherche;
            }
            set
            {
                _recherche = value;
                NotifyPropertyChanged(nameof(Recherche));
            }
        }

        #endregion

        public WindowAccViewModel()
        {
            PersoProfilCommand = new RelayCommand(OnPersoProfil, CanExecutePersoProfil);
            AdministrationCommand = new RelayCommand(OnAdministration, CanExecuteAdministration);
            OuvrirAcceuilCommand = new RelayCommand(OnOuvrirAcceuil, CanExecuteOuvrirAcceuil);
            OuvrirProfilCommand = new RelayCommand(OnOuvrirProfil, CanExecuteOuvrirProfil);
            QuitCommand = new RelayCommand(OnQuit, CanExecuteQuit);
            RechercherCommand = new RelayCommand(OnRechercher, CanExecuteRechercher);

            SelectedViewModel = new ViewAccueilViewModel();
            OpenInfoSerieEvent.GetInstance().Handler += OnOpenInfoSerie;

            _user.Serieadd = GestionBDD.returnSerieUtilisateurFull(_user.Pseudo);

            Pseudo = _user.Pseudo;

        }

        /// <summary>
        /// Récupère le texte taper dans la textbox de recherche et va remplir une liste de toutes les séries existantes et va chercher si la série taper par l'utilisateur, si elle exite, l'utilisateur est redirigé vers la page d'informations de cette série. Sinon il y a un message d'avertissement
        /// </summary>
        /// <param name="obj"></param>
        private void OnRechercher(object obj)
        {
            if (GestionBDD.verifSerie(Recherche))
            {

                Serie seriesearch = GestionBDD.remplirSerie(Recherche);
                SelectedViewModel = new ViewSerieViewModel(seriesearch);
                RetourWindowAccueilEvent.GetInstance().Handler += OnRetourAccueil;
                Recherche = null;
            }
            else
            {
                MessageBox.Show("Cette série n'existe pas !", "", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

        }
        private bool CanExecuteRechercher(object obj)
        {
            return true;
        }

        /// <summary>
        /// Déclenche un événement permettant de fermer la fenêtre principale
        /// </summary>
        /// <param name="obj"></param>
        private void OnQuit(object obj)
        {
            WindowAccClosedEvent.GetInstance().OnWindowAccClosedHandler(EventArgs.Empty);
        }

        private bool CanExecuteQuit(object obj)
        {
            return true;
        }

        /// <summary>
        /// Affiche le usercontrol Profil
        /// </summary>
        /// <param name="obj"></param>
        private void OnOuvrirProfil(object obj)
        {
            IsVisible = false;
            SelectedViewModel = new ViewProfilViewModel();
            OpenInfoSerieEvent.GetInstance().Handler += OnOpenInfoSerie;
        }

        /// <summary>
        /// Affiche le UserControl qui affiche les informations d'une série
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">Argument de l'évenement, contient une série</param>
        private void OnOpenInfoSerie(object sender, EventArgs e)
        {
            var args = e as SerieEventArgs;
            if (args != null)
            {
                var serie = args.Serie;
                SelectedViewModel = new ViewSerieViewModel(serie);
                OpenInfoSerieEvent.GetInstance().Handler -= OnOpenInfoSerie;
                RetourWindowAccueilEvent.GetInstance().Handler += OnRetourAccueil;
            }
        }
        /// <summary>
        /// Déclencher lors d'un événement et permet d'affichier le usercontrol d'accueil
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnRetourAccueil(object sender, EventArgs e)
        {
            SelectedViewModel = new ViewAccueilViewModel();
            RetourWindowAccueilEvent.GetInstance().Handler -= OnRetourAccueil;
            OpenInfoSerieEvent.GetInstance().Handler += OnOpenInfoSerie;
        }

        private bool CanExecuteOuvrirProfil(object obj)
        {
            if (SelectedViewModel.GetType() != typeof(ViewProfilViewModel))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void OnOuvrirAcceuil(object obj)
        {
            IsVisible = true;
            SelectedViewModel = new ViewAccueilViewModel();
            OpenInfoSerieEvent.GetInstance().Handler += OnOpenInfoSerie;
        }

        private bool CanExecuteOuvrirAcceuil(object obj)
        {
            if (SelectedViewModel.GetType() != typeof(ViewAccueilViewModel))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Ouvre la fenêtre d'administration
        /// </summary>
        /// <param name="obj"></param>
        private void OnAdministration(object obj)
        {
            WindowClosedEvent.GetInstance().Handler += OnCloseWindowAdd;
            _wAdd = new WindowAdd();
            _wAdd.ShowDialog();
        }
        private void OnCloseWindowAdd(object sender, EventArgs e)
        {
            _wAdd.Close();
            WindowClosedEvent.GetInstance().Handler -= OnCloseWindowAdd;
        }
        private bool CanExecuteAdministration(object obj)
        {
            IsVisibleAdmin = _user.Modo;
            return true;
        }

        /// <summary>
        /// Ouvre la fenêtre de personnalisation de profil 
        /// </summary>
        /// <param name="obj"></param>
        private void OnPersoProfil(object obj)
        {
            WindowClosedEvent.GetInstance().Handler += OnCloseWindowPersoProfil;
            _wPersoProfil = new WindowPersoProfil();
            _wPersoProfil.ShowDialog();
        }
        private void OnCloseWindowPersoProfil(object sender, EventArgs e)
        {
            _wPersoProfil.Close();
            WindowClosedEvent.GetInstance().Handler -= OnCloseWindowPersoProfil;
        }
        private bool CanExecutePersoProfil(object obj)
        {
            return true;
        }
    }
}
