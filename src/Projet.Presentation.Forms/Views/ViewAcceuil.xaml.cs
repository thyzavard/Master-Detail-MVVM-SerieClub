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
        public List<Serie> listserieAction { get; set; }
        public List<Serie> listserieHorreur { get; set; }
        public List<Serie> listserieFantastique { get; set; }
        public List<Serie> listserieDrame { get; set; }
        public List<Serie> listserieComedie { get; set; }

        private ListBox ListBox1 = new ListBox();

        public ViewAcceuil()
        {
            InitializeComponent();

            List<string> listNom = GestionBDD.returnTouteSerie();
            listserieAction = new List<Serie>();
            listserieFantastique = new List<Serie>();
            listserieHorreur = new List<Serie>();
            listserieDrame = new List<Serie>();
            listserieComedie = new List<Serie>();

            for (int i = 0; i < listNom.Count; i++)
            {
                Serie serie = GestionBDD.remplirSerie(listNom[i]);
                if(serie.genre == Genre.Action)
                {
                    listserieAction.Add(serie);
                }
                if(serie.genre == Genre.Horreur)
                {
                    listserieHorreur.Add(serie);
                }
                if(serie.genre == Genre.Fantastique)
                {
                    listserieFantastique.Add(serie);
                }
                if(serie.genre == Genre.Comedie)
                {
                    listserieComedie.Add(serie);
                }
                if (serie.genre == Genre.Drame)
                {
                    listserieDrame.Add(serie);
                }
            }


           

        }
    }
}
