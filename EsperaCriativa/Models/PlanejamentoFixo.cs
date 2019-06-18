using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EsperaCriativa.Models
{
    [Table("PlanejamentosFixo")]
    public class PlanejamentoFixo
    {
        [Key]
        public int Id { get; set; }

        public string Informacao { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Data { get; set; }

        public int UsuarioId { get; set; }

        public int FormularioId { get; set; }

        public virtual Usuario Usuario { get; set; }
        public virtual ICollection<PlanejamentoFixoExperiencias> PlanejamentoFixoExperiencias { get; set; }
        public virtual ICollection<Experiencia> Experiencias { get; set; }


    }
}