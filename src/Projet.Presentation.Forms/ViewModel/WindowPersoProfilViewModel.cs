using Microsoft.Win32;
using Projet.Entite.Class;
using Projet.Presentation.Forms.Commands;
using Projet.Presentation.Forms.Converters;
using Projet.Presentation.Forms.Events;
using Projet.Service.Fonctions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Projet.Presentation.Forms.ViewModel
{
    public class WindowPersoProfilViewModel : INotifyPropertyChanged
    {

        #region private
        private string _description;
        private string _selectSexe;
        private List<string> _listsexe;
        private DateTime _selectDate;
        private UserCourant user = UserCourant.Instance();
        private OpenFileDialog openFile = new OpenFileDialog();
        private OpenFileDialog _openFileCouverture = new OpenFileDialog();
        private UserCourant user_courant = UserCourant.Instance();
        private BitmapImage _imageCourant;

        private string _path;
        private string _fileName;
        private string _fileNameCouverture;
        private string _sourceImage;
        private string _sourceImageCouverture;
        private bool _verif = false;
        private bool _verifCouv = false;
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

        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                _description = value;
                //SauverModifProfilCommand.RaiseCanExecuteChanged();
                NotifyPropertyChanged(nameof(Description));
            }
        }

        public string SelectSexe
        {
            get
            {
                return _selectSexe;
            }
            set
            {
                _selectSexe = value;
                //SauverModifProfilCommand.RaiseCanExecuteChanged();
                NotifyPropertyChanged(nameof(SelectSexe));
            }
        }

        public List<string> Listsexe { get { return _listsexe; } set { _listsexe = value; } }

        public DateTime SelectDate
        {
            get
            {
                return _selectDate;
            }
            set
            {
                _selectDate = value;
                NotifyPropertyChanged(nameof(SelectDate));
            }
        }

        public BitmapImage ImageCourant
        {
            get
            {
                return _imageCourant;
            }
            set
            {
                _imageCourant = value;
                NotifyPropertyChanged(nameof(ImageCourant));
            }
        }
        public string SourceImage
        {
            get
            {
                return _sourceImage;
            }

            set
            {
                _sourceImage = value;
                NotifyPropertyChanged(nameof(SourceImage));
            }
        }
        public string SourceImageCouverture
        {
            get
            {
                return _sourceImageCouverture;
            }
            set
            {
                _sourceImageCouverture = value;
                NotifyPropertyChanged(nameof(SourceImageCouverture));
            }
        }

        #endregion

        #region Command
        public RelayCommand SauverModifProfilCommand { get; private set; }
        public RelayCommand ParcourirImageCommand { get; private set; }
        public RelayCommand ParcourirImageCouvertureCommand { get; private set; }
        public RelayCommand QuitCommand { get; private set; }
        #endregion

        public WindowPersoProfilViewModel()
        {
            Description = user_courant.Description;
            SelectSexe = user_courant.Sexe;

            _path = Path.Combine(Environment.CurrentDirectory, "Images");
            SourceImage = user_courant.image.ToString();
            SourceImageCouverture = user_courant.couverture.ToString();

            //Remplissage de la combobox Sexe
            Listsexe = new List<string>();
            Listsexe.Add("Pas spécifié...");
            Listsexe.Add("Féminin");
            Listsexe.Add("Masculin");

            //Date de naissance
            DateTime dBase = new DateTime(0001, 01, 01);
            int result = DateTime.Compare(dBase, user_courant.DateDeNaissance);
            if(result == 0) { SelectDate = DateTime.Now; }
            else { SelectDate = user.DateDeNaissance; }

            //COMMANDE
            SauverModifProfilCommand = new RelayCommand(OnSauverModifProfil, CanExecuteSauverModifProfil);
            ParcourirImageCommand = new RelayCommand(OnParcourirImage, CanExecuteParcourirImage);
            ParcourirImageCouvertureCommand = new RelayCommand(OnParcourirImageCouverture, CanExecuteParcourirImageCouverture);
            QuitCommand = new RelayCommand(OnQuit, CanExecuteQuit);
        }

        private void OnQuit(object obj)
        {
            WindowClosedEvent.GetInstance().OnWindowClosedHandler(EventArgs.Empty);
        }

        private bool CanExecuteQuit(object obj)
        {
            return true;
        }

        private void OnParcourirImageCouverture(object obj)
        {
            _openFileCouverture.Title = "Selectionner une photo de couverture";
            _openFileCouverture.DefaultExt = "jpg";
            _openFileCouverture.Filter = "Fichier JPG (*.jpg)|*.jpg";
            if(_openFileCouverture.ShowDialog() == true)
            {
                _fileNameCouverture = Path.GetFileName(_openFileCouverture.FileName);
                SourceImageCouverture = Path.GetFullPath(_openFileCouverture.FileName);
                _verifCouv = true;
            }
        }

        private bool CanExecuteParcourirImageCouverture(object obj)
        {
            return true;
        }

        private void OnParcourirImage(object obj)
        {
            openFile.Title = "Selectionner une image";
            openFile.DefaultExt = "jpg";
            openFile.Filter = " Fichier JPG (*.jpg)|*.jpg";
            if (openFile.ShowDialog() == true)
            {
                //ImageCourant = new BitmapImage(new Uri(openFile.FileName));
                _fileName = Path.GetFileName(openFile.FileName);
                //Aperçu de l'image
                SourceImage = Path.GetFullPath(openFile.FileName);
                _verif = true;
            }
        }

        private bool CanExecuteParcourirImage(object obj)
        {
            return true;
        }

        private void OnSauverModifProfil(object obj)
        {
            user_courant.Description = Description;
            GestionBDD.updateDescription(Description, user_courant.Pseudo);

            if (SelectSexe != "Pas spécifié...")
            {
                user_courant.Sexe = SelectSexe;
                GestionBDD.updateSexe(SelectSexe, user_courant.Pseudo);
            }
           
            if(SelectDate != user_courant.DateDeNaissance)
            {
                user_courant.DateDeNaissance = SelectDate;
                GestionBDD.updateDdn(SelectDate.ToString(), user_courant.Pseudo);
            }

            //GESTION SAUVEGARDE IMAGE
            if (_verif)
            {
                if (!File.Exists($@"{_path}\{_fileName}"))
                {
                    FileInfo f = new FileInfo(openFile.FileName);
                    if (f.Length > 512000)
                    {
                        MessageBox.Show("La taille de l'image de profil est trop grande (500 ko maximum)", "", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    else
                    {
                        File.Copy(openFile.FileName, Path.Combine(_path, _fileName));
                        GestionBDD.enregisterPhotoProfil(_fileName, user.Pseudo);
                        user.image = new BitmapImage(new Uri($@"{_path}\{_fileName}"));
                    }
                }
                else
                {
                    GestionBDD.enregisterPhotoProfil(_fileName, user.Pseudo);
                    user.image = new BitmapImage(new Uri($@"{_path}\{_fileName}"));
                }
            }

            if (_verifCouv)
            {
                if (!File.Exists($@"{_path}\{_fileNameCouverture}"))
                {
                    FileInfo f = new FileInfo(_openFileCouverture.FileName);
                    if (f.Length > 716800)
                    {
                        MessageBox.Show("La taille de l'image de couverture est trop grande (700 ko maximum)", "", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    else
                    {
                        File.Copy(_openFileCouverture.FileName, Path.Combine(_path, _fileNameCouverture));
                        GestionBDD.enregisterPhotoCouverture(_fileNameCouverture, user.Pseudo);
                        user.couverture = new BitmapImage(new Uri($@"{_path}\{_fileNameCouverture}"));
                    }
                }
                else
                {
                    GestionBDD.enregisterPhotoCouverture(_fileNameCouverture, user.Pseudo);
                    user.couverture = new BitmapImage(new Uri($@"{_path}\{_fileNameCouverture}"));
                }
            }
            MessageBox.Show("Modification enregistrée");

        }
        private bool CanExecuteSauverModifProfil(object obj)
        {
            return true;
        }
    }
}
