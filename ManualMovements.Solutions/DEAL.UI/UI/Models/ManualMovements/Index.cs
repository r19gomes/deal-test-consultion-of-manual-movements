using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Deal.UI.Models.ManualMovements
{
    public class Index
    {
        #region Fields

        [Required]
        [DisplayName("Mês")]
        public int Mes { get; set; }

        [Required]
        [DisplayName("Ano")]
        public int Ano { get; set; }

        [Required]
        [DisplayName("Código do Produto")]
        [StringLength(4, ErrorMessage = @"Código do produto deve ter tamanho no máximo 4.")]
        public string ProdutoCodigo { get; set; }

        [DisplayName("Cosif")]
        [StringLength(11, ErrorMessage = "Cosif deve ter tamanho no máximo 11.")]
        public string ProdutoCosif { get; set; }

        [DisplayName("Descrição do Produto")]
        public string ProdutoNome { get; set; }

        [Required]
        [DisplayName("NR Lançamento")]
        public decimal Numero { get; set; }

        [DisplayName("Descrição")]
        [StringLength(50, ErrorMessage = "Descrição do lançamento deve ter tamanho no máximo 30.")]
        public string Descricao { get; set; }

        [DisplayName("Valor")]
        public decimal Valor { get; set; }

        [DisplayName("Data/Hora")]
        public DateTime DataHora { get; set; }

        [DisplayName("Usuário")]
        [StringLength(15, ErrorMessage = "Usuário do lançamento deve ter tamanho no máximo 15.")]
        public string Usuario { get; set; }

        #endregion
    }
}
