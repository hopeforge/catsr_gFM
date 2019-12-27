using HtmlAgilityPack;
using RoboRaspaTela.Model.Crawler;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;
using WatiN.Core;

namespace RoboRaspaTela
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                
                Kill_IE();
                System.Threading.Thread t = new System.Threading.Thread(processar);
                t.SetApartmentState(System.Threading.ApartmentState.STA);
                t.Start();
                t.Join();
                Console.WriteLine("Processo finalizado ... ");
                Console.WriteLine("Aguardando próxima execução ... ");

                System.Threading.Thread.Sleep(10 * 60 * 10000);
            }
        }


        static void processar()
        {

            var b = new WatiN.Core.IE();
            b.Visible = false;
            bool conectado = false;

            try
            {
                var arrParametrosBusca = "Banco+Ita%C3%BA,Banco Santander,Banco Bradesco".Split(',');

                foreach (string criterio in arrParametrosBusca)
                {
                    // Recuperar lista de filtros de pesquisa
                    #region | Navegação inicial

                    if (!conectado)
                    {
                        Console.WriteLine("Iniciando navegação no portal " + "https://esaj.tjsp.jus.br/");
                        b.NativeBrowser.NavigateTo(new Uri("https://esaj.tjsp.jus.br/"));

                        b.WaitForComplete();

                        b.NativeBrowser.NavigateTo(new Uri("https://esaj.tjsp.jus.br/esaj/identificacao.do?retorno=https%3A//esaj.tjsp.jus.br/esaj/portal.do%3Fservico%3D740000"));
                        b.WaitForComplete();

                        Console.WriteLine("Autenticando no portal " + "https://esaj.tjsp.jus.br/");
                        b.TextField(Find.ById("usernameForm")).Value = "312.073.798-40";
                        b.TextField(Find.ById("passwordForm")).Value = "cade@123";
                        b.Form(Find.ById("formCertificado")).Submit();
                        b.WaitForComplete();
                        Console.WriteLine("Autenticação realizada no portal " + "https://esaj.tjsp.jus.br/");

                        conectado = true;
                    }

                    Console.WriteLine("Consultando lista de processos");
                    b.NativeBrowser.NavigateTo(new Uri("https://esaj.tjsp.jus.br/cpopg/open.do"));
                    b.WaitForComplete();

                    b.NativeBrowser.NavigateTo(new Uri("https://esaj.tjsp.jus.br/cpopg/search.do?conversationId=&dadosConsulta.localPesquisa.cdLocal=90&cbPesquisa=NMPARTE&dadosConsulta.tipoNuProcesso=UNIFICADO&dadosConsulta.valorConsulta=" + criterio  + "&chNmCompleto=true&uuidCaptcha="));
                    b.WaitForComplete();

                    #endregion | Navegação inicial

                    #region | Montagem de lista de processos

                    System.Threading.Thread.Sleep(2000);

                    HtmlAgilityPack.HtmlDocument hd = new HtmlAgilityPack.HtmlDocument();
                    hd.LoadHtml(b.Html);

                    var lstProcessoListagem = new List<ProcessoListagem>();

                    HtmlNodeCollection btnProximaPagina = null;

                    do
                    {
                        foreach (HtmlNode lnkA in hd.DocumentNode.SelectNodes("//a[contains(@class,'linkProcesso')]"))
                        {
                            ProcessoListagem objListagem = new ProcessoListagem();
                            objListagem.Link = lnkA.Attributes[1].Value;
                            objListagem.NroProcesso = lnkA.InnerText.Replace("\n", "").Replace("\t", "").Trim();
                            lstProcessoListagem.Add(objListagem);
                        }


                        btnProximaPagina = hd.DocumentNode.SelectNodes("//a[(@title='Próxima página')]");

                        if (btnProximaPagina != null)
                        {
                            b.NativeBrowser.NavigateTo(new Uri("https://esaj.tjsp.jus.br" + btnProximaPagina[1].Attributes[2].Value.Replace("&amp;", "&")));
                            b.WaitForComplete();
                            System.Threading.Thread.Sleep(2000);
                            hd.LoadHtml(b.Html);
                        }

                    } while (btnProximaPagina != null);

                    if (lstProcessoListagem.Count > 0)
                        Console.WriteLine("Lista de processos consultada: " + lstProcessoListagem.Count + " processos encontrados");

                    #endregion | Montagem de lista de processos

                    #region | Consulta detalhes do processo

                    Console.WriteLine("Iniciando consulta de detalhes dos processos");
                    foreach (var itemProcesso in lstProcessoListagem)
                    {
                        Console.WriteLine("Consultando processo " + itemProcesso.NroProcesso);
                        hd = CapturaDadosProcesso(b, itemProcesso);

                        Console.WriteLine("Consultando PARTES do processo " + itemProcesso.NroProcesso);
                        CapturaDadosPartes(hd, itemProcesso);

                        Console.WriteLine("Consultando Movimentos do processo " + itemProcesso.NroProcesso);
                        CapturaDadosMovimentos(hd, itemProcesso);

                        Console.WriteLine("Consultando Petições do processo " + itemProcesso.NroProcesso);
                        CapturaDadosPeticoes(b, itemProcesso);
                    }

                    #endregion | Consulta detalhes do processo

                    #region | Grava processos na base de dados
                    // Eviar dados para API
                    Console.WriteLine("Gravando processos ");
                    foreach (var itemProcesso in lstProcessoListagem)
                    {
                        itemProcesso.DataCadastro = DateTime.Now;
                        itemProcesso.Requerente = (itemProcesso.Requerente.Length > 250) ? itemProcesso.Requerente.Substring(0, 249) : itemProcesso.Requerente;
                        itemProcesso.Requerido = (itemProcesso.Requerido.Length > 250) ? itemProcesso.Requerido.Substring(0, 249) : itemProcesso.Requerido;
                        foreach (var item in itemProcesso.Movimentacoes)
                            item.Descricao = (item.Descricao.Length > 250) ? item.Descricao.Substring(0, 249) : item.Descricao;

                        Utils.Web.Post(System.Configuration.ConfigurationManager.AppSettings["UrlApiCPJ"] + "Processo/Cadastrar", itemProcesso);

                        Console.WriteLine("Inclusão de processo: " + itemProcesso.NroProcesso);
                    }
                    #endregion | Grava processos na base de dados
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro não  previsto: " + ex.Message);
            }
            finally
            {
                b.Close();
                b.Dispose();
                b = null;
            }
        }

        private static void CapturaDadosPeticoes(IE b, ProcessoListagem itemProcesso)
        {
            int ini = b.Html.IndexOf("<!-- Tabela de petições diversas -->");
            int fim = b.Html.IndexOf("</table>", ini) + 9;
            int tam = fim - ini;
            string HmtlTabelaPeticoes = b.Html.Substring(ini, tam);

            HtmlAgilityPack.HtmlDocument hdPeticoes = new HtmlAgilityPack.HtmlDocument();
            hdPeticoes.LoadHtml(HmtlTabelaPeticoes);

            var TBPeticoes = hdPeticoes.DocumentNode.SelectNodes("//table").Where(l => l.Id == "").FirstOrDefault();
            itemProcesso.Peticoes = new List<Peticao>();
            foreach (HtmlNode body in TBPeticoes.SelectNodes("tbody"))
            {

                foreach (HtmlNode tr in body.SelectNodes("tr"))
                {
                    var TDs = tr.SelectNodes("th|td");

                    if (TDs[0].InnerText.Trim().Contains("Não há") == false)
                    {
                        var Coluna = TDs[0].InnerText.Trim();
                        var ValorColuna = TDs[1].InnerText.Trim();

                        itemProcesso.Peticoes.Add(new Peticao() { Data = DateTime.Parse(Coluna), Tipo = ValorColuna });
                    }


                }
            }
        }

        private static void CapturaDadosMovimentos(HtmlAgilityPack.HtmlDocument hd, ProcessoListagem itemProcesso)
        {
            var TB_Movimentos = hd.DocumentNode.SelectNodes("//tbody").Where(l => l.Id == "tabelaUltimasMovimentacoes").FirstOrDefault();

            itemProcesso.Movimentacoes = new List<Movimentacao>();
            foreach (HtmlNode tr in TB_Movimentos.SelectNodes("tr"))
            {
                var TDs = tr.SelectNodes("th|td");

                var Coluna = TDs[0].InnerText.Trim();
                var ColunaMeio = TDs[1].InnerText.Trim();
                var ValorColuna = TDs[2].InnerText.Trim();

                itemProcesso.Movimentacoes.Add(new Movimentacao()
                {
                    Data = DateTime.Parse(Coluna),
                    Descricao = ValorColuna
                });

            }
        }

        private static void CapturaDadosPartes(HtmlAgilityPack.HtmlDocument hd, ProcessoListagem itemProcesso)
        {
            var TB_Partes = hd.DocumentNode.SelectNodes("//table").Where(l => l.Id == "tablePartesPrincipais").FirstOrDefault();
            foreach (HtmlNode body in TB_Partes.SelectNodes("tbody"))
            {

                foreach (HtmlNode tr in body.SelectNodes("tr"))
                {
                    var TDs = tr.SelectNodes("th|td");
                    var Coluna = TDs[0].InnerText.Trim();
                    var ValorColuna = TDs[1].InnerText.Trim();

                    ValorColuna = HttpUtility.HtmlDecode(ValorColuna).Replace("\t", "").Replace("\n", "");
                    ValorColuna = ValorColuna.Split(':')[0].Replace("Advogado","").Replace("Advogada", "").Trim();

                    if (Coluna.Contains("Exeqte:"))
                        itemProcesso.Requerente = ValorColuna;
                    else
                        itemProcesso.Requerido = ValorColuna;
                }
            }
        }

        private static HtmlAgilityPack.HtmlDocument CapturaDadosProcesso(IE b, ProcessoListagem itemProcesso)
        {
            HtmlAgilityPack.HtmlDocument hd;
            b.NativeBrowser.NavigateTo(new Uri("https://esaj.tjsp.jus.br" + itemProcesso.Link));
            b.WaitForComplete();
            System.Threading.Thread.Sleep(2000);

            hd = new HtmlAgilityPack.HtmlDocument();
            hd.LoadHtml(b.Html);

            var TB = hd.DocumentNode.SelectNodes("//table[@class='secaoFormBody']").Where(l => l.Id == "").FirstOrDefault();

            foreach (HtmlNode body in TB.SelectNodes("tbody"))
            {
                foreach (HtmlNode tr in body.SelectNodes("tr"))
                {
                    var TDs = tr.SelectNodes("th|td");

                    var Coluna = TDs[0].InnerText.Trim();
                    var ValorColuna = TDs[1].InnerText.Trim().Replace("\t", "").Replace("\n", "");
                    switch (Coluna)
                    {
                        case "Processo:":
                        case "Classe:":
                        case "":
                        case "Distribuição:":
                        case "CDAs:":
                        case "Controle:":
                            break;

                        case "Assunto:":
                            itemProcesso.OutrosAssuntos = TDs[1].InnerText.Trim().Replace("\t", "").Replace("\n", "");
                            break;

                        case "Juiz:":
                            itemProcesso.NomeJuiz = TDs[1].InnerText.Trim().Replace("\t", "").Replace("\n", "");
                            break;

                        case "Valor da ação:":
                            itemProcesso.ValorAcao = decimal.Parse(TDs[1].InnerText.Trim().Replace("\t", "").Replace("\n", "").Replace(" ", "").Replace("R$", ""));
                            break;


                    }

                }
            }

            return hd;
        }

        private static void Kill_IE()
        {
            Process[] ps = Process.GetProcessesByName("IEXPLORE");

            foreach (Process p in ps)
            {
                try
                {
                    if (!p.HasExited)
                    {
                        p.Kill();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(String.Format("Unable to kill process {0}, exception: {1}", p.ToString(), ex.ToString()));
                }
            }
        }
    }
}
