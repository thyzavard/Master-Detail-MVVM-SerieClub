using Projet.Entite.Class;
using Projet.Presentation.Forms.ViewModel;
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
        private ViewProfilViewModel _vm;

        public ViewProfil()
        {
            InitializeComponent();
            _vm = new ViewProfilViewModel();

            DataContext = _vm;
        }

        //CHARGER IMAGE PROFIL
        /*string path = Path.Combine(Environment.CurrentDirectory, "images_profil_user");
        ImageSelect = $"{path}/{user_courant.Pseudo}.jpg";*/
    }
}
