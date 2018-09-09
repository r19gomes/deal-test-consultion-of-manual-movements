using System.Data.Entity.ModelConfiguration;

namespace deal.Models.Mapping
{
    public class ProdutoMap : EntityTypeConfiguration<ProdutoModel>
    {
        public ProdutoMap()
        {
            // Chave Primaria
            this.HasKey(t => t.ProdutoCodigo);

            // Propriedades
            this.Property(t => t.Descricao)
                .HasMaxLength(30);
            this.Property(t => t.Status)
                .HasMaxLength(1);

            // Table e Mapeamento das Colunas
            this.ToTable("PRODUTO");
            this.Property(t => t.ProdutoCodigo).HasColumnName("COD_PRODUTO");
            this.Property(t => t.Descricao).HasColumnName("DES_PRODUTO");
            this.Property(t => t.Status).HasColumnName("STA_STATUS");

        }
    }
}


