using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EsperaCriativa.Models
{
    [Table("Experiencias4A5")]
    public class Experiencia4A5
    {
        [Key]
        public int ExperienciaId { get; set; }
        public string InfoExperiencia { get; set; }

        public virtual ICollection<FormularioExperiencia> FormulariosExperiencias { get; set; }
    }
}