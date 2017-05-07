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
           SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Thomas\Desktop\Programming software\C#\PROJECT\Projet_BDD\2015\Serie_club.mdf;Integrated Security=True;Connect Timeout=30");
            Utilisateur user = new Utilisateur();
            con.Open();
            user.pseudo = pseudo;
            //*****Récupération de la description de l'utilisateur courant*****
            SqlCommand cmddesc = new SqlCommand("Select Description From Utilisateur where Pseudo='" + pseudo + "' ", con);
            SqlDataReader dr = cmddesc.ExecuteReader();
            dr.Read();
            user.description = dr.GetString(0);
            dr.Close();
            //*****Récupération du sexe de l'utilisateur*****
            SqlCommand cmdsexe = new SqlCommand("Select Sexe From Utilisateur where Pseudo='" + pseudo + "' ", con);
            dr = cmdsexe.ExecuteReader();
            dr.Read();
            user.sexe = dr.GetString(0);
            dr.Close();
            //*****Récupération de la date de naissance de l'utilisateur*****
            SqlCommand cmdddn = new SqlCommand("Select DateDeNaissance From Utilisateur where Pseudo='" + pseudo + "' ", con);
            dr = cmdddn.ExecuteReader();
            dr.Read();
            user.dateDeNaissance = dr.GetString(0);
            dr.Close();
            //*****Récupération de l'autoristation modérateur*****
            SqlCommand cmdmodo = new SqlCommand("Select Modo From Utilisateur where Pseudo='" + pseudo + "'", con);
            dr = cmdmodo.ExecuteReader();
            dr.Read();
            var recup = dr.GetString(0);
            user.modo = Convert.ToBoolean(recup);
            dr.Close();
            con.Close();

            return user;
        }

        public static Boolean verifLoginMdp(String pseudo, String mdp)
        {
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Thomas\Desktop\Programming software\C#\PROJECT\Projet_BDD\2015\Serie_club.mdf;Integrated Security=True;Connect Timeout=30");
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
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Thomas\Desktop\Programming software\C#\PROJECT\Projet_BDD\2015\Serie_club.mdf;Integrated Security=True;Connect Timeout=30");
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
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Thomas\Desktop\Programming software\C#\PROJECT\Projet_BDD\2015\Serie_club.mdf;Integrated Security=True;Connect Timeout=30");
            SqlDataAdapter sda2 = new SqlDataAdapter("Insert into Utilisateur (Pseudo, Password, Description, Sexe, DateDeNaissance, Modo) values('" +pseudo+ "', '" +mdp+ "' , '" + "Description..." + "', '" + "Pas spécifié..." + "', '" + "00/00/0000" + "', '"+"false"+"')", con);
            DataTable dt2 = new DataTable();
            sda2.Fill(dt2);
        }

        public static void updateDescription(String desc, String pseudo)
        {
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Thomas\Desktop\Programming software\C#\PROJECT\Projet_BDD\2015\Serie_club.mdf;Integrated Security=True;Connect Timeout=30");
            SqlDataAdapter sda = new SqlDataAdapter("Update Utilisateur set Description =' " +desc+ "' where Pseudo='" + pseudo + "'", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
        }

        public static void updateDdn(String ddn, String pseudo)
        {
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Thomas\Desktop\Programming software\C#\PROJECT\Projet_BDD\2015\Serie_club.mdf;Integrated Security=True;Connect Timeout=30");
            SqlDataAdapter sdaddn = new SqlDataAdapter("Update Utilisateur set DateDeNaissance ='" +ddn+ "'  where Pseudo='" + pseudo + "'", con);
            DataTable dtddn = new DataTable();
            sdaddn.Fill(dtddn);
        }

        public static void updateSexe(String sexe, String pseudo)
        {
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Thomas\Desktop\Programming software\C#\PROJECT\Projet_BDD\2015\Serie_club.mdf;Integrated Security=True;Connect Timeout=30");
            SqlDataAdapter sdasexe = new SqlDataAdapter("Update Utilisateur set Sexe ='" +sexe+ "' where Pseudo='" + pseudo + "'", con);
            DataTable dtsexe = new DataTable();
            sdasexe.Fill(dtsexe);
        }

        public static Serie remplirSerieAction()
        {
            Serie s = new Serie();
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Thomas\Desktop\Programming software\C#\PROJECT\Projet_BDD\2015\Serie_club.mdf;Integrated Security=True;Connect Timeout=30");
            SqlCommand cmddesc = new SqlCommand("Select Nom From Utilisateur where Genre='" +"Action"+ "' ", con);
            SqlDataReader dr = cmddesc.ExecuteReader();
            dr.Read();
            string nom_serie = dr.GetString(0);
            s.nom = nom_serie;
            dr.Close();

            return s;
        }

       public static void ajouter_Serie(string nom, string desc, string genre, string producteur, int dureemoy)
        {
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Thomas\Desktop\Programming software\C#\PROJECT\Projet_BDD\2015\Serie_club.mdf;Integrated Security=True;Connect Timeout=30");
            SqlDataAdapter sda = new SqlDataAdapter("Insert into Serie (Nom, Description, Genre, Note, Producteur, DureeMoyenne) values('"+nom+"', '"+desc+"', '"+genre+"','"+0+"','"+producteur+"','"+dureemoy+"') ", con);
            DataTable dtserie = new DataTable();
            sda.Fill(dtserie);
        }

        public static Boolean verifSerie(string nom)
        {
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Thomas\Desktop\Programming software\C#\PROJECT\Projet_BDD\2015\Serie_club.mdf;Integrated Security=True;Connect Timeout=30");
            SqlDataAdapter sda = new SqlDataAdapter("Select Count(*) From Serie where Nom='" +nom+ "'", con);
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

        public static List<string> returnTouteSerie()
        {
            List<string> list = new List<string>();
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Thomas\Desktop\Programming software\C#\PROJECT\Projet_BDD\2015\Serie_club.mdf;Integrated Security=True;Connect Timeout=30");
            con.Open();
            var command = new SqlCommand("Select Nom from Serie", con);
            using(var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    list.Add(reader.GetString(0));
                }
            }
            con.Close();
            return list;
        }

        public static List<string> returnToutUtilisateur()
        {
            List<string> list = new List<string>();
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Thomas\Desktop\Programming software\C#\PROJECT\Projet_BDD\2015\Serie_club.mdf;Integrated Security=True;Connect Timeout=30");
            con.Open();
            var command = new SqlCommand("Select Pseudo from Utilisateur", con);
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    list.Add(reader.GetString(0));
                }
            }
            con.Close();
            return list;

        }

        public static void supprSerie(string nom)
        {
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Thomas\Desktop\Programming software\C#\PROJECT\Projet_BDD\2015\Serie_club.mdf;Integrated Security=True;Connect Timeout=30");
            con.Open();
            SqlCommand cmd = new SqlCommand("Delete from Serie where Nom='" +nom+ "' ", con);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public static Serie remplirSerie(string nom)
        {
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Thomas\Desktop\Programming software\C#\PROJECT\Projet_BDD\2015\Serie_club.mdf;Integrated Security=True;Connect Timeout=30");
            Serie serie = new Serie();
            con.Open();
            serie.nom = nom;
            //Description
            SqlCommand cmddesc = new SqlCommand("Select Description from Serie where Nom='" + nom + "'", con);
            SqlDataReader dr = cmddesc.ExecuteReader();
            dr.Read();
            serie.description = dr.GetString(0);
            dr.Close();
            //Producteur
            SqlCommand cmdprod = new SqlCommand("Select Producteur from Serie where Nom='" + nom + "'", con);
            dr = cmdprod.ExecuteReader();
            dr.Read();
            serie.producteur = dr.GetString(0);
            dr.Close();
            //Durée Moyenne
            SqlCommand cmddure = new SqlCommand("Select DureeMoyenne from Serie where Nom='"+nom+"'", con);
            dr = cmddure.ExecuteReader();
            dr.Read();
            serie.dureeMoy = dr.GetInt32(0);
            dr.Close();
            //Genre
            SqlCommand cmdgenre = new SqlCommand("Select Genre from Serie where Nom='" + nom + "'", con);
            dr = cmdgenre.ExecuteReader();
            dr.Read();
            var recup = dr.GetString(0);
            Genre genre = (Genre)Enum.Parse(typeof(Genre), recup);
            serie.genre = genre;
            dr.Close();
            con.Close();
            return serie;
        }

        public static void updateSerie(string nom, string desc, int dureMoy, string producteur, string genre)
        {
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Thomas\Desktop\Programming software\C#\PROJECT\Projet_BDD\2015\Serie_club.mdf;Integrated Security=True;Connect Timeout=30");
            SqlDataAdapter sdaSerie = new SqlDataAdapter("Update Serie set Description ='" + desc + "', Producteur ='"+producteur+"', Genre ='"+genre+"', DureeMoyenne ='"+dureMoy+"' where Nom='"+nom+"'", con);
            DataTable dt = new DataTable();
            sdaSerie.Fill(dt);
        }

        public static bool upModo(string pseudo)
        {
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Thomas\Desktop\Programming software\C#\PROJECT\Projet_BDD\2015\Serie_club.mdf;Integrated Security=True;Connect Timeout=30");
            con.Open();
            SqlCommand cmd = new SqlCommand("Select Modo from Utilisateur where Pseudo='" + pseudo + "'", con);
            SqlDataReader dr = cmd.ExecuteReader();
            dr.Read();
            string recup = dr.GetString(0);
            dr.Close();
            bool verif = Convert.ToBoolean(recup);
            if (verif)
            {
                con.Close();
                return false;
            }
            else
            {
                SqlDataAdapter sda = new SqlDataAdapter("Update Utilisateur set Modo='" + "true" + "' where Pseudo='" + pseudo + "'", con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                con.Close();
                return true;
            } 
        }

        public static bool downModo(string pseudo)
        {
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Thomas\Desktop\Programming software\C#\PROJECT\Projet_BDD\2015\Serie_club.mdf;Integrated Security=True;Connect Timeout=30");
            con.Open();
            SqlCommand cmd = new SqlCommand("Select Modo from Utilisateur where Pseudo='" + pseudo + "'", con);
            SqlDataReader dr = cmd.ExecuteReader();
            dr.Read();
            string recup = dr.GetString(0);
            dr.Close();
            bool verif = Convert.ToBoolean(recup);
            if (!verif)
            {
                con.Close();
                return false;
            }
            else
            {
                SqlDataAdapter sda = new SqlDataAdapter("Update Utilisateur set Modo='" + "false" + "' where Pseudo='" + pseudo + "'", con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                con.Close();
                return true;
            }
        }
        
    }
}