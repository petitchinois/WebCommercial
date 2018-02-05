using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MySql.Data.MySqlClient;

using System.Data;

using WebCommercial.Models.MesExceptions;
using WebCommercial.Models.Persistance.WebApplication1.Models.Persistance;

namespace WebCommercial.Models.Metier
{
    public class DetailCommande : Controller
    {

        private String noCommand;
        private String noArticle;
        private String qteCdee;
        private String livree; 
        // GET: DetailCommande

        public String NoCommand
        {
            get { return noCommand;  }
            set { noCommand = value;  }
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
        public ActionResult Index()
        {
            return View();
        }

        public DetailCommande()
        {
            noArticle = "";
            noCommand = "";
            qteCdee = "";
            livree = "";
        }

        public DetailCommande(string no, string noc, string qte, string l)
        {
            noArticle = no;
            noCommand = noc;
            qteCdee = qte;
            livree = l;
        }
        public static IEnumerable<DetailCommande> getDetailCo(String numCo)
        {
            IEnumerable<DetailCommande> comms = new List<DetailCommande>();
            DataTable dt;
            DetailCommande comm;
            Serreurs er = new Serreurs("Erreur sur lecture des commandes.", "ClientsList.getClients()");
            try
            {
                String mysql = "SELECT NO_COMMAND, NO_ARTICLE, QTE_CDEE, LIVREE " +
                               "FROM detail_cde WHERE NO_COMMAND = '"+numCo+"';";

                dt = DBInterface.Lecture(mysql, er);

                foreach (DataRow dataRow in dt.Rows)
                {
                    comm = new DetailCommande();
                    comm.NoCommand = dataRow[0].ToString();
                    comm.NoArticle = dataRow[1].ToString();
                    comm.QteCdee = dataRow[2].ToString();
                    comm.Livree = dataRow[3].ToString();
                 


                    ((List<DetailCommande>)comms).Add(comm);
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
    }
}