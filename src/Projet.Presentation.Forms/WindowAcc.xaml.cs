﻿using System;
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
        private Entite.Class.Utilisateur utilisateur;
        public WindowAcc(Entite.Class.Utilisateur user)
        {
            InitializeComponent();
            btn_Acc.IsEnabled = false;
            l_user.Content = user.pseudo;
            utilisateur = user;
        }

        private void Acceuil_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new ViewAcceuil();
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

        private void PersoProfil_Click(object sender, RoutedEventArgs e)
        {
            WindowPersoProfil w = new WindowPersoProfil(utilisateur);
            w.Show();
        }
    }
}
