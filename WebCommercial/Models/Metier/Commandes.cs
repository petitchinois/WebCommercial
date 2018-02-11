using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using WebCommercial.Models.MesExceptions;
using WebCommercial.Models.Persistance.WebApplication1.Models.Persistance;

namespace WebCommercial.Models.Metier
{
    public class Commandes
    {
        private String noCommande;
        private String noVendeur;
        private String noClient;
        private String dateCde;
        private String facture;

        /*Attributs des details_cde et articles */
        private String noArticle;
        private String qteCdee;
        private String livree;

        private String libArticle;
        private String prixArt;
        private String prixTotal;
        private String prixFinal;


        public String NoCommande
        {
            get { return noCommande; }
            set { noCommande = value; }
        }
        public String NoVendeur
        {
            get { return noVendeur; }
            set { noVendeur = value; }
        }

        public String NoClient
        {
            get { return noClient; }
            set { noClient = value; }
        }

        public String DateCde
        {
            get { return dateCde; }
            set { dateCde = value; }
        }

        public String Facture
        {
            get { return facture; }
            set { facture = value; }
        }

        public String NoArticle
        {
            get { return noArticle; }
            set { noArticle = value; }
        }
        public String QteCdee
        {
            get { return qteCdee; }
            set { qteCdee = value; }
        }

        public String Livree
        {
            get { return livree; }
            set { livree = value; }
        }
        public String LibArticle
        {
            get { return libArticle; }
            set { libArticle = value; }
        }
        public String PrixArt
        {
            get { return prixArt; }
            set { prixArt = value; }
        }
        public String PrixTotal
        {
            get { return prixTotal; }
            set { prixTotal = value; }
        }
        public String PrixFinal
        {
            get { return prixFinal; }
            set { prixFinal = value; }
        }

        //initialisation 
        public Commandes()
        {
            noCommande = "";
            noVendeur = "";
            noClient = "";
            dateCde = "";
            facture = "";

            noArticle="";
            qteCdee="";
            livree="";

            libArticle="";
            prixArt="";
            prixTotal="";
            prixFinal="";

    }

        public Commandes(string no, string ve, string cl, string date, string fact, string noA, string qte, string liv)
        {
            noCommande = no;
            noVendeur = ve;
            noClient = cl;
            dateCde = date;
            facture = fact;
            NoArticle = noA;
            qteCdee = qte;
            livree = liv;
        }

        public Commandes(string no, string noA, string qte, string liv)
        {
            noCommande = no;
            NoArticle = noA;
            qteCdee = qte;
            livree = liv;
        }

        public static List<String> LectureNoArticle()
        {
            List<String> mesNumeros = new List<String>();
            DataTable dt;
            Serreurs er = new Serreurs("Erreur sur lecture de la commande.", "Client.LectureNoClient()");
            try
            {

                String mysql = "SELECT DISTINCT NO_ARTICLE FROM articles ORDER BY NO_ARTICLE";
                dt = DBInterface.Lecture(mysql, er);

                foreach (DataRow dataRow in dt.Rows)
                {
                    mesNumeros.Add((dataRow[0]).ToString());
                }

                return mesNumeros;
            }
            catch (MySqlException e)
            {
                throw new MonException(er.MessageUtilisateur(), er.MessageApplication(), e.Message);
            }
        }

        public static List<String> LectureNoCommande()
        {
            List<String> mesNumeros = new List<String>();
            DataTable dt;
            Serreurs er = new Serreurs("Erreur sur lecture de la commande.", "Client.LectureNoClient()");
            try
            {

                String mysql = "SELECT DISTINCT NO_COMMAND FROM commandes ORDER BY NO_COMMAND";
                dt = DBInterface.Lecture(mysql, er);

                foreach (DataRow dataRow in dt.Rows)
                {
                    mesNumeros.Add((dataRow[0]).ToString());
                }

                return mesNumeros;
            }
            catch (MySqlException e)
            {
                throw new MonException(er.MessageUtilisateur(), er.MessageApplication(), e.Message);
            }
        }


