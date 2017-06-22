using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Projet.Entite.Class;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Media.Imaging;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Configuration;

namespace Projet.Service.Fonctions
{
    public class GestionBDD
    {
        private static SqlConnection con = new SqlConnection($@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={ConfigurationManager.AppSettings["BDDPath"]};Integrated Security=True");

        #region Utilisateur
        /// <summary>
        /// Cette fonction va se servir du pseudo de l'utilisateur qui est une clé primaire dans la BDD afin de remplir toutes les caractéristiques du l'utilisateur
        /// </summary>
        /// <param name="pseudo">Pseudo de l'utilisateur afin de le retrouver dans la BDD</param>
        public static void remplirUserCourant(string pseudo)
        {
            con.Open();

            //*****Récupération de la description de l'utilisateur courant*****
            SqlCommand cmddesc = new SqlCommand("Select Description From Utilisateur where Pseudo='" + pseudo + "' ", con);
            SqlDataReader dr = cmddesc.ExecuteReader();
            dr.Read();
            string description = dr.GetString(0);
            dr.Close();
            //*****Récupération du sexe de l'utilisateur*****
            SqlCommand cmdsexe = new SqlCommand("Select Sexe From Utilisateur where Pseudo='" + pseudo + "' ", con);
            dr = cmdsexe.ExecuteReader();
            dr.Read();
            string sexe = dr.GetString(0);
            dr.Close();
            //*****Récupération de la date de naissance de l'utilisateur*****
            SqlCommand cmdddn = new SqlCommand("Select DateDeNaissance From Utilisateur where Pseudo='" + pseudo + "' ", con);
            dr = cmdddn.ExecuteReader();
            dr.Read();
            string dateDeNaissance = dr.GetString(0);
            DateTime ddn = Convert.ToDateTime(dateDeNaissance);
            dr.Close();
            //*****Récupération de l'autoristation modérateur*****
            SqlCommand cmdmodo = new SqlCommand("Select Modo From Utilisateur where Pseudo='" + pseudo + "'", con);
            dr = cmdmodo.ExecuteReader();
            dr.Read();
            var recup = dr.GetString(0);
            bool modo = Convert.ToBoolean(recup);
            dr.Close();
            //*****Récupération de l'image de profil de l'utilisateur*****
            SqlCommand cmdprofil = new SqlCommand("Select PathProfil From Utilisateur  where Pseudo='" + pseudo + "'", con);
            dr = cmdprofil.ExecuteReader();
            dr.Read();
            BitmapImage pp = new BitmapImage(new Uri($"{AppDomain.CurrentDomain.BaseDirectory}/Images/{dr.GetString(0)}"));
            dr.Close();
            //*****Récupération de l'image de couverture de l'utilisateur*****
            SqlCommand cmdcouv = new SqlCommand("Select PathCouverture From Utilisateur  where Pseudo='" + pseudo + "'", con);
            dr = cmdcouv.ExecuteReader();
            dr.Read();
            BitmapImage couv = new BitmapImage(new Uri($"{AppDomain.CurrentDomain.BaseDirectory}/Images/{dr.GetString(0)}"));
            dr.Close();
            con.Close();

            UserCourant.Connect(pseudo, description, sexe, ddn, modo, pp, couv);
        }

        /// <summary>
        /// Vérifié si la combinaison de Pseudo et de mot de passe et correct
        /// </summary>
        /// <param name="pseudo">Pseudo de l'utilisateur</param>
        /// <param name="mdp">Mot de passe</param>
        /// <returns>Retourne un bool en fonction de si la combinaison fonctionne ou non</returns>
        public static Boolean verifLoginMdp(String pseudo, String mdp)
        {
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
        /// <summary>
        /// Retourne un bool en en fonction de la présence ou non du pseudo passé en paramètre dans la BDD
        /// </summary>
        /// <param name="pseudo">Pseudo de l'utilisateur rentré</param>
        /// <returns></returns>
        public static Boolean verifLogin(String pseudo)
        {
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
        /// <summary>
        /// Permet d'inscrire un utilisateur et l'ajoute dans la base de donné, et ajoute des valeurs par défaut
        /// </summary>
        /// <param name="pseudo">Pseudo de l'utilisateur à inscrire</param>
        /// <param name="mdp">Mot de passe de l'utilisateur à inscrire</param>
        public static void inscription(String pseudo, String mdp)
        {
            SqlDataAdapter sda2 = new SqlDataAdapter("Insert into Utilisateur (Pseudo, Password, Description, Sexe, DateDeNaissance, Modo, PathProfil, PathCouverture) values('" + pseudo + "', '" + mdp + "' , '" + "Description..." + "', '" + "Pas spécifié..." + "', '" + "01/01/0001 00:00:00" + "', '" + "false" + "', '" + "profil.jpg" + "', '" + "couverture.jpg" + "')", con);
            DataTable dt2 = new DataTable();
            sda2.Fill(dt2);
        }
        /// <summary>
        /// Met à jour la description de l'utilisateur
        /// </summary>
        /// <param name="desc">Description à ajouter</param>
        /// <param name="pseudo">Pseudo de l'utilisateur</param>
        public static void updateDescription(String desc, String pseudo)
        {
            desc = desc.Replace("'", "''");
            SqlDataAdapter sda = new SqlDataAdapter("Update Utilisateur set Description =' " + desc + "' where Pseudo='" + pseudo + "'", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
        }
        /// <summary>
        /// Met à jour la date de naissance de l'utilisateur
        /// </summary>
        /// <param name="ddn">Date de naissance à ajouter</param>
        /// <param name="pseudo">Pseudo de l'utilisateur</param>
        public static void updateDdn(String ddn, String pseudo)
        {
            SqlDataAdapter sdaddn = new SqlDataAdapter("Update Utilisateur set DateDeNaissance ='" + ddn + "'  where Pseudo='" + pseudo + "'", con);
            DataTable dtddn = new DataTable();
            sdaddn.Fill(dtddn);
        }
        /// <summary>
        /// Met à jour le sexe de l'utilisateur
        /// </summary>
        /// <param name="sexe">Sexe à ajouter</param>
        /// <param name="pseudo">Pseudo de l'utilisateur</param>
        public static void updateSexe(String sexe, String pseudo)
        {
            SqlDataAdapter sdasexe = new SqlDataAdapter("Update Utilisateur set Sexe ='" + sexe + "' where Pseudo='" + pseudo + "'", con);
            DataTable dtsexe = new DataTable();
            sdasexe.Fill(dtsexe);
        }
        /// <summary>
        /// Retourne une List avec tout les pseudo présent dans la BDD
        /// </summary>
        /// <returns></returns>
        public static List<string> returnToutUtilisateur()
        {
            List<string> list = new List<string>();
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
        /// <summary>
        /// Retourve un List avec toutes les images utilisé par les utilisateur, cette fonction est utilisé lors du tri d'image non utilisé au démarrage de l'application
        /// </summary>
        /// <returns></returns>
        public static List<string> returnImageUser()
        {
            string path = Path.Combine(Environment.CurrentDirectory, "Images");
            List<string> ficUser = new List<string>();
            con.Open();
            var command = new SqlCommand("Select PathProfil From Utilisateur", con);
            var cmd = new SqlCommand("Select PathCouverture From Utilisateur", con);
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    ficUser.Add($@"{path}\{reader.GetString(0)}");
                }
            }

            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    ficUser.Add($@"{path}\{reader.GetString(0)}");
                }
            }
            con.Close();
            return ficUser;
        }
        #endregion

        #region Serie
        /// <summary>
        /// Ajoute une série dans la BDD
        /// </summary>
        /// <param name="nom">Nom de la série</param>
        /// <param name="desc">Description de la série</param>
        /// <param name="genre">Genre de la série</param>
        /// <param name="producteur">Producteur de la série</param>
        /// <param name="dureemoy">Durée moyenne des épisodes de la série</param>
        /// <param name="nbsaison">Nombre de saisons de la série</param>
        /// <param name="path">Chemin de l'image de la série</param>
        /// <param name="banniere">Chemin de la bannière de la série</param>
        public static void ajouter_Serie(string nom, string desc, string genre, string producteur, int dureemoy, int nbsaison, string path, string banniere, int nbep)
        {
            desc = desc.Replace("'", "''");
            con.Open();
            var command = new SqlCommand("Insert into Serie (Nom, Description, Genre, Note, Producteur, DureeMoyenne, PhotoSerie, NbSaison, PathBanniere, NbEpisode) values('" + nom + "', '" + desc + "', '" + genre + "','" + 0 + "','" + producteur + "','" + dureemoy + "','" + path + "', '" + nbsaison + "', '" + banniere + "', '" + nbep + "') ", con);
            command.ExecuteNonQuery();
            con.Close();
        }
        /// <summary>
        /// Vérifie si le nom de série est déjà utilisé ou non
        /// </summary>
        /// <param name="nom">Nom de la série à vérifier</param>
        /// <returns></returns>
        public static bool verifSerie(string nom)
        {
            SqlDataAdapter sda = new SqlDataAdapter("Select Count(*) From Serie where Nom='" + nom + "'", con);
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
        /// <summary>
        /// Retourne une List<string> de toutes les séries contenus dans la BDD, fonction utilisé pour remplir une combobox afin de supprimer une serie
        /// </summary>
        /// <returns></returns>
        public static List<string> returnTouteSerie()
        {
            List<string> list = new List<string>();
            con.Open();
            var command = new SqlCommand("Select Nom from Serie", con);
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
        /// <summary>
        /// Retourne une List de toutes les séries contenu dans la BDD
        /// </summary>
        /// <returns></returns>
        public static List<Serie> returnTouteSerieFull()
        {
            List<string> list = new List<string>();
            List<Serie> listSerie = new List<Serie>();
            con.Open();
            var command = new SqlCommand("Select Nom from Serie", con);
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    list.Add(reader.GetString(0));
                }
            }


            for (int i = 0; i < list.Count; i++)
            {
                Serie serie = new Serie();
                serie.nom = list[i];
                //Description
                SqlCommand cmddesc = new SqlCommand("Select Description from Serie where Nom='" + list[i] + "'", con);
                SqlDataReader dr = cmddesc.ExecuteReader();
                dr.Read();
                serie.description = dr.GetString(0);
                dr.Close();
                //Producteur
                SqlCommand cmdprod = new SqlCommand("Select Producteur from Serie where Nom='" + list[i] + "'", con);
                dr = cmdprod.ExecuteReader();
                dr.Read();
                serie.producteur = dr.GetString(0);
                dr.Close();
                //Durée Moyenne
                SqlCommand cmddure = new SqlCommand("Select DureeMoyenne from Serie where Nom='" + list[i] + "'", con);
                dr = cmddure.ExecuteReader();
                dr.Read();
                serie.dureeMoy = dr.GetInt32(0);
                dr.Close();
                //Genre
                SqlCommand cmdgenre = new SqlCommand("Select Genre from Serie where Nom='" + list[i] + "'", con);
                dr = cmdgenre.ExecuteReader();
                dr.Read();
                var recup = dr.GetString(0);
                Genre genre = (Genre)Enum.Parse(typeof(Genre), recup);
                serie.genre = genre;
                dr.Close();
                //Nombre de saisons
                SqlCommand cmdsaison = new SqlCommand("Select NbSaison from Serie where Nom='" + list[i] + "'", con);
                dr = cmdsaison.ExecuteReader();
                dr.Read();
                serie.nbSaison = dr.GetInt32(0);
                dr.Close();
                //Image Serie
                SqlCommand cmdImage = new SqlCommand("Select PhotoSerie from Serie where Nom='" + list[i] + "'", con);
                dr = cmdImage.ExecuteReader();
                dr.Read();
                serie.ImageSerie = new BitmapImage(new Uri($"{AppDomain.CurrentDomain.BaseDirectory}/ImagesSerie/{dr.GetString(0)}"));
                dr.Close();
                //Bannière série
                SqlCommand cmdBann = new SqlCommand("Select PathBanniere from Serie where Nom='" + list[i] + "'", con);
                dr = cmdBann.ExecuteReader();
                dr.Read();
                serie.Banniereserie = new BitmapImage(new Uri($"{AppDomain.CurrentDomain.BaseDirectory}/ImagesSerie/{dr.GetString(0)}"));
                dr.Close();
                //Nombre d'épisode
                SqlCommand cmdep = new SqlCommand("Select NbEpisode from Serie where Nom='" + list[i] + "'", con);
                dr = cmdep.ExecuteReader();
                dr.Read();
                serie.nbEpisode = dr.GetInt32(0);
                dr.Close();
                //Commentaire
                using (var cmd = new SqlCommand("Select * from Commentaire where NomSerie='" + list[i] + "'", con))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Commentaire c = new Commentaire();
                            c.nomUtilisateur = reader.GetString(0);
                            c.commentaire = reader.GetString(1);
                            c.nomSerie = list[i];
                            serie.commentaire.Add(c);
                        }
                    }
                }
                //Note
                var cmdNote = new SqlCommand("Select Note from Serie where Nom='" + list[i] + "'", con);
                dr = cmdNote.ExecuteReader();
                dr.Read();
                serie.note = dr.GetInt32(0);
                dr.Close();

