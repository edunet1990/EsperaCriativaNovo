using EsperaCriativa.Contexto;
using EsperaCriativa.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace EsperaCriativa.Planejamentos
{
    public class ExperienciasController : Controller
    {
        private EsperaCriativaContext db = new EsperaCriativaContext();

        // GET: Experiencias
        public ActionResult Index()
        {
            return View(db.Experiencias.ToList());
        }

        // GET: Experiencias/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Experiencia experiencia = db.Experiencias.Find(id);
            if (experiencia == null)
            {
                return HttpNotFound();
            }
            return View(experiencia);
        }

        // GET: Experiencias/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Experiencias/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ExperienciaID,InfoExperiencia")] Experiencia experiencia)
        {
            if (ModelState.IsValid)
            {
                db.Experiencias.Add(experiencia);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(experiencia);
        }

        // GET: Experiencias/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Experiencia experiencia = db.Experiencias.Find(id);
            if (experiencia == null)
            {
                return HttpNotFound();
            }
            return View(experiencia);
        }

        // POST: Experiencias/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ExperienciaID,InfoExperiencia")] Experiencia experiencia)
        {
            if (ModelState.IsValid)
            {
                db.Entry(experiencia).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(experiencia);
        }

        // GET: Experiencias/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Experiencia experiencia = db.Experiencias.Find(id);
            if (experiencia == null)
            {
                return HttpNotFound();
            }
            return View(experiencia);
        }

        // POST: Experiencias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Experiencia experiencia = db.Experiencias.Find(id);
            db.Experiencias.Remove(experiencia);
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