using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EsperaCriativa.Models
{
    public class FormularioExperiencia
    {
        [Key, Column(Order = 0)]
        public int Id { get; set; }

        [ForeignKey("Formulario"), Column(Order = 1)]
        public int FormularioId { get; set; }

        [ForeignKey("Experiencia"), Column(Order = 2)]
        public int ExperienciaId { get; set; }

        //[ForeignKey("Experiencia1A3"), Column(Order = 3)]
        //public int Experiencia2Id { get; set; }


        //[ForeignKey("Experiencias4A5"), Column(Order = 4)]
        //public int Experiencia3Id { get; set; }

        public virtual Formulario Formulario { get; set; }
        public virtual Experiencia Experiencia { get; set; }
        //public virtual Experiencia1A3 Experiencia1A3 { get; set; }

    }
}