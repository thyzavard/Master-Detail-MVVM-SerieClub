using GalaSoft.MvvmLight;
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
using System.Windows.Documents;
using System.Windows.Media.Imaging;

namespace Projet.Presentation.Forms.ViewModel
{
    public class ViewSerieViewModel : ObservableObject
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
        private bool _isVisibleButtonSuppr;
        private Commentaire _selectedCommentaire;
        private string _selectSaison;
        #endregion

        #region Public 
        public string NoteSerie
        {
            get
            {
                return _noteSerie;
            }
            set { Set(() => NoteSerie, ref _noteSerie, value); }
        }
        public string NomSerie
        {
            get
            {
                return _nomSerie;
            }
            set { Set(() => NomSerie, ref _nomSerie, value); }
        }

        public string DescriptionSerie
        {
            get
            {
                return _descriptionSerie;
            }
            set { Set(() => DescriptionSerie, ref _descriptionSerie, value); }
        }

        public string CommentaireSerie
        {
            get
            {
                return _commentaireSerie;
            }
            set { Set(() => CommentaireSerie, ref _commentaireSerie, value); EnvoyerCommentaireCommand.RaiseCanExecuteChanged(); }
        }
        public string SelectedSaison
        {
            get
            {
                return _selectedSaison;
            }
            set { Set(() => SelectedSaison, ref _selectedSaison, value); }
        }
        public string AjoutOuSupprFav
        {
            get
            {
                return _ajoutOuSupprFav;
            }
            set { Set(() => AjoutOuSupprFav, ref _ajoutOuSupprFav, value); }
        }

        public ObservableCollection<Commentaire> ListCommentaireSerie { get; set; }

        public string NbSaison
        {
            get
            {
                return _nbSaison;
            }
            set { Set(() => NbSaison, ref _nbSaison, value); }
        }
        public string Producteur
        {
            get
            {
                return _producteur;
            }
            set { Set(() => Producteur, ref _producteur, value); }
        }
        public string DureeMoyenne
        {
            get
            {
                return _dureeMoyenne;
            }
            set { Set(() => DureeMoyenne, ref _dureeMoyenne, value); }
        }
        public int Note
        {
            get
            {
                return _note;
            }
            set { Set(() => Note, ref _note, value); NoterSerieCommand.RaiseCanExecuteChanged(); }
        }

        public BitmapImage SourceImageCouverture
        {
            get { return _sourceImageCouverture; }
            set { Set(() => SourceImageCouverture, ref _sourceImageCouverture, value); }
        }

        public bool IsVisibleButtonSuppr
        {
            get { return _isVisibleButtonSuppr; }
            set { Set(() => IsVisibleButtonSuppr, ref _isVisibleButtonSuppr, value); }
        }

        public Commentaire SelectedCommentaire
        {
            get { return _selectedCommentaire; }
            set { Set(() => SelectedCommentaire, ref _selectedCommentaire, value); }
        }

        public List<string> SaisonSerie { get; set; }
        public List<Episode> ListEpisode { get; set; }
        public ObservableCollection<Episode> ListAffichier { get; set; }

        public string SelectSaison
        {
            get
            {
                return _selectSaison;
            }
            set { Set(() => SelectSaison, ref _selectSaison, value); afficherEpisode(); }
        }
        #endregion

        #region Command
        public RelayCommand AjouterSerieCommand { get; private set; }
        public RelayCommand EnvoyerCommentaireCommand { get; private set; }
        public RelayCommand NoterSerieCommand { get; private set; }
        public RelayCommand RetourArriereCommand { get; private set; }
        public RelayCommand SupprimerCommentaireCommand { get; private set; }
        #endregion

        #region SourceImage
        private string _source1;
        public string Source1 { get { return _source1; } set { Set(() => Source1, ref _source1, value); } }

        private string _source2;
        public string Source2 { get { return _source2; } set { Set(() => Source2, ref _source2, value); } }

        private string _source3;
        public string Source3 { get { return _source3; } set { Set(() => Source3, ref _source3, value); } }

        private string _source4;
        public string Source4 { get { return _source4; } set { Set(() => Source4, ref _source4, value); } }

        private string _source5;
        public string Source5 { get { return _source5; } set { Set(() => Source5, ref _source5, value); } }

        private string _source6;
        public string Source6 { get { return _source6; } set { Set(() => Source6, ref _source6, value); } }

        private string _source7;
        public string Source7 { get { return _source7; } set { Set(() => Source7, ref _source7, value); } }

        private string _source8;
        public string Source8 { get { return _source8; } set { Set(() => Source8, ref _source8, value); } }

        private string _source9;
        public string Source9 { get { return _source9; } set { Set(() => Source9, ref _source9, value); } }

        private string _source10;
        public string Source10 { get { return _source10; } set { Set(() => Source10, ref _source10, value); } }
        #endregion

        public ViewSerieViewModel(Serie serie)
        {
            ListAffichier = new ObservableCollection<Episode>();
            SaisonSerie = new List<string>();
            //Ajoute le nombre de saison dans la combobox
            for(int i=1; i < serie.nbSaison + 1; i++)
            {
                SaisonSerie.Add($"Saison {i}");
            }
            
            //Gestion épisode
            ListEpisode = new List<Episode>();
            int y = 1;
            for(int i = 1; i< serie.nbSaison + 1; i++)
            {
                for (int o=1 ; o < serie.nbEpisode + 1; o++)
                {
                    Episode ep = new Episode(o, $"Episode {y}", $"Saison {i}");
                    ListEpisode.Add(ep);
                    y++;
                }
            }
            SelectSaison = "Saison 1";

            serieglobal = serie;
            AjouterSerieCommand = new RelayCommand(OnAjouterSerie, CanExecuteAjouterSerie);
            EnvoyerCommentaireCommand = new RelayCommand(OnEnvoyerCommentaire, CanExecuteEnvoyerCommentaire);
            NoterSerieCommand = new RelayCommand(OnNoterSerie, CanExecuteNoterSerie);
            RetourArriereCommand = new RelayCommand(OnRetourArriere, CanExecuteRetourArriere);
            SupprimerCommentaireCommand = new RelayCommand(OnSupprimerCommentaire, CanExecuteSupprimerCommentaire);

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

            IsVisibleButtonSuppr = _user.Modo;

            //Gestion barre de notation
            setSource();
            
        }

        /// <summary>
        /// Change les épisode afficher selon la saison choisi dans la combobox
        /// </summary>
        public void afficherEpisode()
        {
            ListAffichier.Clear();
            var resRecherche = ListEpisode.Where(h => h.saison.Contains(SelectSaison));

            foreach (Episode s in resRecherche)
            {
                ListAffichier.Add(s);
            }
        }

        private void OnSupprimerCommentaire(object obj)
        {
            if(SelectedCommentaire != null)
            {
                GestionBDD.supprimerCommentaire(SelectedCommentaire);
                ListCommentaireSerie.Remove(SelectedCommentaire);
                MessageBox.Show("Commentaire supprimé !", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private bool CanExecuteSupprimerCommentaire(object obj)
        {
            return true;
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
