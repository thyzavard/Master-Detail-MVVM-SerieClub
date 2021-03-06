﻿using Projet.Entite.Class;
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
        private WindowAccViewModel _vm;

        public WindowAcc()
        {
            InitializeComponent();
            _vm = new WindowAccViewModel();

            DataContext = _vm;
        }
        
    }
}