        /// <summary>
        /// Lire un utilisateur sur son ID
        /// </summary>
        /// <param name="numCo">N° de la commande à lire</param>
        public static Commandes getCommande(String numCo)
        {

            String mysql;
            DataTable dt;
            Serreurs er = new Serreurs("Erreur sur recherche d'une commande.", "Commande.RechercheUnClient()");
            try
            {

                mysql = "SELECT NO_VENDEUR, NO_CLIENT, DATE_CDE,";
                mysql += "FACTURE ";
                mysql += "FROM commandes WHERE NO_COMMAND='" + numCo + "'";
                dt = DBInterface.Lecture(mysql, er);

                if (dt.IsInitialized && dt.Rows.Count > 0)
                {
                    Commandes comm = new Commandes();
                    DataRow dataRow = dt.Rows[0];
                    comm.NoCommande = numCo;
                    comm.NoVendeur = dataRow[0].ToString();
                    comm.NoClient = dataRow[1].ToString();
                    comm.DateCde = dataRow[2].ToString();
                    comm.Facture = dataRow[3].ToString();


                    return comm;
                }
                else
                    return null;
            }
            catch (MySqlException e)
            {
                throw new MonException(er.MessageUtilisateur(), er.MessageApplication(), e.Message);
            }

        }
        public static IEnumerable<Commandes> getCommands()
        {
            IEnumerable<Commandes> comms = new List<Commandes>();
            DataTable dt;
            Commandes comm;
            Serreurs er = new Serreurs("Erreur sur lecture des commandes.", "CommandesList.getCommands()");
            try
            {
                String mysql = "SELECT NO_VENDEUR, NO_CLIENT, DATE_CDE, FACTURE, " +
                               "NO_COMMAND FROM commandes ORDER BY NO_COMMAND";

                dt = DBInterface.Lecture(mysql, er);

                foreach (DataRow dataRow in dt.Rows)
                {
                    comm = new Commandes();
                    comm.NoCommande = dataRow[4].ToString();
                    comm.NoVendeur = dataRow[0].ToString();
                    comm.NoClient = dataRow[1].ToString();
                    comm.DateCde = dataRow[2].ToString();
                    comm.Facture = dataRow[3].ToString();


                    ((List<Commandes>)comms).Add(comm);
                }

                return comms;
            }
            catch (MonException e)
            {
                throw new MonException(er.MessageUtilisateur(), er.MessageApplication(), e.Message);
            }
            catch (MySqlException e)
            {
                throw new MonException(er.MessageUtilisateur(), er.MessageApplication(), e.Message);
            }
        }

        public static void updateCommande(Commandes unCli)
        {

            String[] date = unCli.DateCde.Split('.');
            String jour = date[0];
            String mois = date[1];
            String annee = date[2];
            String dateFinale = annee + '-' + mois + '-' + jour;

            Serreurs er = new Serreurs("Erreur sur l'écriture d'une commande.", "Commande.update()");
            String requete = "UPDATE Commandes SET " +
                                  "NO_VENDEUR = '" + unCli.NoVendeur + "'" +
                                  ", NO_CLIENT = '" + unCli.NoClient + "'" +
                                  ", DATE_CDE = '" + dateFinale + "'" +
                                  ", FACTURE = '" + unCli.Facture + "'" +
                                  " WHERE NO_COMMAND LIKE '" + unCli.NoCommande + "'";
            try
            {
                DBInterface.Insertion_Donnees(requete);
            }
            catch (MonException erreur)
            {
                throw erreur;
            }
            catch (MySqlException e)
            {
                throw new MonException(er.MessageUtilisateur(), er.MessageApplication(), e.Message);
            }

        }

        public static void addDetailCommande(Commandes uneCde)
        {
            Serreurs er = new Serreurs("Erreur sur l'écriture d'une commande.", "Commandes.add()");
            String requete = "INSERT INTO detail_cde (NO_COMMAND, NO_ARTICLE, QTE_CDEE, LIVREE) Values (" +
                                  "'" + uneCde.NoCommande + "'" +
                                  ", '" + uneCde.NoArticle + "'" +
                                  ", '" + uneCde.QteCdee + "'" +
                                  ", '" + uneCde.Livree + "');";

            try
            {
                DBInterface.Insertion_Donnees(requete);
            }
            catch (MonException erreur)
            {
                throw erreur;
            }
            catch (MySqlException e)
            {
                throw new MonException(er.MessageUtilisateur(), er.MessageApplication(), e.Message);
            }
        }
        

