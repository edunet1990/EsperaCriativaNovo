using EsperaCriativa.Contexto;
using EsperaCriativa.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace EsperaCriativa.Controllers.Gráficos
{
    public class InsightsController : Controller
    {
        private EsperaCriativaContext db = new EsperaCriativaContext();

        // GET: Insights
        public ActionResult Index()
        {
            var insights = db.Insights.Include(i => i.Usuario);
            return View(insights.ToList());
        }

        // GET: GraficosInsights
        [Authorize]
        public ActionResult GetAll()
        {
            int recebeSim = 0;
            int recebeNao = 0;
            Insight graficosInsight = new Insight();
            List<int> listaRecebeOsDoisValores = new List<int>();

            for (int i = 0; i <= 1; i++)
            {

            }
            foreach (var item in db.Insights)
            {
                if (item.SurgiuInsightSim == 1)
                {
                    recebeSim++;
                }

                if (item.SurgiuInsightNao == 1)
                {
                    recebeNao++;
                }
            }
            listaRecebeOsDoisValores.Add(recebeSim);
            listaRecebeOsDoisValores.Add(recebeNao);


            return View(listaRecebeOsDoisValores.ToList());
        }


        // GET: Insights/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Insight insight = db.Insights.Find(id);
            if (insight == null)
            {
                return HttpNotFound();
            }
            return View(insight);
        }

        // GET: Insights/Create
        public ActionResult Create()
        {
            ViewBag.UsuarioId = new SelectList(db.Usuarios, "Id", "Nome");
            return View();
        }

        // POST: Insights/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,SurgiuInsightSim,SurgiuInsightNao,Data,UsuarioId")] Insight insight)
        {
            if (ModelState.IsValid)
            {
                db.Insights.Add(insight);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UsuarioId = new SelectList(db.Usuarios, "Id", "Nome", insight.UsuarioId);
            return View(insight);
        }

        // GET: Insights/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Insight insight = db.Insights.Find(id);
            if (insight == null)
            {
                return HttpNotFound();
            }
            ViewBag.UsuarioId = new SelectList(db.Usuarios, "Id", "Nome", insight.UsuarioId);
            return View(insight);
        }

        // POST: Insights/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,SurgiuInsightSim,SurgiuInsightNao,Data,UsuarioId")] Insight insight)
        {
            if (ModelState.IsValid)
            {
                db.Entry(insight).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UsuarioId = new SelectList(db.Usuarios, "Id", "Nome", insight.UsuarioId);
            return View(insight);
        }

        // GET: Insights/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Insight insight = db.Insights.Find(id);
            if (insight == null)
            {
                return HttpNotFound();
            }
            return View(insight);
        }

        // POST: Insights/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Insight insight = db.Insights.Find(id);
            db.Insights.Remove(insight);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: GraficosInsights/Create
        [Authorize]
        public ActionResult ShowInsight()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public void InsertInsight(String movieName, int formularioID)
        {
            var InsightRepetido = 0;
            var InsightRepetidoId = 0;
            Insight graficosInsight = new Insight();
            var GetIdUser = from b in db.Usuarios where b.Nome == User.Identity.Name select b;

            //verifica se já existe insight criado daquele formulário
            foreach (var item in db.Insights)
            {
                if (item.FormularioID == formularioID)
                {
                    InsightRepetido = formularioID;
                    InsightRepetidoId = item.Id;
                }
            }

            if (InsightRepetido == formularioID)
            {
                if (movieName == "Sim")
                {
                    graficosInsight.SurgiuInsightSim += 1;
                    graficosInsight.SurgiuInsightNao = 0;
                }
                else if (movieName == "Nao")
                {
                    graficosInsight.SurgiuInsightNao += 1;
                    graficosInsight.SurgiuInsightSim = 0;
                }



                foreach (var item in GetIdUser)
                {
                    graficosInsight.UsuarioId = item.Id;

                }

                graficosInsight.FormularioID = formularioID;
                graficosInsight.Data = DateTime.Now;
                graficosInsight.Id = InsightRepetidoId;

                if (ModelState.IsValid)
                {

                    //db.Insights.Attach(graficosInsight);
                    db.Set<Insight>().AddOrUpdate(graficosInsight);
                    //db.Entry(graficosInsight).State = EntityState.Modified;
                    db.SaveChanges();

                }

            }
            else
            {
                if (movieName == "Sim")
                {
                    graficosInsight.SurgiuInsightSim += 1;
                    graficosInsight.SurgiuInsightNao = 0;
                }
                else if (movieName == "Nao")
                {
                    graficosInsight.SurgiuInsightNao += 1;
                    graficosInsight.SurgiuInsightSim = 0;
                }


                foreach (var item in GetIdUser)
                {
                    graficosInsight.UsuarioId = item.Id;

                }

                graficosInsight.FormularioID = formularioID;
                graficosInsight.Data = DateTime.Now;
                if (ModelState.IsValid)
                {
                    db.Insights.Add(graficosInsight);
                    db.SaveChanges();

                }
            }

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