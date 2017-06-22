using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet.Entite.Class
{
    public class Episode
    {
        public int numeroEp { get; set; }
        public string nomEp { get; set; }
        public string saison { get; set; }

        public Episode(int num, string nom, string saison)
        {
            numeroEp = num;
            nomEp = nom;
            this.saison = saison;
        }
    }
}
