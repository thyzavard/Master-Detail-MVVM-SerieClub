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


            /*cmbSexe.Items.Add("Non spécifié");
            cmbSexe.Items.Add("Masculin");
            cmbSexe.Items.Add("Féminin");
            cmbSexe.SelectedIndex = 0;*/
        }

       /* private void Quit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            if(passwordBox1.Password == passwordBox2.Password && passwordBox1.Password != "" && passwordBox2.Password != "")
            {
                if(textboxId.Text == "")
                {
                    MessageBox.Show("Veuillez rentrer un nom d'utilisateur", "Nom utilisateur",MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                {
                    if (GestionBDD.verifLogin(textboxId.Text))
                    {
                        MessageBox.Show("Ce nom de compte est déjà utilisé", "Erreur", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    else
                    {
                        GestionBDD.inscription(textboxId.Text, passwordBox1.Password);
                        if (cmbSexe.Text != "Non spécifié")
                        {
                            GestionBDD.updateSexe(cmbSexe.Text, textboxId.Text);
                        }
                        MessageBox.Show("Inscription enregistrée", "Confirmation", MessageBoxButton.OK);
                        this.Close();
                    }
                }
            }
            else
            {
                MessageBox.Show("Veuillez rentrer le même mot de passe", "Mot de passe incorrect", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

            if(textboxId.Text == "" || passwordBox1.Password == "" || passwordBox2.Password == "")
            {
                MessageBox.Show("Tout les champs doivent être remplis", "Champs incomplet" ,MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }*/
    }
}
