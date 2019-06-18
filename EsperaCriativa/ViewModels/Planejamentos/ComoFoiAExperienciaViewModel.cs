using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EsperaCriativa.ViewModels.Planejamentos
{
    public class ComoFoiAExperienciaViewModel
    {

        public int ComoFoiAExperienciaID { get; set; }
        public string MsgComoFoiAExperiencia { get; set; }
        public bool CompartilharExperiencia { get; set; }

        public int PanejamentoFixoId { get; set; }
        public int FormularioId { get; set; }
        public int UsuarioId { get; set; }

        [Required]
        [DataType(DataType.Upload)]
        [Display(Name = "Imagem")]
        public HttpPostedFileBase ImageUpload { get; set; }
    }
}