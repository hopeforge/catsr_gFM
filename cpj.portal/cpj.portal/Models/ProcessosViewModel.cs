using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cpj.portal.Models
{
    public class ProcessosViewModel
    {
        public int ID { get; set; }
        [JsonProperty("nroProcesso")]
        public string Numero { get; set; }
        public string Requerido { get; set; }
        [JsonProperty("valorAcao")]
        public decimal Valor { get; set; }

        [JsonProperty("outrosAssuntos")]
        public string OutrosAssuntos { get; set; }

        [JsonProperty("nomeJuiz")]
        public string NomeJuiz { get; set; }

        [JsonProperty("requerente")]
        public string Requerente { get; set; }

        [JsonProperty("favoritado")]
        public bool? Favoritado { get; set; }

        [JsonProperty("dataCadastro")]
        public DateTimeOffset DataCadastro { get; set; }

        [JsonProperty("movimentacoes")]
        public List<Movimentacao> Movimentacoes { get; set; }

        [JsonProperty("peticoes")]
        public List<Peticao> Peticoes { get; set; }
    }

    public partial class Movimentacao
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("data")]
        public DateTimeOffset Data { get; set; }

        [JsonProperty("descricao")]
        public string Descricao { get; set; }

        [JsonProperty("idProcesso")]
        public long IdProcesso { get; set; }
    }

    public partial class Peticao
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("idProcesso")]
        public long IdProcesso { get; set; }

        [JsonProperty("data")]
        public DateTimeOffset Data { get; set; }

        [JsonProperty("tipo")]
        public string Tipo { get; set; }
    }
}
