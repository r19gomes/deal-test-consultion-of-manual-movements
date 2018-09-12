using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace deal.Models
{
    public class MovimentoManualModel
    {
        #region Builders
        public MovimentoManualModel()
        {
        }

        public MovimentoManualModel(MovimentoManualModel data) : base()
        {
            Mes = data.Mes;
            Ano = data.Ano;
            Numero = data.Numero;
            ProdutoCodigo = data.ProdutoCodigo;
            ProdutoCosif = data.ProdutoCosif;
            DataHora = data.DataHora;
            Valor = data.Valor;
            Descricao = data.Descricao;
            Usuario = data.Usuario;
        }

        #endregion

        #region Fields

        public int Mes { get; set; }

        public int Ano { get; set; }

        public decimal Numero { get; set; }

        [MaxLength(4, ErrorMessage = "Código do produto só pode conter apenas 4 caracteres.")]
        public string ProdutoCodigo { get; set; }

        [MaxLength(11, ErrorMessage = "Código do cosif só pode conter até 11 caracteres.")]
        public string ProdutoCosif { get; set; }

        public DateTime DataHora { get; set; }

        public decimal Valor { get; set; }

        [MaxLength(50, ErrorMessage = "Descrição do lançamento só pode conter até 50 caracteres.")]
        public string Descricao { get; set; }

        [MaxLength(15, ErrorMessage = "Usuário só pode conter até 15 caracteres.")]
        public string Usuario { get; set; }

        public virtual ProdutoCosifModel ProdutoCosifs { get; set; }

        #endregion
    }
}