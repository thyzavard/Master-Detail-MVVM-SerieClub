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
    /// Logique d'interaction pour ViewAcceuil.xaml
    /// </summary>
    public partial class ViewAcceuil : UserControl
    {
        private ViewAccueilViewModel _vm;

        public ViewAcceuil()
        {
            InitializeComponent();
            _vm = new ViewAccueilViewModel();

            DataContext = _vm;
        }

        //private void StackPanel_PreviewMouseDown(object sender, MouseButtonEventArgs e)
    }
}
