using EsperaCriativa.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EsperaCriativa.ViewModels.Autenticacao
{
    public class CadastroUsuarioViewModel
    {
        [Required(ErrorMessage = "Informe o Nome")]
        [MaxLength(100, ErrorMessage = "O nome deve ter no máximo 100 caractéres")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Informe o Login")]
        [MaxLength(50, ErrorMessage = "O login deve ter no máximo 50 caractéres")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Informe a senha")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "A senha deve ter pelo menos 6 caractéres")]
        public string Senha { get; set; }

        [Required(ErrorMessage = "Confirme sua senha")]
        [MinLength(6, ErrorMessage = "A senha deve ter pelo menos 6 caractéres")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirmar senha")]
        [Compare(nameof(Senha), ErrorMessage = "A senhas não coincidem")]
        public string ConfirmarSenha { get; set; }

        [Required(ErrorMessage = "Informe o tipo de usuário")]
        public TipoUsuario Tipo { get; set; }
    }
}