using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using deal.Models;
using System.Web.Http.Cors;

namespace deal.Controllers
{
    [RoutePrefix("api/productcosif")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ProductCosifController : ApiController
    {
        #region Fields

        private ManualMovementsContext db = new ManualMovementsContext();

        #endregion

        #region Builders

        public ProductCosifController()
        {

        }

        #endregion

        #region Methods

        [HttpGet]
        public IHttpActionResult Get
            (string produtoCodigo = null, string produtoCosif = null)
        {
            var message = string.Empty;
            IList<ProdutoCosifModel> model = new List<ProdutoCosifModel>();

            try
            {
                if ( !string.IsNullOrEmpty(produtoCodigo) || !string.IsNullOrEmpty(produtoCosif))
                {
                    var productCosif = db.ProdutosCosifs.Where
                        (x=> (x.ProdutoCodigo.ToString().Trim() == produtoCodigo.ToString().Trim() || produtoCodigo == null) 
                        && (x.ProdutoCosif.ToString().Trim() == produtoCosif.ToString().Trim() || produtoCosif == null))
                        .FirstOrDefault();
                    if (productCosif != null)
                    {
                        model.Add(new ProdutoCosifModel(productCosif));
                    }
                }
                else
                {
                    var productCosif = db.ProdutosCosifs;                    
                    foreach (var item in productCosif)
                    {
                        var product = new ProdutoModel();
                        product = item.Produto;
                        try
                        {
                            if (item.Produto == null || item.Produto.ProdutoCodigo == null)
                            {
                                if (item.Produto == null)
                                    item.Produto = new ProdutoModel();

                                var result = db.Produtos.Find(item.ProdutoCodigo);
                            }
                        }
                        catch (Exception ex)
                        {
                            message = ex.ToString();
                        }
                        var register = new ProdutoCosifModel
                        {
                            ProdutoCodigo = item.ProdutoCodigo,
                            ProdutoCosif = item.ProdutoCosif,
                            CodigoClassificacao = item.CodigoClassificacao,
                            Status = item.Status,
                            Produto = product
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

        #endregion
    }
}