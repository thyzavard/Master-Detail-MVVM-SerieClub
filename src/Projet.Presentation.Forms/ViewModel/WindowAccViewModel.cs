using Projet.Entite.Class;
using Projet.Presentation.Forms.Commands;
using Projet.Service.Fonctions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Projet.Presentation.Forms.ViewModel
{
    public class WindowAccViewModel : INotifyPropertyChanged
    {
        #region private
        private UserCourant _user = UserCourant.Instance();
        private string _pseudo;
        private object _selectedViewModel;
        private string _imageSelect;
        private bool _isVisible = true;
        private bool _isVisibleAdmin;
        #endregion

        #region Command
        public RelayCommand PersoProfilCommand { get; private set; }
        public RelayCommand AdministrationCommand { get; private set; }
        public RelayCommand OuvrirAcceuilCommand { get; private set; }
        public RelayCommand OuvrirProfilCommand { get; private set; }
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

        public string Pseudo
        {
            get
            {
                return _pseudo;
            }

            set
            {
                _pseudo = value;
            }
        }
        public object SelectedViewModel
        {
            get { return _selectedViewModel; }
            set
            {
                _selectedViewModel = value;
                OuvrirProfilCommand.RaiseCanExecuteChanged();
                OuvrirAcceuilCommand.RaiseCanExecuteChanged();
                NotifyPropertyChanged(nameof(SelectedViewModel));
            }
        }

        public string ImageSelect
        {
            get
            {
                return _imageSelect;
            }
            set { _imageSelect = value; }
        }

        public bool IsVisible
        {
            get
            {
                return _isVisible;
            }

            set
            {
                _isVisible = value;
                NotifyPropertyChanged(nameof(IsVisible));
            }
        }

        public bool IsVisibleAdmin
        {
            get
            {
                return _isVisibleAdmin;
            }

            set
            {
                _isVisibleAdmin = value;
                NotifyPropertyChanged(nameof(IsVisibleAdmin));
            }
        }
        #endregion

        public WindowAccViewModel()
        {
            PersoProfilCommand = new RelayCommand(OnPersoProfil, CanExecutePersoProfil);
            AdministrationCommand = new RelayCommand(OnAdministration, CanExecuteAdministration);
            OuvrirAcceuilCommand = new RelayCommand(OnOuvrirAcceuil, CanExecuteOuvrirAcceuil);
            OuvrirProfilCommand = new RelayCommand(OnOuvrirProfil, CanExecuteOuvrirProfil);

            SelectedViewModel = new ViewAccueilViewModel();
            _user.Serieadd = GestionBDD.returnSerieUtilisateurFull(_user.Pseudo);
            string path = GestionBDD.loadPhotoProfil(_user.Pseudo);
            _user.image = new BitmapImage(new Uri(path));
            Pseudo = _user.Pseudo;
        }

        private void OnOuvrirProfil(object obj)
        {
            IsVisible = false;
            SelectedViewModel = new ViewProfilViewModel();
        }

        private bool CanExecuteOuvrirProfil(object obj)
        {
            if (SelectedViewModel.GetType() != typeof(ViewProfilViewModel))
            {
                return true;
            }
            else
            {
                return false;
            }
            //return true;
        }

        private void OnOuvrirAcceuil(object obj)
        {
            IsVisible = true;
            SelectedViewModel = new ViewAccueilViewModel();
        }

        private bool CanExecuteOuvrirAcceuil(object obj)
        {
            if (SelectedViewModel.GetType() != typeof(ViewAccueilViewModel))
            {
                return true;
            }
            else
            {
                return false;
            }
            //return true;
        }

        private void OnAdministration(object obj)
        {
            WindowAdd wp = new WindowAdd();
            wp.ShowDialog();
        }

        private bool CanExecuteAdministration(object obj)
        {
            IsVisibleAdmin = _user.Modo;
            return true;
        }

        private void OnPersoProfil(object obj)
        {
            WindowPersoProfil w = new WindowPersoProfil();
            w.ShowDialog();
        }

        private bool CanExecutePersoProfil(object obj)
        {
            return true;
        }
    }
}
