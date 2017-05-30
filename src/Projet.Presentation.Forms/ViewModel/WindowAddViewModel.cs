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
    public class WindowAddViewModel : INotifyPropertyChanged
    {
        #region private
        private List<string> _genresource;
        private string _nomserie;
        private string _descriptionserie;
        private string _producteurserie;
        private string _dureemoyenneserie;
        private string _selectgenre;
        private int _result;
        private List<string> _listserie;
        private string _selectserie;
        private string _descriptionseriemodif;
        private string _producteurseriemodif;
        private string _dureemoyenneseriemodif;
        private string _selectgenremodif;
        private List<string> _listpseudo;
        private string _selectseriesuppr;
        private string _selectpseudo;

        #endregion

        #region Command
        public RelayCommand AjouterSerieCommand { get; private set; }
        public RelayCommand ModifierSerieCommand { get; private set; }
        public RelayCommand SupprimerSerieCommand { get; private set; }
        public RelayCommand downAdminCommand { get; private set; }
        public RelayCommand upAdminCommand { get; private set; }
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

        public List<string> GenreSource
        {
            get
            {
                return _genresource;
            }
            set
            {
                _genresource = value;
            }
        }

        public string NomSerie
        {
            get
            {
                return _nomserie;
            }
            set
            {
                _nomserie = value;
                AjouterSerieCommand.RaiseCanExecuteChanged();
            }
        }

        public string DescriptionSerie
        {
            get
            {
                return _descriptionserie;
            }
            set
            {
                _descriptionserie = value;
                AjouterSerieCommand.RaiseCanExecuteChanged();
            }
        }
        public string ProducteurSerie
        {
            get
            {
                return _producteurserie;
            }
            set
            {
                _producteurserie = value;
                AjouterSerieCommand.RaiseCanExecuteChanged();
            }
        }
        public string DureeMoyenneSerie
        {
            get
            {
                return _dureemoyenneserie;
            }
            set
            {
                _dureemoyenneserie = value;
                AjouterSerieCommand.RaiseCanExecuteChanged();
            }
        }

        public string SelectGenre
        {
            get
            {
                return _selectgenre;
            }
            set
            {
                _selectgenre = value;
                AjouterSerieCommand.RaiseCanExecuteChanged();
            }
        }

        public List<string> Listserie
        {
            get
            {
                return _listserie;
            }
            set
            {
                _listserie = value;
            }
        }
        public string SelectSerie
        {
            get
            {
                return _selectserie;
            }
            set
            {
                _selectserie = value;
                NotifyPropertyChanged(nameof(SelectSerie));
                Serie seriemodif = GestionBDD.remplirSerie(SelectSerie);
                Descriptionseriemodif = seriemodif.description;
                Producteurseriemodif = seriemodif.producteur;
                Dureemoyenneseriemodif = seriemodif.dureeMoy.ToString();
                Selectgenremodif = seriemodif.genre.ToString();
            }
        }

        public string Descriptionseriemodif
        {
            get
            {
                return _descriptionseriemodif;
            }
            set
            {
                _descriptionseriemodif = value;
                ModifierSerieCommand.RaiseCanExecuteChanged();
                NotifyPropertyChanged(nameof(Descriptionseriemodif));
            }
        }
        public string Producteurseriemodif
        {
            get
            {
                return _producteurseriemodif;
            }
            set
            {
                _producteurseriemodif = value;
                ModifierSerieCommand.RaiseCanExecuteChanged();
                NotifyPropertyChanged(nameof(Producteurseriemodif));
            }
        }
        public string Dureemoyenneseriemodif
        {
            get
            {
                return _dureemoyenneseriemodif;
            }
            set
            {
                _dureemoyenneseriemodif = value;
                ModifierSerieCommand.RaiseCanExecuteChanged();
                NotifyPropertyChanged(nameof(Dureemoyenneseriemodif));
            }
        }
        public string Selectgenremodif
        {
            get
            {
                return _selectgenremodif;
            }
            set
            {
                _selectgenremodif = value;
                ModifierSerieCommand.RaiseCanExecuteChanged();
                NotifyPropertyChanged(nameof(Selectgenremodif));
            }
        }

        public List<string> ListPseudo { get { return _listpseudo; } set { _listpseudo = value; } }
        public string Selectseriesuppr
        {
            get
            {
                return _selectseriesuppr;
            }
            set
            {
                _selectseriesuppr = value;
                SupprimerSerieCommand.RaiseCanExecuteChanged();
            }
        }

        public string Selectpseudo
        {
            get
            {
                return _selectpseudo;
            }
            set
            {
                _selectpseudo = value;
                upAdminCommand.RaiseCanExecuteChanged();
                downAdminCommand.RaiseCanExecuteChanged();
            }

        }
        #endregion

        public WindowAddViewModel()
        {
            Listserie = GestionBDD.returnTouteSerie();
            ListPseudo = GestionBDD.returnToutUtilisateur();

            GenreSource = new List<string>();
            GenreSource.Add("Action");
            GenreSource.Add("Comedie");
            GenreSource.Add("Drame");
            GenreSource.Add("Fantastique");
            GenreSource.Add("Horreur");

            AjouterSerieCommand = new RelayCommand(OnAjouterSerie, CanexecuteAjouterSerie);
            ModifierSerieCommand = new RelayCommand(OnModifierSerie, CanexecuteModifierSerie);
            SupprimerSerieCommand = new RelayCommand(OnSupprmierSerie, CanexecuteSupprimerSerie);
            downAdminCommand = new RelayCommand(OnDownAdmin, CanexecuteDownAdmin);
            upAdminCommand = new RelayCommand(OnUpAdmin, CanexecuteUpAdmin);
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
                //Listserie.RemoveAll(element => element.Contains(Selectseriesuppr));
                for (int i = Listserie.Count - 1; i >= 0; i--)
                {
                    if (Listserie[i].Contains(Selectseriesuppr))
                    {
                        Listserie.RemoveAt(i);
                    }
                }
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
            GestionBDD.updateSerie(SelectSerie, Descriptionseriemodif, int.Parse(Dureemoyenneseriemodif), Producteurseriemodif, Selectgenremodif);
            MessageBox.Show("Modification enrgistrées");
        }
        private bool CanexecuteModifierSerie(object obj)
        {
            if (SelectSerie != null)
            {
                return true;
            }
            else
            {
                return false;
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
                    GestionBDD.ajouter_Serie(NomSerie, DescriptionSerie, SelectGenre, ProducteurSerie, int.Parse(DureeMoyenneSerie));
                    Listserie.Add(NomSerie);
                    MessageBox.Show("Inscription enregistrée", "Confirmation", MessageBoxButton.OK);
                    /*NomSerie = "";
                    DescriptionSerie = null;
                    SelectGenre = null;
                    ProducteurSerie = null;
                    DureeMoyenneSerie = null;*/
                }
                else
                {
                    MessageBox.Show("Veuillez rentrer uniquement des nombres pour la Durée Moyenne de la serie", "Erreur", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }

        }
        private bool CanexecuteAjouterSerie(object obj)
        {
            if (NomSerie != null && DescriptionSerie != null && ProducteurSerie != null && DureeMoyenneSerie != null && SelectGenre != null)
            {
                return true;
            }
            return false;
        }
    }
}
