using Projet.Entite.Class;
using Projet.Presentation.Forms.Commands;
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
    public class ViewProfilViewModel : INotifyPropertyChanged
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
        #endregion

        #region Public
        public string CurrentPseudo
        {
            get { return _currentPseudo; }
            set
            {
                _currentPseudo = value;
            }
        }

        public string CurrentDescription
        {
            get
            {
                return _currentDescription;
            }
            set
            {
                _currentDescription = value;
            }
        }

        public Serie SelectedSerie
        {
            get
            {
                return _selectedSerie;
            }
            set
            {
                _selectedSerie = value;
                EnleverSerieCommand.RaiseCanExecuteChanged();
                NotifyPropertyChanged(nameof(SelectedSerie));
            }
        }

        public string TitreEnFonctionDuNbDeSerie
        {
            get
            {
                return _titreEnFonctionDuNbDeSerie;
            }
            set
            {
                _titreEnFonctionDuNbDeSerie = value;
            }
        }

        public ObservableCollection<Serie> SerieUtilisateur { get; set; }

        public object SelectedViewModel
        {
            get { return _selectedViewModel; }
            set
            {
                _selectedViewModel = value;
                NotifyPropertyChanged(nameof(SelectedViewModel));
            }
        }


        public BitmapImage ImageUserCourant
        {
            get
            {
                return _imageUserCourant;
            }
            set
            {
                _imageUserCourant = value;
                NotifyPropertyChanged(nameof(ImageUserCourant));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
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
            CurrentDescription = _user.Description;
            ImageUserCourant = _user.image;

            //*****GESTION SÉRIE UTILISATEUR*****
            SerieUtilisateur = _user.Serieadd.ToObservableCollection();

            //*****GESTION DU MESSAGE EN FONCTION DU NOMBRE DE SÉRIE*****
            if (SerieUtilisateur.Count == 0) { TitreEnFonctionDuNbDeSerie = "Je n'ai pas de séries préférées..."; }
            else if (SerieUtilisateur.Count == 1) { TitreEnFonctionDuNbDeSerie = "Ma série préférée"; }
            else { TitreEnFonctionDuNbDeSerie = "Mes séries préférées"; }   
        }

        private void OnInfoSerie(object obj)
        {

        }

        private bool CanExecuteInfoSerie(object obj)
        {
            return true;
        }

        private void OnEnleverSerie(object obj)
        {
            if (SelectedSerie != null)
            {
                _user_courant.Serieadd.Remove(SelectedSerie);
                GestionBDD.removeSerieUtilisateur(_user_courant.Pseudo, SelectedSerie.nom);
                MessageBox.Show("Série enlevée des favoris !", "", MessageBoxButton.OK, MessageBoxImage.Information);
                SerieUtilisateur.Remove(SelectedSerie);
               
            }
        }

        private bool CanExecuteEnleverSerie(object obj)
        {
            return true;
        }
    }
}
