using System.Linq;
using System.Web.Mvc;
using AppAgenceVoyage.Models;

namespace AppAgenceVoyage2025.Controllers
{
    public class VoyagesController : Controller
    {
        private readonly BdAgenceVoyageContext db = new BdAgenceVoyageContext();

        // Afficher la vue Index
        public ActionResult Index()
        {
            return View();
        }

        // Liste des voyages
        public JsonResult List()
        {
            var voyages = db.Voyages.ToList();  
            return Json(voyages, JsonRequestBehavior.AllowGet);
        }

        // Ajouter un voyage
        [HttpPost]
        public JsonResult Add(Voyage voyage)
        {
            if (ModelState.IsValid)
            {
                db.Voyages.Add(voyage);
                db.SaveChanges();
                return Json(new { success = true, message = "Voyage ajouté avec succès" });
            }
            else
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return Json(new { success = false, message = string.Join(", ", errors) });
            }
        }

        // Modifier un voyage
        [HttpPost]
        public JsonResult Update(Voyage voyage)
        {
            var modifieVoyage = db.Voyages.Find(voyage.IdVoyage);
            if (modifieVoyage != null)
            {
                modifieVoyage.Destination = voyage.Destination;
                modifieVoyage.DateDebut = voyage.DateDebut;
                modifieVoyage.DateFin = voyage.DateFin;
                modifieVoyage.Prix = voyage.Prix;

                if (ModelState.IsValid)
                {
                    db.SaveChanges();
                    return Json(new { success = true, message = "Voyage mis à jour avec succès" });
                }
                else
                {
                    var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                    return Json(new { success = false, message = string.Join(", ", errors) });
                }
            }
            return Json(new { success = false, message = "Voyage non trouvé" });
        }

        // Supprimer un voyage
        [HttpPost]
        public JsonResult Delete(int id)
        {
            var voyage = db.Voyages.Find(id);
            if (voyage != null)
            {
                db.Voyages.Remove(voyage);
                db.SaveChanges();
                return Json(new { success = true, message = "Voyage supprimé avec succès" });
            }
            return Json(new { success = false, message = "Voyage non trouvé" });
        }

        // Récupérer un voyage par ID
        public JsonResult GetByID(int id)
        {
            var voyage = db.Voyages.Find(id);
            if (voyage != null)
            {
                return Json(new
                {
                    success = true,
                    IdVoyage = voyage.IdVoyage,
                    Destination = voyage.Destination,
                    DateDebut = voyage.DateDebut.ToString("yyyy-MM-dd"),
                    DateFin = voyage.DateFin.ToString("yyyy-MM-dd"),
                    Prix = voyage.Prix
                }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { success = false, message = "Voyage non trouvé" }, JsonRequestBehavior.AllowGet);
        }
    }
}
