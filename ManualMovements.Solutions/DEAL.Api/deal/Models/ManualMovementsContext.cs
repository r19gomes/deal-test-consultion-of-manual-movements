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
        }

        public DbSet<ProdutoModel> Produtos { get; set; }
        public DbSet<ProdutoCosifModel> ProdutosCosifs { get; set; }
        public DbSet<MovimentoManualModel> MovimentosManuais { get; set; }
    }
}