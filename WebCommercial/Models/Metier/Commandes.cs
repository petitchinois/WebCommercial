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

        //initialisation 
        public Commandes()
        {
            noCommande = "";
            noVendeur = "";
            noClient = "";
            dateCde = "";
            facture = "";

        }

        public Commandes(string no, string ve, string cl, string date, string fact)
        {
            noCommande = no;
            noVendeur = ve;
            noClient = cl;
            dateCde = date;
            facture = fact;
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
            Serreurs er = new Serreurs("Erreur sur recherche d'une commande.", "Client.RechercheUnClient()");
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
            Serreurs er = new Serreurs("Erreur sur lecture des commandes.", "ClientsList.getClients()");
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
            String anne = date[2];
            String dateFinale = anne + '-' + mois + '-' + jour;

            Serreurs er = new Serreurs("Erreur sur l'écriture d'une commande.", "Client.update()");
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
       
    }
}