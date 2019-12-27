using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cpj.portal.Models
{
    public class UsuarioViewModel
    {
        [JsonProperty("ID")]
        public int ID { get; set; }
        [JsonProperty("Nome")]
        public string Nome { get; set; }
        [JsonProperty("Email")]
        public string Email { get; set; }
        [JsonProperty("Senha")]
        public string Senha { get; set; }
    }
}
