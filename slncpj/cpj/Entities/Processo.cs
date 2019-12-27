using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cpj.Entities
{
    /// <summary>
    /// Classe com detalhes de processos capturados.
    /// </summary>
    public class Processo
    {
        public int Id { get; set; }
        public string NroProcesso { get; set; }
        public string OutrosAssuntos { get; set; }
        public string NomeJuiz { get; set; }
        public decimal ValorAcao { get; set; }
        public string Requerido { get; set; }
        public string Requerente { get; set; }
        public bool? Favoritado { get; set; }
        public DateTime DataCadastro { get; set; }
        public IEnumerable<Movimentacao> Movimentacoes { get; set; }
        public IEnumerable<Peticao> Peticoes { get; set; }
    }
}
