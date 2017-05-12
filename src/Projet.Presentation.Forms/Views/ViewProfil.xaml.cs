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
    /// Logique d'interaction pour ViewProfil.xaml
    /// </summary>
    public partial class ViewProfil : UserControl
    {
        public List<Serie> listSerieUser { get; set; }

        public ViewProfil(Utilisateur user)
        {
            InitializeComponent();
            l_pseudoUser.Content = user.pseudo;
            if (user.description == "Description...")
            {
                l_descritpion.Content = "";
            }
            else
            {
                l_descritpion.Content = user.description;
            }

            //*****Gestion Série*****
            listSerieUser = new List<Serie>();

            List<string> listnbSerie = new List<string>();

            listnbSerie = GestionBDD.returnSerieUtilisateur(user.pseudo);

            for(int i = 0; i < listnbSerie.Count; i++)
            {
                Serie serie = GestionBDD.remplirSerie(listnbSerie[i]);
                listSerieUser.Add(serie);
            }



            if (listSerieUser.Count == 0)
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
            }
        }
        public ViewProfil()
        {
            InitializeComponent();
        }

        private void Info_btn(object sender, RoutedEventArgs e)
        {
            DataContext = new ViewSerie();
        }
    }
}
