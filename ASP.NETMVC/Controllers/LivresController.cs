using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ASP.NETMVC.Models;

namespace ASP.NETMVC.Controllers
{
    public class LivresController : Controller
    {
        private BIBLIOTHEQUEEntities db = new BIBLIOTHEQUEEntities();

        // GET: Livres
        public ActionResult Index(string  genre, string annee,string auteur)
        {
            //liste genre
            var GenreLst = new List<string>();

            var GenreQry = from g in db.LIVRES
                         orderby g.GENRES.LIBELLE_GENRE
                         select g.GENRES.LIBELLE_GENRE;

            GenreLst.AddRange((IEnumerable<string>)GenreQry.Distinct());
            ViewBag.genre = new SelectList(GenreLst);

            //liste auteur
            var auteurLst = new List<string>();
            var auteurQry = from a in db.LIVRES
                            orderby a.AUTEURS.NOM_AUTEUR
                            select a.AUTEURS.NOM_AUTEUR;
            auteurLst.AddRange(auteurQry.Distinct());
            ViewBag.auteur = new SelectList(auteurLst);

            //liste courant 
            
            //liste livre
            var livres = from l in db.LIVRES
                        select l;
            //filtrage annee
            if (!String.IsNullOrEmpty(annee))
            { 
                livres = livres.Where(s => s.ANNEE_EDITION.Value.Year.ToString() == annee );
            }
            //filtrage genre
            if (!string.IsNullOrEmpty(genre))
            {
                livres = livres.Where(x => x.GENRES.LIBELLE_GENRE == genre);
            }
            //filtrage auteur
            if (!string.IsNullOrEmpty(auteur))
            {    
                livres = livres.Where(x => x.AUTEURS.NOM_AUTEUR == auteur);
            }
            return View(livres);
        }

        // GET: Livres/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LIVRES lIVRES = db.LIVRES.Find(id);
            if (lIVRES == null)
            {
                return HttpNotFound();
            }
            return View(lIVRES);
        }

        // GET: Livres/Create
        public ActionResult Create()
        {
            ViewBag.ID_AUTEUR = new SelectList(db.AUTEURS, "ID_AUTEUR", "NOM_AUTEUR");
            ViewBag.ID_GENRE = new SelectList(db.GENRES, "ID_GENRE", "LIBELLE_GENRE");
            return View();
        }

        // POST: Livres/Create
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_LIVRE,ISBN,TITRE,EDITEUR,ANNEE_EDITION,ID_AUTEUR,ID_GENRE")] LIVRES lIVRES,HttpPostedFileBase LIVRE)
        {
            
            if (ModelState.IsValid)
            {

                var fileName = Path.GetFileName(LIVRE.FileName);
                var path = Path.Combine(Server.MapPath("~/pdf"), fileName);
                LIVRE.SaveAs(path);
                lIVRES.LIVRE = fileName;
                lIVRES.URL_LIVRE = "/pdf";
                //string path = Server.MapPath("~/pdf");
                //    try
                //    {
                //        if (Request.Files.Count > 0)
                //        {
                //            var fichier = Request.Files[0];
                //            if (fichier != null && fichier.ContentLength > 0)
                //            {
                //                var fileName = Path.GetFileName(fichier.FileName);
                //                var path = Path.Combine(Server.MapPath("~/pdf"), fileName);
                //                fichier.SaveAs(path);
                //                lIVRES.LIVRE = fileName;
                //                lIVRES.URL_LIVRE = "/pdf";
                //            }
                //        }
                //        ViewBag.Message = "File Uploaded Successfully!!";
                //    }
                //    catch
                //    {
                //        ViewBag.Message = "File upload failed!!";
                //        return View();
                //    }
                db.LIVRES.Add(lIVRES);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_AUTEUR = new SelectList(db.AUTEURS, "ID_AUTEUR", "NOM_AUTEUR", lIVRES.ID_AUTEUR);
            ViewBag.ID_GENRE = new SelectList(db.GENRES, "ID_GENRE", "LIBELLE_GENRE", lIVRES.ID_GENRE);
            return View(lIVRES);
        }

        // GET: Livres/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LIVRES lIVRES = db.LIVRES.Find(id);
            if (lIVRES == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_AUTEUR = new SelectList(db.AUTEURS, "ID_AUTEUR", "NOM_AUTEUR", lIVRES.ID_AUTEUR);
            ViewBag.ID_GENRE = new SelectList(db.GENRES, "ID_GENRE", "LIBELLE_GENRE", lIVRES.ID_GENRE);
            return View(lIVRES);
        }

        // POST: Livres/Edit/5
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_LIVRE,ISBN,TITRE,EDITEUR,ANNEE_EDITION,ID_AUTEUR,ID_GENRE")] LIVRES lIVRES)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lIVRES).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_AUTEUR = new SelectList(db.AUTEURS, "ID_AUTEUR", "NOM_AUTEUR", lIVRES.ID_AUTEUR);
            ViewBag.ID_GENRE = new SelectList(db.GENRES, "ID_GENRE", "LIBELLE_GENRE", lIVRES.ID_GENRE);
            return View(lIVRES);
        }

        // GET: Livres/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LIVRES lIVRES = db.LIVRES.Find(id);
            if (lIVRES == null)
            {
                return HttpNotFound();
            }
            return View(lIVRES);
        }

        // POST: Livres/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LIVRES lIVRES = db.LIVRES.Find(id);
            db.LIVRES.Remove(lIVRES);
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
