﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EsperaCriativa.ViewModels.Planejamentos
{
    public class FormulariosViewModel
    {
        public int FormularioID { get; set; }
        public string Informacao { get; set; }
        public int UsuarioId { get; set; }
        public int PlanejamentoFixoId { get; set; }



        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Data { get; set; }


        public List<CheckBoxViewModel> Experiencias { get; set; }
        public List<FormulariosViewModel> Formularios { get; set; }


    }
}