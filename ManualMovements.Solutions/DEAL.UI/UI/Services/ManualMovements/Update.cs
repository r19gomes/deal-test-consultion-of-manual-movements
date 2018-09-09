using Deal.UI.Models.ManualMovements;
using Deal.UI.Services.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace Deal.UI.Services.ManualMovements
{
    public class Update
    {
        /// <summary>
        /// Cria um novo movimento manual.
        /// </summary>
        /// <param name="model">Modelo do movimento manual</param>
        /// <returns></returns>
        public static ViewModel Create(ViewModel model)
        {
            var urlApi = ConfigurationManager.AppSettings["Api_Server"];

            var request = new Models.ManualMovements.Update();
            ViewModel ret = new ViewModel();

            try
            {
                if (model != null)
                {
                    if (model.Update != null)
                    {
                        request = model.Update;
                    }
                }

                if (request != null)
                {
                    if (request.DataHora == null || request.DataHora <= DateTime.Now)
                        request.DataHora= DateTime.Now;
                    if (string.IsNullOrEmpty(request.Usuario))
                        request.Usuario = "TESTE";
                }

                string reqString = JsonConvert.SerializeObject(request);
                var retApiString = CallWebApi.CallWebApiPost(reqString, urlApi + "/ManualMovements/post");

                ret.Message = "Processado com sucesso!";

                ret.Success = true;
                ret.PersistFields = false;
            }
            catch (Exception ex)
            {
                ret.Success = false;
                ret.PersistFields = true;
                ret.Message = ex.Message.ToString();
            }

            return ret;
        }

    }
}