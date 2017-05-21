using Projet.Entite.Class;
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
    /// Logique d'interaction pour WindowAdd.xaml
    /// </summary>
    public partial class WindowAdd : Window
    {
        private WindowAddViewModel _vm;
        public WindowAdd()
        {
            InitializeComponent();
            _vm = new WindowAddViewModel();
            DataContext = _vm;

           /* List<string> listSerie = GestionBDD.returnTouteSerie();
            for(int i = 0; i < listSerie.Count; i++)
            {
                cmbserie.Items.Add(listSerie[i]);
                cmbsuppr.Items.Add(listSerie[i]);
            }*/

            List<string> listPseudo = GestionBDD.returnToutUtilisateur();
            for(int i = 0; i < listPseudo.Count; i++)
            {
                cmb_pseudo.Items.Add(listPseudo[i]);
            }
            
          /*  textboxmodif_DescSerie.Visibility = Visibility.Hidden;
            textboxmodif_dureep.Visibility = Visibility.Hidden;
            //cmb_genremodif.Visibility = Visibility.Hidden;
            textboxmodif_Producteur.Visibility = Visibility.Hidden;
            imgmodif.Visibility = Visibility.Hidden;
            btnmodif_parcourir.Visibility = Visibility.Hidden;
            l_duremoy.Visibility = Visibility.Hidden;
            l_genre.Visibility = Visibility.Hidden;
            l_img.Visibility = Visibility.Hidden;
            l_prod.Visibility = Visibility.Hidden;*/

            //l_desc.Content = "Aucune série sélectionnée";


           /* cmb_genre.Items.Add("Action");
            cmb_genre.Items.Add("Comedie");
            cmb_genre.Items.Add("Drame");
            cmb_genre.Items.Add("Fantastique");
            cmb_genre.Items.Add("Horreur");

            cmb_genremodif.Items.Add("Action");
            cmb_genremodif.Items.Add("Comédie");
            cmb_genremodif.Items.Add("Drame");
            cmb_genremodif.Items.Add("Fantastique");
            cmb_genremodif.Items.Add("Horreur");*/
        }

        private void Retour_Click(object sender, RoutedEventArgs e)
        {
            Close();        
        }

        /*private void AddSerie_Click(object sender, RoutedEventArgs e)
        {
            if (textbox_NomSerie.Text == "" || textbox_Producteur.Text == "" || cmb_genre.Text == "" || textbox_DescSerie.Text == "" || textbox_dureep.Text == "")
            {
                MessageBox.Show("Tout les champs doivent être remplis", "Champs incomplet", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            if (GestionBDD.verifSerie(textbox_NomSerie.Text))
            {
                MessageBox.Show("Cette série est déjà enregistrée", "Erreur", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                GestionBDD.ajouter_Serie(textbox_NomSerie.Text, textbox_DescSerie.Text, cmb_genre.Text, textbox_Producteur.Text, int.Parse(textbox_dureep.Text));
                MessageBox.Show("Inscription enregistrée", "Confirmation", MessageBoxButton.OK);
                textbox_DescSerie.Text = "";
                textbox_dureep.Text = "";
                textbox_NomSerie.Text = "";
                textbox_Producteur.Text = "";
            }
        }*/

        private void Supprimer_Click(object sender, RoutedEventArgs e)
        {
            if(cmbsuppr.Text == "")
            {
                MessageBox.Show("Veuillez choisir une série à supprimer","Erreur",MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                if(MessageBox.Show("Voulez vous vraiment supprimer la série : " + cmbsuppr.Text + " ?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    GestionBDD.supprSerie(cmbsuppr.Text);
                    MessageBox.Show("Série supprimé");
                }
            }
        }

        private void ok_btn_Click(object sender, RoutedEventArgs e)
        {
            /* if(cmbserie.Text == "")
             {
                 MessageBox.Show("Veuillez choisir une série à modifier");
             }
             else
             {
                 Serie seriemodif = GestionBDD.remplirSerie(cmbserie.Text);
                 textboxmodif_DescSerie.Visibility = Visibility.Visible;
                 textboxmodif_dureep.Visibility = Visibility.Visible;
                 cmb_genremodif.Visibility = Visibility.Visible;
                 textboxmodif_Producteur.Visibility = Visibility.Visible;
                 imgmodif.Visibility = Visibility.Visible;
                 btnmodif_parcourir.Visibility = Visibility.Visible;
                 l_desc.Content = "Description";
                 l_duremoy.Visibility = Visibility.Visible;
                 l_genre.Visibility = Visibility.Visible;
                 l_img.Visibility = Visibility.Visible;
                 l_prod.Visibility = Visibility.Visible;

                 textboxmodif_DescSerie.Text = seriemodif.description;
                 textboxmodif_dureep.Text = seriemodif.dureeMoy.ToString();
                 cmb_genremodif.Text = seriemodif.genre.ToString();
                 textboxmodif_Producteur.Text = seriemodif.producteur;
             }*/
         }

            private void Modif_Click(object sender, RoutedEventArgs e)
        {
            /*if (textboxmodif_Producteur.Text == "" || cmb_genremodif.Text == "" || textboxmodif_DescSerie.Text == "" || textboxmodif_dureep.Text == "")
            {
                MessageBox.Show("Tout les champs doivent être remplis", "Champs incomplet", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                GestionBDD.updateSerie(cmbserie.Text, textboxmodif_DescSerie.Text, int.Parse(textboxmodif_dureep.Text), textboxmodif_Producteur.Text, cmb_genremodif.Text);
                MessageBox.Show("Modification enrgistrées");
                textboxmodif_DescSerie.Visibility = Visibility.Hidden;
                textboxmodif_dureep.Visibility = Visibility.Hidden;
                cmb_genremodif.Visibility = Visibility.Hidden;
                textboxmodif_Producteur.Visibility = Visibility.Hidden;
                imgmodif.Visibility = Visibility.Hidden;
                btnmodif_parcourir.Visibility = Visibility.Hidden;
                btnmodif_parcourir.Visibility = Visibility.Hidden;
                l_duremoy.Visibility = Visibility.Hidden;
                l_genre.Visibility = Visibility.Hidden;
                l_img.Visibility = Visibility.Hidden;
                l_prod.Visibility = Visibility.Hidden;
                cmbserie.Text = "";
                l_desc.Content = "Aucune série sélectionnée";
            }*/
        }

        private void up_Click(object sender, RoutedEventArgs e)
        {
            if(cmb_pseudo.Text == "")
            {
                MessageBox.Show("Veuillez choisir un utilisateur à promouvoir/désister au grade de modérateur","Attention", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                if(MessageBox.Show("Voulez vous promouvoir "+cmb_pseudo.Text+" au poste de modérateur ?","Confirmation",MessageBoxButton.YesNo,MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    if (GestionBDD.upModo(cmb_pseudo.Text))
                    {
                        MessageBox.Show("Modérateur ajouté");
                    }
                    else
                    {
                        MessageBox.Show("Cet utilisateur est déjà modérateur", "Erreur", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
            }
        }

        private void down_Click(object sender, RoutedEventArgs e)
        {
            if (cmb_pseudo.Text == "")
            {
                MessageBox.Show("Veuillez choisir un utilisateur à promouvoir/désister au grade de modérateur", "Attention", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                if (MessageBox.Show("Voulez vous désister " + cmb_pseudo.Text + " du poste de modérateur ?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    if (GestionBDD.downModo(cmb_pseudo.Text))
                    {
                        MessageBox.Show("Modérateur supprimé");
                    }
                    else
                    {
                        MessageBox.Show("Cet utilisateur n'est pas modérateur", "Erreur", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }


                }
            }
        }
    }
}

