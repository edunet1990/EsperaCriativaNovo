using EsperaCriativa.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace EsperaCriativa.Contexto
{
    public class EsperaCriativaContext : DbContext
    {
        public EsperaCriativaContext() : base("EsperaCriativaConexao")
        {

        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Formulario> Formularios { get; set; }
        public DbSet<Experiencia> Experiencias { get; set; }
        public DbSet<Experiencia1A3> Experiencias1A3 { get; set; }
        public DbSet<Experiencia4A5> Experiencias4A5 { get; set; }
        public DbSet<FormularioExperiencia> FormularioExperiencias { get; set; }
        public DbSet<ComoFoiAExperiencia> ComoFoiAsExperiencias { get; set; }
        public DbSet<Insight> Insights { get; set; }
        public DbSet<PlanejamentoFixo> PlanejamentosFixo { get; set; }
        public DbSet<PlanejamentoFixoExperiencias> PlanejamentosFixoExperiencias { get; set; }
    }
}