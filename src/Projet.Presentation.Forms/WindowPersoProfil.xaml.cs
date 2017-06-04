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
using Projet.Entite.Class;
using Projet.Service.Fonctions;
using System.Data.SqlClient;
using System.Data;
using Microsoft.Win32;
using Projet.Presentation.Forms.ViewModel;

namespace Projet.Presentation.Forms
{
    /// <summary>
    /// Logique d'interaction pour WindowPersoProfil.xaml
    /// </summary>
    public partial class WindowPersoProfil : Window
    {
        private WindowPersoProfilViewModel _vm;

        public WindowPersoProfil()
        {
            InitializeComponent();
            _vm = new WindowPersoProfilViewModel();

            DataContext = _vm;
        }
    }
}
