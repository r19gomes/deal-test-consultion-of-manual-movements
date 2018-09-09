using System.ComponentModel;

namespace Deal.UI.Models.ManualMovements
{
    public class Filter
    {
        [DisplayName("Mês")]
        public int Mes { get; set; }

        [DisplayName("Ano")]
        public int Ano { get; set; }

        [DisplayName("NR Lançamento")]
        public decimal Numero { get; set; }

        [DisplayName("Código do Produto")]
        public string ProdutoCodigo { get; set; }

        [DisplayName("Descrição do Produto")]
        public string ProdutoNome { get; set; }

        [DisplayName("Descrição")]
        public string Descricao { get; set; }

        [DisplayName("Valor")]
        public decimal Valor { get; set; }

    }
}
