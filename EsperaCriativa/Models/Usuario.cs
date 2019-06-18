using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EsperaCriativa.Models
{
    [Table("Usuarios")]
    public class Usuario
    {


        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Nome { get; set; }

        [Required]
        [MaxLength(100)]
        public string Login { get; set; }

        [Required]
        [MaxLength(100)]
        public string Senha { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Data { get; set; }


        public TipoUsuario Tipo { get; set; } = TipoUsuario.Padrao;

    }
}