using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EsperaCriativa.ViewModels.Graficos
{
    public class HabilidadesCriativasViewModel
    {
        public int HabilidadesCriativasGraficoID { get; set; }
        public int SurgiuInsightSim { get; set; }
        public int SurgiuInsightNao { get; set; }
        public bool DesejaCompartilhar { get; set; }

        public int FormularioId { get; set; }
    }
}