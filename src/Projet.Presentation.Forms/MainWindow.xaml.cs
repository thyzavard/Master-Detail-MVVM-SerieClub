using Projet.Entite.Class;
using Projet.Presentation.Forms.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
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
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainWindowViewModel _vm;

        public MainWindow()
        {
            InitializeComponent();
            _vm = new MainWindowViewModel();

            DataContext = _vm;
        }

        /* private void btnConfirmer_Click(object sender, RoutedEventArgs e)
         {
             if(textboxId_Acc.Text != "" || passwordBox_Acc.Password != "")
             {
                 if(Service.Fonctions.GestionBDD.verifLoginMdp(textboxId_Acc.Text, passwordBox_Acc.Password))
                 {
                     Utilisateur user = Service.Fonctions.GestionBDD.remplirUser(textboxId_Acc.Text);
                     WindowAcc w = new WindowAcc(user);
                     this.Hide();
                     w.Show();
                 }
                 else
                 {
                     MessageBox.Show("Les informations transmises n'ont pas permis de vous authentifier", "Erreur d'identification", MessageBoxButton.OK, MessageBoxImage.Warning);
                 }
             }
             else
             {
                 MessageBox.Show("Veuillez remplir tous les champs", "Champs incomplet", MessageBoxButton.OK, MessageBoxImage.Warning);
             }
         }*/

    }
}
