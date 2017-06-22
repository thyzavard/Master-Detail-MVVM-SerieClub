using GalaSoft.MvvmLight;
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
using System.Windows.Documents;
using System.Windows.Media.Imaging;

namespace Projet.Presentation.Forms.ViewModel
{
    public class WindowAccViewModel : ObservableObject
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
            set { Set(() => Pseudo, ref _pseudo, value); }
        }
        public object SelectedViewModel
        {
            get { return _selectedViewModel; }
            set { Set(() => SelectedViewModel, ref _selectedViewModel, value); OuvrirAcceuilCommand.RaiseCanExecuteChanged(); OuvrirProfilCommand.RaiseCanExecuteChanged(); }
        }

        public string ImageSelect
        {
            get
            {
                return _imageSelect;
            }
            set { Set(() => ImageSelect, ref _imageSelect, value); }
        }

        public bool IsVisible
        {
            get
            {
                return _isVisible;
            }
            set { Set(() => IsVisible, ref _isVisible, value); }
        }

        public bool IsVisibleAdmin
        {
            get
            {
                return _isVisibleAdmin;
            }
            set { Set(() => IsVisibleAdmin, ref _isVisibleAdmin, value); }
        }

        public string Recherche
        {
            get
            {
                return _recherche;
            }
            set { Set(() => Recherche, ref _recherche, value); OnRechercher(new object()); }
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
        /// Fonction appelé dès que l'utilisateur entre une lettre dans la combobox de recherche, et compare la liste des série avec ce qu'a écris l'utilisateur et ouvre la viewRecherche et affiche les série correspondante ou non
        /// </summary>
        /// <param name="obj"></param>
        private void OnRechercher(object obj)
        {
            if (Recherche == "")
            {
                SelectedViewModel = new ViewAccueilViewModel();
                OpenInfoSerieEvent.GetInstance().Handler += OnOpenInfoSerie;
            }
            else
            {
                List<Serie> listSerie = GestionBDD.returnTouteSerieFull();

                var resRecherche = listSerie.Where(h => h.nom.ToLower().StartsWith(Recherche.ToLower()));

                SelectedViewModel = new ViewRechercheViewModel(resRecherche, Recherche);
                RetourWindowAccueilEvent.GetInstance().Handler += OnRetourAccueil;
                OpenInfoSerieEvent.GetInstance().Handler += OnOpenInfoSerie;
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
            Recherche = "";
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
            RefreshEvent.GetInstance().Handler += OnRefresh;
            _wAdd = new WindowAdd();
            _wAdd.ShowDialog();
        }

        private void OnRefresh(object sender, EventArgs e)
        {
            if(SelectedViewModel.GetType() == typeof(ViewAccueilViewModel) || SelectedViewModel.GetType() == typeof(ViewSerieViewModel) || SelectedViewModel.GetType() == typeof(ViewRechercheViewModel))
            {
                SelectedViewModel = new ViewAccueilViewModel();
                OpenInfoSerieEvent.GetInstance().Handler += OnOpenInfoSerie;
                RetourWindowAccueilEvent.GetInstance().Handler += OnRetourAccueil;
            }
            if(SelectedViewModel.GetType() == typeof(ViewProfilViewModel)){
                SelectedViewModel = new ViewProfilViewModel();
                OpenInfoSerieEvent.GetInstance().Handler += OnOpenInfoSerie;
            }
            
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
            RefreshEvent.GetInstance().Handler += OnRefresh;
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
