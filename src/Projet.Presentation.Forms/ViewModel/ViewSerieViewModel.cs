using Projet.Presentation.Forms.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet.Presentation.Forms.ViewModel
{
    public class ViewSerieViewModel
    {
        #region private
        private string _nomSerie;
        private string _noteSerie;
        private string _descriptionSerie;
        private string _commentaireSerie;
        private string _selectedSaison;
        private List<string> _listSaisonSerie;
        private List<string> _listEpisode;
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
        public List<string> ListSaisonSerie
        {
            get
            {
                return _listSaisonSerie;
            }
            set
            {
                _listSaisonSerie = value;
            }
        }
        public List<string> ListEpisode
        {
            get
            {
                return _listEpisode;
            }
            set
            {
                _listEpisode = value;
            }
        }
        #endregion

        #region Command
        public RelayCommand AjouterSerieCommand { get; set; }
        public RelayCommand EnvoyerCommentaireCommand { get; set; }
        #endregion

        public ViewSerieViewModel()
        {
            ListSaisonSerie = new List<string>();

            AjouterSerieCommand = new RelayCommand(OnAjouterSerie, CanExecuteAjouterSerie);
            EnvoyerCommentaireCommand = new RelayCommand(OnEnvoyerCommentaire, CanExecuteEnvoyerCommentaire);
        }

        private void OnAjouterSerie(object obj)
        {
            throw new NotImplementedException();
        }

        private bool CanExecuteAjouterSerie(object obj)
        {
            return true;
        }

        private void OnEnvoyerCommentaire(object obj)
        {
            throw new NotImplementedException();
        }

        private bool CanExecuteEnvoyerCommentaire(object obj)
        {
            if(CommentaireSerie != null)
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
