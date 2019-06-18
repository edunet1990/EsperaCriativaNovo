using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EsperaCriativa.ViewModels.Autenticacao
{
    public class AlterarSenhaViewModel
    {
        [Required(ErrorMessage = "Informe sua senha atual")]
        [DataType(DataType.Password)]
        [Display(Name = "Senha atual")]
        [MinLength(6, ErrorMessage = "O nome deve ter no mínimo 6 caracteres")]
        public string SenhaAtual { get; set; }

        [Required(ErrorMessage = "Informe sua nova senha")]
        [DataType(DataType.Password)]
        [Display(Name = "Nova senha")]
        [MinLength(6, ErrorMessage = "O nome deve ter no mínimo 6 caracteres")]
        public string NovaSenha { get; set; }

        [Required(ErrorMessage = "Informe a senha novamente")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirmar senha")]
        [Compare(nameof(NovaSenha), ErrorMessage = "A senha e a confirmação não correspondem")]
        [MinLength(6, ErrorMessage = "O nome deve ter no mínimo 6 caracteres")]
        public string ConfirmacaoSenha { get; set; }
    }
}