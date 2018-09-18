using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace deal.Models.Mapping
{
    public class MovimentoManualMap: EntityTypeConfiguration<MovimentoManualModel>
    {
        public MovimentoManualMap()
        {
            // Chave Primaria
            this.HasKey(t => new { t.Mes, t.Ano, t.Numero, t.ProdutoCodigo, t.ProdutoCosif });

            // Propriedades
            this.Property(t => t.DataHora)
                .HasColumnType("datetime2");
            this.Property(t => t.Valor)
                .HasColumnType("numeric");
            this.Property(t => t.Descricao)
                .HasColumnType("varchar").HasMaxLength(50);
            this.Property(t => t.Usuario)
                .HasColumnType("varchar").HasMaxLength(15);

            // Table e Mapeamento das Colunas
            this.ToTable("MOVIMENTO_MANUAL");
            this.Property(t => t.Mes).HasColumnName("DAT_MES");
            this.Property(t => t.Ano).HasColumnName("DAT_ANO");
            this.Property(t => t.Numero).HasColumnName("NUM_LANCAMENTO");
            this.Property(t => t.ProdutoCodigo).HasColumnName("COD_PRODUTO");
            this.Property(t => t.ProdutoCosif).HasColumnName("COD_COSIF");
            this.Property(t => t.DataHora).HasColumnName("DAT_MOVIMENTO");
            this.Property(t => t.Valor).HasColumnName("VAL_VALOR");
            this.Property(t => t.Descricao).HasColumnName("DES_DESCRICAO");
            this.Property(t => t.Usuario).HasColumnName("COD_USUARIO");

            // Relacionamento 
            this.HasRequired(t => t.ProdutoCosifs)
                .WithMany(t => t.MovimentosManuais)
                .HasForeignKey(d => new { d.ProdutoCodigo, d.ProdutoCosif });

        }
    }
}