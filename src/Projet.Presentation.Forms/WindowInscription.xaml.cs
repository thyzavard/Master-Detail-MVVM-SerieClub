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
using Projet.Presentation.Forms.ViewModel;

namespace Projet.Presentation.Forms
{
    /// <summary>
    /// Logique d'interaction pour WindowInscription.xaml
    /// </summary>

    public partial class WindowInscription : Window
    {
        private WindowInscriptionViewModel _vm;

        public WindowInscription()
        {
            InitializeComponent();
            _vm = new WindowInscriptionViewModel();

            DataContext = _vm;
        }
    }
}
