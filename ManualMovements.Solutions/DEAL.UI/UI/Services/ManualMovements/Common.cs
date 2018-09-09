using Deal.UI.Models.ManualMovements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Deal.UI.Services.ManualMovements
{
    public class Common
    {

        public static Models.ManualMovements.Index ConvertJsonStringToIndex(string json)
        {
            var jss = new JavaScriptSerializer();
            Dictionary<string, string> data = jss.Deserialize<Dictionary<string, string>>(json);

            var result = new Models.ManualMovements.Index();
            result.Mes = int.Parse(data["Mes"].ToString());
            result.Ano = int.Parse(data["Ano"].ToString());
            result.Numero = decimal.Parse(data["Numero"].ToString());
            result.ProdutoCodigo = data["ProdutoCodigo"].ToString();
            result.ProdutoCosif = data["ProdutoCosif"].ToString();
            result.DataHora =  DateTime.Parse(data["DataHora"].ToString());
            result.Valor = decimal.Parse(data["Valor"].ToString());
            result.Descricao = data["Descricao"].ToString();
            result.Usuario = data["Usuario"].ToString();

            return result;
        }

    }
}