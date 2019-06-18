using EsperaCriativa.Contexto;
using EsperaCriativa.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace EsperaCriativa.Planejamentos_Fixo
{
    public class PlanejamentosFixoController : Controller
    {
        private EsperaCriativaContext db = new EsperaCriativaContext();

        // GET: PlanejamentosFixo
        public ActionResult Index()
        {
            var planejamentosFixo = db.PlanejamentosFixo.Include(p => p.Usuario);
            return View(planejamentosFixo.ToList());
        }

        // GET: PlanejamentosFixo/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlanejamentoFixo planejamentoFixo = db.PlanejamentosFixo.Find(id);
            if (planejamentoFixo == null)
            {
                return HttpNotFound();
            }
            return View(planejamentoFixo);
        }

        // GET: PlanejamentosFixo/Create
        public ActionResult Create()
        {
            ViewBag.UsuarioId = new SelectList(db.Usuarios, "Id", "Nome");
            return View();
        }

        // POST: PlanejamentosFixo/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Informacao,Data,UsuarioId")] PlanejamentoFixo planejamentoFixo)
        {
            if (ModelState.IsValid)
            {
                db.PlanejamentosFixo.Add(planejamentoFixo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UsuarioId = new SelectList(db.Usuarios, "Id", "Nome", planejamentoFixo.UsuarioId);
            return View(planejamentoFixo);
        }

        // GET: PlanejamentosFixo/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlanejamentoFixo planejamentoFixo = db.PlanejamentosFixo.Find(id);
            if (planejamentoFixo == null)
            {
                return HttpNotFound();
            }
            ViewBag.UsuarioId = new SelectList(db.Usuarios, "Id", "Nome", planejamentoFixo.UsuarioId);
            return View(planejamentoFixo);
        }

        // POST: PlanejamentosFixo/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Informacao,Data,UsuarioId")] PlanejamentoFixo planejamentoFixo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(planejamentoFixo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UsuarioId = new SelectList(db.Usuarios, "Id", "Nome", planejamentoFixo.UsuarioId);
            return View(planejamentoFixo);
        }

        // GET: PlanejamentosFixo/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlanejamentoFixo planejamentoFixo = db.PlanejamentosFixo.Find(id);
            if (planejamentoFixo == null)
            {
                return HttpNotFound();
            }
            return View(planejamentoFixo);
        }

        // POST: PlanejamentosFixo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PlanejamentoFixo planejamentoFixo = db.PlanejamentosFixo.Find(id);
            db.PlanejamentosFixo.Remove(planejamentoFixo);
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