using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebCommercial.Models.Metier;
using WebCommercial.Models.MesExceptions;

namespace WebCommercial.Controllers
{
    public class DetailCommandeController : Controller
    {
        // GET: DetailCde
        public ActionResult Index(string id)
        {
            try
            {


                DetailCommande.getDetailCo(id);
                return View();
            }
            catch (MonException e)
            {
                return HttpNotFound();
            }
        }
        public ActionResult Detail(string id)
        {
            try
            {


                DetailCommande.getDetailCo(id);
                return View();
            }
            catch (MonException e)
            {
                return HttpNotFound();
            }

        }
    }
}