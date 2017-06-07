using Projet.Entite.Class;
using Projet.Presentation.Forms.Commands;
using Projet.Presentation.Forms.Extension;
using Projet.Service.Fonctions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private string _noteSerie;
        private string _descriptionSerie;
        private string _commentaireSerie;
        private string _selectedSaison;
        private string _ajoutOuSupprFav;
        private string _nbSaison;
        private string _producteur;
        private string _dureeMoyenne;
        private int _note;

        private Serie serieglobal;
        #endregion

        #region Public 
        public string NoteSerie
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
                EnvoyerCommentaireCommand.RaiseCanExecuteChanged();
                NotifyPropertyChanged(nameof(CommentaireSerie));
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

    public ObservableCollection<Commentaire> ListCommentaireSerie { get; set; }

    public string NbSaison
        {
            get
            {
                return _nbSaison;
            }
            set
            {
                _nbSaison = value;
            }
        }
        public string Producteur
        {
            get
            {
                return _producteur;
            }
            set
            {
                _producteur = value;
            }
        }
        public string DureeMoyenne
        {
            get
            {
                return _dureeMoyenne;
            }
            set
            {
                _dureeMoyenne = value;
            }
        }
        public int Note
        {
            get
            {
                return _note;
            }
            set
            {
                _note = value;
                NotifyPropertyChanged(nameof(Note));
            }
        }
        #endregion

        #region Command
        public RelayCommand AjouterSerieCommand { get; private set; }
        public RelayCommand EnvoyerCommentaireCommand { get; private set; }
        public RelayCommand NoterSerieCommand { get; private set; }
        #endregion

        public void test()
        {
           /* NomSerie = serie.nom;
            NoteSerie = $"Note : {serie.note}";
            DescriptionSerie = serie.description;
            NbSaison = $"Nombre de saisons : {serie.nbSaison.ToString()}\n";
            Producteur = $"Producteur : {serie.producteur}\n";
            DureeMoyenne = $"Durée moyenne des épisodes : {serie.dureeMoy.ToString()} minutes\n";
            */
            
            
        }

        public ViewSerieViewModel(Serie serie)
        {
            serieglobal = serie;
            AjouterSerieCommand = new RelayCommand(OnAjouterSerie, CanExecuteAjouterSerie);
            EnvoyerCommentaireCommand = new RelayCommand(OnEnvoyerCommentaire, CanExecuteEnvoyerCommentaire);
            NoterSerieCommand = new RelayCommand(OnNoterSerie, CanExecuteNoterSerie);


            //***********
            NomSerie = serie.nom;
            NoteSerie = $"Note : {serie.note}";
            DescriptionSerie = serie.description;
            NbSaison = $"Nombre de saisons : {serie.nbSaison.ToString()}\n";
            Producteur = $"Producteur : {serie.producteur}\n";
            DureeMoyenne = $"Durée moyenne des épisodes : {serie.dureeMoy.ToString()} minutes\n";

            //Chargement des commentaires de la série
            ListCommentaireSerie = serie.commentaire.ToObservableCollection();

            //Gestion bouton ajout ou supprimer
            if (GestionBDD.checkSiSerieAjouter(_user.Pseudo, serie.nom)) { AjoutOuSupprFav = "Ajouter au favoris"; }
            else { AjoutOuSupprFav = "Supprimer des favoris"; }

        }

        private void OnNoterSerie(object obj)
        {
            GestionBDD.ajouterNoteSerie(serieglobal.nom, Note,_user.Pseudo);
            GestionBDD.updateNoteSerie(serieglobal.nom);
            NoterSerieCommand.RaiseCanExecuteChanged();
        }

        private bool CanExecuteNoterSerie(object obj)
        {
            if (GestionBDD.checkSiDejaNoter(serieglobal.nom, _user.Pseudo))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void OnAjouterSerie(object obj)
        {
            if (AjoutOuSupprFav == "Ajouter au favoris")
            {
                _user.Serieadd.Add(serieglobal);
                GestionBDD.addSerieUtilisateur(_user.Pseudo, serieglobal.nom);
                AjoutOuSupprFav = "Supprimer des favoris";
            }
            else
            {
                _user.Serieadd.Remove(serieglobal);
                GestionBDD.removeSerieUtilisateur(_user.Pseudo, serieglobal.nom);
                MessageBox.Show("Serie suppr");
                AjoutOuSupprFav = "Ajouter au favoris";
            }
        }

        private bool CanExecuteAjouterSerie(object obj)
        {
            return true;
        }

        private void OnEnvoyerCommentaire(object obj)
        {
            Commentaire com = new Commentaire(CommentaireSerie, _user.Pseudo, serieglobal.nom);
            serieglobal.commentaire.Add(com);
            GestionBDD.ajouterCommentaireSerie(com);
            ListCommentaireSerie.Add(com);
            MessageBox.Show("Commentaire enregistré !");
            CommentaireSerie = null;
        }

        private bool CanExecuteEnvoyerCommentaire(object obj)
        {
            if (CommentaireSerie != null)
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
