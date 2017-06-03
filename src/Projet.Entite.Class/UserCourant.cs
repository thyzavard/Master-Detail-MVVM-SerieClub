using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Projet.Entite.Class
{
    public class UserCourant
    {
        private static UserCourant instance = null;

        private static readonly object myLock = new object();

        public string Pseudo { get; set; }
        public string Password { get; set; }
        public string Description { get; set; }
        public String Sexe { get; set; }
        public string DateDeNaissance { get; set; }
        public List<Serie> Serieadd { get; set; }
        public bool Modo { get; set; }
        public BitmapImage image { get; set; }

        public static void Connect(string pseudo, string mdp, string desc, string sexe, string ddn, bool modo)
        {
            lock (myLock)
            {
                if (instance != null) throw new InvalidOperationException();
                instance = new UserCourant(pseudo, mdp, desc, sexe, ddn, modo);
            }
        }

        private UserCourant(string pseudo, string mdp, string desc, string sexe, string ddn, bool modo)
        {
            Pseudo = pseudo;
            Password = mdp;
            Description = desc;
            Sexe = sexe;
            DateDeNaissance = ddn;
            Modo = modo;
            Serieadd = new List<Serie>();
        }

        public static UserCourant Instance()
        {
            return instance;
        }

        public static void SetNull()
        {
            instance = null;
        }
    }
}
