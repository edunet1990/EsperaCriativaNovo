using EsperaCriativa.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EsperaCriativa.ViewModels.Forum
{
    public class ForumViewModel
    {
        public int Id { get; set; }
        public int ComoFoiAExperienciaID { get; set; }
        public string MsgComoFoiAExperiencia { get; set; }

        [Required]
        [DataType(DataType.Upload)]
        [Display(Name = "Imagem")]
        public HttpPostedFileBase ImageUpload { get; set; }

        public bool CompartilharExperiencia { get; set; }

        public int UsuarioId { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}