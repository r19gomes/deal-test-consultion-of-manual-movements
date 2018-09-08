using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace deal.Models
{
    public class ProdutoModel
    {
        #region Builders

        public ProdutoModel()
        {
            ProdutosCosifs = new List<ProdutoCosifModel>();
        }

        #endregion

        #region Fields

        [MaxLength(4, ErrorMessage = "Código do produto só pode conter apenas 4 caracteres.")]
        public string ProdutoCodigo{ get; set; }

        [MaxLength(30, ErrorMessage = "Descrição do produto só pode conter até 30 caracteres.")]
        public string Descricao { get; set; }

        [MaxLength(1, ErrorMessage = "Status do produto só pode conter apenas 1 caracter.")]
        public string Status { get; set; }

        public virtual ICollection<ProdutoCosifModel> ProdutosCosifs { get; set; }

        #endregion
    }
}