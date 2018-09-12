using deal.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.Http;


namespace deal.Controllers
{
    public class ManualMovementsController : ApiController
    {
        #region Fields

        private ManualMovementsContext db = new ManualMovementsContext();

        #endregion

        #region Builders

        public ManualMovementsController()
        {

        }

        #endregion

        #region Methods

        public IHttpActionResult Get
            (int? mes = null, int? ano = null, decimal? numero = null,
                string produtoCodigo = null, string produtoDescricao = null,
                string descricao = null, decimal? valor = null)
        {
            var message = string.Empty;
            IList<MovimentoManualGridModel> data = null;

            try
            {
                SqlConnection conn = new SqlConnection(db.Database.Connection.ConnectionString);

                SqlCommand cmd = new SqlCommand("USP_SEL_MOVIMENTOS_MANUAIS", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@DAT_MES", mes);
                cmd.Parameters.AddWithValue("@DAT_ANO", ano);
                cmd.Parameters.AddWithValue("@NUM_LANCAMENTO", numero);
                cmd.Parameters.AddWithValue("@COD_PRODUTO", produtoCodigo);
                cmd.Parameters.AddWithValue("@DES_PRODUTO", produtoDescricao);
                cmd.Parameters.AddWithValue("@VAL_VALOR", valor);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var items = new MovimentoManualGridModel();

                        items.LancamentoMes = int.Parse(reader["LancamentoMes"].ToString());
                        items.LancamentoAno = int.Parse(reader["LancamentoAno"].ToString());
                        items.ProdutoCodigo = reader["ProdutoCodigo"].ToString().Trim();
                        items.ProdutoNome = reader["ProdutoNome"].ToString().Trim();
                        items.LancamentoCosif = reader["LancamentoCosif"].ToString().Trim();
                        items.LancamentoNumero = decimal.Parse(reader["LancamentoNumero"].ToString());
                        items.LancamentoDescricao = reader["LancamentoDescricao"].ToString().Trim();
                        items.LancamentoValor = String.Format("{0:C}", reader["LancamentoValor"].ToString());
                        items.LancamentoDataHora = DateTime.Parse(reader["LancamentoDataHora"].ToString());
                        items.LancamentoUsuario = reader["LancamentoUsuario"].ToString().Trim();

                        if (data == null)
                            data = new List<MovimentoManualGridModel>();

                        data.Add(new MovimentoManualGridModel(items));
                    }
                }
                reader.Close();
                conn.Close();

                if (data == null)
                    return NotFound(); // Erro 400

                return Ok(data); //  Sucesso 200
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }


        [HttpPost]
        public IHttpActionResult Post(MovimentoManualModel data)
        {
            var message = string.Empty;
            try
            {
                //  Valida os requisitos
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                //  Valida se os campos desejados foram recebidos.
                if (data == null)
                {
                    message = "Parametros desejados não recebido.";
                    return BadRequest(message);
                }

                //  Valida se o produto existe na Tabela PRODUTO
                if (string.IsNullOrEmpty(data.ProdutoCodigo ))
                {
                    message = "Código do produto não foi informado.";
                    return BadRequest(message);
                } else
                {
                    var result = db.Produtos.Find(data.ProdutoCodigo);
                    if (result == null)
                    {
                        message = string.Format("Código do produto {0} não encontrado na entidade PRODUTO.", data.ProdutoCodigo);
                        return BadRequest(message);
                    }
                }

                //  Valida se a chave estrangeira de PRODUTO COSIF existe.
                if (string.IsNullOrEmpty(data.ProdutoCosif))
                {
                    message = "Código cosif não foi informado";
                } else
                {
                    var result = db.ProdutosCosifs.Find(new String[] { data.ProdutoCodigo, data.ProdutoCosif });
                    if (result == null)
                    {
                        message = string.Format("Produto cosif {0} não encontrado na entidade PRODUTO COSIF.", data.ProdutoCosif);
                        return BadRequest(message);
                    }
                }

                db.MovimentosManuais.Add(data);
                db.SaveChanges();

                //  Código 201 (Created-http) - Vamos retornarr o código 201 + URL + Classe Movimento Manual.
                return CreatedAtRoute("DefaultApi",
                    new
                    {
                        id = new string[] {
                        data.Mes.ToString(),
                        data.Ano.ToString(),
                        data.Numero.ToString(),
                        data.ProdutoCodigo,
                        data.ProdutoCosif
                        }
                    }, data);
            }
            catch (Exception ex)
            {
                message = ex.Message.ToString();
                return BadRequest(message);
            }

        }

        #endregion
    }
}
