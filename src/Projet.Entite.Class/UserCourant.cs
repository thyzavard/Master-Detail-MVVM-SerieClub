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
        public string Description { get; set; }
        public String Sexe { get; set; }
        public DateTime DateDeNaissance { get; set; }
        public List<Serie> Serieadd { get; set; }
        public bool Modo { get; set; }
        public BitmapImage image { get; set; }
        public BitmapImage couverture { get; set; }

        public static void Connect(string pseudo, string desc, string sexe, DateTime ddn, bool modo, BitmapImage profil, BitmapImage couverture)
        {
            lock (myLock)
            {
                if (instance != null) throw new InvalidOperationException();
                instance = new UserCourant(pseudo, desc, sexe, ddn, modo, profil, couverture);
            }
        }

        private UserCourant(string pseudo, string desc, string sexe, DateTime ddn, bool modo, BitmapImage profil, BitmapImage couverture)
        {
            Pseudo = pseudo;
            Description = desc;
            Sexe = sexe;
            DateDeNaissance = ddn;
            Modo = modo;
            Serieadd = new List<Serie>();
            image = profil;
            this.couverture = couverture;
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
