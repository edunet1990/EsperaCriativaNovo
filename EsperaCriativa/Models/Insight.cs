using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EsperaCriativa.Models
{
    [Table("GraficosInsight")]
    public class Insight
    {
        [Key]
        public int Id { get; set; }
        public int SurgiuInsightSim { get; set; }
        public int SurgiuInsightNao { get; set; }
        public DateTime Data { get; set; }

        public int UsuarioId { get; set; }
        public int FormularioID { get; set; }

        public virtual Usuario Usuario { get; set; }
    }
}