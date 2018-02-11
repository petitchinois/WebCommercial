﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebCommercial.Models.Metier;
using WebCommercial.Models.MesExceptions;

namespace WebCommercial.Controllers
{
    public class CommandeController : Controller
    {

        public ActionResult Index()
        {
            IEnumerable<Commandes> comms = null;

            try
            {
                comms = Commandes.getCommands();
            }
            catch (MonException e)
            {
                ModelState.AddModelError("Erreur", "Erreur lors de la récupération des commandes : " + e.Message);
                return View("Error");
            }

            return View(comms);
        }

        public ActionResult Modifier(string id)
        {
            try
            {
                Commandes uneCde = Commandes.getCommande(id);
                return View(uneCde);
            }
            catch (MonException e)
            {
                return HttpNotFound();
            }
        }

        [HttpPost]
        public ActionResult Modifier(Commandes uneCde)
        {
            try
            {
                

                Commandes.updateCommande(uneCde);
                return View();
            }
            catch (MonException e)
            {
                return HttpNotFound();
            }
        }

        public ActionResult Ajouter()
        {
            try
            {
                //gerer la dropdown list de client
                List<String> noClientList = new List<String>();
                noClientList = (Clientel.LectureNoClient());
                ViewBag.ListOfNoClient = noClientList;

                //gerer la dropdown list de Vendeur
                List<String> noVendeurList = new List<String>();
                noVendeurList = (Vendeur.LectureNoVendeur());
                ViewBag.ListOfNoVendeur = noVendeurList;

                return View("");
            }
            catch (MonException e)
            {
                return HttpNotFound();
            }
        }



        [HttpPost]
        public ActionResult Ajouter(String noVendeur, String noClient, String noCommande, String dateCde, String Facture)
        {
            try
            {
               

                Commandes uneCde = new Commandes(noCommande, noClient, noVendeur, dateCde, Facture);
                Commandes.addCommande(uneCde);
                

                return RedirectToAction("Index", "Commande");
            }
            catch (MonException e)
            {
                return HttpNotFound();
            }
        }

        public ActionResult Detail(string id)
        {
            IEnumerable<Commandes> uneCde = null;

            try
            {
                uneCde = Commandes.getListeDeCommande(id);

                return View(uneCde);
            }
            catch (MonException e)
            {
                ModelState.AddModelError("Erreur", "Erreur lors de la récupération des commandes : " + e.Message);
                return View("Error");
            }
        }




    }
}