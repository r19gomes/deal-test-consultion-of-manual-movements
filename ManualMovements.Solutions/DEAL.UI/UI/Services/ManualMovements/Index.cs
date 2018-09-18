using Deal.UI.Services.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace Deal.UI.Services.ManualMovements
{
    public class Index
    {
        public static IList<Models.ManualMovements.Index>
            List(Models.ManualMovements.Index filter = null)
        {
            var urlApi = ConfigurationManager.AppSettings["Api_Server"];
            var request = new Models.ManualMovements.Index();

            if (filter != null)
            {
                request = filter;
            }

            string reqString = JsonConvert.SerializeObject(request);
            var retApiString = CallWebApi.CallWebApiGet(null, urlApi + "/ManualMovements/");
            var retApi = retApiString.Result;
            var ret = new List<Models.ManualMovements.Index>();

            List<Dictionary<string, string>> data = JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(retApi);
            if (data != null)
            {
                foreach (Dictionary<string, string> lst in data)
                {
                    var register = new Models.ManualMovements.Index();
                    foreach (KeyValuePair<string, string> item in lst)
                    {
                        if (register != null)
                        {
                            if (!string.IsNullOrEmpty(item.Key))
                            {
                                if (item.Value != null)
                                {
                                    register.Mes = item.Key.ToLower().Trim() == "lancamentomes" ? int.Parse(item.Value) : register.Mes;
                                    register.Ano = item.Key.ToLower().Trim() == "lancamentoano" ? int.Parse(item.Value) : register.Ano;
                                    register.ProdutoCodigo = item.Key.ToLower().Trim() == "produtocodigo" ? item.Value : register.ProdutoCodigo;
                                    register.ProdutoNome = item.Key.ToLower().Trim() == "produtonome" ? item.Value : register.ProdutoNome;
                                    register.ProdutoCosif = item.Key.ToLower().Trim() == "lancamentocosif" ? item.Value : register.ProdutoCosif;
                                    register.Numero = item.Key.ToLower().Trim() == "lancamentonumero" ? decimal.Parse(item.Value) : register.Numero;
                                    register.Descricao = item.Key.ToLower().Trim() == "lancamentodescricao" ? item.Value : register.Descricao;
                                    register.Valor = item.Key.ToLower().Trim() == "lancamentovalor" ? decimal.Parse(item.Value) : register.Valor;
                                    register.DataHora = item.Key.ToLower().Trim() == "lancamentodatahora" ? DateTime.Parse(item.Value) : register.DataHora;
                                    register.Usuario = item.Key.ToLower().Trim() == "lancamentousuario" ? item.Value : register.Usuario;
                                }
                            }
                        }
                    }
                    ret.Add(register);
                }
            }

            return ret;
        }

    }
}