using System;
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
    }
}