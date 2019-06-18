using EsperaCriativa.Contexto;
using EsperaCriativa.Models;
using EsperaCriativa.ViewModels.Planejamentos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Drawing;

namespace EsperaCriativa.Planejamentos
{
    public class ComoFoiAsExperienciasController : Controller
    {
        private EsperaCriativaContext db = new EsperaCriativaContext();

        // GET: ComoFoiAsExperiencias
        [Authorize]
        public ActionResult Index()
        {

            return View(db.ComoFoiAsExperiencias.ToList());
        }

        // GET: ComoFoiAsExperiencias/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ComoFoiAExperiencia comoFoiAExperiencia = db.ComoFoiAsExperiencias.Find(id);
            if (comoFoiAExperiencia == null)
            {
                return HttpNotFound();
            }
            return View(comoFoiAExperiencia);
        }


        // GET: ComoFoiAsExperiencias/Create
        [Authorize]
        public ActionResult Create(int? idForm, int? idUsuario)
        {

            var model = new ComoFoiAExperienciaViewModel();

            ViewBag.form = idForm;
            ViewBag.user = idUsuario;

            return View(model);


        }



        // POST: ComoFoiAsExperiencias/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ComoFoiAExperienciaViewModel comoFoiAExperiencia, int? id)
        {

            //Pegar a Inform do form e comparar com o planfix para inserir o ID
            PlanejamentoFixo planejamentoFixo = new PlanejamentoFixo();


            var GetFormId = from b in db.Formularios select b;
            var comoFoiExperiencia = new ComoFoiAExperiencia();
            var imageTypes = new string[]
            {
                    "image/gif",
                    "image/jpeg",
                    "image/pjpeg",
                    "image/png"
            };
            if (comoFoiAExperiencia.ImageUpload == null || comoFoiAExperiencia.ImageUpload.ContentLength == 0)
            {

                var GetIdUser = from b in db.Usuarios where b.Nome == User.Identity.Name select b;

                foreach (var item in GetIdUser)
                {
                    GetFormId = from p in db.Formularios where p.UsuarioId == item.Id select p;

                }

                var GetForm = from p in GetFormId where p.FormularioID == comoFoiAExperiencia.PanejamentoFixoId select p;

                foreach (var item in GetForm)
                {
                    planejamentoFixo.Informacao = item.Informacao;
                    planejamentoFixo.Data = item.Data;
                    planejamentoFixo.UsuarioId = item.UsuarioId;
                    planejamentoFixo.FormularioId = item.FormularioID;

                }

                foreach (var item2 in GetFormId)
                {
                    comoFoiExperiencia.PlanejamentoFixoId = planejamentoFixo.Id;
                }

                comoFoiExperiencia.MsgComoFoiAExperiencia = comoFoiAExperiencia.MsgComoFoiAExperiencia;
                comoFoiExperiencia.CompartilharExperiencia = comoFoiAExperiencia.CompartilharExperiencia;
                comoFoiExperiencia.UsuarioId = comoFoiAExperiencia.UsuarioId;
                comoFoiExperiencia.PlanejamentoFixoId = comoFoiAExperiencia.PanejamentoFixoId;

                db.ComoFoiAsExperiencias.Add(comoFoiExperiencia);
                db.PlanejamentosFixo.Add(planejamentoFixo);

                ViewBag.info = 100;
                db.SaveChanges();
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }
            else if (!imageTypes.Contains(comoFoiAExperiencia.ImageUpload.ContentType))
            {
                ModelState.AddModelError("ImageUpload", "Escolha uma iamgem GIF, JPG ou PNG.");
            }

            if (ModelState.IsValid)
            {
                var GetIdUser = from b in db.Usuarios where b.Nome == User.Identity.Name select b;

                foreach (var item in GetIdUser)
                {
                    GetFormId = from p in db.Formularios where p.UsuarioId == item.Id select p;


                }


                var GetForm = from p in GetFormId where p.FormularioID == comoFoiAExperiencia.PanejamentoFixoId select p;

                foreach (var item in GetForm)
                {
                    planejamentoFixo.Informacao = item.Informacao;
                    planejamentoFixo.Data = item.Data;
                    planejamentoFixo.UsuarioId = item.UsuarioId;
                    planejamentoFixo.FormularioId = item.FormularioID;

                }

                foreach (var item2 in GetFormId)
                {
                    comoFoiExperiencia.PlanejamentoFixoId = planejamentoFixo.Id;
                }

                foreach (var item2 in GetFormId)
                {
                    comoFoiExperiencia.PlanejamentoFixoId = planejamentoFixo.Id;
                }

                comoFoiExperiencia.MsgComoFoiAExperiencia = comoFoiAExperiencia.MsgComoFoiAExperiencia;
                comoFoiExperiencia.CompartilharExperiencia = comoFoiAExperiencia.CompartilharExperiencia;
                comoFoiExperiencia.UsuarioId = comoFoiAExperiencia.UsuarioId;
                comoFoiExperiencia.PlanejamentoFixoId = comoFoiAExperiencia.PanejamentoFixoId;

                //Salvar imagem para a pasta e pega o caminho
                var imagemNome = String.Format("{0:yyyyMMdd-HHmmssfff}", DateTime.Now);
                var extensao = System.IO.Path.GetExtension(comoFoiAExperiencia.ImageUpload.FileName).ToLower();

                using (var img = Image.FromStream(comoFoiAExperiencia.ImageUpload.InputStream))
                {
                    comoFoiExperiencia.Imagem = String.Format("/ProdutoImagens/{0}{1}", imagemNome, extensao);
                    //Salvar imagem
                    SalvarNaPasta(img, comoFoiExperiencia.Imagem);
                }

                db.PlanejamentosFixo.Add(planejamentoFixo);
                db.ComoFoiAsExperiencias.Add(comoFoiExperiencia);
                db.SaveChanges();
                ViewBag.info = 100;

                db.SaveChanges();

                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }


            ViewBag.ComoFoiAExperiencia = db.ComoFoiAsExperiencias;
            return View(comoFoiAExperiencia);
        }

