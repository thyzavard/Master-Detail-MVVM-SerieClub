using GalaSoft.MvvmLight;
using Projet.Entite.Class;
using Projet.Presentation.Forms.Commands;
using Projet.Presentation.Forms.Events;
using Projet.Presentation.Forms.Extension;
using Projet.Service.Fonctions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Projet.Presentation.Forms.ViewModel
{
    public class ViewProfilViewModel : ObservableObject
    {
        #region private
        private string _currentPseudo;
        private string _currentDescription;
        private string _titreEnFonctionDuNbDeSerie;
        private UserCourant _user = UserCourant.Instance();
        private object _selectedViewModel;
        private Serie _selectedSerie;
        private UserCourant _user_courant = UserCourant.Instance();
        private BitmapImage _imageUserCourant;
        private BitmapImage _sourceImageCouverture;
        #endregion

        #region Public
        public string CurrentPseudo
        {
            get { return _currentPseudo; }
            set { Set(() => CurrentPseudo, ref _currentPseudo, value); }
        }

        public string CurrentDescription
        {
            get
            {
                return _currentDescription;
            }
            set { Set(() => CurrentDescription, ref _currentDescription, value); }
        }

        public Serie SelectedSerie
        {
            get
            {
                return _selectedSerie;
            }
            set { Set(() => SelectedSerie, ref _selectedSerie, value); EnleverSerieCommand.RaiseCanExecuteChanged(); }
        }

        public string TitreEnFonctionDuNbDeSerie
        {
            get
            {
                return _titreEnFonctionDuNbDeSerie;
            }
            set { Set(() => TitreEnFonctionDuNbDeSerie, ref _titreEnFonctionDuNbDeSerie, value); EnleverSerieCommand.RaiseCanExecuteChanged(); }
        }

        public ObservableCollection<Serie> SerieUtilisateur { get; set; }

        public object SelectedViewModel
        {
            get { return _selectedViewModel; }
            set { Set(() => SelectedViewModel, ref _selectedViewModel, value); }
        }


        public BitmapImage ImageUserCourant
        {
            get
            {
                return _imageUserCourant;
            }
            set { Set(() => ImageUserCourant, ref _imageUserCourant, value); }
        }
        public BitmapImage SourceImageCouverture
        {
            get
            {
                return _sourceImageCouverture;
            }
            set { Set(() => SourceImageCouverture, ref _sourceImageCouverture, value); }
        }
        #endregion

        #region Command
        public RelayCommand InfoSerieCommand { get; private set; }
        public RelayCommand EnleverSerieCommand { get; private set; }
        #endregion

        
        public ViewProfilViewModel()
        {
            EnleverSerieCommand = new RelayCommand(OnEnleverSerie, CanExecuteEnleverSerie);
            InfoSerieCommand = new RelayCommand(OnInfoSerie, CanExecuteInfoSerie);

            CurrentPseudo = _user.Pseudo;
            if (_user.Description != "Description...")
            {
                CurrentDescription = _user.Description;
            }
            ImageUserCourant = _user.image;
            SourceImageCouverture = _user.couverture;

            //*****GESTION SÉRIE UTILISATEUR*****
            SerieUtilisateur = _user.Serieadd.ToObservableCollection();

            updateMessage();
        }

        public void updateMessage()
        {
            //*****GESTION DU MESSAGE EN FONCTION DU NOMBRE DE SÉRIE*****
            if (SerieUtilisateur.Count == 0) { TitreEnFonctionDuNbDeSerie = "Je n'ai pas de séries préférées..."; }
            else if (SerieUtilisateur.Count == 1) { TitreEnFonctionDuNbDeSerie = "Ma série préférée"; }
            else { TitreEnFonctionDuNbDeSerie = "Mes séries préférées"; }
        }

        /// <summary>
        /// Affiche le UserControl qui affiche les informations d'une série
        /// </summary>
        /// <param name="obj"></param>
        private void OnInfoSerie(object obj)
        {
            if(SelectedSerie != null)
            {
                OpenInfoSerieEvent.GetInstance().OnOpenInfoSerieHandler(new SerieEventArgs() { Serie = SelectedSerie });
            }
        }

        private bool CanExecuteInfoSerie(object obj)
        {
            return true;
        }

        /// <summary>
        /// Retire une série des favoris d'un utilisateur
        /// </summary>
        /// <param name="obj"></param>
        private void OnEnleverSerie(object obj)
        {
            if (SelectedSerie != null)
            {
                _user_courant.Serieadd.Remove(SelectedSerie);
                GestionBDD.removeSerieUtilisateur(_user_courant.Pseudo, SelectedSerie.nom);
                MessageBox.Show("Série enlevée des favoris !", "", MessageBoxButton.OK, MessageBoxImage.Information);
                SerieUtilisateur.Remove(SelectedSerie);
                updateMessage();
            }
        }

        private bool CanExecuteEnleverSerie(object obj)
        {
            return true;
        }
    }
}
