using deal.Models;
using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Linq;

namespace deal.Controllers
{
    [RoutePrefix("api/product")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ProductController : ApiController
    {
        #region Fields

        private ManualMovementsContext db = new ManualMovementsContext();

        #endregion

        #region Builders

        public ProductController()
        {

        }

        #endregion


        #region Methods

        [HttpGet]
        public IHttpActionResult Get(string produtoId = null)
        {
            var message = string.Empty;
            IList<ProdutoModel> model = new List<ProdutoModel>();

            try
            {
                if (produtoId != null)
                {
                    var product = db.Produtos.Find(produtoId);
                    if (product != null)
                    {
                        model.Add(new ProdutoModel(product));
                    }
                }
                else
                {
                    var product = db.Produtos;
                    foreach (var item in product)
                    {
                        var productCosifs = ProdutosCosifs(item.ProdutoCodigo.ToString().Trim());
                        var register = new ProdutoModel
                        {
                            ProdutoCodigo = item.ProdutoCodigo,
                            Descricao = item.Descricao,
                            Status = item.Status
                        };
                        model.Add(register);
                    }
                }

                if (model == null)
                    return NotFound(); // Erro 404s

                return Ok(model); // Sucesso 200
            }
            catch (Exception ex)
            {
                message = ex.Message.ToString();
                throw new Exception(message);
            }
        }

        private IList<ProdutoCosifModel> ProdutosCosifs(string produtoCodigo)
        {
            IList<ProdutoCosifModel> model = new List<ProdutoCosifModel>();
            var message = string.Empty;
            try
            {

                if (!string.IsNullOrEmpty(produtoCodigo))
                {
                    var cosifs = db.ProdutosCosifs
                        .Where(x => x.ProdutoCosif == produtoCodigo.Trim());
                    foreach (var item in cosifs)
                    {
                        model.Add(new ProdutoCosifModel
                        {
                            ProdutoCodigo = item.ProdutoCodigo,
                            ProdutoCosif = item.ProdutoCosif,
                            CodigoClassificacao = item.CodigoClassificacao,
                            Status = item.Status,
                            Produto = null
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                message = ex.ToString();
            }
            return model;
        }

        #endregion
    }
}
