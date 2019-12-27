using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cpj.portal.Models
{
    public class ContaViewModel
    {
        public int ID { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
        [JsonProperty("usuarioId")]
        public int UsuarioID { get; set; }
        public string UsuarioNome { get; set; }
        public string PortalNome { get; set; }
        public int PortalID { get; set; }
        public string PortalURL { get; set; }

    }
}
