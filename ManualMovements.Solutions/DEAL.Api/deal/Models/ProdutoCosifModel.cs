using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace deal.Models
{
    public class ProdutoCosifModel
    {
        #region Builders

        public ProdutoCosifModel()
        {
            MovimentosManuais = new List<MovimentoManualModel>();
        }

        #endregion

        #region Fields

        [MaxLength(4, ErrorMessage = "Código do produto só pode conter até 4 caracteres.")]
        public string ProdutoCodigo { get; set; }

        [MaxLength(11, ErrorMessage ="Código do cosif só pode conter até 11 caracteres.")]
        public string ProdutoCosif { get; set; }

        [MaxLength(6,ErrorMessage ="Código da classificação só pode conter até 6 caracteres.")]
        public string CodigoClassificacao { get; set; }

        [MaxLength(1, ErrorMessage = "Status do produto só pode conter apenas 1 caracter.")]
        public string Status { get; set; }

        public virtual ProdutoModel Produto { get; set; }

        public virtual ICollection<MovimentoManualModel> MovimentosManuais { get; set; }

        #endregion
    }
}