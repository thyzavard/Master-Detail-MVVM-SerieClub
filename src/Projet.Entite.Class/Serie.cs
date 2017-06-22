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
        public int nbEpisode { get; set; }
        public List<Episode> listeEpisode { get; set; }

        public Serie(string nom, string description, Genre genre, int dureeMoy, string producteur, int nbSaison, string pathImage, string pathBanniere, int nbEpisode)
        {
            this.nom = nom;
            this.description = description;
            this.genre = genre;
            this.dureeMoy = dureeMoy;
            note = 0;
            this.producteur = producteur;
            this.nbSaison = nbSaison;
            commentaire = new List<Commentaire>();
            ImageSerie = new BitmapImage(new Uri($"{AppDomain.CurrentDomain.BaseDirectory}/ImagesSerie/{pathImage}"));
            Banniereserie = new BitmapImage(new Uri($"{AppDomain.CurrentDomain.BaseDirectory}/ImagesSerie/{pathBanniere}"));
            listeEpisode = new List<Episode>();
            this.nbEpisode = nbEpisode;
        }

        public Serie()
        {
            commentaire = new List<Commentaire>();
            listeEpisode = new List<Episode>();
        }

        public Serie(string nom)
        {
            this.nom = nom;
        }
    }
}
