using cpj.portal.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace cpj.portal.Utils
{
    public class Web
    {
        public static dynamic Get(string url, IDictionary<dynamic, dynamic> parametros) 
        {

            string param = "?";
            foreach (var item in parametros)
            {
                param += $"{item.Key}={item.Value}&";
            }
                
            HttpClient client = new HttpClient();
            var response = client.GetAsync(url + param.Substring(0, param.Count() - 1)).Result;
            return response.Content.ReadAsStringAsync().Result;
        }
        public static dynamic Get(string url)
        {
            HttpClient client = new HttpClient();
            var response = client.GetAsync(url).Result;
            return response.Content.ReadAsStringAsync().Result;
        }

        public static dynamic Post(string url, object parametros)
        {
            string json = JsonConvert.SerializeObject(parametros);

            HttpClient client = new HttpClient();
            using (var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json"))
            {
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                var response = client.PostAsync(url, content).Result;
                return response.Content.ReadAsStringAsync().Result;
            }            
        }

        public static object Put(string url, ConfiguracaoViewModel configuracao)
        {
            string json = JsonConvert.SerializeObject(configuracao);

            HttpClient client = new HttpClient();
            using (var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json"))
            {
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                var response = client.PutAsync(url, content).Result;
                return response.Content.ReadAsStringAsync().Result;
            }
        }
    }
}
