using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace cpj.Entities
{
    public class Portal
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string Descricao { get; set; }
        public virtual List<Conta> Contas { get; set; }
    }
}
