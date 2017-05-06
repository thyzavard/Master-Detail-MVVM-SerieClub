using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace Projet.Presentation.Forms
{
    /// <summary>
    /// Logique d'interaction pour WindowProfil.xaml
    /// </summary>
    public partial class WindowProfil : Window
    {
        public WindowProfil(Entite.Class.Utilisateur user)
        {
            InitializeComponent();
            l_user.Content = user.pseudo;
        }

        private void btnDeco_Click(object sender, RoutedEventArgs e)
        {
            MainWindow m = new MainWindow();
            Close();
            m.Show();
        }

        private void PersoProfil_Click(object sender, RoutedEventArgs e)
        {
            /*WindowPersoProfil w = new WindowPersoProfil();
            w.Show();*/
        }
    }
}
