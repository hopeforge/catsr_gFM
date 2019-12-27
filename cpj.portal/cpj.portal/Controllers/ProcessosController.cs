using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cpj.portal.Models;
using cpj.portal.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace cpj.portal.Controllers
{
    public class ProcessosController : Controller
    {
        readonly IConfiguration _configuration;

        public ProcessosController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var retorno = new List<ProcessosViewModel>();
            retorno = JsonConvert.DeserializeObject<List<ProcessosViewModel>>(Web.Get($"{_configuration["API_URL"]}/Processo/Busca"));

            return View(retorno);

        }

        public IActionResult Visualizar(int id)
        {
            //buscar processo por id 
            ProcessosViewModel processo = JsonConvert.DeserializeObject<ProcessosViewModel>(Web.Get($"{_configuration["API_URL"]}/Processo/{id}"));
            
            processo.Movimentacoes = JsonConvert.DeserializeObject<List<Movimentacao>>(Web.Get($"{_configuration["API_URL"]}/Movimentacao/Busca/Processo?idProcesso={id}"));
            processo.Peticoes = JsonConvert.DeserializeObject<List<Peticao>>(Web.Get($"{_configuration["API_URL"]}/Peticao/Busca/Processo?idProcesso={id}"));

            return View(processo);
        }

        public IActionResult Favoritar(int id)
        {
            var ret = Web.Post($"{_configuration["API_URL"]}/Processo/{id}/Favoritar","");

            return RedirectToAction("Visualizar",new { id = id });
        }

        public IActionResult RemoverFavorito(int id)
        {
            var ret = Web.Post($"{_configuration["API_URL"]}/Processo/{id}/Favoritar", "");

            return RedirectToAction("Visualizar", new { id = id });
        }
    }
}