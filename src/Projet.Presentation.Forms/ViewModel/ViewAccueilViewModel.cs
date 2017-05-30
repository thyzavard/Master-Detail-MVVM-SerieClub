using Projet.Entite.Class;
using Projet.Presentation.Forms.Commands;
using Projet.Service.Fonctions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Projet.Presentation.Forms.ViewModel
{

    public class ViewAccueilViewModel : INotifyPropertyChanged
    {
        #region private
        private List<Serie> _listserieAction;
        private List<Serie> _listserieHorreur;
        private List<Serie> _listserieFantastique;
        private List<Serie> _listserieDrame;
        private List<Serie> _listserieComedie;
        private List<string> _listNom;
        private object _selectedViewModel;
        private Serie _selectedSerie;
        private UserCourant _user_courant = UserCourant.Instance();
        #endregion

        #region Public
        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

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
        #endregion

        #region Command
        public RelayCommand AjouterSerieCommand { get; private set; }
        #endregion

        public ViewAccueilViewModel()
        {
            AjouterSerieCommand = new RelayCommand(OnAjouterSerie, CanExecuteAjouterSerie);

            _listNom = GestionBDD.returnTouteSerie();
            ListserieAction = new List<Serie>();
            ListserieFantastique = new List<Serie>();
            ListserieHorreur = new List<Serie>();
            ListserieDrame = new List<Serie>();
            ListserieComedie = new List<Serie>();

            for (int i = 0; i < _listNom.Count; i++)
            {
                Serie serie = GestionBDD.remplirSerie(_listNom[i]);
                if (serie.genre == Genre.Action)
                {
                    ListserieAction.Add(serie);
                }
                if (serie.genre == Genre.Horreur)
                {
                    ListserieHorreur.Add(serie);
                }
                if (serie.genre == Genre.Fantastique)
                {
                    ListserieFantastique.Add(serie);
                }
                if (serie.genre == Genre.Comedie)
                {
                    ListserieComedie.Add(serie);
                }
                if (serie.genre == Genre.Drame)
                {
                    ListserieDrame.Add(serie);
                }
            }
        }

        private void OnAjouterSerie(object obj)
        {
            if (GestionBDD.checkSiSerieAjouter(_user_courant.Pseudo, SelectedSerie.nom))
            {
                _user_courant.Serieadd.Add(SelectedSerie);
                GestionBDD.addSerieUtilisateur(_user_courant.Pseudo, SelectedSerie.nom);
            }
            else
            {
                MessageBox.Show("Série déjà ajoutée en favoris !", "Déjà ajouté !", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

        }

        private bool CanExecuteAjouterSerie(object obj)
        {
            return true;
        }
    }
}