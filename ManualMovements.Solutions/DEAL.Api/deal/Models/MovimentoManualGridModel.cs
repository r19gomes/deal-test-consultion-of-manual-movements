using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace deal.Models
{
    public class MovimentoManualGridModel
    {
        #region Builders

        public MovimentoManualGridModel()
        {
        }

        public MovimentoManualGridModel(MovimentoManualGridModel data) : base()
        {
            LancamentoMes = data.LancamentoMes;
            LancamentoAno = data.LancamentoAno;
            ProdutoCodigo = data.ProdutoCodigo;
            ProdutoNome = data.ProdutoNome;
            LancamentoCosif = data.LancamentoCosif;
            LancamentoNumero = data.LancamentoNumero;
            LancamentoDescricao = data.LancamentoDescricao;
            LancamentoValor = data.LancamentoValor;
            LancamentoDataHora = data.LancamentoDataHora;
            LancamentoUsuario = data.LancamentoUsuario;
        }

        #endregion

        #region Fields

        public int? LancamentoMes { get; set; }

        public int? LancamentoAno { get; set; }

        public string ProdutoCodigo { get; set; }

        public string ProdutoNome { get; set; }

        public string LancamentoCosif { get; set; }

        public decimal? LancamentoNumero { get; set; }

        public string LancamentoDescricao { get; set; }

        public string LancamentoValor { get; set; }

        public DateTime? LancamentoDataHora { get; set; }

        public string LancamentoUsuario { get; set; }

        #endregion
    }
}