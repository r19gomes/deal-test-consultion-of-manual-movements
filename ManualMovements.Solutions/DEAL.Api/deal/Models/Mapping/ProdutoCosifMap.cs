using System.Data.Entity.ModelConfiguration;

namespace deal.Models.Mapping
{
    public class ProdutoCosifMap : EntityTypeConfiguration<ProdutoCosifModel>
    {
        public ProdutoCosifMap()
        {
            // Chave Primaria
            this.HasKey(t => t.ProdutoCodigo);
            this.HasKey(t => new { t.ProdutoCodigo, t.ProdutoCosif });

            // Propriedades
            this.Property(t => t.CodigoClassificacao)
                .HasMaxLength(6);
            this.Property(t => t.Status)
                .HasMaxLength(1);

            // Table e Mapeamento das Colunas
            this.ToTable("PRODUTO_COSIF");
            this.Property(t => t.ProdutoCodigo).HasColumnName("COD_PRODUTO");
            this.Property(t => t.ProdutoCosif).HasColumnName("COD_COSIF");
            this.Property(t => t.CodigoClassificacao).HasColumnName("COD_CLASSIFICACAO");
            this.Property(t => t.Status).HasColumnName("STA_STATUS");

            // Relacionamento 
            this.HasRequired(t => t.Produto)
                .WithMany(t => t.ProdutosCosifs)
                .HasForeignKey(d => d.ProdutoCodigo);
        }
    }
}