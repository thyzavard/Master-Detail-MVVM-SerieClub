﻿using Projet.Entite.Class;
using Projet.Presentation.Forms.Commands;
using Projet.Service.Fonctions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet.Presentation.Forms.ViewModel
{
    public class ViewProfilViewModel : INotifyPropertyChanged
    {
        #region private
        private string _currentPseudo;
        private string _currentDescription;
        private string _selectedSerie;
        private string _titreEnFonctionDuNbDeSerie;
        private List<Serie> _serieUtilisateur;
        private UserCourant _user = UserCourant.Instance();
        private object _selectedViewModel;
        #endregion

        #region Public
        public string CurrentPseudo
        {
            get { return _currentPseudo; }
            set
            {
                _currentPseudo = value;
            }
        }

        public string CurrentDescription
        {
            get
            {
                return _currentDescription;
            }
            set
            {
                _currentDescription = value;
            }
        }

        public string SelectedSerie
        {
            get
            {
                return _selectedSerie;
            }
            set
            {
                _selectedSerie = value;
                NotifyPropertyChanged(nameof(SelectedSerie));
            }
        }

        public string TitreEnFonctionDuNbDeSerie
        {
            get
            {
                return _titreEnFonctionDuNbDeSerie;
            }
            set
            {
                _titreEnFonctionDuNbDeSerie = value;
            }
        }

        public List<Serie> SerieUtilisateur
        {
            get
            {
                return _serieUtilisateur;
            }

            set
            {
                _serieUtilisateur = value;
            }
        }

        public object SelectedViewModel
        {
            get { return _selectedViewModel; }
            set
            {
                _selectedViewModel = value;
                NotifyPropertyChanged(nameof(SelectedViewModel));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
        #endregion

        #region Command
        public RelayCommand InfoSerieCommand { get; private set; }
        #endregion

        public ViewProfilViewModel()
        {
            CurrentPseudo = _user.Pseudo;
            CurrentDescription = _user.Description;

            //*****GESTION SÉRIE UTILISATEUR*****
            SerieUtilisateur = _user.Serieadd;
            /*_listNom = GestionBDD.returnSerieUtilisateur(_user.Pseudo);
            SerieUtilisateur = new List<Serie>();
            for (int i = 0; i < _listNom.Count; i++)
            {
                Serie serie = GestionBDD.remplirSerie(_listNom[i]);
                SerieUtilisateur.Add(serie);
            }*/

            //*****GESTION DU MESSAGE EN FONCTION DU NOMBRE DE SÉRIE*****
            if (SerieUtilisateur.Count == 0) { TitreEnFonctionDuNbDeSerie = "Je n'ai pas de séries préférées..."; }
            else if (SerieUtilisateur.Count == 1) { TitreEnFonctionDuNbDeSerie = "Ma série préférée"; }
            else { TitreEnFonctionDuNbDeSerie = "Mes séries préférées"; }

            InfoSerieCommand = new RelayCommand(OnInfoSerie, CanExecuteInfoSerie);
        }

        private void OnInfoSerie(object obj)
        {

        }

        private bool CanExecuteInfoSerie(object obj)
        {
            return true;
        }




        /*if (listSerieUser.Count == 0)
            {
                l_seriefav.Content = "Aucune série ajoutée en favoris...";
            }
            else if (listSerieUser.Count == 1)
            {
                l_seriefav.Content = "Série en favoris";
            }
            else
            {
                l_seriefav.Content = "Séries en favoris";
            }*/
    }
}