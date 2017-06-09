using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Projet.Service.Fonctions
{
    public class GestionFichierImage
    {
        /// <summary>
        /// Cette fonction a pour but de supprimer toutes les images non utilisé, pour cela on va stocker dans une liste tous les noms d'images stocké dans la BDD et tous les fichiers présents dans le dossier, on compare les deux listes et si des fichiers qui sont dans le dossier mais pas dans la BDD il est ajouté dans une troisième liste qui supprimera tous son contenu
        /// </summary>
        public static void triImageUser()
        {
            string path = Path.Combine(Environment.CurrentDirectory, "Images");

            List<string> fichierBDD = GestionBDD.returnImageUser();
            List<string> fichierASuppr = new List<string>();
            List<string> fichierImage = new List<string>();
            string[] fichierSource = Directory.GetFiles(path);

            for(int i = 0; i < fichierSource.Length; i++)
            {
                fichierImage.Add(fichierSource[i]);
            }

            for (int y = 0; y < fichierImage.Count; y++)
            {
                if (!fichierBDD.Contains(fichierImage[y]))
                {
                    fichierASuppr.Add(fichierImage[y]);
                }
            }
            
            for (int o = 0; o < fichierASuppr.Count; o++)
            {
                File.Delete(Path.Combine(path, fichierASuppr[o]));
            }
        }
        /// <summary>
        /// Même type de fonction que "triImageUser" sauf que celle ci gère les images lié au séries
        /// </summary>
        public static void triImageSerie()
        {
            string path = Path.Combine(Environment.CurrentDirectory, "ImagesSerie");

            List<string> fichierBDD = GestionBDD.returnImageSerie();
            List<string> fichierASuppr = new List<string>();
            List<string> fichierImage = new List<string>();
            string[] fichierSource = Directory.GetFiles(path);

            for(int i = 0; i < fichierSource.Length; i++)
            {
                fichierImage.Add(fichierSource[i]);
            }

            for (int y = 0; y < fichierImage.Count; y++)
            {
                if (!fichierBDD.Contains(fichierImage[y]))
                {
                    fichierASuppr.Add(fichierImage[y]);
                }
            }

            for (int o = 0; o < fichierASuppr.Count; o++)
            {
                File.Delete(Path.Combine(path, fichierASuppr[o]));
            }
        }

        /// <summary>
        /// Cette fonction permet de vérifier si les dossiers "Images" et "ImagesSerie" existent, si non, ils vont être crée et rempli avec des images de bases de l'application (images de profil et couverture de base...)
        /// </summary>
        /// <returns></returns>
        public static bool creerFichierImages()
        {
            bool retour = false;
            string pathUser = Path.Combine(Environment.CurrentDirectory, "Images");
            string pathSerie = Path.Combine(Environment.CurrentDirectory, "ImagesSerie");

            
            try
            {
                if (!Directory.Exists(pathUser))
                {
                    Directory.CreateDirectory(pathUser);
                    File.Copy($@"{ConfigurationManager.AppSettings["ImagesPath"]}\profil.jpg", Path.Combine(pathUser, "profil.jpg"));
                    File.Copy($@"{ConfigurationManager.AppSettings["ImagesPath"]}\couverture.jpg", Path.Combine(pathUser, "couverture.jpg"));
                    retour = true;
                }
                else { retour = false; }

                if (!Directory.Exists(pathSerie))
                {
                    Directory.CreateDirectory(pathSerie);
                    retour = true;
                }
                else { retour = false; }
            }
            catch
            {
                MessageBox.Show("Veuillez modifier le chemin d'accès au images dans le fichier app.config et supprimer le dossier 'Images' dans le dossier Debug de l'application.");
            }

            return retour;
        }
    }
}

