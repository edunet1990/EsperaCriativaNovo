using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EsperaCriativa.Models
{
    [Table("ComoFoiAExperiencia")]
    public class ComoFoiAExperiencia
    {
        [Key]
        public int ComoFoiAExperienciaID { get; set; }
        public string MsgComoFoiAExperiencia { get; set; }
        public string Imagem { get; set; }
        public bool CompartilharExperiencia { get; set; }


        public int PlanejamentoFixoId { get; set; }

        [ForeignKey("Usuario")]
        public int UsuarioId { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}