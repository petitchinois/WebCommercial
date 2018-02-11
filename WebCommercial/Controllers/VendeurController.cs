using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebCommercial.Models.Metier;
using WebCommercial.Models.MesExceptions;

namespace WebCommercial.Controllers
{
    public class VendeurController : Controller
    {
        //GET: Vendeur
        public ActionResult Index()
        {
            IEnumerable<Vendeur> vendeurs = null;

            try
            {
                vendeurs = Vendeur.getVendeurs();
            }
            catch (MonException e)
            {
                ModelState.AddModelError("Erreur", "Erreur lors de la récupération des vendeurs : " + e.Message);
                return View("Error");
            }

            return View(vendeurs);
        }
    }
}