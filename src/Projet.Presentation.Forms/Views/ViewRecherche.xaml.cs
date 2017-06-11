using Projet.Presentation.Forms.ViewModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Projet.Presentation.Forms
{
    /// <summary>
    /// Logique d'interaction pour ViewRecherche.xaml
    /// </summary>
    public partial class ViewRecherche : UserControl
    {
        ViewRechercheViewModel _vm;

        public ViewRecherche()
        {
            InitializeComponent();

            //_vm = new ViewRechercheViewModel();

            //DataContext = _vm;
        }
    }
}
