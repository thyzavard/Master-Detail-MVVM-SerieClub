using Projet.Entite.Class;
using Projet.Presentation.Forms.ViewModel;
using Projet.Service.Fonctions;
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
    /// Logique d'interaction pour WindowAcc.xaml
    /// </summary>
    public partial class WindowAcc : Window
    {
        private Utilisateur utilisateur;
        private WindowAccViewModel _vm;

        public WindowAcc(Utilisateur user)
        {
            InitializeComponent();
            _vm = new WindowAccViewModel(user);

            DataContext = _vm;
            /*
            l_user.Content = user.pseudo;

            utilisateur = user;

            List<string> listnbSerie = new List<string>();
            List<Serie> listSerie = new List<Serie>();
            listnbSerie = GestionBDD.returnSerieUtilisateur(user.pseudo);

            for (int i = 0; i < listnbSerie.Count; i++)
            {
                Serie serie = GestionBDD.remplirSerie(listnbSerie[i]);
                listSerie.Add(serie);
            }
            utilisateur.serieadd = listSerie;

            if (user.modo)
            {
                btn_admin.Visibility = Visibility.Visible;
            }
            else
            {
                btn_admin.Visibility = Visibility.Hidden;
            }
            DataContext = new ViewAcceuil(utilisateur);*/
        }

        private void Acceuil_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new ViewAcceuil(utilisateur);
            btn_Profil.IsEnabled = true;
            btn_Acc.IsEnabled = false;
            textBox_search.Visibility = Visibility.Visible;
        }

        private void Profil_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new ViewProfil(utilisateur);
            btn_Acc.IsEnabled = true;
            btn_Profil.IsEnabled = false;
            textBox_search.Visibility = Visibility.Hidden;
        }

        private void btnDeco_Click(object sender, RoutedEventArgs e)
        {
            MainWindow m = new MainWindow();
            Close();
            m.Show();
        }

       /* private void PersoProfil_Click(object sender, RoutedEventArgs e)
        {
            WindowPersoProfil w = new WindowPersoProfil(utilisateur);
            w.Show();
        }

        private void admin_Click(object sender, RoutedEventArgs e)
        {
            WindowAdd w = new WindowAdd();
            w.Show();
        }*/

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new ViewSerie();
        }
    }
}
