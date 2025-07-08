using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AppAgenceVoyage.Models;
using AppAgenceVoyage2025.App_Start;
using PagedList;

namespace AppAgenceVoyage2025.Controllers
{
    public class AgencesController : Controller
    {
        private BdAgenceVoyageContext db = new BdAgenceVoyageContext();
        const int pageSize = 10;

        // GET: Agences
        public ActionResult Index(string adresse, string ninea, string rccm, int? page)
        {
            ViewBag.adresse = adresse ?? string.Empty;
            ViewBag.ninea = ninea ?? string.Empty;
            ViewBag.rccm = rccm ?? string.Empty;

            var liste = db.Agences.AsQueryable();

            if (!string.IsNullOrEmpty(adresse))
                liste = liste.Where(a => a.AdresseAgence.ToLower().Contains(adresse.ToLower()));

            if (!string.IsNullOrEmpty(ninea))
                liste = liste.Where(a => a.NineaGestionnaire.ToLower().Contains(ninea.ToLower()));

            if (!string.IsNullOrEmpty(rccm))
                liste = liste.Where(a => a.RccmGestionnaire.ToLower().Contains(rccm.ToLower()));

            int pageNumber = page ?? 1;

            var resultats = liste.OrderBy(a => a.AdresseAgence).ToPagedList(pageNumber, pageSize);

            return View(resultats);
        }

        public JsonResult FiltrerAgences(string adresse, string ninea, string rccm, int? page)
        {
            var liste = db.Agences.AsQueryable();

            if (!string.IsNullOrEmpty(adresse))
                liste = liste.Where(a => a.AdresseAgence.ToLower().Contains(adresse.ToLower()));

            if (!string.IsNullOrEmpty(ninea))
                liste = liste.Where(a => a.NineaGestionnaire.ToLower().Contains(ninea.ToLower()));

            if (!string.IsNullOrEmpty(rccm))
                liste = liste.Where(a => a.RccmGestionnaire.ToLower().Contains(rccm.ToLower()));

            int pageNumber = page ?? 1;
            var resultats = liste.OrderBy(a => a.AdresseAgence).ToPagedList(pageNumber, pageSize);

            var data = new
            {
                agences = resultats.Select(a => new
                {
                    a.AdresseAgence,
                    a.NineaGestionnaire,
                    a.RccmGestionnaire,
                    a.Latitude,
                    a.Longitude
                }).ToList(),
                pagination = new
                {
                    currentPage = resultats.PageNumber,
                    totalPages = resultats.PageCount
                }
            };

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        // GET: Agences/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Agence agence = db.Agences.Find(id);
            if (agence == null)
            {
                return HttpNotFound();
            }
            return View(agence);
        }

        // GET: Agences/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Agences/Create
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdAgence,AdresseAgence,Longitude,Latitude,NineaGestionnaire,RccmGestionnaire")] Agence agence)
        {
            if (ModelState.IsValid)
            {
                db.Agences.Add(agence);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(agence);
        }

        // GET: Agences/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Agence agence = db.Agences.Find(id);
            if (agence == null)
            {
                return HttpNotFound();
            }
            return View(agence);
        }

        // POST: Agences/Edit/5
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdAgence,AdresseAgence,Longitude,Latitude,NineaGestionnaire,RccmGestionnaire")] Agence agence)
        {
            if (ModelState.IsValid)
            {
                db.Entry(agence).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(agence);
        }

        // GET: Agences/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Agence agence = db.Agences.Find(id);
            if (agence == null)
            {
                return HttpNotFound();
            }
            return View(agence);
        }

        // POST: Agences/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Agence agence = db.Agences.Find(id);
            db.Agences.Remove(agence);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
