using Projet.Entite.Class;
using Projet.Presentation.Forms.Commands;
using Projet.Presentation.Forms.Events;
using Projet.Presentation.Forms.Extension;
using Projet.Service.Fonctions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

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
        private string _entetecommentaire;
        private BitmapImage _sourceImageCouverture;
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
                NotifyPropertyChanged(nameof(NoteSerie));
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
                NotifyPropertyChanged(nameof(NomSerie));
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
                NotifyPropertyChanged(nameof(DescriptionSerie));
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
                NotifyPropertyChanged(nameof(NbSaison));
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
                NotifyPropertyChanged(nameof(Producteur));
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
                NotifyPropertyChanged(nameof(DureeMoyenne));
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
                NoterSerieCommand.RaiseCanExecuteChanged();
                NotifyPropertyChanged(nameof(Note));
            }
        }
        public string Entetecommentaire
        {
            get
            {
                return _entetecommentaire;
            }
            set
            {
                _entetecommentaire = value;
                NotifyPropertyChanged(nameof(Entetecommentaire));
            }
        }
        public BitmapImage SourceImageCouverture
        {
            get { return _sourceImageCouverture; }
            set
            {
                _sourceImageCouverture = value;
                NotifyPropertyChanged(nameof(SourceImageCouverture));
            }
        }
        #endregion

        #region Command
        public RelayCommand AjouterSerieCommand { get; private set; }
        public RelayCommand EnvoyerCommentaireCommand { get; private set; }
        public RelayCommand NoterSerieCommand { get; private set; }
        public RelayCommand RetourArriereCommand { get; private set; }
        #endregion
        
        #region SourceImage
        private string _source1;
        public string Source1 { get { return _source1; } set { _source1 = value; NotifyPropertyChanged(nameof(Source1)); } }
        
        private string _source2;
        public string Source2 { get { return _source2; } set { _source2 = value; NotifyPropertyChanged(nameof(Source2)); } }

        private string _source3;
        public string Source3 { get { return _source3; } set { _source3 = value; NotifyPropertyChanged(nameof(Source3)); } }

        private string _source4;
        public string Source4 { get { return _source4; } set { _source4 = value; NotifyPropertyChanged(nameof(Source4)); } }

        private string _source5;
        public string Source5 { get { return _source5; } set { _source5 = value; NotifyPropertyChanged(nameof(Source5)); } }

        private string _source6;
        public string Source6 { get { return _source6; } set { _source6 = value; NotifyPropertyChanged(nameof(Source6)); } }

        private string _source7;
        public string Source7 { get { return _source7; } set { _source7 = value; NotifyPropertyChanged(nameof(Source7)); } }

        private string _source8;
        public string Source8 { get { return _source8; } set { _source8 = value; NotifyPropertyChanged(nameof(Source8)); } }

        private string _source9;
        public string Source9 { get { return _source9; } set { _source9 = value; NotifyPropertyChanged(nameof(Source9)); } }

        private string _source10;
        public string Source10 { get { return _source10; } set { _source10 = value; NotifyPropertyChanged(nameof(Source10)); } }
        #endregion

        public ViewSerieViewModel(Serie serie)
        {
            serieglobal = serie;
            AjouterSerieCommand = new RelayCommand(OnAjouterSerie, CanExecuteAjouterSerie);
            EnvoyerCommentaireCommand = new RelayCommand(OnEnvoyerCommentaire, CanExecuteEnvoyerCommentaire);
            NoterSerieCommand = new RelayCommand(OnNoterSerie, CanExecuteNoterSerie);
            RetourArriereCommand = new RelayCommand(OnRetourArriere, CanExecuteRetourArriere);

            if (!GestionBDD.checkSiDejaNoter(serieglobal.nom, _user.Pseudo)) { Note = GestionBDD.returnNoteUserSerie(serie.nom, _user.Pseudo); }
            else { Note = 0; }

            //Chargement des variables à afficher sur le usercontrol
            NomSerie = serie.nom;
            NoteSerie = $"Note : {serie.note}/10";
            DescriptionSerie = serie.description;
            NbSaison = $"{serie.nbSaison.ToString()}\n";
            Producteur = $"{serie.producteur}\n";
            DureeMoyenne = serie.dureeMoy.ToString();
            SourceImageCouverture = serie.Banniereserie;

            //Chargement des commentaires de la série
            ListCommentaireSerie = serie.commentaire.ToObservableCollection();

            //Gestion bouton ajout ou supprimer
            if (GestionBDD.checkSiSerieAjouter(_user.Pseudo, serie.nom)) { AjoutOuSupprFav = "Ajouter au favoris"; }
            else { AjoutOuSupprFav = "Supprimer des favoris"; }


            //Gestion barre de notation
            setSource();

            setTitreCommentaire();
        }

        public void setTitreCommentaire()
        {
            if (ListCommentaireSerie.Count != 0)
            {
                Entetecommentaire = "COMMENTAIRE";
            }
        }

        /// <summary>
        /// Déclenche un événement permettant de revenir sur l'accueil
        /// </summary>
        /// <param name="obj"></param>
        private void OnRetourArriere(object obj)
        {
            RetourWindowAccueilEvent.GetInstance().OnRetourWindowAccueilHandler(EventArgs.Empty);
        }

        private bool CanExecuteRetourArriere(object obj)
        {
            return true;
        }

        /// <summary>
        /// Ajoute une note d'un utilisateur à une série, si celui-ci à déjà voté pour cette série la note est modifié
        /// </summary>
        /// <param name="obj"></param>
        private void OnNoterSerie(object obj)
        {
            if (GestionBDD.checkSiDejaNoter(serieglobal.nom, _user.Pseudo))
            {
                GestionBDD.ajouterNoteSerie(serieglobal.nom, Note, _user.Pseudo);
                GestionBDD.updateNoteSerie(serieglobal.nom);
            }
            else
            {
                GestionBDD.updateNote(serieglobal.nom, Note, _user.Pseudo);
                GestionBDD.updateNoteSerie(serieglobal.nom);
            }
        }

        private bool CanExecuteNoterSerie(object obj)
        {
            updateBarreNote();
            if(GestionBDD.checkSiSerieAjouter(_user.Pseudo, serieglobal.nom))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Ajoute une série selectionné dans la BDD et dans la liste de série de l'utilisateur
        /// </summary>
        /// <param name="obj"></param>
        private void OnAjouterSerie(object obj)
        {
            if (AjoutOuSupprFav == "Ajouter au favoris")
            {
                _user.Serieadd.Add(serieglobal);
                GestionBDD.addSerieUtilisateur(_user.Pseudo, serieglobal.nom);
                AjoutOuSupprFav = "Supprimer des favoris";
                NoterSerieCommand.RaiseCanExecuteChanged();
            }
            else
            {
                _user.Serieadd.Remove(serieglobal);
                GestionBDD.removeSerieUtilisateur(_user.Pseudo, serieglobal.nom);
                MessageBox.Show("Série enlevée des favoris !", "", MessageBoxButton.OK, MessageBoxImage.Information);
                AjoutOuSupprFav = "Ajouter au favoris";
                NoterSerieCommand.RaiseCanExecuteChanged();
            }
        }

        private bool CanExecuteAjouterSerie(object obj)
        {
            return true;
        }

        /// <summary>
        /// Créer un nouveau commentaire et l'ajoute à la BDD et à la liste de commentaire de la série
        /// </summary>
        /// <param name="obj"></param>
        private void OnEnvoyerCommentaire(object obj)
        {
            Commentaire com = new Commentaire(CommentaireSerie, _user.Pseudo, serieglobal.nom);
            serieglobal.commentaire.Add(com);
            GestionBDD.ajouterCommentaireSerie(com);
            ListCommentaireSerie.Add(com);
            setTitreCommentaire();
            MessageBox.Show("Commentaire enregistré !");
            CommentaireSerie = null;
        }

        private bool CanExecuteEnvoyerCommentaire(object obj)
        {
            if (CommentaireSerie != null && !GestionBDD.checkSiSerieAjouter(_user.Pseudo, serieglobal.nom))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #region fonction barre de note
        /// <summary>
        /// Gère l'affichage de la barre de notation en fonction de la valeur du slider
        /// </summary>
        public void setSource()
        {
            Source1 = "/Images/logoSerieClubnoirblanc.png";
            Source2 = "/Images/logoSerieClubnoirblanc.png";
            Source3 = "/Images/logoSerieClubnoirblanc.png";
            Source4 = "/Images/logoSerieClubnoirblanc.png";
            Source5 = "/Images/logoSerieClubnoirblanc.png";
            Source6 = "/Images/logoSerieClubnoirblanc.png";
            Source7 = "/Images/logoSerieClubnoirblanc.png";
            Source8 = "/Images/logoSerieClubnoirblanc.png";
            Source9 = "/Images/logoSerieClubnoirblanc.png";
            Source10 = "/Images/logoSerieClubnoirblanc.png";
        }
        public void updateBarreNote()
        {
            if (Note >= 1) { Source1 = "/Images/logoSerieClub.png"; }
            if (Note >= 2) { Source2 = "/Images/logoSerieClub.png"; }
            if (Note >= 3) { Source3 = "/Images/logoSerieClub.png"; }
            if (Note >= 4) { Source4 = "/Images/logoSerieClub.png"; }
            if (Note >= 5) { Source5 = "/Images/logoSerieClub.png"; }
            if (Note >= 6) { Source6 = "/Images/logoSerieClub.png"; }
            if (Note >= 7) { Source7 = "/Images/logoSerieClub.png"; }
            if (Note >= 8) { Source8 = "/Images/logoSerieClub.png"; }
            if (Note >= 9) { Source9 = "/Images/logoSerieClub.png"; }
            if (Note >= 10) { Source10 = "/Images/logoSerieClub.png"; }
            //*********
            if (Note < 10) { Source10 = "/Images/logoSerieClubnoirblanc.png"; }
            if (Note < 9) { Source9 = "/Images/logoSerieClubnoirblanc.png"; }
            if (Note < 8) { Source8 = "/Images/logoSerieClubnoirblanc.png"; }
            if (Note < 7) { Source7 = "/Images/logoSerieClubnoirblanc.png"; }
            if (Note < 6) { Source6 = "/Images/logoSerieClubnoirblanc.png"; }
            if (Note < 5) { Source5 = "/Images/logoSerieClubnoirblanc.png"; }
            if (Note < 4) { Source4 = "/Images/logoSerieClubnoirblanc.png"; }
            if (Note < 3) { Source3 = "/Images/logoSerieClubnoirblanc.png"; }
            if (Note < 2) { Source2 = "/Images/logoSerieClubnoirblanc.png"; }
            if (Note < 1) { Source1 = "/Images/logoSerieClubnoirblanc.png"; }
        }
        #endregion
    }
}
