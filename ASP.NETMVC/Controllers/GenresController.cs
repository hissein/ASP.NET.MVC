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
    public class GenresController : Controller
    {
        private  BIBLIOTHEQUEEntities db = new BIBLIOTHEQUEEntities();

        // GET: Genres
        public ActionResult Index()
        {
            return View(db.GENRES.ToList());
        }

        // GET: Genres/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GENRES gENRES = db.GENRES.Find(id);
            if (gENRES == null)
            {
                return HttpNotFound();
            }
            return View(gENRES);
        }

        // GET: Genres/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Genres/Create
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_GENRE,LIBELLE_GENRE")] GENRES gENRES)
        {
            if (ModelState.IsValid)
            {
                db.GENRES.Add(gENRES);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(gENRES);
        }

        // GET: Genres/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GENRES gENRES = db.GENRES.Find(id);
            if (gENRES == null)
            {
                return HttpNotFound();
            }
            return View(gENRES);
        }

        // POST: Genres/Edit/5
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_GENRE,LIBELLE_GENRE")] GENRES gENRES)
        {
            if (ModelState.IsValid)
            {
                db.Entry(gENRES).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(gENRES);
        }

        // GET: Genres/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GENRES gENRES = db.GENRES.Find(id);
            if (gENRES == null)
            {
                return HttpNotFound();
            }
            return View(gENRES);
        }

        // POST: Genres/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            GENRES gENRES = db.GENRES.Find(id);
            db.GENRES.Remove(gENRES);
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
