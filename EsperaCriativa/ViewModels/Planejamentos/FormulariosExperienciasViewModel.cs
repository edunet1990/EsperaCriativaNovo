using EsperaCriativa.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EsperaCriativa.ViewModels.Planejamentos
{
    public class FormulariosExperienciasViewModel
    {
        public int FormularioExperienciasViewModelID { get; set; }
        public int CheckBoxID { get; set; }
        public int FormularioID { get; set; }

        public string Informacao { get; set; }
        public string Name { get; set; }

        public bool Checked { get; set; }

        public int UsuarioId { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Data { get; set; }

        public int FormulariosID { get; set; }

        public List<FormulariosExperienciasViewModel> FormulariosExperienciasVM { get; set; }
        public List<FormulariosExperienciasViewModel> ExperienciasFormulariosVM { get; set; }
        public ComoFoiAExperienciaViewModel ComoFoiAExperiencia { get; set; }
        public Formulario Formularios { get; set; }

        //Variáveis para comparar se o insight foi inserido e depois apaga-lo
        public int UsuarioIdComoFoiAExper { get; set; }
        public List<int> FormIdComoFoiAExper = new List<int>();
        public bool OcultaInsight { get; set; }

    }
}