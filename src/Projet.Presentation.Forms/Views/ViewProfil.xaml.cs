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
        public ViewProfil(Entite.Class.Utilisateur user)
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
        }
        public ViewProfil()
        {
            InitializeComponent();
        }
    }
}
