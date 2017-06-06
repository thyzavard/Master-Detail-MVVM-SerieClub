using Projet.Entite.Class;
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
    public class ViewSerieViewModel : BaseViewModel
    {
        #region private
        private UserCourant _user = UserCourant.Instance();
        private string _nomSerie;
        private float _noteSerie;
        private string _descriptionSerie;
        private string _commentaireSerie;
        private string _selectedSaison;
        private List<string> _listSaisonSerie;
        private List<string> _listEpisode;
        private string _ajoutOuSupprFav;

        private Serie serie;
        #endregion

        #region Public 
        public float NoteSerie
        {
            get
            {
                return _noteSerie;
            }
            set
            {
                _noteSerie = value;
            }
        }
        public string NomSerie
        {
            get
            {
                return _nomSerie;
            }
            set
            {
                _nomSerie = value;
            }
        }

        public string DescriptionSerie
        {
            get
            {
                return _descriptionSerie;
            }
            set
            {
                _descriptionSerie = value;
            }
        }

        public string CommentaireSerie
        {
            get
            {
                return _commentaireSerie;
            }
            set
            {
                _commentaireSerie = value;
            }
        }
        public string SelectedSaison
        {
            get
            {
                return _selectedSaison;
            }
            set
            {
                _selectedSaison = value;
            }
        }
        public List<string> ListSaisonSerie
        {
            get
            {
                return _listSaisonSerie;
            }
            set
            {
                _listSaisonSerie = value;
            }
        }
        public List<string> ListEpisode
        {
            get
            {
                return _listEpisode;
            }
            set
            {
                _listEpisode = value;
            }
        }
        public string AjoutOuSupprFav
        {
            get
            {
                return _ajoutOuSupprFav;
            }
            set
            {
                _ajoutOuSupprFav = value;
                NotifyPropertyChanged(nameof(AjoutOuSupprFav));
            }
        }
        #endregion

        #region Command
        public RelayCommand AjouterSerieCommand { get; set; }
        public RelayCommand EnvoyerCommentaireCommand { get; set; }
        #endregion

        public ViewSerieViewModel()
        {
            AjouterSerieCommand = new RelayCommand(OnAjouterSerie, CanExecuteAjouterSerie);
            EnvoyerCommentaireCommand = new RelayCommand(OnEnvoyerCommentaire, CanExecuteEnvoyerCommentaire);

            serie = GestionBDD.remplirSerie("Action");
            ListSaisonSerie = new List<string>();

            NomSerie = serie.nom;
            NoteSerie = serie.note;
            DescriptionSerie = serie.description;
            
            //Gestion bouton ajout ou supprimer
            if(GestionBDD.checkSiSerieAjouter(_user.Pseudo, serie.nom)) { AjoutOuSupprFav = "Ajouter au favoris"; }
            else { AjoutOuSupprFav = "Supprimer des favoris"; }
        }

        private void OnAjouterSerie(object obj)
        {
            if (AjoutOuSupprFav == "Ajouter au favoris")
            {
                _user.Serieadd.Add(serie);
                GestionBDD.addSerieUtilisateur(_user.Pseudo, serie.nom);
            }
            else
            {
                _user.Serieadd.Remove(serie);
                GestionBDD.removeSerieUtilisateur(_user.Pseudo, serie.nom);
                MessageBox.Show("Serie suppr");
            }
        }

        private bool CanExecuteAjouterSerie(object obj)
        {
            return true;
        }

        private void OnEnvoyerCommentaire(object obj)
        {
            throw new NotImplementedException();
        }

        private bool CanExecuteEnvoyerCommentaire(object obj)
        {
            if(CommentaireSerie != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
