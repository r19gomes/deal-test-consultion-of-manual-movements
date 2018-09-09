using Deal.UI.Models.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Deal.UI.Services.Common
{
    public class CallWebApi
    {
        public static string CallWebApiPost
            (string postData, string url, int timeout = 0, Errors errors = null)
        {
            string ret = "";
            errors = new Errors();

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

            byte[] bytes;
            bytes = System.Text.Encoding.UTF8.GetBytes(postData);

            request.ContentType = "application/json; charset=UTF-8";
            request.ContentLength = bytes.Length;
            request.Method = "POST";
            if (timeout > 0)
                request.Timeout = timeout;

            Stream requestStream = request.GetRequestStream();
            requestStream.Write(bytes, 0, bytes.Length);
            requestStream.Close();

            try
            {
                HttpWebResponse response;
                response = (HttpWebResponse)request.GetResponse();
                if ((response.StatusCode == HttpStatusCode.Created) || (response.StatusCode == HttpStatusCode.OK))
                {
                    Stream responseStream = response.GetResponseStream();
                    ret = new StreamReader(responseStream).ReadToEnd();
                }
            }
            catch (WebException ex)
            {
                if (errors == null)
                    errors = new Errors();
                errors.Code = 1;
                errors.SourceCode = null;
                errors.Name = ex.Message.ToString();
                errors.Description = null;
                errors.Message = ex.Message.ToString();
            }

            return ret;
        }

        public async static Task<string> CallWebApiGet
            (Dictionary<string, string> header, string url, int timeout = 0, Errors errors = null)
        {
            string ret = "";
            errors = new Errors();

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);

                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));

                    if (header != null)
                    {
                        if (header.Any())
                        {
                            foreach (var item in header)
                                client.DefaultRequestHeaders.Add(item.Key, item.Value);
                        }
                    }

                    if (timeout > 0)
                        client.Timeout = new TimeSpan(0, 0, 0, 0, timeout);

                    HttpResponseMessage response = client.GetAsync(url).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        ret = await response.Content.ReadAsStringAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                if (errors == null)
                    errors = new Errors();
                errors.Code = 1;
                errors.SourceCode = null;
                errors.Name = ex.Message.ToString();
                errors.Description = null;
                errors.Message = ex.Message.ToString();
            }

            return ret;
        }
    }
}