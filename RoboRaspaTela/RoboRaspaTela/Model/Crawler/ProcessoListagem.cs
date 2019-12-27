using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoboRaspaTela.Model.Crawler
{
    internal class ProcessoListagem
    {
        public int Id { get; set; }
        public string NroProcesso { get; set; }
        public string OutrosAssuntos { get; set; }
        public string NomeJuiz { get; set; }
        public decimal ValorAcao { get; set; }
        public string Requerido { get; set; }
        public string Requerente { get; set; }
        public DateTime DataCadastro { get; set; }
        public List<Movimentacao> Movimentacoes { get; set; }
        public List<Peticao> Peticoes { get; set; }
        public string Link { get; set; }
    }
    internal class Movimentacao
    {
        public int Id { get; set; }
        public DateTime Data { get; set; }
        public string Descricao { get; set; }
        public int IdProcesso { get; set; }
    }
    internal class Peticao
    {
        public int Id { get; set; }
        public int IdProcesso { get; set; }
        public DateTime Data { get; set; }
        public string Tipo { get; set; }
    }
}
