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

namespace Projet.Presentation.Forms
{
    /// <summary>
    /// Logique d'interaction pour WindowPersoProfil.xaml
    /// </summary>
    public partial class WindowPersoProfil : Window
    {
        private Utilisateur utilisateur;

        public WindowPersoProfil(Utilisateur user)
        {
            InitializeComponent();
            utilisateur = user;

            TextBox_desc.Text = user.description;

            if(user.sexe == "Pas spécifié...")
            {
                cmbSexe.Items.Add("Pas spécifié...");
                cmbSexe.Items.Add("Masculin");
                cmbSexe.Items.Add("Féminin");
            }
            else
            {
                cmbSexe.Items.Add(user.sexe);
                cmbSexe.IsEnabled = false;
            }

            if(user.dateDeNaissance != "00/00/0000")
            {
                cmbMois.Items.Add("-");
                cmbMois.Items.Add("01");
                cmbMois.Items.Add("02");
                cmbMois.Items.Add("03");
                cmbMois.Items.Add("04");
                cmbMois.Items.Add("05");
                cmbMois.Items.Add("06");
                cmbMois.Items.Add("07");
                cmbMois.Items.Add("08");
                cmbMois.Items.Add("09");
                cmbMois.Items.Add("10");
                cmbMois.Items.Add("11");
                cmbMois.Items.Add("12");

                cmbJour.Items.Add("-");
                cmbAnnee.Items.Add("-");

                for (int i = 1; i < 32; i++)
                {
                    cmbJour.Items.Add(i.ToString());
                }
                for (int i = 1950; i < 2018; i++)
                {
                    cmbAnnee.Items.Add(i.ToString());
                }
            }
            else
            {

                cmbAnnee.IsEnabled = false;
                cmbJour.IsEnabled = false;
                cmbMois.IsEnabled = false;
            }
            
            cmbAnnee.SelectedIndex = 0;
            cmbJour.SelectedIndex = 0;
            cmbMois.SelectedIndex = 0;
            cmbSexe.SelectedIndex = 0;
        }

        

        private void Retour_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            if (TextBox_desc.Text != null)
            {
                utilisateur.description = TextBox_desc.Text;
                GestionBDD.updateDescription(TextBox_desc.Text, utilisateur.pseudo);
            }

            if(cmbSexe.Text != "Non spécifié")
            {
                utilisateur.sexe = cmbSexe.Text;
                GestionBDD.updateSexe(cmbSexe.Text, utilisateur.pseudo);
            }

            if (cmbAnnee.Text != "-" || cmbJour.Text != "-" || cmbMois.Text!= "-")
            {
                string ddn = cmbJour.Text + "/" + cmbMois.Text + "/" + cmbAnnee.Text;
                utilisateur.dateDeNaissance = ddn;
                GestionBDD.updateDdn(ddn, utilisateur.pseudo);
            }
            Close();
        }
    }
}
