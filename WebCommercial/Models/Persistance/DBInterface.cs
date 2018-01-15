using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebCommercial.Models.Persistance
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using MySql.Data.MySqlClient;
    using System.Data;
    using WebCommercial.Models.MesExceptions;

    namespace WebApplication1.Models.Persistance
    {
        public class DBInterface
        {
            /// <summary>
            /// Exécution de la requête demandée en paramètre, req, 
            /// et retour du resultat : un DataTable
            /// Si tout se passe bien la connexion est prête à être fermée
            /// par le client qui utilisera cette connexion
            /// </summary>
            /// <param name="req">RequêteMySql à exécuter</param>
            /// <returns></returns>
            public static DataTable Lecture(String req, Serreurs er)
            {
                MySqlConnection cnx = null;
                try
                {
                    cnx = Connexion.getInstance().getConnexion();
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.Connection = cnx;
                    cmd.CommandText = req;
                    MySqlDataAdapter da = new MySqlDataAdapter();
                    da.SelectCommand = cmd;

                    // Construire le DataSet
                    DataSet ds = new DataSet();
                    da.Fill(ds, "resultat");
                    cnx.Close();

                    // Retourner la table
                    return (ds.Tables["resultat"]);
                }
                catch (MonException me)
                {
                    throw (me);
                }
                catch (Exception e)
                {

                    throw new MonException(er.MessageUtilisateur(), er.MessageApplication(), e.Message);
                }
                finally
                {
                    // S'il y a eu un problème, la connexion
                    // peut être encore ouverte, dans ce cas
                    // il faut la fermer.                 
                    if (cnx != null)
                        cnx.Close();
                }
            }

            /// <summary>
            /// Insertion d'une requête avec OleDb
            /// </summary>
            /// <param name="requete"></param>
            public static void Insertion_Donnees(String requete)
            {
                MySqlConnection cnx = null;
                try
                {
                    // On ouvre une transaction 
                    cnx = Connexion.getInstance().getConnexion();
                    MySqlTransaction OleTrans = cnx.BeginTransaction();
                    MySqlCommand OleCmd = new MySqlCommand();
                    OleCmd = cnx.CreateCommand();
                    OleCmd.Transaction = OleTrans;
                    OleCmd.CommandText = requete;
                    OleCmd.ExecuteNonQuery();
                    OleTrans.Commit();
                }
                catch (MySqlException uneException)
                {
                    throw new MonException(uneException.Message, "Insertion", "SQL");
                }
            }
        }
    }
}