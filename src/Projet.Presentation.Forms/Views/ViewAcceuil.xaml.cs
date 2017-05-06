using Projet.Entite.Class;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Projet.Presentation.Forms
{
    /// <summary>
    /// Logique d'interaction pour ViewAcceuil.xaml
    /// </summary>
    public partial class ViewAcceuil : UserControl
    {
        public List<Serie> listserie
        {
            get;
            set;
        }
        public ViewAcceuil()
        {
            InitializeComponent();

            listserie = new List<Serie>();

            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=U:\1ère_année\C#\Projet_BDD\Projet\Persistance\SerieClub.mdf;Integrated Security=True");
            con.Open();
            SqlCommand cmd = new SqlCommand("Select count(*) from Serie where Genre ='" +"Fantastique"+ "' ", con);
            SqlDataReader dr = cmd.ExecuteReader();
            dr.Read();
            int nb = dr.GetInt32(0);
            dr.Close();

            for (int i = 0; i < nb; i++)
            {
                SqlCommand cmdnom = new SqlCommand("Select Nom from Serie where  Genre ='" + "Fantastique" + "' ", con);
                SqlDataReader drnom = cmdnom.ExecuteReader();
                drnom.Read();
                Serie s = new Serie();
                s.nom = drnom.GetString(0);
                drnom.Close();
                listserie.Add(s);
            }




            Serie serie = new Serie();
            serie.nom = "TEST";
            serie.note = nb;

            Serie serie2 = new Serie();
            serie2.nom = "TEST2";
            serie2.note = 5;

            Serie serie3 = new Serie();
            serie3.nom = "TEST2";
            serie3.note = 5;

            Serie serie4 = new Serie();
            serie4.nom = "TEST2";
            serie4.note = 5;

            Serie serie5 = new Serie();
            serie5.nom = "TEST2";
            serie5.note = 5;

            Serie serie6 = new Serie();
            serie6.nom = "TEST2";
            serie6.note = 5;

            Serie serie7 = new Serie();
            serie7.nom = "TEST2";
            serie7.note = 5;

            Serie serie8 = new Serie();
            serie8.nom = "TEST2";
            serie8.note = 5;


            listserie.Add(serie);
            listserie.Add(serie2);
            listserie.Add(serie3);
            listserie.Add(serie4);
            listserie.Add(serie5);
            listserie.Add(serie6);
            listserie.Add(serie7);
            listserie.Add(serie8);
        }
    }
}
