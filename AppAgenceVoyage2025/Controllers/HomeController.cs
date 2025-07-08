using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AppAgenceVoyage2025.App_Start;
using AppAgenceVoyage2025.Models.App_LocalResources;
namespace AppAgenceVoyage2025.Controllers
{
    public class HomeController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            //Utils.WriteLogSystem("Juste un test pour AppAgenceVoyage", "HomeController-Index");
            this.Flash(ResourceFr.TextBienvenue, Flashlevel.Success);
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}