using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace deal.Models
{
    [Table("PRODUTO_COSIF")]
    public class ProdutoCosifModel
    {
        [Key, Column(Order = 0)]
        [MaxLength(4, ErrorMessage = "Código do produto só pode conter até 4 caracteres.")]
        public string COD_PRODUTO { get; set; }

        [Key, Column(Order = 1)]
        [MaxLength(11, ErrorMessage ="Código do cosif só pode conter até 11 caracteres.")]
        public string COD_COSIF { get; set; }

        [MaxLength(6,ErrorMessage ="Código da classificação só pode conter até 6 caracteres.")]
        public string COD_CLASSIFICACAO { get; set; }

        [MaxLength(1, ErrorMessage = "Status do produto só pode conter apenas 1 caracter.")]
        public string STA_STATUS { get; set; }
    }
}