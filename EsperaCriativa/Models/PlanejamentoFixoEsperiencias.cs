using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EsperaCriativa.Models
{
    public class PlanejamentoFixoExperiencias
    {
        [Key, Column(Order = 0)]
        public int PlanejamentoFixoExperienciasId { get; set; }

        [ForeignKey("PlanejamentoFixo"), Column(Order = 1)]
        public int PlanejamentoFixoId { get; set; }

        [ForeignKey("Experiencia"), Column(Order = 2)]
        public int ExperienciaId { get; set; }

        public virtual PlanejamentoFixo PlanejamentoFixo { get; set; }
        public virtual Experiencia Experiencia { get; set; }
    }
}