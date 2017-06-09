using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Projet.Entite.Class
{
    public class Serie
    {
        public string nom { get; set; }
        public string description { get; set; }
        public Genre genre { get; set; }
        public float note { get; set; }
        public List<Commentaire> commentaire { get; set; }
        public string producteur { get; set; }
        public int dureeMoy { get; set; }
        public int nbSaison { get; set; }
        public BitmapImage ImageSerie { get; set; }
        public BitmapImage Banniereserie { get; set; }
        public int nbPersonVote { get; set; }

        public Serie(string nom, string description, Genre genre, int dureeMoy, string producteur, int nbSaison)
        {
            this.nom = nom;
            this.description = description;
            this.genre = genre;
            this.dureeMoy = dureeMoy;
            note = 0;
            this.producteur = producteur;
            this.nbSaison = nbSaison;
            commentaire = new List<Commentaire>();
        }

        public Serie()
        {
            commentaire = new List<Commentaire>();
        }

        public Serie(string nom)
        {
            this.nom = nom;
        }
    }
}
