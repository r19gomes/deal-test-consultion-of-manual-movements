using deal.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.Http;
using System.Linq;
using System.Net;
using System.Web.Http.Cors;

namespace deal.Controllers
{
    [RoutePrefix("api/manualmovements")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
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

        [HttpGet]
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

                //  Valida o valor do campo Mês recebido.
                if (!(data.Mes >= 1 && data.Mes <= 12))
                {
                    message = string.Format
                        ("Mês '{0}' inválido. Permitido o intervalo de mês 1 a 12.", data.Mes);
                    return BadRequest(message);
                }

                //  Valida o valor do campo Ano recebido.
                if (data.Ano < 1980)
                {
                    message = string.Format
                        ("Ano '{0}' inválido. Informe o ano igual ou superior 1980.", data.Ano);
                    return BadRequest(message);
                }

                //  Valida o valor do campo Número do Lançamento do Movimento recebido.
                if (data.Numero <= 0)
                {
                    // Obtém o próximo número disponível.
                    decimal? result = db.MovimentosManuais.Max(u => u.Numero);
                    decimal numero = 0;
                    decimal.TryParse(result.ToString(), out numero);
                    data.Numero = ++numero;
                }

                //  Valida se Número do Lançamento do Movimento já existe na base.
                if (data.Numero > 0)
                {
                    var result = db.MovimentosManuais
                        .Where(x => x.Mes > 0 && x.Ano > 0 && x.Numero == data.Numero);
                    if (result != null)
                    {
                        var query = result.ToList();
                        if (query.Count > 0)
                        {
                            message = string.Format
                                ("Número do Lançamento do Movimento '{0}' existente. Informe um número superior.",
                                    data.Numero);
                            return BadRequest(message);
                        }

                    }
                }

                //  Valida se o produto existe na Tabela PRODUTO
                if (string.IsNullOrEmpty(data.ProdutoCodigo))
                {
                    message = "Código do produto não foi informado.";
                    return BadRequest(message);
                }
                else
                {
                    var result = db.Produtos.Find(data.ProdutoCodigo);
                    if (result == null)
                    {
                        message = string.Format("Código do produto '{0}' não encontrado na entidade PRODUTO.", data.ProdutoCodigo);
                        return BadRequest(message);
                    }
                }

                //  Valida se a chave estrangeira de PRODUTO COSIF existe.
                if (string.IsNullOrEmpty(data.ProdutoCosif))
                {
                    message = "Código cosif não foi informado";
                    return BadRequest(message);
                }
                else
                {
                    var result = db.ProdutosCosifs.Find(new String[] { data.ProdutoCodigo, data.ProdutoCosif });
                    if (result == null)
                    {
                        message = string.Format("Produto cosif '{0}' não encontrado na entidade PRODUTO COSIF.", data.ProdutoCosif);
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

        //[AcceptVerbs("PUT")]
        [HttpPut]
        public IHttpActionResult Put(int mes, int ano, decimal numero, string produtoCodigo, string produtoCosif,
            MovimentoManualModel model)
        {
            var message = string.Empty;

            if (!ModelState.IsValid)
                return BadRequest(ModelState); // Erro 400

            //  Valida se os campos desejados foram recebidos.
            if (model == null)
            {
                message = "Modelo dos parâmetros/campos desejados não foi recebido.";
                return BadRequest(message);
            }

            //  Valida se o parâmetro/campo Mês desejado foi recebido.
            if (!(model.Mes >= 1 && model.Mes <= 12))
            {
                message = string.Format
                    ("Mês '{0}' inválido. Permitido o intervalo de mês 1 a 12.", model.Mes);
                return BadRequest(message);
            }
            if (mes != model.Mes)
            {
                message = string.Format("O mês informado '{0}' é diferente do Mês informado no corpo da requisição '{1}'.",
                    mes, model.Mes);
                return BadRequest(message);
            }

            //  Valida se o parâmetro/campo Ano desejado foi recebido.
            if (model.Ano < 1980)
            {
                message = string.Format
                    ("Ano '{0}' inválido. Informe o ano igual ou superior 1980.", model.Ano);
                return BadRequest(message);
            }
            if (ano != model.Ano)
            {
                message = string.Format("O ano informado '{0}' é diferente do Ano informado no corpo da requisição '{1}'.",
                    ano, model.Ano);
                return BadRequest(message);
            }

            //  Valida se o parâmetro/campo Número do Lançamento desejado foi recebido.
            if (numero != model.Numero)
            {
                message = string.Format("O número informado '{0}' é diferente do Número do Lançamento informado no corpo da requisição '{1}'.",
                    numero, model.Numero);
                return BadRequest(message);
            }

            //  Valida se o parâmetro/campo Código do Produto desejado foi recebido.
            if (string.IsNullOrEmpty(model.ProdutoCodigo))
            {
                message = "Código do produto não foi informado.";
                return BadRequest(message);
            }
            else
            {
                var result = db.Produtos.Find(model.ProdutoCodigo);
                if (result == null)
                {
                    message = string.Format("Código do produto '{0}' não encontrado na entidade PRODUTO.", model.ProdutoCodigo);
                    return BadRequest(message);
                }
            }
            if (produtoCodigo.Trim().ToLower() != model.ProdutoCodigo.Trim().ToLower())
            {
                message = string.Format("O código do produto '{0}' informado é diferente do Código do Produto informado no corpo da requisição '{1}'.",
                    produtoCodigo, model.ProdutoCodigo);
                return BadRequest(message);
            }

            //  Valida se o parâmetro/campo Código Cosif desejado foi recebido.
            if (string.IsNullOrEmpty(model.ProdutoCosif))
            {
                message = "Código cosif não foi informado";
                return BadRequest(message);
            }
            else
            {
                var result = db.ProdutosCosifs.Find(new String[] { model.ProdutoCodigo, model.ProdutoCosif });
                if (result == null)
                {
                    message = string.Format("Produto cosif '{0}' não encontrado na entidade PRODUTO COSIF.", model.ProdutoCosif);
                    return BadRequest(message);
                }
            }
            if (produtoCosif.Trim().ToLower() != model.ProdutoCosif.Trim().ToLower())
            {
                message = string.Format("O código cosif '{0}' informado é diferente do Código Cosif informado no corpo da requisição '{1}'.",
                    produtoCosif, model.ProdutoCosif);
                return BadRequest();
            }

            /*  Valida se o registro do Movimento Manual está cadastrado na tabela MOVIMENTOS MANUAIS 
            **  prosseguir com a alteração dos campos.
            */
            if (db.MovimentosManuais.Count(c => c.Mes == mes && c.Ano == ano &&
                c.Numero == numero && c.ProdutoCodigo == produtoCodigo && c.ProdutoCosif == produtoCosif) == 0)
            {
                return NotFound();
            }

            db.Entry(model).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        //[AcceptVerbs("DELETE")]
        [HttpDelete]
        public IHttpActionResult Delete(int mes, int ano, decimal numero, string produtoCodigo, string produtoCosif)
        {
            var message = string.Empty;

            if (mes <= 0)
            {
                message = string.Format("O mês '{0}' deve ser um número maior que zero.", mes);
                return BadRequest(message);
            }

            if (ano <= 0)
            {
                message = string.Format("O ano '{0}' deve ser um número maior que zero.", ano);
                return BadRequest(message);
            }

            if (numero <= 0)
            {
                message = string.Format("O número do lançamento '{0}' deve ser um número maior que zero.", numero);
                return BadRequest(message);
            }

            if (string.IsNullOrEmpty(produtoCodigo))
            {
                message = string.Format("O código do produto '{0}' deve ser informado.", produtoCodigo);
                return BadRequest(message);
            }

            if (string.IsNullOrEmpty(produtoCosif))
            {
                message = string.Format("O código cosif '{0}' deve ser informado.", produtoCosif);
                return BadRequest(message);
            }

            var sql = string.Format
                ("SELECT * FROM MOVIMENTO_MANUAL WHERE DAT_MES = {0} AND DAT_ANO = {1} AND NUM_LANCAMENTO = {2} AND COD_PRODUTO = '{3}' AND COD_COSIF = '{4}'",
                    mes, ano, numero, produtoCodigo.Trim(), produtoCosif.Trim());
            SqlConnection conn = new SqlConnection(db.Database.Connection.ConnectionString);
            SqlCommand cmd = new SqlCommand(sql, conn);
            conn.Open();

            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    var delete = string.Format("DELETE FROM MOVIMENTO_MANUAL WHERE  DAT_MES = {0} AND DAT_ANO = {1} AND NUM_LANCAMENTO = {2} AND COD_PRODUTO = '{3}' AND COD_COSIF = '{4}'",
                        mes, ano, numero, produtoCodigo.Trim(), produtoCosif.Trim());

                    conn = new SqlConnection(db.Database.Connection.ConnectionString);
                    SqlCommand deleted = new SqlCommand(delete, conn);
                    deleted.Connection.Open();
                    deleted.ExecuteNonQuery();
                }
            }
            else
            {
                return NotFound();
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        #endregion
    }
}
