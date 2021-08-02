using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ASP.NETMVC.Models;

namespace ASP.NETMVC.Controllers
{
    public class AuteursController : Controller
    {
        private  BIBLIOTHEQUEEntities db = new BIBLIOTHEQUEEntities();

        // GET: Auteurs
        public ActionResult Index()
        {
            return View(db.AUTEURS.ToList());
        }

        // GET: Auteurs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AUTEURS aUTEURS = db.AUTEURS.Find(id);
            if (aUTEURS == null)
            {
                return HttpNotFound();
            }
            return View(aUTEURS);
        }

        // GET: Auteurs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Auteurs/Create
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_AUTEUR,NOM_AUTEUR")] AUTEURS aUTEURS)
        {
            if (ModelState.IsValid)
            {
                db.AUTEURS.Add(aUTEURS);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(aUTEURS);
        }

        // GET: Auteurs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AUTEURS aUTEURS = db.AUTEURS.Find(id);
            if (aUTEURS == null)
            {
                return HttpNotFound();
            }
            return View(aUTEURS);
        }

        // POST: Auteurs/Edit/5
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_AUTEUR,NOM_AUTEUR")] AUTEURS aUTEURS)
        {
            if (ModelState.IsValid)
            {
                db.Entry(aUTEURS).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(aUTEURS);
        }

        // GET: Auteurs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AUTEURS aUTEURS = db.AUTEURS.Find(id);
            if (aUTEURS == null)
            {
                return HttpNotFound();
            }
            return View(aUTEURS);
        }

        // POST: Auteurs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AUTEURS aUTEURS = db.AUTEURS.Find(id);
            db.AUTEURS.Remove(aUTEURS);
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
