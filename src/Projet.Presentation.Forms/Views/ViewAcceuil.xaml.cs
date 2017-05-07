using Projet.Entite.Class;
using Projet.Service.Fonctions;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Projet.Presentation.Forms
{
    /// <summary>
    /// Logique d'interaction pour ViewAcceuil.xaml
    /// </summary>
    public partial class ViewAcceuil : UserControl
    {
        public List<Serie> listserie
        {
            get;
            set;
        }
        public ViewAcceuil()
        {
            InitializeComponent();

            List<string> listNom = GestionBDD.returnTouteSerie();
            listserie = new List<Serie>();
            for (int i = 0; i < listNom.Count; i++)
            {
                Serie serie = GestionBDD.remplirSerie(listNom[i]);
                listserie.Add(serie);
            }

        }
    }
}