        public static void addCommande(Commandes uneCde)
        {
            /*String[] date = uneCde.DateCde.ToString().Split('-');
            String jour = date[0];
            String mois = date[1];
            String annee = date[2];
            String dateFinale = annee + '-' + mois + '-' + jour;*/

            Serreurs er = new Serreurs("Erreur sur l'écriture d'une commande.", "Commandes.add()");
            String requete = "INSERT INTO Commandes (NO_COMMAND,NO_VENDEUR,NO_CLIENT,DATE_CDE,FACTURE) Values (" +
                                  "'" + uneCde.NoCommande + "'" +
                                  ",'" + uneCde.NoVendeur + "'" +
                                  ",'" + uneCde.NoClient + "'" +
                                  ",'" + uneCde.DateCde + "'" +
                                  ",'" + uneCde.Facture + "'); \n" +
                                  "INSERT INTO detail_cde (NO_COMMAND, NO_ARTICLE, QTE_CDEE, LIVREE) Values (" +
                                  "'" + uneCde.NoCommande + "'" +
                                  ", '"+ uneCde.NoArticle + "'" +
                                  ", '"+uneCde.QteCdee + "'" +
                                  ", '"+uneCde.Livree+"');";
            try
            {
                DBInterface.Insertion_Donnees(requete);
            }
            catch (MonException erreur)
            {
                throw erreur;
            }
            catch (MySqlException e)
            {
                throw new MonException(er.MessageUtilisateur(), er.MessageApplication(), e.Message);
            }

        }

        public static IEnumerable<Commandes> getListeDeCommande(String id)
        {
            IEnumerable<Commandes> uneCde = new List<Commandes>();
            Commandes commande;
            DataTable dt;
            Serreurs er = new Serreurs("Erreur sur recherche d'une commande.", "Commande.RechercheUneCommande()");

            try
            {

                String mysql = "SELECT NO_ARTICLE, LIB_ARTICLE, QTE_CDEE, PRIX_ART, QTE_CDEE * PRIX_ART AS PRIX_TOT " +
                              "FROM detail_cde NATURAL JOIN articles WHERE NO_COMMAND='" + id + "'";
                dt = DBInterface.Lecture(mysql, er);
                double price = 0;
                foreach (DataRow dataRow in dt.Rows)
                {
                    commande = new Commandes();
                    commande.NoArticle = dataRow[0].ToString();
                    commande.LibArticle = dataRow[1].ToString();
                    commande.QteCdee = dataRow[2].ToString();
                    commande.PrixArt = dataRow[3].ToString();
                    commande.PrixTotal = dataRow[4].ToString();
                    price = Convert.ToDouble(dataRow[4].ToString()) + price;
                    /*MAJ DU PRIX*/
                    commande.PrixFinal = Convert.ToString(price);

                    ((List<Commandes>)uneCde).Add(commande);
                }

                return uneCde;

            }
            catch (MySqlException e)
            {
                throw new MonException(er.MessageUtilisateur(), er.MessageApplication(), e.Message);
            }

        }

		public static void Supprimer(Commandes uneCde)
		{
			SuppDetail(uneCde);
			Serreurs er = new Serreurs("Erreur sur la suppression d'une commande.", "Commandes.Supprimer()");
			String requete = "DELETE FROM Commandes WHERE NO_COMMAND='" + uneCde.NoCommande + "'";
			Console.WriteLine(requete);
			try
			{
				DBInterface.Insertion_Donnees(requete);
			}
			catch (MonException erreur)
			{
				throw erreur;
			}
			catch (MySqlException e)
			{
				throw new MonException(er.MessageUtilisateur(), er.MessageApplication(), e.Message);
			}
		}

		public static void SuppDetail(Commandes uneCde)
		{
			Serreurs er = new Serreurs("Erreur sur la suppression d'une commande.", "Commandes.Supprimer()");
			String requete = "DELETE FROM detail_cde WHERE NO_COMMAND='" + uneCde.NoCommande + "'";
			Console.WriteLine(requete);
			try
			{
				DBInterface.Insertion_Donnees(requete);
			}
			catch (MonException erreur)
			{
				throw erreur;
			}
			catch (MySqlException e)
			{
				throw new MonException(er.MessageUtilisateur(), er.MessageApplication(), e.Message);
			}
		}

    }
}