using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EsperaCriativa.ViewModels.Autenticacao
{
    public class LoginViewModel
    {
        public string UrlRetorno { get; set; }

        [Required(ErrorMessage = "Informe o login")]
        [MaxLength(50, ErrorMessage = "O nome deve ter até 50 caracteres")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Informe a senha")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "O nome deve ter no mínimo 6 caracteres")]
        public string Senha { get; set; }
    }
}