using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet.Entite.Class
{
    public class Utilisateur
    {
        public string pseudo { get; set; }
        public string password { get; set; }
        public string description { get; set; }
        public String sexe { get; set; }
        public string dateDeNaissance { get; set; }
        public List<Serie> serieadd { get; set; }

        public Utilisateur(string pseudo, string password)
        {
            this.pseudo = pseudo;
            this.password = password;
            serieadd = new List<Serie>();
        }

        public Utilisateur()
        {
        }
    }
}