                listSerie.Add(serie);
            }
            con.Close();
            return listSerie;
        }
        /// <summary>
        /// Supprime un série de la BDD
        /// </summary>
        /// <param name="nom">Nom de la série à supprimer</param>
        public static void supprSerie(string nom)
        {
            con.Open();
            SqlCommand cmdNote = new SqlCommand("Delete from NoteSerie where NomSerie ='" + nom + "' ", con);
            cmdNote.ExecuteNonQuery();
            SqlCommand cmdComm = new SqlCommand("Delete from Commentaire where NomSerie ='" + nom + "' ", con);
            cmdComm.ExecuteNonQuery();
            SqlCommand cmdSerieUser = new SqlCommand("Delete from UtilisateurSerie where IdSerie ='" + nom + "'", con);
            cmdSerieUser.ExecuteNonQuery();
            SqlCommand cmd = new SqlCommand("Delete from Serie where Nom='" + nom + "' ", con);
            cmd.ExecuteNonQuery();
            con.Close();
        }
        /// <summary>
        /// Permet de remplir une série à partir de son nom et de la retourner complète
        /// </summary>
        /// <param name="nom">Nom de la série à remplir</param>
        /// <returns></returns>
        public static Serie remplirSerie(string nom)
        {
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
            SqlCommand cmddure = new SqlCommand("Select DureeMoyenne from Serie where Nom='" + nom + "'", con);
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
            //Nombre de saison
            SqlCommand cmdsaison = new SqlCommand("Select NbSaison from Serie where Nom='" + nom + "'", con);
            dr = cmdsaison.ExecuteReader();
            dr.Read();
            serie.nbSaison = dr.GetInt32(0);
            dr.Close();
            //Image
            SqlCommand cmdImage = new SqlCommand("Select PhotoSerie from Serie where Nom='" + nom + "'", con);
            dr = cmdImage.ExecuteReader();
            dr.Read();
            serie.ImageSerie = new BitmapImage(new Uri($"{AppDomain.CurrentDomain.BaseDirectory}/ImagesSerie/{dr.GetString(0)}"));
            dr.Close();
            //Banniere série
            SqlCommand cmdBann = new SqlCommand("Select PathBanniere from Serie where Nom='" + nom + "'", con);
            dr = cmdBann.ExecuteReader();
            dr.Read();
            serie.Banniereserie = new BitmapImage(new Uri($"{AppDomain.CurrentDomain.BaseDirectory}/ImagesSerie/{dr.GetString(0)}"));
            dr.Close();
            SqlCommand cmdep = new SqlCommand("Select NbEpisode from Serie where Nom='" + nom + "'", con);
            dr = cmdep.ExecuteReader();
            dr.Read();
            serie.nbEpisode = dr.GetInt32(0);
            dr.Close();
            //Commentaire
            using (var cmd = new SqlCommand("Select * from Commentaire where NomSerie='" + nom + "'", con))
            {
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Commentaire c = new Commentaire();
                        c.nomUtilisateur = reader.GetString(0);
                        c.commentaire = reader.GetString(1);
                        c.nomSerie = nom;
                        serie.commentaire.Add(c);
                    }
                }
            }
            //Note
            var cmdNote = new SqlCommand("Select Note from Serie where Nom='" + nom + "'", con);
            dr = cmdNote.ExecuteReader();
            dr.Read();
            serie.note = dr.GetInt32(0);
            dr.Close();

            con.Close();
            return serie;
        }
        /// <summary>
        /// Modifie une série
        /// </summary>
        /// <param name="nom">Nom de la série à modifier</param>
        /// <param name="desc">Description à modifier</param>
        /// <param name="dureMoy">Durée moyenne à modifier</param>
        /// <param name="producteur">Producteur à modifier</param>
        /// <param name="genre">Genre à modifier</param>
        /// <param name="path">Chemin de l'image à modifier</param>
        /// <param name="nbsaison">Nombre de saison à modifier</param>
        /// <param name="banniere">Chemon de la banniere à modifier</param>
        public static void updateSerie(string nom, string desc, int dureMoy, string producteur, string genre, string path, int nbsaison, string banniere, int nbep)
        {
            desc = desc.Replace("'", "''");
            con.Open();
            SqlCommand cmd = new SqlCommand("Update Serie set Description ='" + desc + "', Producteur ='" + producteur + "', Genre ='" + genre + "', DureeMoyenne ='" + dureMoy + "', PhotoSerie='" + path + "', NbSaison='" + nbsaison + "', '" + banniere + "', NbEpisode='" + nbep + "' where Nom='" + nom + "'", con);
            cmd.ExecuteNonQuery();
            con.Close();
        }
        /// <summary>
        /// Même fonction que au dessus, mais il n'y a pas de modification d'image
        /// </summary>
        /// <param name="nom"></param>
        /// <param name="desc"></param>
        /// <param name="dureMoy"></param>
        /// <param name="producteur"></param>
        /// <param name="genre"></param>
        /// <param name="nbsaison"></param>
        public static void updateSeriesansImage(string nom, string desc, int dureMoy, string producteur, string genre, int nbsaison, int nbep)
        {
            desc = desc.Replace("'", "''");
            con.Open();
            SqlCommand cmd = new SqlCommand("Update Serie set Description ='" + desc + "', Producteur ='" + producteur + "', Genre ='" + genre + "', DureeMoyenne ='" + dureMoy + "', NbSaison='" + nbsaison + "', NbEpisode='" + nbep + "' where Nom='" + nom + "'", con);
            cmd.ExecuteNonQuery();
            con.Close();
        }
        /// <summary>
        /// Retourne une List de toutes les images utilisé pour les série, fonction utilisé lors de la suppression des images non utilisé au démarrage de l'application
        /// </summary>
        /// <returns></returns>
        public static List<string> returnImageSerie()
        {
            string path = Path.Combine(Environment.CurrentDirectory, "ImagesSerie");
            List<string> ficSerie = new List<string>();
            con.Open();
            var command = new SqlCommand("Select PhotoSerie From Serie", con);
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    ficSerie.Add($@"{path}\{reader.GetString(0)}");
                }
            }
            var commande = new SqlCommand("Select PathBanniere From Serie", con);
            using (var reader = commande.ExecuteReader())
            {
                while (reader.Read())
                {
                    ficSerie.Add($@"{path}\{reader.GetString(0)}");
                }
            }
            con.Close();
            return ficSerie;
        }
        #endregion

        #region Administration
        /// <summary>
        /// Fais passer un utilisateur au rang de modérateur
        /// </summary>
        /// <param name="pseudo">Nom de l'utilisateur à promouvoir</param>
        /// <returns>Retourne si l'utilisateur étais déjà modérateur ou non</returns>
        public static bool upModo(string pseudo)
        {
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
                SqlCommand command = new SqlCommand("Update Utilisateur set Modo='" + "true" + "' where Pseudo='" + pseudo + "'", con);
                command.ExecuteNonQuery();
                con.Close();
                return true;
            }
        }
        /// <summary>
        /// Dégrade un utilisateur du rang d'administrateur
        /// </summary>
        /// <param name="pseudo">Nom de l'utilisateur à rétrograder</param>
        /// <returns>Retourne si l'utilisateur étais déjà modérateur ou non</returns>
        public static bool downModo(string pseudo)
        {
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
                SqlCommand command = new SqlCommand("Update Utilisateur set Modo='" + "false" + "' where Pseudo='" + pseudo + "'", con);
                command.ExecuteNonQuery();
                con.Close();
                return true;
            }
        }
        /// <summary>
        /// Modifie la photo de profil d'un utilisateur
        /// </summary>
        /// <param name="path">Chemin de la photo à ajouter à la BDD</param>
        /// <param name="pseudo">Pseudo de l'utilisateur courant</param>
        public static void enregisterPhotoProfil(string path, string pseudo)
        {
            con.Open();
            var command = new SqlCommand("Update Utilisateur set PathProfil='" + path + "' where Pseudo='" + pseudo + "'", con);
            command.ExecuteNonQuery();
            con.Close();
        }
        /// <summary>
        /// Modifie la photo de couverture d'un utilisateur
        /// </summary>
        /// <param name="path">Chemin de la photo de couverture à ajouter à la BDD</param>
        /// <param name="pseudo">Pseudo de l'utilisateur courant</param>
        public static void enregisterPhotoCouverture(string path, string pseudo)
        {
            con.Open();
            var command = new SqlCommand("Update Utilisateur set PathCouverture='" + path + "' where Pseudo='" + pseudo + "'", con);
            command.ExecuteNonQuery();
            con.Close();
        }
        #endregion

        #region SerieUtilisateur
        /// <summary>
        /// Supprimer une série des favoris d'un utilisateur
        /// </summary>
        /// <param name="pseudo">Pseudo de l'utilisateur à enlever la série</param>
        /// <param name="nomSerie">Série à enlever</param>
        public static void removeSerieUtilisateur(string pseudo, string nomSerie)
        {
            con.Open();
            var cmd = new SqlCommand("Delete from UtilisateurSerie where IdSerie='" + nomSerie + "' and IdUtilisateur='" + pseudo + "' ", con);
            cmd.ExecuteNonQuery();
            con.Close();
        }
        /// <summary>
        /// Ajoute une série au favoris d'un utilisateur
        /// </summary>
        /// <param name="pseudo">Pseudo de l'utilisateur à ajouter la série</param>
        /// <param name="nomSerie">Série à ajouter au favoris</param>
        public static void addSerieUtilisateur(string pseudo, string nomSerie)
        {
            con.Open();
            var cmd = new SqlCommand("Insert into UtilisateurSerie (IdUtilisateur, IdSerie) values('" + pseudo + "', '" + nomSerie + "') ", con);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        /// <summary>
        /// Retourne une liste de toutes les séries ajouter en favoris de l'utilisateur passé en paramètre
        /// </summary>
        /// <param name="pseudo">Pseudo de l'utilisateur</param>
        /// <returns></returns>
        public static List<Serie> returnSerieUtilisateurFull(string pseudo)
        {
            List<string> list = new List<string>();
            List<Serie> serieUser = new List<Serie>();
            con.Open();
            var command = new SqlCommand("Select IdSerie from UtilisateurSerie where IdUtilisateur='" + pseudo + "'", con);
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    list.Add(reader.GetString(0));
                }
            }

            for (int i = 0; i < list.Count; i++)
            {
                Serie serie = new Serie();
                serie.nom = list[i];

                SqlCommand cmddesc = new SqlCommand("Select Description from Serie where Nom='" + list[i] + "'", con);
                SqlDataReader dr = cmddesc.ExecuteReader();
                dr.Read();
                serie.description = dr.GetString(0);
                dr.Close();
                //Producteur
                SqlCommand cmdprod = new SqlCommand("Select Producteur from Serie where Nom='" + list[i] + "'", con);
                dr = cmdprod.ExecuteReader();
                dr.Read();
                serie.producteur = dr.GetString(0);
                dr.Close();
                //Durée Moyenne
                SqlCommand cmddure = new SqlCommand("Select DureeMoyenne from Serie where Nom='" + list[i] + "'", con);
                dr = cmddure.ExecuteReader();
                dr.Read();
                serie.dureeMoy = dr.GetInt32(0);
                dr.Close();
                //Genre
                SqlCommand cmdgenre = new SqlCommand("Select Genre from Serie where Nom='" + list[i] + "'", con);
                dr = cmdgenre.ExecuteReader();
                dr.Read();
                var recup = dr.GetString(0);
                Genre genre = (Genre)Enum.Parse(typeof(Genre), recup);
                serie.genre = genre;
                dr.Close();
                //Image
                SqlCommand cmdImage = new SqlCommand("Select PhotoSerie from Serie where Nom='" + list[i] + "'", con);
                dr = cmdImage.ExecuteReader();
                dr.Read();
                serie.ImageSerie = new BitmapImage(new Uri($"{AppDomain.CurrentDomain.BaseDirectory}/ImagesSerie/{dr.GetString(0)}"));
                dr.Close();
                //Banniere série
                SqlCommand cmdBann = new SqlCommand("Select PathBanniere from Serie where Nom='" + list[i] + "'", con);
                dr = cmdBann.ExecuteReader();
                dr.Read();
                serie.Banniereserie = new BitmapImage(new Uri($"{AppDomain.CurrentDomain.BaseDirectory}/ImagesSerie/{dr.GetString(0)}"));
                dr.Close();
                //Commentaire
                using (var cmd = new SqlCommand("Select * from Commentaire where NomSerie='" + list[i] + "'", con))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Commentaire c = new Commentaire();
                            c.nomUtilisateur = reader.GetString(0);
                            c.commentaire = reader.GetString(1);
                            c.nomSerie = list[i];
                            serie.commentaire.Add(c);
                        }
                    }
                }
                //Note
                var cmdNote = new SqlCommand("Select Note from Serie where Nom='" + list[i] + "'", con);
                dr = cmdNote.ExecuteReader();
                dr.Read();
                serie.note = dr.GetInt32(0);
                dr.Close();
                serieUser.Add(serie);
            }
            con.Close();
            return serieUser;
        }


        /// <summary>
        /// Vérifié si la série est déjà dans les favoris de l'utilisateur ou non
        /// </summary>
        /// <param name="pseudo">Pseudo de l'utilisateur</param>
        /// <param name="nomserie">Nom de la série</param>
        /// <returns></returns>
        public static bool checkSiSerieAjouter(string pseudo, string nomserie)
        {
            SqlDataAdapter sda = new SqlDataAdapter("Select count(*) From UtilisateurSerie where IdUtilisateur='" + pseudo + "' and IdSerie='" + nomserie + "'", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows[0][0].ToString() == "1")
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        #endregion

        #region Commentaire
        /// <summary>
        /// Ajoute un commentaire pour une série
        /// </summary>
        /// <param name="com">Commentaire à ajouter</param>
        public static void ajouterCommentaireSerie(Commentaire com)
        {
            com.commentaire = com.commentaire.Replace("'", "''");
            var command = new SqlCommand("Insert into Commentaire (NomUser, Commentaire, NomSerie) values('" + com.nomUtilisateur + "', '" + com.commentaire + "', '" + com.nomSerie + "')", con);
            con.Open();
            command.ExecuteNonQuery();
            con.Close();
        }

        /// <summary>
        /// Retourne une liste de commentaire pour une série
        /// </summary>
        /// <param name="nomSerie">Nom de la série</param>
        /// <returns></returns>
        public static List<Commentaire> returnCommentaireSerie(string nomSerie)
        {
            List<Commentaire> com = new List<Commentaire>();
            con.Open();
            using (var cmd = new SqlCommand("Select * from Commentaire where NomSerie='" + nomSerie + "'", con))
            {
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Commentaire c = new Commentaire();
                        c.nomUtilisateur = reader.GetString(0);
                        c.commentaire = reader.GetString(1);
                        c.nomSerie = nomSerie;
                        com.Add(c);
                    }
                }
            }
            con.Close();
            return com;
        }

        public static void supprimerCommentaire(Commentaire com)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("Delete from Commentaire where NomSerie='" + com.nomSerie + "' and Commentaire='" + com.commentaire + "'", con);
            cmd.ExecuteNonQuery();
            con.Close();
        }
        #endregion

        #region Note
        /// <summary>
        /// Ajoute une note à une série
        /// </summary>
        /// <param name="nomSerie">Nom de la série à noter</param>
        /// <param name="note">Note à ajouter</param>
        /// <param name="pseudo">Pseudo de l'utilisateur qui à noter</param>
        public static void ajouterNoteSerie(string nomSerie, int note, string pseudo)
        {
            var command = new SqlCommand("Insert into NoteSerie (NomUser, Note, NomSerie) values('" + pseudo + "', '" + note + "','" + nomSerie + "') ", con);
            con.Open();
            command.ExecuteNonQuery();
            con.Close();
        }
        /// <summary>
        /// Modifie une note donner à une série
        /// </summary>
        /// <param name="nomSerie">Nom de la série à noter</param>
        /// <param name="note">Note à modifier</param>
        /// <param name="pseudo">Pseudo de l'utilisateur qui à modifier la note</param>
        public static void updateNote(string nomSerie, int note, string pseudo)
        {
            var command = new SqlCommand("Update NoteSerie set Note = '" + note + "' where NomUser='" + pseudo + "' and NomSerie='" + nomSerie + "'", con);
            con.Open();
            command.ExecuteNonQuery();
            con.Close();
        }
        /// <summary>
        /// Retourne la note donné par un utilisateur pour une série
        /// </summary>
        /// <param name="nomSerie">Nom de la série</param>
        /// <param name="pseudo">Nom de l'utilisateur</param>
        /// <returns></returns>
        public static int returnNoteUserSerie(string nomSerie, string pseudo)
        {
            var command = new SqlCommand("Select Note from NoteSerie where NomUser='" + pseudo + "' and NomSerie='" + nomSerie + "'", con);
            con.Open();
            var reader = command.ExecuteReader();
            reader.Read();
            int note = reader.GetInt32(0);

            con.Close();
            return note;
        }
        /// <summary>
        /// Met à jour la note, récupère le nombre de personne qui ont voté pour une série, récupère toutes les notes, les additionne et divise le résultat par le nombre de personne et met à jour la note dans la BDD
        /// </summary>
        /// <param name="nomSerie">Nom de la série</param>
        public static void updateNoteSerie(string nomSerie)
        {
            var command = new SqlCommand("Select count(*) from NoteSerie where NomSerie='" + nomSerie + "'", con);

            con.Open();
            //Nombre de personne qui ont noté
            int nbpers = (int)command.ExecuteScalar();
            //Récupère le total des notes
            int allNote = 0;
            var commandAllNote = new SqlCommand("Select Note from NoteSerie where NomSerie='" + nomSerie + "'", con);
            using (var reader = commandAllNote.ExecuteReader())
            {
                while (reader.Read())
                {
                    allNote = allNote + reader.GetInt32(0);
                }
            }
            //Calcul de la moyenne
            float note = allNote / nbpers;

            //Ajout de la note dans la table Serie
            var commandInsert = new SqlCommand("Update Serie set Note='" + note + "' where Nom='" + nomSerie + "'", con);
            commandInsert.ExecuteNonQuery();
            con.Close();
        }
        /// <summary>
        /// Vérifié si un utilisateur à déjà noté une série
        /// </summary>
        /// <param name="nomSerie">Nom de l'utilisateur</param>
        /// <param name="pseudo">Nom de la série</param>
        /// <returns></returns>
        public static bool checkSiDejaNoter(string nomSerie, string pseudo)
        {
            var command = new SqlCommand("Select count(*) from NoteSerie where NomSerie='" + nomSerie + "' and NomUser='" + pseudo + "'", con);
            con.Open();
            int recup = (int)command.ExecuteScalar();
            con.Close();
            if (recup >= 1)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        #endregion

    }
}