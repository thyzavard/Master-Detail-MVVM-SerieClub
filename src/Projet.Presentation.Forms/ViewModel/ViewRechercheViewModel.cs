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

namespace Projet.Presentation.Forms.ViewModel
{
    public class ViewRechercheViewModel : BaseViewModel
    {
        #region private
        private string _recherchepour;
        private Serie _selectedSerie;
        private UserCourant _user_courant = UserCourant.Instance();
        private bool _isVisible = false;
        private List<Serie> _list = new List<Serie>();
        #endregion

        #region Public
        public string Recherchepour
        {
            get
            {
                return _recherchepour;
            }
            set
            {
                _recherchepour = value;
                NotifyPropertyChanged(nameof(Recherchepour));
            }
        }
        public ObservableCollection<Serie> ListserieRecherche { get; set; }

        public Serie SelectedSerie { get { return _selectedSerie; } set
            {
                _selectedSerie = value;
                NotifyPropertyChanged(nameof(SelectedSerie));
            }
        }
        public bool IsVisible
        {
            get { return _isVisible; }
            set
            {
                _isVisible = value;
                NotifyPropertyChanged(nameof(IsVisible));
            }
        }
        #endregion

        #region Command
        public RelayCommand RetourArriereCommand { get; private set; }
        public RelayCommand AjouterSerieCommand { get; private set; }
        public RelayCommand InfoSerieCommand { get; private set; }
        #endregion


        public ViewRechercheViewModel(IEnumerable<Serie> rechercheserie, string txt)
        {
            RetourArriereCommand = new RelayCommand(OnRetourArriere, CanExecuteRetourArriere);
            AjouterSerieCommand = new RelayCommand(OnAjouterSerie, CanExecuteAjouterSerie);
            InfoSerieCommand = new RelayCommand(OnInfoSerie, CanExecuteInfoSerie);

            Recherchepour = $"Résultat de la recherche pour '{txt}'";

            
            foreach(Serie s in rechercheserie)
            {
                _list.Add(s);
            }
            ListserieRecherche = _list.ToObservableCollection();
            if(ListserieRecherche.Count == 0)
            {
                IsVisible = true;
            }
        }

        private void OnInfoSerie(object obj)
        {
            if (SelectedSerie != null)
            {
                OpenInfoSerieEvent.GetInstance().OnOpenInfoSerieHandler(new SerieEventArgs() { Serie = SelectedSerie });
            }
        }

        private bool CanExecuteInfoSerie(object obj)
        {
            return true;
        }

        private void OnAjouterSerie(object obj)
        {
            if (SelectedSerie != null)
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

        private void OnRetourArriere(object obj)
        {
            RetourWindowAccueilEvent.GetInstance().OnRetourWindowAccueilHandler(EventArgs.Empty);
        }

        private bool CanExecuteRetourArriere(object obj)
        {
            return true;
        }
    }
}
