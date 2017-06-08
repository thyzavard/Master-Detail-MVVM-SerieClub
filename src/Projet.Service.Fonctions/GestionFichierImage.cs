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

        public static bool creerFichierImages()
        {
            bool retour = false;
            string pathUser = Path.Combine(Environment.CurrentDirectory, "Images");
            string pathSerie = Path.Combine(Environment.CurrentDirectory, "ImagesSerie");

            

            if (!Directory.Exists(pathUser))
            {
                Directory.CreateDirectory(pathUser);
                File.Copy($@"{ConfigurationManager.AppSettings["ImagesPath"]}\profil.jpg", Path.Combine(pathUser, "profil.jpg"));
                File.Copy($@"{ConfigurationManager.AppSettings["ImagesPath"]}\couverture.jpg" , Path.Combine(pathUser, "couverture.jpg"));
                retour = true;
            }
            else { retour = false; }

            if (!Directory.Exists(pathSerie))
            {
                Directory.CreateDirectory(pathSerie);
                retour = true;
            }
            else { retour = false; }

            return retour;
        }
    }
}

