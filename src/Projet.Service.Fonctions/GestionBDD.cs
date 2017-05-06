using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Projet.Entite.Class;
using System.Data.SqlClient;
using System.Data;

namespace Projet.Service.Fonctions
{
    public class GestionBDD
    {
        public static Utilisateur remplirUser (String pseudo)
        {
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=U:\1ère_année\C#\Projet_BDD\Projet\Persistance\SerieClub.mdf;Integrated Security=True");
            Utilisateur user = new Utilisateur();

            con.Open();
            user.pseudo = pseudo;
            //*****Récupération de la description de l'utilisateur courant*****
            SqlCommand cmddesc = new SqlCommand("Select Description From Utilisateur where Pseudo='" + pseudo + "' ", con);
            SqlDataReader dr = cmddesc.ExecuteReader();
            dr.Read();
            string contenu_description = dr.GetString(0);
            user.description = contenu_description;
            dr.Close();
            //*****Récupération du sexe de l'utilisateur*****
            SqlCommand cmdsexe = new SqlCommand("Select Sexe From Utilisateur where Pseudo='" + pseudo + "' ", con);
            SqlDataReader drsexe = cmdsexe.ExecuteReader();
            drsexe.Read();
            string contenu_sexe = drsexe.GetString(0);
            user.sexe = contenu_sexe;
            drsexe.Close();
            //*****Récupération de la date de naissance de l'utilisateur*****
            SqlCommand cmdddn = new SqlCommand("Select DateDeNaissance From Utilisateur where Pseudo='" + pseudo + "' ", con);
            SqlDataReader drddn = cmdddn.ExecuteReader();
            drddn.Read();
            string contenu_ddn = drddn.GetString(0);
            user.dateDeNaissance = contenu_ddn;
            drddn.Close();

            con.Close();

            return user;
        }

        public static Boolean verifLoginMdp(String pseudo, String mdp)
        {
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=U:\1ère_année\C#\Projet_BDD\Projet\Persistance\SerieClub.mdf;Integrated Security=True");
            SqlDataAdapter sda = new SqlDataAdapter("Select Count(*) From Utilisateur where Pseudo='" + pseudo + "' AND Password='" + mdp + "'", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);

            if (dt.Rows[0][0].ToString() == "1")
            {
                return true;
            }
            else
            {
                return false;
            }   
        }

        public static Boolean verifLogin(String pseudo)
        {
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=U:\1ère_année\C#\Projet_BDD\Projet\Persistance\SerieClub.mdf;Integrated Security=True");
            SqlDataAdapter sda = new SqlDataAdapter("Select Count(*) From Utilisateur where Pseudo='" + pseudo + "'", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);

            if (dt.Rows[0][0].ToString() == "1")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static void inscription(String pseudo, String mdp)
        {
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=U:\1ère_année\C#\Projet_BDD\Projet\Persistance\SerieClub.mdf;Integrated Security=True");
            SqlDataAdapter sda2 = new SqlDataAdapter("Insert into Utilisateur (Pseudo, Password, Description, Sexe, DateDeNaissance) values('" +pseudo+ "', '" +mdp+ "' , '" + "Description..." + "', '" + "Pas spécifié..." + "', '" + "00/00/0000" + "')", con);
            DataTable dt2 = new DataTable();
            sda2.Fill(dt2);
        }

        public static void updateDescription(String desc, String pseudo)
        {
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=U:\1ère_année\C#\Projet_BDD\Projet\Persistance\SerieClub.mdf;Integrated Security=True");
            SqlDataAdapter sda = new SqlDataAdapter("Update Utilisateur set Description =' " +desc+ "' where Pseudo='" + pseudo + "'", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
        }

        public static void updateDdn(String ddn, String pseudo)
        {
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=U:\1ère_année\C#\Projet_BDD\Projet\Persistance\SerieClub.mdf;Integrated Security=True");
            SqlDataAdapter sdaddn = new SqlDataAdapter("Update Utilisateur set DateDeNaissance ='" +ddn+ "'  where Pseudo='" + pseudo + "'", con);
            DataTable dtddn = new DataTable();
            sdaddn.Fill(dtddn);
        }

        public static void updateSexe(String sexe, String pseudo)
        {
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=U:\1ère_année\C#\Projet_BDD\Projet\Persistance\SerieClub.mdf;Integrated Security=True");
            SqlDataAdapter sdasexe = new SqlDataAdapter("Update Utilisateur set Sexe ='" +sexe+ "' where Pseudo='" + pseudo + "'", con);
            DataTable dtsexe = new DataTable();
            sdasexe.Fill(dtsexe);
        }

        public static Serie remplirSerieAction()
        {
            Serie s = new Serie();
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=U:\1ère_année\C#\Projet_BDD\Projet\Persistance\SerieClub.mdf;Integrated Security=True");
            SqlCommand cmddesc = new SqlCommand("Select Nom From Utilisateur where Genre='" +"Action"+ "' ", con);
            SqlDataReader dr = cmddesc.ExecuteReader();
            dr.Read();
            string nom_serie = dr.GetString(0);
            s.nom = nom_serie;
            dr.Close();

            return s;
        }
    }
}
