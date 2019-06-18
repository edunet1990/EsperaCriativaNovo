using EsperaCriativa.Contexto;
using EsperaCriativa.Models;
using EsperaCriativa.ViewModels.Planejamentos;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace EsperaCriativa.Planejamentos
{
    public class FormulariosController : Controller
    {
        private EsperaCriativaContext db = new EsperaCriativaContext();

        // GET: Formularios
        [Authorize]
        public ActionResult Index(int? Id, int? formularioID, int? ocultaFormID)
        {
            ViewBag.OcultForm2 = ocultaFormID;
            var formularioList = new FormulariosExperienciasViewModel();
            var MinhaListaDeCheckBox = new List<FormulariosExperienciasViewModel>();

            var MinhaViewModelFormulario = new List<FormulariosExperienciasViewModel>();

            foreach (var item in db.Formularios)
            {
                int c = 0;
                c++;
                MinhaViewModelFormulario.Add(new FormulariosExperienciasViewModel { FormularioExperienciasViewModelID = c, FormularioID = item.FormularioID, Informacao = item.Informacao, Data = item.Data, UsuarioId = item.UsuarioId });
                ViewBag.UsuarioId = item.UsuarioId;
            }
            formularioList.FormulariosExperienciasVM = MinhaViewModelFormulario;

            var testee = db.Experiencias;
            int ContForms = 0;
            List<int> guardaFormsContados = new List<int>();

            int getValorMaxIDFormulario = 0;
            foreach (var item in db.Formularios)
            {
                getValorMaxIDFormulario = item.FormularioID;
            }

            for (int i = 0; i <= getValorMaxIDFormulario; i++)
            {

                var Resultados = from b in db.Experiencias
                                 select new
                                 {
                                     b.ExperienciaId,
                                     b.InfoExperiencia,
                                     Checked = ((from ab in db.FormularioExperiencias
                                                 where (ab.FormularioId == i) & (ab.ExperienciaId == b.ExperienciaId)
                                                 select ab).Count() > 0)
                                 };




                foreach (var item in Resultados)
                {
                    if (item.Checked)
                    {
                        MinhaListaDeCheckBox.Add(new FormulariosExperienciasViewModel { FormularioID = ContForms, CheckBoxID = item.ExperienciaId, Name = item.InfoExperiencia, Checked = item.Checked });
                    }

                }

                if (ContForms == i)
                {
                    guardaFormsContados.Add(ContForms);
                }
                ContForms++;
            }


            var GetIdUser = from b in db.Usuarios where b.Nome == User.Identity.Name select b;

            foreach (var item in GetIdUser)
            {
                ViewBag.UsuarioID = item.Id;

            }


            //ver se o insight foi informado e usar ComoFoiAexperiencia para comparar
            List<int> listVerSeTemFormsRepetidos1 = new List<int>();
            List<int> listVerSeTemFormsRepetidos2 = new List<int>();

            foreach (var item2 in db.ComoFoiAsExperiencias)
            {
                var verSeFoiCad2ComoFoiNoMesmoForm = item2.PlanejamentoFixoId;


                if (ViewBag.UsuarioID == item2.UsuarioId)
                {
                    listVerSeTemFormsRepetidos1.Add(item2.PlanejamentoFixoId);


                }

            }
            listVerSeTemFormsRepetidos2 = listVerSeTemFormsRepetidos1;
            var count = listVerSeTemFormsRepetidos2.Count;
            var count2 = 1;
            var count3 = 0;
            foreach (var item in listVerSeTemFormsRepetidos1)
            {

                if (listVerSeTemFormsRepetidos2.Count == count)
                {
                    formularioList.FormIdComoFoiAExper.Add(item);
                    count = 1;
                    count3 = listVerSeTemFormsRepetidos2[0];

                }
                else if (listVerSeTemFormsRepetidos2[count2] != count3)
                {
                    formularioList.FormIdComoFoiAExper.Add(item);
                    count3 = listVerSeTemFormsRepetidos2[count2];
                    count2++;
                }
            }

            if (true)
            {

            }
            ViewBag.guardaFormsContados = guardaFormsContados;
            formularioList.UsuarioId = ViewBag.UsuarioID;





            formularioList.ExperienciasFormulariosVM = MinhaListaDeCheckBox;
            ViewBag.UsuarioFormularioTeste = from b in db.Usuarios where b.Nome == User.Identity.Name select b;


            return View(formularioList);
        }

        //POST Index
        [HttpPost]
        public ActionResult Index(int? ocultaFormID)
        {
            ViewBag.OcultForm = ocultaFormID;
            return RedirectToAction("Index");
        }
        // GET: Formularios/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Formulario formulario = db.Formularios.Find(id);
            if (formulario == null)
            {
                return HttpNotFound();
            }

            //Capturar os textos em experiencias e filtrar para que o checkbox seja igual ao inserido pelo FormularioExperiencias
            var Resultados = from b in db.Experiencias
                             select new
                             {
                                 b.ExperienciaId,
                                 b.InfoExperiencia,
                                 Checked = ((from ab in db.FormularioExperiencias
                                             where (ab.FormularioId == id) & (ab.ExperienciaId == b.ExperienciaId)
                                             select ab).Count() > 0)
                             };

            // instancia formulariosviewmodel
            var MinhaViewModelFormulario = new FormulariosViewModel();

            //recebe os dados de formulario pelo banco
            MinhaViewModelFormulario.FormularioID = id.Value;
            MinhaViewModelFormulario.Informacao = formulario.Informacao;
            MinhaViewModelFormulario.Data = formulario.Data;

            //instancia o checkboxviewmodel
            var MinhaListaDeCheckBox = new List<CheckBoxViewModel>();

            //adiciona ao checkbox viewmodel aquele filtro que foi feito na expressão lambda
            foreach (var item in Resultados)
            {
                MinhaListaDeCheckBox.Add(new CheckBoxViewModel { Id = item.ExperienciaId, Name = item.InfoExperiencia, Checked = item.Checked });
            }

            //guarda esses valores em formularioviewmodel
            MinhaViewModelFormulario.Experiencias = MinhaListaDeCheckBox;


            //envia
            return View(MinhaViewModelFormulario);
        }

        // GET: Formularios/Create
        public ActionResult Create()
        {
            var Resultados = from b in db.Experiencias
                             select new
                             {
                                 b.ExperienciaId,
                                 b.InfoExperiencia,

                             };

            FormulariosViewModel MinhaViewModelFormulario = new FormulariosViewModel();


            var MinhaListaDeCheckBox = new List<CheckBoxViewModel>();

            foreach (var item in Resultados)
            {
                MinhaListaDeCheckBox.Add(new CheckBoxViewModel { Id = item.ExperienciaId, Name = item.InfoExperiencia, Checked = false });
            }

            MinhaViewModelFormulario.Experiencias = MinhaListaDeCheckBox;


            return View(MinhaViewModelFormulario);
        }

        // POST: Formularios/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FormulariosViewModel formulariosViewModel)
        {
            Formulario formulario = new Formulario();


            if (ModelState.IsValid)
            {
                var MeuFormulario = db.Formularios.Find(formulariosViewModel.FormularioID);

                //Identificar o id do usuário que está logado
                var GetIdUser = from b in db.Usuarios where b.Nome == User.Identity.Name select b;

                foreach (var item in GetIdUser)
                {
                    formulario.UsuarioId = item.Id;
                    ViewBag.UsuarioID = item.Id;
                }
                formulario.Informacao = formulariosViewModel.Informacao;
                formulario.Data = formulariosViewModel.Data;
                formulario.Data = formulariosViewModel.Data;


                //formulario.UsuarioId = 1;
                db.Formularios.Add(formulario);


                foreach (var item in db.FormularioExperiencias)
                {
                    if (item.FormularioId == formulariosViewModel.FormularioID)
                    {
                        db.Entry(item).State = EntityState.Deleted;
                    }
                }


                foreach (var item in formulariosViewModel.Experiencias)
                {
                    if (item.Checked)
                    {
                        db.FormularioExperiencias.Add(new FormularioExperiencia() { FormularioId = formulariosViewModel.FormularioID, ExperienciaId = item.Id, });

                    }
                }

                db.SaveChanges();




                return RedirectToAction("Index");
            }
            return View(formulariosViewModel);
        }

        // GET: Formularios/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Formulario formulario = db.Formularios.Find(id);
            if (formulario == null)
            {
                return HttpNotFound();
            }

            var Resultados = from b in db.Experiencias
                             select new
                             {
                                 b.ExperienciaId,
                                 b.InfoExperiencia,
                                 Checked = ((from ab in db.FormularioExperiencias
                                             where (ab.FormularioId == id) & (ab.ExperienciaId == b.ExperienciaId)
                                             select ab).Count() > 0)
                             };

            var MinhaViewModelFormulario = new FormulariosViewModel();

            MinhaViewModelFormulario.FormularioID = id.Value;
            MinhaViewModelFormulario.Informacao = formulario.Informacao;
            MinhaViewModelFormulario.Data = formulario.Data;

            var MinhaListaDeCheckBox = new List<CheckBoxViewModel>();

            foreach (var item in Resultados)
            {
                MinhaListaDeCheckBox.Add(new CheckBoxViewModel { Id = item.ExperienciaId, Name = item.InfoExperiencia, Checked = item.Checked });
            }

            MinhaViewModelFormulario.Experiencias = MinhaListaDeCheckBox;


            return View(MinhaViewModelFormulario);
        }

        // POST: Formularios/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(FormulariosViewModel formulariosViewModel)
        {
            if (ModelState.IsValid)
            {
                var MeuFormulario = db.Formularios.Find(formulariosViewModel.FormularioID);

                MeuFormulario.Informacao = formulariosViewModel.Informacao;
                MeuFormulario.Data = formulariosViewModel.Data;

                foreach (var item in db.FormularioExperiencias)
                {
                    if (item.FormularioId == formulariosViewModel.FormularioID)
                    {
                        db.Entry(item).State = EntityState.Deleted;
                    }
                }

                foreach (var item in formulariosViewModel.Experiencias)
                {
                    if (item.Checked)
                    {
                        db.FormularioExperiencias.Add(new FormularioExperiencia() { FormularioId = formulariosViewModel.FormularioID, ExperienciaId = item.Id });
                    }
                }

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(formulariosViewModel);
        }


        // Editar Modificacao de padrao
        [Authorize]
        public ActionResult EditarModificacaoDePadrao(int? id, int idUsuario)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Formulario formulario = db.Formularios.Find(id);
            if (formulario == null)
            {
                return HttpNotFound();
            }

            var Resultados = from b in db.Experiencias
                             select new
                             {
                                 b.ExperienciaId,
                                 b.InfoExperiencia,

                             };

            var MinhaViewModelFormulario = new FormulariosViewModel();

            MinhaViewModelFormulario.FormularioID = id.Value;
            var MinhaListaDeCheckBox = new List<CheckBoxViewModel>();

            foreach (var item in Resultados)
            {
                MinhaListaDeCheckBox.Add(new CheckBoxViewModel { Id = item.ExperienciaId, Name = item.InfoExperiencia, Checked = false });
            }

            MinhaViewModelFormulario.Experiencias = MinhaListaDeCheckBox;
            MinhaViewModelFormulario.UsuarioId = idUsuario;


            return View(MinhaViewModelFormulario);
        }

        // POST: Formularios/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditarModificacaoDePadrao(FormulariosViewModel formulariosViewModel)
        {
            if (ModelState.IsValid)
            {
                var MeuFormulario = db.Formularios.Find(formulariosViewModel.FormularioID);

                foreach (var item in db.FormularioExperiencias)
                {
                    if (item.FormularioId == formulariosViewModel.FormularioID)
                    {
                        db.Entry(item).State = EntityState.Deleted;
                    }
                }

                foreach (var item in formulariosViewModel.Experiencias)
                {
                    if (item.Checked)
                    {
                        db.FormularioExperiencias.Add(new FormularioExperiencia() { FormularioId = formulariosViewModel.FormularioID, ExperienciaId = item.Id });
                    }
                }
                ViewBag.FormularioID = formulariosViewModel.FormularioID;
                db.SaveChanges();
                return RedirectToAction("Create", "ComoFoiAsExperiencias", new { formulariosViewModel.FormularioID });
            }
            return View(formulariosViewModel);
        }

        //Verifica se o botão para compartilhar a experiência foi clicado


        // GET: Formularios/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Formulario formulario = db.Formularios.Find(id);
            if (formulario == null)
            {
                return HttpNotFound();
            }
            return View(formulario);
        }

        // POST: Formularios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Formulario formulario = db.Formularios.Find(id);
            db.Formularios.Remove(formulario);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult OcultaForm(int id)
        {
            Formulario formulario = db.Formularios.Find(id);
            ViewBag.OcultaForm = formulario.FormularioID;

            return RedirectToAction("Index");
        }
        [Authorize]
        public ActionResult GetAll()
        {
            var formularioList = new FormulariosExperienciasViewModel();
            var MinhaListaDeCheckBox = new List<FormulariosExperienciasViewModel>();


            var MinhaViewModelFormulario = new List<FormulariosExperienciasViewModel>();

            foreach (var item in db.Formularios)
            {
                int c = 0;
                c++;
                MinhaViewModelFormulario.Add(new FormulariosExperienciasViewModel { FormularioExperienciasViewModelID = c, FormularioID = item.FormularioID, Informacao = item.Informacao, Data = item.Data });
            }
            formularioList.FormulariosExperienciasVM = MinhaViewModelFormulario;

            var testee = db.Experiencias;
            int ContForms = 0;
            List<int> guardaFormsContados = new List<int>();

            int getValorMaxIDFormulario = 0;
            foreach (var item in db.Formularios)
            {
                getValorMaxIDFormulario = item.FormularioID;
            }

            for (int i = 0; i <= getValorMaxIDFormulario; i++)
            {

                var Resultados = from b in db.Experiencias
                                 select new
                                 {
                                     b.ExperienciaId,
                                     b.InfoExperiencia,
                                     Checked = ((from ab in db.FormularioExperiencias
                                                 where (ab.FormularioId == i) & (ab.ExperienciaId == b.ExperienciaId)
                                                 select ab).Count() > 0)
                                 };



                foreach (var item in Resultados)
                {
                    if (item.Checked)
                    {
                        MinhaListaDeCheckBox.Add(new FormulariosExperienciasViewModel { FormularioID = ContForms, CheckBoxID = item.ExperienciaId, Name = item.InfoExperiencia, Checked = item.Checked });
                    }

                }

                if (ContForms == i)
                {
                    guardaFormsContados.Add(ContForms);
                }
                ContForms++;
            }

            ViewBag.guardaFormsContados = guardaFormsContados;
            formularioList.ExperienciasFormulariosVM = MinhaListaDeCheckBox;
            return View(MinhaListaDeCheckBox);
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