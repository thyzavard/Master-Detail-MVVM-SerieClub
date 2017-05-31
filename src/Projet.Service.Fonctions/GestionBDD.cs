﻿using System;
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
        private static SqlConnection log = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Utilisateur\Source\Repos\Projet\serie-club\src\Persistance\Serie-Club.mdf;Integrated Security=True;Connect Timeout=30");
        public static Utilisateur remplirUser (String pseudo)
        {
            Utilisateur user = new Utilisateur();
            log.Open();
            user.pseudo = pseudo;
            //*****Récupération de la description de l'utilisateur courant*****
            SqlCommand cmddesc = new SqlCommand("Select Description From Utilisateur where Pseudo='" + pseudo + "' ", log);
            SqlDataReader dr = cmddesc.ExecuteReader();
            dr.Read();
            user.description = dr.GetString(0);
            dr.Close();
            //*****Récupération du sexe de l'utilisateur*****
            SqlCommand cmdsexe = new SqlCommand("Select Sexe From Utilisateur where Pseudo='" + pseudo + "' ", log);
            dr = cmdsexe.ExecuteReader();
            dr.Read();
            user.sexe = dr.GetString(0);
            dr.Close();
            //*****Récupération de la date de naissance de l'utilisateur*****
            SqlCommand cmdddn = new SqlCommand("Select DateDeNaissance From Utilisateur where Pseudo='" + pseudo + "' ", log);
            dr = cmdddn.ExecuteReader();
            dr.Read();
            user.dateDeNaissance = dr.GetString(0);
            dr.Close();
            //*****Récupération de l'autoristation modérateur*****
            SqlCommand cmdmodo = new SqlCommand("Select Modo From Utilisateur where Pseudo='" + pseudo + "'", log);
            dr = cmdmodo.ExecuteReader();
            dr.Read();
            var recup = dr.GetString(0);
            user.modo = Convert.ToBoolean(recup);
            dr.Close();
            log.Close();

            return user;
        }

        public static Boolean verifLoginMdp(String pseudo, String mdp)
        {
            SqlDataAdapter sda = new SqlDataAdapter("Select Count(*) From Utilisateur where Pseudo='" + pseudo + "' AND Password='" + mdp + "'", log);
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
            SqlDataAdapter sda = new SqlDataAdapter("Select Count(*) From Utilisateur where Pseudo='" + pseudo + "'", log);
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
            SqlDataAdapter sda2 = new SqlDataAdapter("Insert into Utilisateur (Pseudo, Password, Description, Sexe, DateDeNaissance, Modo) values('" +pseudo+ "', '" +mdp+ "' , '" + "Description..." + "', '" + "Pas spécifié..." + "', '" + "00/00/0000" + "', '"+"false"+"')", log);
            DataTable dt2 = new DataTable();
            sda2.Fill(dt2);
        }

        public static void updateDescription(String desc, String pseudo)
        {
            SqlDataAdapter sda = new SqlDataAdapter("Update Utilisateur set Description =' " +desc+ "' where Pseudo='" + pseudo + "'", log);
            DataTable dt = new DataTable();
            sda.Fill(dt);
        }

        public static void updateDdn(String ddn, String pseudo)
        {
            SqlDataAdapter sdaddn = new SqlDataAdapter("Update Utilisateur set DateDeNaissance ='" +ddn+ "'  where Pseudo='" + pseudo + "'", log);
            DataTable dtddn = new DataTable();
            sdaddn.Fill(dtddn);
        }

        public static void updateSexe(String sexe, String pseudo)
        {
            SqlDataAdapter sdasexe = new SqlDataAdapter("Update Utilisateur set Sexe ='" +sexe+ "' where Pseudo='" + pseudo + "'", log);
            DataTable dtsexe = new DataTable();
            sdasexe.Fill(dtsexe);
        }

        public static Serie remplirSerieAction()
        {
            Serie s = new Serie();
            SqlCommand cmddesc = new SqlCommand("Select Nom From Utilisateur where Genre='" +"Action"+ "' ", log);
            SqlDataReader dr = cmddesc.ExecuteReader();
            dr.Read();
            string nom_serie = dr.GetString(0);
            s.nom = nom_serie;
            dr.Close();

            return s;
        }

       public static void ajouter_Serie(string nom, string desc, string genre, string producteur, int dureemoy)
        {
            SqlDataAdapter sda = new SqlDataAdapter("Insert into Serie (Nom, Description, Genre, Note, Producteur, DureeMoyenne) values('"+nom+"', '"+desc+"', '"+genre+"','"+0+"','"+producteur+"','"+dureemoy+"') ", log);
            DataTable dtserie = new DataTable();
            sda.Fill(dtserie);
        }

        public static Boolean verifSerie(string nom)
        {
            SqlDataAdapter sda = new SqlDataAdapter("Select Count(*) From Serie where Nom='" +nom+ "'", log);
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
            log.Open();
            var command = new SqlCommand("Select Nom from Serie", log);
            using(var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    list.Add(reader.GetString(0));
                }
            }
            log.Close();
            return list;
        }

        public static List<string> returnToutUtilisateur()
        {
            List<string> list = new List<string>();
            log.Open();
            var command = new SqlCommand("Select Pseudo from Utilisateur", log);
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    list.Add(reader.GetString(0));
                }
            }
            log.Close();
            return list;

        }

        public static void supprSerie(string nom)
        {
            log.Open();
            SqlCommand cmd = new SqlCommand("Delete from Serie where Nom='" +nom+ "' ", log);
            cmd.ExecuteNonQuery();
            log.Close();
        }

        public static Serie remplirSerie(string nom)
        {
            Serie serie = new Serie();
            log.Open();
            serie.nom = nom;
            //Description
            SqlCommand cmddesc = new SqlCommand("Select Description from Serie where Nom='" + nom + "'", log);
            SqlDataReader dr = cmddesc.ExecuteReader();
            dr.Read();
            serie.description = dr.GetString(0);
            dr.Close();
            //Producteur
            SqlCommand cmdprod = new SqlCommand("Select Producteur from Serie where Nom='" + nom + "'", log);
            dr = cmdprod.ExecuteReader();
            dr.Read();
            serie.producteur = dr.GetString(0);
            dr.Close();
            //Durée Moyenne
            SqlCommand cmddure = new SqlCommand("Select DureeMoyenne from Serie where Nom='"+nom+"'", log);
            dr = cmddure.ExecuteReader();
            dr.Read();
            serie.dureeMoy = dr.GetInt32(0);
            dr.Close();
            //Genre
            SqlCommand cmdgenre = new SqlCommand("Select Genre from Serie where Nom='" + nom + "'", log);
            dr = cmdgenre.ExecuteReader();
            dr.Read();
            var recup = dr.GetString(0);
            Genre genre = (Genre)Enum.Parse(typeof(Genre), recup);
            serie.genre = genre;
            dr.Close();
            log.Close();
            return serie;
        }

        public static void updateSerie(string nom, string desc, int dureMoy, string producteur, string genre)
        {
            SqlDataAdapter sdaSerie = new SqlDataAdapter("Update Serie set Description ='" + desc + "', Producteur ='"+producteur+"', Genre ='"+genre+"', DureeMoyenne ='"+dureMoy+"' where Nom='"+nom+"'", log);
            DataTable dt = new DataTable();
            sdaSerie.Fill(dt);
        }

        public static bool upModo(string pseudo)
        {
            log.Open();
            SqlCommand cmd = new SqlCommand("Select Modo from Utilisateur where Pseudo='" + pseudo + "'", log);
            SqlDataReader dr = cmd.ExecuteReader();
            dr.Read();
            string recup = dr.GetString(0);
            dr.Close();
            bool verif = Convert.ToBoolean(recup);
            if (verif)
            {
                log.Close();
                return false;
            }
            else
            {
                SqlDataAdapter sda = new SqlDataAdapter("Update Utilisateur set Modo='" + "true" + "' where Pseudo='" + pseudo + "'", log);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                log.Close();
                return true;
            } 
        }

        public static bool downModo(string pseudo)
        {
            log.Open();
            SqlCommand cmd = new SqlCommand("Select Modo from Utilisateur where Pseudo='" + pseudo + "'", log);
            SqlDataReader dr = cmd.ExecuteReader();
            dr.Read();
            string recup = dr.GetString(0);
            dr.Close();
            bool verif = Convert.ToBoolean(recup);
            if (!verif)
            {
                log.Close();
                return false;
            }
            else
            {
                SqlDataAdapter sda = new SqlDataAdapter("Update Utilisateur set Modo='" + "false" + "' where Pseudo='" + pseudo + "'", log);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                log.Close();
                return true;
            }
        }
        
    }
}