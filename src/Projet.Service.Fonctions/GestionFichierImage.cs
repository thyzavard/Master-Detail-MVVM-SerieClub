using System;
using System.Collections.Generic;
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
    }
}

