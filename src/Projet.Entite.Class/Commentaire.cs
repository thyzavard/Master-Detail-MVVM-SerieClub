using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet.Entite.Class
{
    public class Commentaire
    {
        public string commentaire { get; set; }
        public string nomUtilisateur { get; set; }

        public Commentaire(string com, string nomUser)
        {
            commentaire = com;
            nomUtilisateur = nomUser;
        }
    }
}