        //Método para salvar a imagem
        private void SalvarNaPasta(Image img, string caminho)
        {
            using (System.Drawing.Image novaImagem = new Bitmap(img))
            {
                novaImagem.Save(Server.MapPath(caminho), img.RawFormat);
            }
        }

        // GET: ComoFoiAsExperiencias/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ComoFoiAExperiencia comoFoiAExperiencia = db.ComoFoiAsExperiencias.Find(id);

            ComoFoiAExperienciaViewModel comoFoiAExperienciaViewModel = new ComoFoiAExperienciaViewModel();

            comoFoiAExperienciaViewModel.ComoFoiAExperienciaID = comoFoiAExperiencia.ComoFoiAExperienciaID;
            comoFoiAExperienciaViewModel.CompartilharExperiencia = comoFoiAExperiencia.CompartilharExperiencia;
            comoFoiAExperienciaViewModel.PanejamentoFixoId = comoFoiAExperiencia.PlanejamentoFixoId;
            comoFoiAExperienciaViewModel.MsgComoFoiAExperiencia = comoFoiAExperiencia.MsgComoFoiAExperiencia;


            if (comoFoiAExperienciaViewModel == null)
            {
                return HttpNotFound();
            }


            return View(comoFoiAExperienciaViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ComoFoiAExperienciaID,MsgComoFoiAExperiencia,Imagem,CompartilharExperiencia, FormularioId")] ComoFoiAExperienciaViewModel comoFoiAExperiencia)
        {


            var GetIdForm = from b in db.Formularios select b;
            var comoFoiExperiencia = new ComoFoiAExperiencia();
            var imageTypes = new string[]
            {
                    "image/gif",
                    "image/jpeg",
                    "image/pjpeg",
                    "image/png"
            };
            if (comoFoiAExperiencia.ImageUpload == null || comoFoiAExperiencia.ImageUpload.ContentLength == 0)
            {

                var GetIdUser = from b in db.Usuarios where b.Nome == User.Identity.Name select b;

                foreach (var item in GetIdUser)
                {
                    GetIdForm = from b in db.Formularios where b.UsuarioId == item.Id select b;

                }

                foreach (var item2 in GetIdForm)
                {

                    comoFoiExperiencia.PlanejamentoFixoId = item2.FormularioID;
                }

                comoFoiExperiencia.MsgComoFoiAExperiencia = comoFoiAExperiencia.MsgComoFoiAExperiencia;
                comoFoiExperiencia.CompartilharExperiencia = comoFoiAExperiencia.CompartilharExperiencia;

                db.ComoFoiAsExperiencias.Add(comoFoiExperiencia);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else if (!imageTypes.Contains(comoFoiAExperiencia.ImageUpload.ContentType))
            {
                ModelState.AddModelError("ImageUpload", "Escolha uma iamgem GIF, JPG ou PNG.");
            }

            if (ModelState.IsValid)
            {
                var GetIdUser = from b in db.Usuarios where b.Nome == User.Identity.Name select b;

                foreach (var item in GetIdUser)
                {
                    GetIdForm = from b in db.Formularios where b.UsuarioId == item.Id select b;

                }

                foreach (var item2 in GetIdForm)
                {

                    comoFoiExperiencia.PlanejamentoFixoId = item2.FormularioID;
                }

                comoFoiExperiencia.MsgComoFoiAExperiencia = comoFoiAExperiencia.MsgComoFoiAExperiencia;
                comoFoiExperiencia.CompartilharExperiencia = comoFoiAExperiencia.CompartilharExperiencia;


                //Salvar imagem para a pasta e pega o caminho
                var imagemNome = String.Format("{0:yyyyMMdd-HHmmssfff}", DateTime.Now);
                var extensao = System.IO.Path.GetExtension(comoFoiAExperiencia.ImageUpload.FileName).ToLower();

                using (var img = Image.FromStream(comoFoiAExperiencia.ImageUpload.InputStream))
                {
                    comoFoiExperiencia.Imagem = String.Format("/ProdutoImagens/{0}{1}", imagemNome, extensao);
                    //Salvar imagem
                    SalvarNaPasta(img, comoFoiExperiencia.Imagem);
                }

                db.ComoFoiAsExperiencias.Add(comoFoiExperiencia);

                if (ModelState.IsValid)
                {
                    db.Entry(comoFoiExperiencia).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }


            ViewBag.ComoFoiAExperiencia = db.ComoFoiAsExperiencias;
            return View(comoFoiAExperiencia);

        }

        // GET: ComoFoiAsExperiencias/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ComoFoiAExperiencia comoFoiAExperiencia = db.ComoFoiAsExperiencias.Find(id);
            if (comoFoiAExperiencia == null)
            {
                return HttpNotFound();
            }
            return View(comoFoiAExperiencia);
        }

        // POST: ComoFoiAsExperiencias/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ComoFoiAExperiencia comoFoiAExperiencia = db.ComoFoiAsExperiencias.Find(id);
            db.ComoFoiAsExperiencias.Remove(comoFoiAExperiencia);
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