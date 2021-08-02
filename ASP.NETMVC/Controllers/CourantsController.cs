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
    public class CourantsController : Controller
    {
        private  BIBLIOTHEQUEEntities db = new BIBLIOTHEQUEEntities();

        // GET: Courants
        public ActionResult Index()
        {
            var cOURANTS = db.COURANTS.Include(c => c.GENRES);
            return View(cOURANTS.ToList());
        }

        // GET: Courants/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            COURANTS cOURANTS = db.COURANTS.Find(id);
            if (cOURANTS == null)
            {
                return HttpNotFound();
            }
            return View(cOURANTS);
        }

        // GET: Courants/Create
        public ActionResult Create()
        {
            ViewBag.ID_GENRE = new SelectList(db.GENRES, "ID_GENRE", "LIBELLE_GENRE");
            return View();
        }

        // POST: Courants/Create
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_COURANT,ID_GENRE,LIBELLE_COURANT")] COURANTS cOURANTS)
        {
            if (ModelState.IsValid)
            {
                db.COURANTS.Add(cOURANTS);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_GENRE = new SelectList(db.GENRES, "ID_GENRE", "LIBELLE_GENRE", cOURANTS.ID_GENRE);
            return View(cOURANTS);
        }

        // GET: Courants/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            COURANTS cOURANTS = db.COURANTS.Find(id);
            if (cOURANTS == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_GENRE = new SelectList(db.GENRES, "ID_GENRE", "LIBELLE_GENRE", cOURANTS.ID_GENRE);
            return View(cOURANTS);
        }

        // POST: Courants/Edit/5
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_COURANT,ID_GENRE,LIBELLE_COURANT")] COURANTS cOURANTS)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cOURANTS).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_GENRE = new SelectList(db.GENRES, "ID_GENRE", "LIBELLE_GENRE", cOURANTS.ID_GENRE);
            return View(cOURANTS);
        }

        // GET: Courants/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            COURANTS cOURANTS = db.COURANTS.Find(id);
            if (cOURANTS == null)
            {
                return HttpNotFound();
            }
            return View(cOURANTS);
        }

        // POST: Courants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            COURANTS cOURANTS = db.COURANTS.Find(id);
            db.COURANTS.Remove(cOURANTS);
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
