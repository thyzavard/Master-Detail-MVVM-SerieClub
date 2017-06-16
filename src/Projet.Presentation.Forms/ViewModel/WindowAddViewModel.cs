using GalaSoft.MvvmLight;
using Microsoft.Win32;
using Projet.Entite.Class;
using Projet.Presentation.Forms.Commands;
using Projet.Presentation.Forms.Events;
using Projet.Service.Fonctions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Projet.Presentation.Forms.ViewModel
{
    public class WindowAddViewModel : ObservableObject
    {
        #region private
        private List<string> _genresource;
        private string _nomserie;
        private string _descriptionserie;
        private string _producteurserie;
        private string _dureemoyenneserie;
        private string _selectgenre;
        private string _nbSaison;
        private int _result;
        private int _res;
        private List<string> _listserie;
        private string _selectserie;
        private string _descriptionseriemodif;
        private string _producteurseriemodif;
        private string _dureemoyenneseriemodif;
        private string _selectgenremodif;
        private string _nbSaisonModif;
        private List<string> _listpseudo;
        private string _selectseriesuppr;
        private string _selectpseudo;
        private OpenFileDialog _openFile = new OpenFileDialog();
        private OpenFileDialog _openFileModif = new OpenFileDialog();
        private OpenFileDialog _openFileBanniere = new OpenFileDialog();
        private OpenFileDialog _openFileBanniereModif = new OpenFileDialog();
        private BitmapImage _imageSerieCourante;
        private BitmapImage _imageSerieModif;
        private Serie _seriemodif;

        //Gestion image
        private string _path;
        private string _fileName;
        private string _fileNameModif;
        private string _fileNameBanniere;
        private string _fileNameBanniereModif;
        private string _sourceImageModif;
        private string _sourceImageModifBanniere;
        private string _sourceImage;
        private string _sourceImageBanniere;
        private bool _verif = false;

        #endregion

        #region Command
        public RelayCommand AjouterSerieCommand { get; private set; }
        public RelayCommand ModifierSerieCommand { get; private set; }
        public RelayCommand SupprimerSerieCommand { get; private set; }
        public RelayCommand downAdminCommand { get; private set; }
        public RelayCommand upAdminCommand { get; private set; }
        public RelayCommand ParcourirImageAddCommand { get; private set; }
        public RelayCommand ParcourirImageUpdateCommand { get; private set; }
        public RelayCommand QuitCommand { get; private set; }
        public RelayCommand ParcourirImageBanniereAddCommand { get; private set; }
        public RelayCommand ParcourirImageBanniereUpdateCommand { get; private set; }
        #endregion

        #region Public
        public List<string> GenreSource
        {
            get
            {
                return _genresource;
            }
            set { Set(() => GenreSource, ref _genresource, value); }
        }

        public string NomSerie
        {
            get
            {
                return _nomserie;
            }
            set { Set(() => NomSerie, ref _nomserie, value); AjouterSerieCommand.RaiseCanExecuteChanged(); }
        }

        public string DescriptionSerie
        {
            get
            {
                return _descriptionserie;
            }
            set { Set(() => DescriptionSerie, ref _descriptionserie, value); AjouterSerieCommand.RaiseCanExecuteChanged(); }
        }
        public string ProducteurSerie
        {
            get
            {
                return _producteurserie;
            }
            set { Set(() => ProducteurSerie, ref _producteurserie, value); AjouterSerieCommand.RaiseCanExecuteChanged(); }
        }
        public string DureeMoyenneSerie
        {
            get
            {
                return _dureemoyenneserie;
            }
            set { Set(() => DureeMoyenneSerie, ref _dureemoyenneserie, value); AjouterSerieCommand.RaiseCanExecuteChanged(); }
        }

        public string SelectGenre
        {
            get
            {
                return _selectgenre;
            }
            set { Set(() => SelectGenre, ref _selectgenre, value); AjouterSerieCommand.RaiseCanExecuteChanged(); }
        }

        public List<string> Listserie
        {
            get
            {
                return _listserie;
            }
            set { Set(() => Listserie, ref _listserie, value); }
        }
        public string SelectSerie
        {
            get
            {
                return _selectserie;
            }
            set { Set(() => SelectSerie, ref _selectserie, value); ParcourirImageUpdateCommand.RaiseCanExecuteChanged(); if (SelectSerie != null) { remplirSerieModif(); } }
        }

        public string Descriptionseriemodif
        {
            get
            {
                return _descriptionseriemodif;
            }
            set { Set(() => Descriptionseriemodif, ref _descriptionseriemodif, value); ModifierSerieCommand.RaiseCanExecuteChanged(); }
        }
        public string Producteurseriemodif
        {
            get
            {
                return _producteurseriemodif;
            }
            set { Set(() => Producteurseriemodif, ref _producteurseriemodif, value); ModifierSerieCommand.RaiseCanExecuteChanged(); }
        }
        public string Dureemoyenneseriemodif
        {
            get
            {
                return _dureemoyenneseriemodif;
            }
            set { Set(() => Dureemoyenneseriemodif, ref _dureemoyenneseriemodif, value); ModifierSerieCommand.RaiseCanExecuteChanged(); }
        }
        public string Selectgenremodif
        {
            get
            {
                return _selectgenremodif;
            }
            set { Set(() => Selectgenremodif, ref _selectgenremodif, value); ModifierSerieCommand.RaiseCanExecuteChanged(); }
        }

        public List<string> ListPseudo { get { return _listpseudo; } set { Set(() => ListPseudo, ref _listpseudo, value); } }

        public string Selectseriesuppr
        {
            get
            {
                return _selectseriesuppr;
            }
            set { Set(() => Selectseriesuppr, ref _selectseriesuppr, value); SupprimerSerieCommand.RaiseCanExecuteChanged(); }
        }

        public string Selectpseudo
        {
            get
            {
                return _selectpseudo;
            }
            set { Set(() => Selectpseudo, ref _selectpseudo, value); upAdminCommand.RaiseCanExecuteChanged(); downAdminCommand.RaiseCanExecuteChanged(); }
        }

        public BitmapImage ImageSerieCourante
        {
            get
            {
                return _imageSerieCourante;
            }
            set { Set(() => ImageSerieCourante, ref _imageSerieCourante, value); }
        }

        public BitmapImage ImageSerieModif
        {
            get
            {
                return _imageSerieModif;
            }
            set { Set(() => ImageSerieModif, ref _imageSerieModif, value); }
        }

        public string SourceImageModif
        {
            get
            {
                return _sourceImageModif;
            }
            set { Set(() => SourceImageModif, ref _sourceImageModif, value); ModifierSerieCommand.RaiseCanExecuteChanged(); ParcourirImageUpdateCommand.RaiseCanExecuteChanged(); }
        }

        public string SourceImage
        {
            get
            {
                return _sourceImage;
            }
            set { Set(() => SourceImage, ref _sourceImage, value); AjouterSerieCommand.RaiseCanExecuteChanged(); ParcourirImageAddCommand.RaiseCanExecuteChanged(); }
        }

        public string NbSaison
        {
            get
            {
                return _nbSaison;
            }
            set { Set(() => NbSaison, ref _nbSaison, value); AjouterSerieCommand.RaiseCanExecuteChanged(); }
        }

        public string NbSaisonModif
        {
            get
            {
                return _nbSaisonModif;
            }
            set { Set(() => NbSaisonModif, ref _nbSaisonModif, value); ModifierSerieCommand.RaiseCanExecuteChanged(); }
        }

        public string SourceImageBanniere
        {
            get { return _sourceImageBanniere; }
            set { Set(() => SourceImageBanniere, ref _sourceImageBanniere, value); AjouterSerieCommand.RaiseCanExecuteChanged(); ParcourirImageBanniereAddCommand.RaiseCanExecuteChanged(); }
        }
        public string SourceImageModifBanniere
        {
            get { return _sourceImageModifBanniere; }
            set { Set(() => SourceImageModifBanniere, ref _sourceImageModifBanniere, value); AjouterSerieCommand.RaiseCanExecuteChanged(); ParcourirImageBanniereUpdateCommand.RaiseCanExecuteChanged(); }
        }
        #endregion

        public WindowAddViewModel()
        {
            //Chemin du fichier ImagesSerie
            _path = Path.Combine(Environment.CurrentDirectory, "ImagesSerie");

            chargerListSerie();
            ListPseudo = GestionBDD.returnToutUtilisateur();

            //Chargement de tous les enum 'Genre' dans la liste 'GenreSource' pour être affiché dans les listBox
            GenreSource = new List<string>();
            Array array = Enum.GetValues(typeof(Genre));
            foreach(object obj  in array)
            {
                GenreSource.Add(obj.ToString());
            }

            AjouterSerieCommand = new RelayCommand(OnAjouterSerie, CanexecuteAjouterSerie);
            ModifierSerieCommand = new RelayCommand(OnModifierSerie, CanexecuteModifierSerie);
            SupprimerSerieCommand = new RelayCommand(OnSupprmierSerie, CanexecuteSupprimerSerie);
            downAdminCommand = new RelayCommand(OnDownAdmin, CanexecuteDownAdmin);
            upAdminCommand = new RelayCommand(OnUpAdmin, CanexecuteUpAdmin);
            ParcourirImageAddCommand = new RelayCommand(OnParcourirImageAdd, CanExecuteParcourirImageAdd);
            ParcourirImageUpdateCommand = new RelayCommand(OnParcourirImageUpdate, CanExecuteParcourirImageUpdate);
            ParcourirImageBanniereAddCommand = new RelayCommand(OnParcourirImageBanniereAdd, CanExecuteParcourirImageBanniereAdd);
            ParcourirImageBanniereUpdateCommand = new RelayCommand(OnParcourirImageBanniereUpdate, CanExecuteParcourirImageBanniereUpdate);
            QuitCommand = new RelayCommand(OnQuit, CanExecuteQuit);
        }

        private void OnParcourirImageBanniereUpdate(object obj)
        {
            _openFileBanniereModif.Title = "Selectionner une image";
            _openFileBanniereModif.DefaultExt = "jpg";
            _openFileBanniereModif.Filter = " Fichier JPG (*.jpg)|*.jpg";
            if (_openFileBanniereModif.ShowDialog() == true)
            {
                //ImageSerieCourante = new BitmapImage(new Uri(_openFile.FileName));
                _fileNameBanniereModif = Path.GetFileName(_openFileBanniereModif.FileName);
                //Aperçu de l'image
                SourceImageModifBanniere = Path.GetFullPath(_openFileBanniereModif.FileName);
            }
        }

        private bool CanExecuteParcourirImageBanniereUpdate(object obj)
        {
            if (SelectSerie == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void OnParcourirImageBanniereAdd(object obj)
        {
            _openFileBanniere.Title = "Selectionner une image";
            _openFileBanniere.DefaultExt = "jpg";
            _openFileBanniere.Filter = " Fichier JPG (*.jpg)|*.jpg";
            if (_openFileBanniere.ShowDialog() == true)
            {
                //ImageSerieCourante = new BitmapImage(new Uri(_openFile.FileName));
                _fileNameBanniere = Path.GetFileName(_openFileBanniere.FileName);
                //Aperçu de l'image
                SourceImageBanniere = Path.GetFullPath(_openFileBanniere.FileName);
            }
        }

        private bool CanExecuteParcourirImageBanniereAdd(object obj)
        {
            return true;
        }

        //Fonctions utilitaires
        public void chargerListSerie()
        {
            Listserie = GestionBDD.returnTouteSerie();
        }
        public void setChampNullAjouter()
        {
            SourceImage = null;
            SourceImageBanniere = null;
            NomSerie = null;
            DescriptionSerie = null;
            SelectGenre = null;
            ProducteurSerie = null;
            NbSaison = null;
            DureeMoyenneSerie = null;
        }
        public void setChampModifNull()
        {
            SelectSerie = null;
            SourceImageModif = null;
            Descriptionseriemodif = null;
            Selectgenremodif = null;
            Producteurseriemodif = null;
            NbSaisonModif = null;
            Dureemoyenneseriemodif = null;
            SourceImageModifBanniere = null;
        }
        public void remplirSerieModif()
        {
            _seriemodif = GestionBDD.remplirSerie(SelectSerie);
            Descriptionseriemodif = _seriemodif.description;
            Producteurseriemodif = _seriemodif.producteur;
            Dureemoyenneseriemodif = _seriemodif.dureeMoy.ToString();
            Selectgenremodif = _seriemodif.genre.ToString();
            NbSaisonModif = _seriemodif.nbSaison.ToString();
            SourceImageModif = _seriemodif.ImageSerie.ToString();
            SourceImageModifBanniere = _seriemodif.Banniereserie.ToString();
        }

        //Fonctions Commandes
        private void OnQuit(object obj)
        {
            WindowClosedEvent.GetInstance().OnWindowClosedHandler(EventArgs.Empty);
        }

        private bool CanExecuteQuit(object obj)
        {
            return true;
        }

        private void OnParcourirImageUpdate(object obj)
        {
            _openFileModif.Title = "Selectionner une image";
            _openFileModif.DefaultExt = "jpg";
            _openFileModif.Filter = " Fichier JPG (*.jpg)|*.jpg";
            if (_openFileModif.ShowDialog() == true)
            {
                //ImageSerieModif = new BitmapImage(new Uri(_openFileModif.FileName));
                _fileNameModif = Path.GetFileName(_openFileModif.FileName);
                //Aperçu de l'image
                SourceImageModif = Path.GetFullPath(_openFileModif.FileName);
                _verif = true;
            }
        }

        private bool CanExecuteParcourirImageUpdate(object obj)
        {
            if (SelectSerie == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void OnParcourirImageAdd(object obj)
        {
            _openFile.Title = "Selectionner une image";
            _openFile.DefaultExt = "jpg";
            _openFile.Filter = " Fichier JPG (*.jpg)|*.jpg";
            if (_openFile.ShowDialog() == true)
            {
                //ImageSerieCourante = new BitmapImage(new Uri(_openFile.FileName));
                _fileName = Path.GetFileName(_openFile.FileName);
                //Aperçu de l'image
                SourceImage = Path.GetFullPath(_openFile.FileName);
            }
        }

        private bool CanExecuteParcourirImageAdd(object obj)
        {
            return true;
        }

        private void OnDownAdmin(object obj)
        {
            if (MessageBox.Show($"Voulez vous désister {Selectpseudo} du poste de modérateur ?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                if (GestionBDD.downModo(Selectpseudo))
                {
                    MessageBox.Show("Modérateur supprimé");
                }
                else
                {
                    MessageBox.Show("Cet utilisateur n'est pas modérateur", "Erreur", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }

        private bool CanexecuteDownAdmin(object obj)
        {
            if (Selectpseudo != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void OnUpAdmin(object obj)
        {
            if (MessageBox.Show($"Voulez vous promouvoir {Selectpseudo} au poste de modérateur ?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                if (GestionBDD.upModo(Selectpseudo))
                {
                    MessageBox.Show("Modérateur ajouté");
                }
                else
                {
                    MessageBox.Show("Cet utilisateur est déjà modérateur", "Erreur", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }

        private bool CanexecuteUpAdmin(object obj)
        {
            if (Selectpseudo != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void OnSupprmierSerie(object obj)
        {
            if (MessageBox.Show($"Voulez vous vraiment supprimer la série : {Selectseriesuppr} ?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                GestionBDD.supprSerie(Selectseriesuppr);
                for (int i = Listserie.Count - 1; i >= 0; i--)
                {
                    if (Listserie[i].Contains(Selectseriesuppr))
                    {
                        Listserie.RemoveAt(i);
                    }
                }
                RefreshEvent.GetInstance().OnRefreshAcceuilHandler(EventArgs.Empty);
                chargerListSerie();
                MessageBox.Show("Série supprimé");

                
            }
        }

        private bool CanexecuteSupprimerSerie(object obj)
        {
            if (Selectseriesuppr != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //COMMANDE MODIFIER SERIE
        private void OnModifierSerie(object obj)
        {
            if (int.TryParse(Dureemoyenneseriemodif, out _result) == true)
            {
                if (int.TryParse(NbSaisonModif, out _res) == true)
                {
                    if (_verif)
                    {
                        if (!File.Exists($@"{_path}\{_fileNameModif}"))
                        {
                            FileInfo f = new FileInfo(_openFileModif.FileName);
                            if (f.Length > 512000)
                            {
                                MessageBox.Show("La taille de l'image est trop grande (500 ko maximum)", "", MessageBoxButton.OK, MessageBoxImage.Warning);
                            }
                            else
                            {
                                File.Copy(_openFileModif.FileName, Path.Combine(_path, _fileNameModif));
                                File.Copy(_openFileBanniereModif.FileName, Path.Combine(_path, _fileNameBanniereModif));
                                GestionBDD.updateSerie(SelectSerie, Descriptionseriemodif, int.Parse(Dureemoyenneseriemodif), Producteurseriemodif, Selectgenremodif, _fileNameModif, int.Parse(NbSaisonModif), _fileNameBanniereModif);
                                MessageBox.Show("Modification enrgistrée");
                                RefreshEvent.GetInstance().OnRefreshAcceuilHandler(EventArgs.Empty);
                                setChampModifNull();
                            }
                        }
                    }
                    else
                    {
                        GestionBDD.updateSeriesansImage(SelectSerie, Descriptionseriemodif, int.Parse(Dureemoyenneseriemodif), Producteurseriemodif, Selectgenremodif, int.Parse(NbSaisonModif));
                        MessageBox.Show("Modification enrgistrée");
                        RefreshEvent.GetInstance().OnRefreshAcceuilHandler(EventArgs.Empty);
                        setChampModifNull();
                    }
                }
                else
                {
                    MessageBox.Show("Veuillez rentrer uniquement des nombres pour le nombre de saison(s) de la serie", "Erreur", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                MessageBox.Show("Veuillez rentrer uniquement des nombres pour la Durée Moyenne de la serie", "Erreur", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        private bool CanexecuteModifierSerie(object obj)
        {
            if (SelectSerie != null && NbSaisonModif != null && Descriptionseriemodif != null && Producteurseriemodif != null && Dureemoyenneseriemodif != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void CheckImageSerie()
        {
            if (!File.Exists($@"{_path}\{_fileName}"))
            {
                File.Copy(_openFile.FileName, Path.Combine(_path, _fileName));
            }
        }
        private void CheckBanniereSerie()
        {
            if (!File.Exists($@"{_path}\{_fileNameBanniere}"))
            {
                File.Copy(_openFileBanniere.FileName, Path.Combine(_path, _fileNameBanniere));
            }
        }

        //COMMANDE AJOUT SERIE
        private void OnAjouterSerie(object obj)
        {
            if (GestionBDD.verifSerie(NomSerie))
            {
                MessageBox.Show("Cette série est déjà enregistrée", "Erreur", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                if (int.TryParse(DureeMoyenneSerie, out _result) == true)
                {
                    if (int.TryParse(NbSaison, out _res) == true)
                    {
                        FileInfo f = new FileInfo(_openFile.FileName);
                        if (f.Length > 512000)
                        {
                            MessageBox.Show("La taille de l'image est trop grande (500 ko maximum)", "", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                        else
                        {
                            CheckImageSerie();
                            CheckBanniereSerie();

                            GestionBDD.ajouter_Serie(NomSerie, DescriptionSerie, SelectGenre, ProducteurSerie, int.Parse(DureeMoyenneSerie), int.Parse(NbSaison), _fileName, _fileNameBanniere);
                            Listserie.Add(NomSerie);
                            chargerListSerie();
                            MessageBox.Show("Ajout enregistrée", "Confirmation", MessageBoxButton.OK);

                            //Déclenchement de l'événement pour mettre à jour le viewAccueil
                            RefreshEvent.GetInstance().OnRefreshAcceuilHandler(EventArgs.Empty);

                            setChampNullAjouter();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Veuillez rentrer uniquement des nombres pour le nombre de saison(s) de la serie", "Erreur", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Veuillez rentrer uniquement des nombres pour la Durée Moyenne de la serie", "Erreur", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }
        private bool CanexecuteAjouterSerie(object obj)
        {
            if (NomSerie != null && DescriptionSerie != null && ProducteurSerie != null && DureeMoyenneSerie != null && SelectGenre != null && SourceImage != null && SourceImageBanniere != null)
            {
                return true;
            }
            return false;
        }
    }
}
