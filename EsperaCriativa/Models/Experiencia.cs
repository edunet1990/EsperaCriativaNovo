using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EsperaCriativa.Models
{
    [Table("Experiencias")]
    public class Experiencia
    {
        [Key]
        public int ExperienciaId { get; set; }
        public string InfoExperiencia { get; set; }

        public virtual ICollection<FormularioExperiencia> FormulariosExperiencias { get; set; }

    }
}