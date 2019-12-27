using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RoboRaspaTela.Utils
{
    internal class Web
    {
        public static dynamic Get(string url, IDictionary<string, string> parametros)
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
    }
}
