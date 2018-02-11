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
    public class Vendeur
    {
        // GET: Vendor
        private String noVendeur;
        private String noVendChefEq;
        private String nomVend;
        private String prenomVend;
        private String dateEmbau;
        private String villeVend;
        private String salaireVend;
        private String commission;
        public String NoVendeur
        {
            get { return noVendeur; }
            set { noVendeur = value; }
        }

        public String NomVend
        {
            get { return nomVend; }
            set { nomVend = value; }
        }

        public String NoVendChefEq
        {
            get { return noVendChefEq; }
            set { noVendChefEq = value; }
        }

        public String PrenomVend
        {
            get { return prenomVend; }
            set { prenomVend = value; }
        }

        public String DateEmbau
        {
            get { return dateEmbau; }
            set { dateEmbau = value; }
        }

        public String VilleVend
        {
            get { return villeVend; }
            set { villeVend = value; }
        }

        public String SalaireVend
        {
            get { return salaireVend; }
            set { salaireVend = value; }
        }

        public String Commission
        {
            get { return commission; }
            set { commission = value; }
        }

        public Vendeur()
        {
            noVendeur = "";
            noVendChefEq = "";
            nomVend = "";
            prenomVend = "";
            dateEmbau = "";
            villeVend = "";
            salaireVend = "";
            commission = "";
        }

        public Vendeur(string noV, string noVendChef, string nom, string prenom, string dateE, string villeE, String sal, String comm)
        {
            noVendeur = noV;
            noVendChefEq = noVendChef;
            nomVend = nom;
            prenomVend = prenom;
            dateEmbau = dateE;
            villeVend = villeE;
            salaireVend = sal;
            commission = comm;

        }
        public static Vendeur getVendeur(String numVend)
        {

            String mysql;
            DataTable dt;
            Serreurs er = new Serreurs("Erreur sur recherche d'un vendeur.", "Vendeur.RechercheUnVendeur()");
            try
            {

                mysql = "SELECT NO_VEND_CHEF_EQ, NOM_VEND, PRENOM_VEND,";
                mysql += "DATE_EMBAU, VILLE_VEND, SALAIRE_VEND, COMMISSION ";
                mysql += "FROM vendeur WHERE NO_VENDEUR='" + numVend + "'";
                dt = DBInterface.Lecture(mysql, er);

                if (dt.IsInitialized && dt.Rows.Count > 0)
                {
                    Vendeur vendeur = new Vendeur();
                    DataRow dataRow = dt.Rows[0];
                    vendeur.NoVendeur = numVend;
                    vendeur.NomVend = dataRow[1].ToString();
                    vendeur.NoVendChefEq = dataRow[0].ToString();
                    vendeur.PrenomVend = dataRow[2].ToString();
                    vendeur.DateEmbau = dataRow[3].ToString();
                    vendeur.VilleVend = dataRow[4].ToString();
                    vendeur.SalaireVend = dataRow[5].ToString();
                    vendeur.Commission = dataRow[6].ToString();

                    return vendeur;
                }
                else
                    return null;
            }
            catch (MySqlException e)
            {
                throw new MonException(er.MessageUtilisateur(), er.MessageApplication(), e.Message);
            }

        }

        public static IEnumerable<Vendeur> getVendeurs()
        {
            IEnumerable<Vendeur> vendeurs = new List<Vendeur>();
            DataTable dt;
            Vendeur vendeur;
            Serreurs er = new Serreurs("Erreur sur lecture des vendeurs.", "VendeursList.getVendeurs()");
            try
            {
                String mysql = "SELECT NO_VENDEUR, NO_VEND_CHEF_EQ, NOM_VEND, PRENOM_VEND, DATE_EMBAU, VILLE_VEND, SALAIRE_VEND, " +
                               "COMMISSION FROM vendeur ORDER BY NO_VENDEUR";

                dt = DBInterface.Lecture(mysql, er);

                foreach (DataRow dataRow in dt.Rows)
                {
                    vendeur = new Vendeur();
                    vendeur.SalaireVend = dataRow[6].ToString();
                    vendeur.NoVendChefEq = dataRow[1].ToString();
                    vendeur.NoVendeur = dataRow[0].ToString();
                    vendeur.NomVend = dataRow[2].ToString();
                    vendeur.PrenomVend = dataRow[3].ToString();
                    vendeur.DateEmbau = dataRow[4].ToString();
                    vendeur.VilleVend = dataRow[5].ToString();
                    vendeur.Commission = dataRow[7].ToString();

                    ((List<Vendeur>)vendeurs).Add(vendeur);
                }

                return vendeurs;
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
        public static List<String> LectureNoVendeur()
        {
            List<String> mesNumeros = new List<String>();
            DataTable dt;
            Serreurs er = new Serreurs("Erreur sur lecture du vendeur.", "Vendeur.LectureNoVendeur()");
            try
            {

                String mysql = "SELECT DISTINCT NO_VENDEUR FROM vendeur ORDER BY NO_VENDEUR";
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
    }
}