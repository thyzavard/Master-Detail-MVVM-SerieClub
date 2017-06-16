using Projet.Entite.Class;
using Projet.Presentation.Forms.Commands;
using Projet.Service.Fonctions;
using Projet.Presentation.Forms.Extension;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Collections.ObjectModel;
using Projet.Presentation.Forms.Events;
using GalaSoft.MvvmLight;

namespace Projet.Presentation.Forms.ViewModel
{

    public class ViewAccueilViewModel : ObservableObject
    {
        #region private
        private List<Serie> _listSerie;
        private object _selectedViewModel;
        private Serie _selectedSerie;
        private UserCourant _user_courant = UserCourant.Instance();
        private string _ajouterOuSupprimerButton;
        #endregion

        #region Public
        public ObservableCollection<Serie> ListserieAction { get; set; }
        public ObservableCollection<Serie> ListserieHorreur { get; set; }
        public ObservableCollection<Serie> ListserieFantastique { get; set; }
        public ObservableCollection<Serie> ListserieDrame { get; set; }
        public ObservableCollection<Serie> ListserieComedie { get; set; }

        public object SelectedViewModel
        {
            get
            {
                return _selectedViewModel;
            }
            set { Set(() => SelectedViewModel, ref _selectedViewModel, value); AjouterSerieCommand.RaiseCanExecuteChanged(); }
        }


        public Serie SelectedSerie
        {
            get
            {
                return _selectedSerie;
            }
            set { Set(() => SelectedSerie, ref _selectedSerie, value); AjouterSerieCommand.RaiseCanExecuteChanged(); }
        }
        public string AjouterOuSupprimerButton
        {
            get
            {
                return _ajouterOuSupprimerButton;
            }
            set { Set(() => AjouterOuSupprimerButton, ref _ajouterOuSupprimerButton, value); AjouterSerieCommand.RaiseCanExecuteChanged(); }
        }
        #endregion

        #region Command
        public RelayCommand AjouterSerieCommand { get; private set; }
        public RelayCommand InfoSerieCommand { get; private set; }
        #endregion

        /// <summary>
        /// Récupère une liste de toutes les séries existantes et celle-ci sont triées dans des listes en fonction de leurs genre
        /// </summary>
        public ViewAccueilViewModel()
        {
            AjouterSerieCommand = new RelayCommand(OnAjouterSerie, CanExecuteAjouterSerie);
            InfoSerieCommand = new RelayCommand(OnInfoSerie, CanExecuteInfoSerie);

            ListserieAction = new ObservableCollection<Serie>();
            ListserieHorreur = new ObservableCollection<Serie>();
            ListserieFantastique = new ObservableCollection<Serie>();
            ListserieDrame = new ObservableCollection<Serie>();
            ListserieComedie = new ObservableCollection<Serie>();

            _listSerie = GestionBDD.returnTouteSerieFull();

            for(int i = 0; i < _listSerie.Count; i++)
            {
                if (_listSerie[i].genre == Genre.Action)
                {
                    ListserieAction.Add(_listSerie[i]);
                }
                if (_listSerie[i].genre == Genre.Horreur)
                {
                    ListserieHorreur.Add(_listSerie[i]);
                }
                if (_listSerie[i].genre == Genre.Fantastique)
                {
                    ListserieFantastique.Add(_listSerie[i]);
                }
                if (_listSerie[i].genre == Genre.Comedie)
                {
                    ListserieComedie.Add(_listSerie[i]);
                }
                if (_listSerie[i].genre == Genre.Drame)
                {
                    ListserieDrame.Add(_listSerie[i]);
                }
            }
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
        /// Ajoute une série selectionné dans la BDD et dans la liste de série de l'utilisateur
        /// </summary>
        /// <param name="obj"></param>
        private void OnAjouterSerie(object obj)
        {
            if(SelectedSerie != null)
            {
                if (GestionBDD.checkSiSerieAjouter(_user_courant.Pseudo, SelectedSerie.nom))
                {
                    _user_courant.Serieadd.Add(SelectedSerie);
                    GestionBDD.addSerieUtilisateur(_user_courant.Pseudo, SelectedSerie.nom);
                }
                else
                {
                    MessageBox.Show("Série déjà ajoutée en favoris !", "Déjà ajouté !", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

        private bool CanExecuteAjouterSerie(object obj)
        {
            return true;
        }
    }
}