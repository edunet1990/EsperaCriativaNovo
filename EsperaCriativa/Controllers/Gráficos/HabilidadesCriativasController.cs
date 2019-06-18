using EsperaCriativa.Contexto;
using EsperaCriativa.Models;
using EsperaCriativa.ViewModels.Graficos;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace EsperaCriativa.Controllers.Gráficos
{
    public class HabilidadesCriativasController : Controller
    {
        private EsperaCriativaContext db = new EsperaCriativaContext();




        // GET: HabilidadesCriativas
        [Authorize]
        public ActionResult Index()
        {
            //Variável criada para rodar no foreach e capturar todos os compartilhar experiência
            var desejaCompartilhar = new List<bool>();

            foreach (var item in db.ComoFoiAsExperiencias)
            {
                desejaCompartilhar.Add(item.CompartilharExperiencia);
            }

            var habilidadesCriativasGraficoVMsListInsight = new List<HabilidadesCriativasViewModel>();
            var habilidadesCriativasGraficoVMsListCompart = new List<HabilidadesCriativasViewModel>();
            var habilidadesCriativasGraficoVMsListDosDois = new List<HabilidadesCriativasViewModel>();
            int contQtddInsights = 0;
            int contQtddCompart = 0;
            //pega todos insights
            foreach (var item in db.Insights)
            {
                habilidadesCriativasGraficoVMsListInsight.Add(new HabilidadesCriativasViewModel { FormularioId = item.FormularioID, SurgiuInsightSim = item.SurgiuInsightSim, SurgiuInsightNao = item.SurgiuInsightNao });
                contQtddInsights++;

            }

            //pega todos os compartilhamentos
            foreach (var item in db.ComoFoiAsExperiencias)
            {
                habilidadesCriativasGraficoVMsListCompart.Add(new HabilidadesCriativasViewModel { FormularioId = item.PlanejamentoFixoId, DesejaCompartilhar = item.CompartilharExperiencia });
                contQtddCompart++;
            }

            //verifica se o formularioid do insight é igual ao do compartilhamento
            var cont = 1;
            var PrimeiroLaco = 0;
            for (int i = 0; i <= contQtddInsights; i++)
            {
                if ((contQtddCompart <= contQtddInsights) && (contQtddInsights <= contQtddCompart) && (contQtddCompart > 0) && (contQtddInsights > 0))
                {
                    if (habilidadesCriativasGraficoVMsListCompart[cont - 1].FormularioId != null && habilidadesCriativasGraficoVMsListCompart[cont - 1].FormularioId > 0)
                    {
                        if (habilidadesCriativasGraficoVMsListInsight[cont - 1].FormularioId == habilidadesCriativasGraficoVMsListCompart[cont - 1].FormularioId)
                        {
                            habilidadesCriativasGraficoVMsListDosDois.Add(new HabilidadesCriativasViewModel
                            {
                                FormularioId = habilidadesCriativasGraficoVMsListInsight[i].FormularioId,
                                SurgiuInsightSim = habilidadesCriativasGraficoVMsListInsight[i].SurgiuInsightSim,
                                SurgiuInsightNao = habilidadesCriativasGraficoVMsListInsight[i].SurgiuInsightNao,
                                DesejaCompartilhar = habilidadesCriativasGraficoVMsListCompart[i].DesejaCompartilhar
                            });
                        }
                    }
                }

                //para passar no primeiro laço e não ficar -1 no array
                if (PrimeiroLaco == 0)
                {
                    cont--;
                    PrimeiroLaco++;
                }
                else
                {
                    cont++;
                }

            }

            return View(habilidadesCriativasGraficoVMsListDosDois.ToList());
        }

        // GET: HabilidadesCriativas/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Insight graficosInsight = db.Insights.Find(id);
            if (graficosInsight == null)
            {
                return HttpNotFound();
            }
            return View(graficosInsight);
        }

        // GET: HabilidadesCriativas/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: HabilidadesCriativas/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDGraficosInsight,SurgiuInsightSim,SurgiuInsightNao,Data")] Insight graficosInsight)
        {
            if (ModelState.IsValid)
            {
                db.Insights.Add(graficosInsight);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(graficosInsight);
        }

        // GET: HabilidadesCriativas/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Insight graficosInsight = db.Insights.Find(id);
            if (graficosInsight == null)
            {
                return HttpNotFound();
            }
            return View(graficosInsight);
        }

        // POST: HabilidadesCriativas/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDGraficosInsight,SurgiuInsightSim,SurgiuInsightNao,Data")] Insight graficosInsight)
        {
            if (ModelState.IsValid)
            {
                db.Entry(graficosInsight).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(graficosInsight);
        }

        // GET: HabilidadesCriativas/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Insight graficosInsight = db.Insights.Find(id);
            if (graficosInsight == null)
            {
                return HttpNotFound();
            }
            return View(graficosInsight);
        }

        // POST: HabilidadesCriativas/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Insight graficosInsight = db.Insights.Find(id);
            db.Insights.Remove(graficosInsight);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [Authorize]
        public ActionResult GetAll()
        {   //Variável criada para rodar no foreach e capturar todos os compartilhar experiência
            var desejaCompartilhar = new List<bool>();

            foreach (var item in db.ComoFoiAsExperiencias)
            {
                desejaCompartilhar.Add(item.CompartilharExperiencia);
            }

            var habilidadesCriativasGraficoVMsListInsight = new List<HabilidadesCriativasViewModel>();
            var habilidadesCriativasGraficoVMsListCompart = new List<HabilidadesCriativasViewModel>();
            var habilidadesCriativasGraficoVMsListDosDois = new List<HabilidadesCriativasViewModel>();
            int contQtddInsights = 0;
            int contQtddCompart = 0;
            //pega todos insights
            foreach (var item in db.Insights)
            {
                habilidadesCriativasGraficoVMsListInsight.Add(new HabilidadesCriativasViewModel { FormularioId = item.FormularioID, SurgiuInsightSim = item.SurgiuInsightSim, SurgiuInsightNao = item.SurgiuInsightNao });
                contQtddInsights++;

            }

            //pega todos os compartilhamentos
            foreach (var item in db.ComoFoiAsExperiencias)
            {
                habilidadesCriativasGraficoVMsListCompart.Add(new HabilidadesCriativasViewModel { FormularioId = item.PlanejamentoFixoId, DesejaCompartilhar = item.CompartilharExperiencia });
                contQtddCompart++;
            }

            //verifica se o formularioid do insight é igual ao do compartilhamento
            var cont = 0;
            for (int i = 0; i <= contQtddInsights; i++)
            {
                if (contQtddCompart > 0 && contQtddCompart <= contQtddInsights)
                {

                    if (habilidadesCriativasGraficoVMsListCompart[cont].FormularioId != null)
                    {
                        if (habilidadesCriativasGraficoVMsListInsight[cont].FormularioId == habilidadesCriativasGraficoVMsListCompart[cont].FormularioId)
                        {
                            habilidadesCriativasGraficoVMsListDosDois.Add(new HabilidadesCriativasViewModel
                            {
                                FormularioId = habilidadesCriativasGraficoVMsListInsight[i].FormularioId,
                                SurgiuInsightSim = habilidadesCriativasGraficoVMsListInsight[i].SurgiuInsightSim,
                                SurgiuInsightNao = habilidadesCriativasGraficoVMsListInsight[i].SurgiuInsightNao,
                                DesejaCompartilhar = habilidadesCriativasGraficoVMsListCompart[i].DesejaCompartilhar
                            });
                        }
                    }
                }

                //verifica se o tamanho não passa do indice do array
                if (cont < contQtddInsights)
                {
                    cont++; //para sempre iniciar o indíce com 0;

                    if (cont == contQtddInsights)
                    {
                        cont--;
                    }
                }

            }

            return View(habilidadesCriativasGraficoVMsListDosDois.ToList());
        }


        [Authorize]
        public ActionResult GetAllGeral()
        {  //Variável criada para rodar no foreach e capturar todos os compartilhar experiência
            var desejaCompartilhar = new List<bool>();

            foreach (var item in db.ComoFoiAsExperiencias)
            {
                desejaCompartilhar.Add(item.CompartilharExperiencia);
            }

            var habilidadesCriativasGraficoVMsListInsight = new List<HabilidadesCriativasViewModel>();
            var habilidadesCriativasGraficoVMsListCompart = new List<HabilidadesCriativasViewModel>();
            var habilidadesCriativasGraficoVMsListDosDois = new List<HabilidadesCriativasViewModel>();
            int contQtddInsights = 0;
            int contQtddCompart = 0;
            //pega todos insights
            foreach (var item in db.Insights)
            {
                habilidadesCriativasGraficoVMsListInsight.Add(new HabilidadesCriativasViewModel { FormularioId = item.FormularioID, SurgiuInsightSim = item.SurgiuInsightSim, SurgiuInsightNao = item.SurgiuInsightNao });
                contQtddInsights++;

            }

            //pega todos os compartilhamentos
            foreach (var item in db.ComoFoiAsExperiencias)
            {
                habilidadesCriativasGraficoVMsListCompart.Add(new HabilidadesCriativasViewModel { FormularioId = item.PlanejamentoFixoId, DesejaCompartilhar = item.CompartilharExperiencia });
                contQtddCompart++;
            }

            //verifica se o formularioid do insight é igual ao do compartilhamento
            var cont = 0;
            for (int i = 0; i <= contQtddInsights; i++)
            {
                if (contQtddCompart > 0 && contQtddCompart <= contQtddInsights)
                {

                    if (habilidadesCriativasGraficoVMsListCompart[cont].FormularioId != null)
                    {
                        if (habilidadesCriativasGraficoVMsListInsight[cont].FormularioId == habilidadesCriativasGraficoVMsListCompart[cont].FormularioId)
                        {
                            habilidadesCriativasGraficoVMsListDosDois.Add(new HabilidadesCriativasViewModel
                            {
                                FormularioId = habilidadesCriativasGraficoVMsListInsight[i].FormularioId,
                                SurgiuInsightSim = habilidadesCriativasGraficoVMsListInsight[i].SurgiuInsightSim,
                                SurgiuInsightNao = habilidadesCriativasGraficoVMsListInsight[i].SurgiuInsightNao,
                                DesejaCompartilhar = habilidadesCriativasGraficoVMsListCompart[i].DesejaCompartilhar
                            });
                        }
                    }
                }

                //verifica se o tamanho não passa do indice do array
                if (cont < contQtddInsights)
                {
                    cont++; //para sempre iniciar o indíce com 0;

                    if (cont == contQtddInsights)
                    {
                        cont--;
                    }
                }

            }

            return View(habilidadesCriativasGraficoVMsListDosDois.ToList());
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