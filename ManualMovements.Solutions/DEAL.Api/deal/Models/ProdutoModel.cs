using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace deal.Models
{
    [Table("PRODUTO")]
    public class ProdutoModel
    {
        [Key, Column(Order = 0)]
        [MaxLength(4, ErrorMessage ="Código do produto só pode conter apenas 4 caracteres.")]
        public string COD_PRODUTO { get; set; }

        [MaxLength(30, ErrorMessage ="Descrição do produto só pode conter até 30 caracteres.")]
        public string DES_PRODUTO { get; set; }

        [MaxLength(1, ErrorMessage ="Status do produto só pode conter apenas 1 caracter.")]
        public string STA_STATUS { get; set; }
    }
}