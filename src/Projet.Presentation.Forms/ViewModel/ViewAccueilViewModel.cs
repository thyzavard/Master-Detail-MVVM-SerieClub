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

namespace Projet.Presentation.Forms.ViewModel
{

    public class ViewAccueilViewModel : BaseViewModel
    {
        #region private
        private List<Serie> _listSerie;
        private List<Serie> _listserieAction;
        private List<Serie> _listserieHorreur;
        private List<Serie> _listserieFantastique;
        private List<Serie> _listserieDrame;
        private List<Serie> _listserieComedie;
        private object _selectedViewModel;
        private Serie _selectedSerie;
        private UserCourant _user_courant = UserCourant.Instance();
        private string _ajouterOuSupprimerButton;
        #endregion

        #region Public
        public List<Serie> ListserieAction
        {
            get { return _listserieAction; }
            set
            {
                _listserieAction = value;
                NotifyPropertyChanged(nameof(ListserieAction));
            }
        }

        public List<Serie> ListserieHorreur
        {
            get { return _listserieHorreur; }
            set
            {
                _listserieHorreur = value;
                NotifyPropertyChanged(nameof(ListserieHorreur));
            }
        }

        public List<Serie> ListserieFantastique
        {
            get { return _listserieFantastique; }
            set
            {
                _listserieFantastique = value;
                NotifyPropertyChanged(nameof(ListserieFantastique));
            }
        }
        public List<Serie> ListserieDrame
        {
            get
            {
                return _listserieDrame;
            }
            set
            {
                _listserieDrame = value;
                NotifyPropertyChanged(nameof(ListserieDrame));
            }
        }
        public List<Serie> ListserieComedie
        {
            get
            {
                return _listserieComedie;
            }
            set
            {
                _listserieComedie = value;
                NotifyPropertyChanged(nameof(ListserieComedie));
            }
        }

        public object SelectedViewModel
        {
            get
            {
                return _selectedViewModel;
            }
            set
            {
                _selectedViewModel = value;
                AjouterSerieCommand.RaiseCanExecuteChanged();
                NotifyPropertyChanged(nameof(SelectedViewModel));
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
                AjouterSerieCommand.RaiseCanExecuteChanged();
                NotifyPropertyChanged(nameof(SelectedSerie));
            }
        }
        public string AjouterOuSupprimerButton
        {
            get
            {
                return _ajouterOuSupprimerButton;
            }

            set
            {
                _ajouterOuSupprimerButton = value;
                AjouterSerieCommand.RaiseCanExecuteChanged();
                NotifyPropertyChanged(nameof(AjouterOuSupprimerButton));
            }
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
            
            ListserieAction = new List<Serie>();
            ListserieFantastique = new List<Serie>();
            ListserieHorreur = new List<Serie>();
            ListserieDrame = new List<Serie>();
            ListserieComedie = new List<Serie>();

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