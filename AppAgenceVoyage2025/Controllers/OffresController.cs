using System.Linq;
using System.Web.Mvc;
using AppAgenceVoyage.Models;

namespace AppAgenceVoyage2025.Controllers
{
    public class OffresController : Controller
    {
        private readonly BdAgenceVoyageContext db = new BdAgenceVoyageContext();

        // Afficher la vue Index avec les agences
        public ActionResult Index()
        {
            var offres = db.Offres.Include("Agence").ToList(); // Charger les offres avec leurs agences
            var agences = db.Agences.ToList(); // Récupérer toutes les agences
            ViewBag.Agences = agences; // Passer les agences à la vue
            return View(offres);
        }

        // Liste des offres
        public JsonResult List()
        {
            var offres = db.Offres.Select(o => new
            {
                o.IdOffre,
                o.DescriptionOffre,
                o.PrixJourOffre,
                o.Disponibilite,
                o.IdAgence,
                NomAgence = o.Agence.AdresseAgence
            }).ToList();

            return Json(offres, JsonRequestBehavior.AllowGet);
        }

        // Ajouter une offre
        [HttpPost]
        public JsonResult Add(Offre offre)
        {
            if (db.Agences.Find(offre.IdAgence) == null)
            {
                return Json(new { success = false, message = "L'agence associée n'existe pas." });
            }

            if (ModelState.IsValid)
            {
                db.Offres.Add(offre);
                db.SaveChanges();
                return Json(new { success = true, message = "Offre ajoutée avec succès" });
            }
            else
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return Json(new { success = false, message = string.Join(", ", errors) });
            }
        }

        // Modifier une offre
        [HttpPost]
        public JsonResult Update(Offre offre)
        {
            var modifieOffre = db.Offres.Find(offre.IdOffre);
            if (modifieOffre != null)
            {
                modifieOffre.DescriptionOffre = offre.DescriptionOffre;
                modifieOffre.PrixJourOffre = offre.PrixJourOffre;
                modifieOffre.Disponibilite = offre.Disponibilite;
                modifieOffre.IdAgence = offre.IdAgence;

                if (ModelState.IsValid)
                {
                    db.SaveChanges();
                    return Json(new { success = true, message = "Offre mise à jour avec succès" });
                }
                else
                {
                    var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                    return Json(new { success = false, message = string.Join(", ", errors) });
                }
            }
            return Json(new { success = false, message = "Offre non trouvée" });
        }

        // Supprimer une offre
        [HttpPost]
        public JsonResult Delete(int id)
        {
            var offre = db.Offres.Find(id);
            if (offre != null)
            {
                db.Offres.Remove(offre);
                db.SaveChanges();
                return Json(new { success = true, message = "Offre supprimée avec succès" });
            }
            return Json(new { success = false, message = "Offre non trouvée" });
        }

        // Récupérer une offre par ID
        public JsonResult GetByID(int id)
        {
            var offre = db.Offres.Select(o => new
            {
                o.IdOffre,
                o.DescriptionOffre,
                o.PrixJourOffre,
                o.Disponibilite,
                o.IdAgence,
                NomAgence = o.Agence.AdresseAgence
            }).FirstOrDefault(o => o.IdOffre == id);

            if (offre != null)
            {
                return Json(new { success = true, offre }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { success = false, message = "Offre non trouvée" }, JsonRequestBehavior.AllowGet);
        }
    }
}
