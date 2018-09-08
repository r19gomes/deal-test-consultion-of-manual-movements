using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace deal.Models
{
    [Table("MOVIMENTO_MANUAL")]
    public class MovimentoManualModel
    {
        #region Builders
        public MovimentoManualModel()
        {
        }

        public MovimentoManualModel(MovimentoManualModel data) : base()
        {
            DAT_MES = data.DAT_MES;
            DAT_ANO = data.DAT_ANO;
            NUM_LANCAMENTO = data.NUM_LANCAMENTO;
            COD_USUARIO = data.COD_USUARIO;
            COD_COSIF = data.COD_COSIF;
            DAT_MOVIMENTO = data.DAT_MOVIMENTO;
            VAL_VALOR = data.VAL_VALOR;
            DES_DESCRICAO = data.DES_DESCRICAO;
            COD_USUARIO = data.COD_USUARIO;
        }

        #endregion

        #region Fields

        [Key, Column(Order = 0)]
        public int DAT_MES { get; set; }

        public int DAT_ANO { get; set; }

        public decimal NUM_LANCAMENTO { get; set; }

        [MaxLength(4, ErrorMessage = "Código do produto só pode conter apenas 4 caracteres.")]
        public string COD_PRODUTO { get; set; }

        [MaxLength(11, ErrorMessage = "Código do cosif só pode conter até 11 caracteres.")]
        public string COD_COSIF { get; set; }

        public DateTime DAT_MOVIMENTO { get; set; }

        public decimal VAL_VALOR { get; set; }

        [MaxLength(50, ErrorMessage = "Descrição do lançamento só pode conter até 50 caracteres.")]
        public string DES_DESCRICAO { get; set; }

        [MaxLength(15, ErrorMessage = "Usuário só pode conter até 15 caracteres.")]
        public string COD_USUARIO { get; set; }

        #endregion
    }
}