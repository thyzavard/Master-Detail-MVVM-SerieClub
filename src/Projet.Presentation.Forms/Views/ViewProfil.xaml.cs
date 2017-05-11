using Projet.Entite.Class;
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

            Serie s = new Serie();
            s.nom = "Test";

            Serie s1 = new Serie();
            s1.nom = "yolo";

            Serie s3 = new Serie();
            s3.nom = "Test";

            Serie s4 = new Serie();
            s4.nom = "yolo";

            Serie s5 = new Serie();
            s5.nom = "Test";

            Serie s6 = new Serie();
            s6.nom = "yolo";

            Serie s7 = new Serie();
            s7.nom = "Test";

            Serie s8 = new Serie();
            s8.nom = "Test";

            Serie s9 = new Serie();
            s9.nom = "Test";

            Serie s10 = new Serie("Test");

            listSerieUser.Add(s);
            listSerieUser.Add(s1);
            listSerieUser.Add(s3);
            listSerieUser.Add(s4);
            listSerieUser.Add(s5);
            listSerieUser.Add(s6);
            listSerieUser.Add(s7);
            listSerieUser.Add(s8);
            listSerieUser.Add(s9);
            listSerieUser.Add(s10);
        }
        public ViewProfil()
        {
            InitializeComponent();
        }
    }
}
