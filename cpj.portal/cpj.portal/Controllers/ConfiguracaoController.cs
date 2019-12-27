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
    public class ConfiguracaoController : Controller
    {
        readonly IConfiguration _configuration;

        public ConfiguracaoController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IActionResult Geral(int id)
        {
            if (id == 0 && TempData["usuarioID"] != null) 
                id = Convert.ToInt32(TempData["usuarioID"]);
            // Buscar informações de usuário
            var model = JsonConvert.DeserializeObject<ConfiguracaoViewModel>(Web.Get($"{_configuration["API_URL"]}/Usuario/{id}"));
                       
            return View(model);
        }

        [HttpPost]
        public IActionResult Geral(ConfiguracaoViewModel configuracao)
        {
            // Buscar informações de usuário
            var model = Web.Put($"{_configuration["API_URL"]}/Usuario",configuracao);
            TempData["usuarioID"] = configuracao.UsuarioID;

            return RedirectToAction("Geral","Configuracao");
        }
    }
}