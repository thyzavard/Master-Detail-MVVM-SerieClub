using Microsoft.Win32;
using Projet.Entite.Class;
using Projet.Presentation.Forms.Commands;
using Projet.Presentation.Forms.Converters;
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
        private List<string> _listJour;
        private List<string> _listMois;
        private List<string> _listAnne;
        private string _selectedJour;
        private string _selectedMois;
        private string _selectedAnne;
        private UserCourant user = UserCourant.Instance();
        private OpenFileDialog openFile = new OpenFileDialog();
        private UserCourant user_courant = UserCourant.Instance();
        private BitmapImage _imageCourant;

        private string _path;
        private string _fileName;
        private string _sourceImage;
        private bool _verif = false;
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
        public List<string> ListJour { get { return _listJour; } set { _listJour = value; } }
        public List<string> ListMois { get { return _listMois; } set { _listMois = value; } }
        public List<string> ListAnne { get { return _listAnne; } set { _listAnne = value; } }


        public string SelectedJour
        {
            get
            {
                return _selectedJour;
            }
            set
            {
                _selectedJour = value;
                SauverModifProfilCommand.RaiseCanExecuteChanged();
            }
        }
        public string SelectedMois
        {
            get
            {
                return _selectedMois;
            }
            set
            {
                _selectedMois = value;
                SauverModifProfilCommand.RaiseCanExecuteChanged();
            }
        }
        public string SelectedAnne
        {
            get
            {
                return _selectedAnne;
            }
            set
            {
                _selectedAnne = value;
                SauverModifProfilCommand.RaiseCanExecuteChanged();
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
        #endregion

        #region Command
        public RelayCommand SauverModifProfilCommand { get; private set; }
        public RelayCommand ParcourirImageCommand { get; private set; }
        #endregion

        public WindowPersoProfilViewModel()
        {
            Description = user_courant.Description;
            SelectSexe = user_courant.Sexe;

            _path = Path.Combine(Environment.CurrentDirectory, "Images");
            SourceImage = user_courant.image.ToString();


            //Remplissage de la combobox Sexe
            Listsexe = new List<string>();
            Listsexe.Add("Pas spécifié...");
            Listsexe.Add("Féminin");
            Listsexe.Add("Masculin");

            //Remplissage combobox Date de naissance 
            ListMois = new List<string>();
            ListJour = new List<string>();
            ListAnne = new List<string>();
            if (user_courant.DateDeNaissance == "00/00/0000")
            {
                ListMois.Add("-");
                ListMois.Add("01");
                ListMois.Add("02");
                ListMois.Add("03");
                ListMois.Add("04");
                ListMois.Add("05");
                ListMois.Add("06");
                ListMois.Add("07");
                ListMois.Add("08");
                ListMois.Add("09");
                ListMois.Add("10");
                ListMois.Add("11");
                ListMois.Add("12");

                ListJour.Add("-");
                ListAnne.Add("-");

                for (int i = 1; i < 32; i++)
                {
                    ListJour.Add(i.ToString());
                }
                for (int i = 1950; i < 2018; i++)
                {
                    ListAnne.Add(i.ToString());
                }
            }

            //COMMANDE
            SauverModifProfilCommand = new RelayCommand(OnSauverModifProfil, CanExecuteSauverModifProfil);
            ParcourirImageCommand = new RelayCommand(OnParcourirImage, CanExecuteParcourirImage);
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

            if (SelectedJour != "-" || SelectedMois != "-" || SelectedAnne != "-")
            {
                string ddn = $"{SelectedJour}/{SelectedMois}/{SelectedAnne}";
                user_courant.DateDeNaissance = ddn;
                GestionBDD.updateDdn(ddn, user_courant.Pseudo);
            }
            else
            {
                MessageBox.Show("Veuillez rentrer une date correct", "Erreur Date", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

            //GESTION SAUVEGARDE IMAGE
            if (_verif)
            {
                if (!File.Exists($@"{_path}\{_fileName}"))
                {
                    File.Copy(openFile.FileName, Path.Combine(_path, _fileName));
                    GestionBDD.enregisterPhotoProfil(_fileName, user.Pseudo);
                    user.image = new BitmapImage(new Uri($@"{_path}\{_fileName}"));
                }
                else
                {
                    GestionBDD.enregisterPhotoProfil(_fileName, user.Pseudo);
                    user.image = new BitmapImage(new Uri($@"{_path}\{_fileName}"));
                }
                //File.Move($@"Images\{_fileName}", $@"Images\{user.Pseudo}.jpg");
            }
            MessageBox.Show("Modification enregistrée");
        }
        private bool CanExecuteSauverModifProfil(object obj)
        {
            return true;
        }
    }
}
