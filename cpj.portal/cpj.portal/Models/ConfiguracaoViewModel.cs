using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cpj.portal.Models
{
    public class ConfiguracaoViewModel
    {
        [JsonProperty("id")]
        public int UsuarioID { get; set; }
        
        [JsonProperty("nome")]
        public string Nome { get; set; }
        
        [JsonProperty("email")]
        public string Email { get; set; }
        
        [JsonProperty("senha")]
        public string Senha { get; set; }

    }
}
