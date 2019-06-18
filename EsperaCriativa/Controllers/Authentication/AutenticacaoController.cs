using EsperaCriativa.Contexto;
using EsperaCriativa.Models;
using EsperaCriativa.ViewModels.Autenticacao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Policy;
using System.Web;
using System.Web.Mvc;

namespace EsperaCriativa.Controllers
{
    public class AutenticacaoController : Controller
    {
        EsperaCriativaContext db = new EsperaCriativaContext();

        // GET: Autenticacao

        public ActionResult Cadastrar()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Cadastrar(CadastroUsuarioViewModel recebeCadastroViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(recebeCadastroViewModel);
            }

            //se o login for o mesmo
            if (db.Usuarios.Count(u => u.Login == recebeCadastroViewModel.Login) > 0)
            {
                ModelState.AddModelError("Login", "Esse login já existe!");
                return View(recebeCadastroViewModel);
            }
            Usuario novoUsuario = new Usuario
            {
                Nome = recebeCadastroViewModel.Nome,
                Login = recebeCadastroViewModel.Login,
                Senha = EsperaCriativa.Utils.Hash.GeraHash(recebeCadastroViewModel.Senha), //criptografa a senha
                Data = DateTime.Now,
                Tipo = recebeCadastroViewModel.Tipo


            };

            db.Usuarios.Add(novoUsuario);
            db.SaveChanges();

            ViewData["MensagemPosCadastro"] = "Usuário cadastrado com sucesso";

            return RedirectToAction("Index", "Home");
        }

        public ActionResult Login(string ReturnUrl)
        {
            var loginViewModel = new LoginViewModel
            {
                UrlRetorno = ReturnUrl
            };

            return View(loginViewModel);
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel loginviewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(loginviewModel);
            }

            Usuario usuario = db.Usuarios.FirstOrDefault(u => u.Login == loginviewModel.Login);
            int IdUsuario = 0;


            if (usuario == null)
            {
                ModelState.AddModelError("Login", "Login incorreto");
                return View(loginviewModel);
            }

            if (usuario.Senha != EsperaCriativa.Utils.Hash.GeraHash(loginviewModel.Senha))
            {
                ModelState.AddModelError("Senha", "Senha incorreta");
                return View(loginviewModel);
            }

            var identity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, usuario.Nome),
                new Claim("Login", usuario.Login),
                new Claim("UsuarioId", usuario.Id.ToString()),
                new Claim(ClaimTypes.Role, usuario.Tipo.ToString())

            }, "ApplicationCookie");

            Request.GetOwinContext().Authentication.SignIn(identity);



            if (!String.IsNullOrWhiteSpace(loginviewModel.UrlRetorno) || Url.IsLocalUrl(loginviewModel.UrlRetorno))
            {
                return RedirectToAction("Index", "Home");
            }
            // if (User.IsInRole("Faixa0A1"))
            //{
            //    return RedirectToAction("Index", "Formularios");
            //}

            // if (User.IsInRole("Faixa1A3"))
            //{
            //    return RedirectToAction("Index", "Formularios2");
            //}

            // if (User.IsInRole("Faixa4A5"))
            //{
            //    return RedirectToAction("Index", "Formularios3");
            //}

            // if (User.IsInRole("admin"))
            //{
            //    return RedirectToAction("Index", "Formularios");
            //}

            // if (User.IsInRole("Administrador"))
            //{
            //    return RedirectToAction("Index", "Administrador");
            //}

            //guardar dado para enviar o ID ao formulário
            IdUsuario = usuario.Id;
            ViewData["UsuarioId"] = IdUsuario;
            return RedirectToAction("Index", "Home", new { IdUsuario });

        }

        [Authorize]
        public ActionResult AlterarSenha()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult AlterarSenha(AlterarSenhaViewModel AlterarSenhaViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var identity = User.Identity as ClaimsIdentity;
            var login = identity.Claims.FirstOrDefault(c => c.Type == "Login").Value;

            var usuario = db.Usuarios.FirstOrDefault(u => u.Login == login);
            if (EsperaCriativa.Utils.Hash.GeraHash(AlterarSenhaViewModel.SenhaAtual) != usuario.Senha)
            {
                ModelState.AddModelError("SenhaAtual", "Senha incorreta");
                return View();
            }

            usuario.Senha = EsperaCriativa.Utils.Hash.GeraHash(AlterarSenhaViewModel.NovaSenha);
            db.Entry(usuario).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            TempData["Mensagem"] = "Senha alterada com sucesso";

            return RedirectToAction("Index", "Home");

        }


        public ActionResult Logout()
        {
            Request.GetOwinContext().Authentication.SignOut("ApplicationCookie");
            return RedirectToAction("Login", "Autenticacao");
        }

    }
}