using deal.Models.Mapping;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace deal.Models
{
    public class ManualMovementsContext: DbContext
    {
        public ManualMovementsContext() : base("DealLocal")
        {
#if DEBUG
            Database.Log = d => System.Diagnostics.Debug.WriteLine(d);
#endif
            //Database.SetInitializer<ManualMovementsContext>(null);
        }

        public DbSet<ProdutoModel> Produtos { get; set; }
        public DbSet<ProdutoCosifModel> ProdutosCosifs { get; set; }
        public DbSet<MovimentoManualModel> MovimentosManuais { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ProdutoMap());
            modelBuilder.Configurations.Add(new ProdutoCosifMap());
            modelBuilder.Configurations.Add(new MovimentoManualMap());
        }
    }
}