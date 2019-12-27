using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using cpj.portal.Models;
using Microsoft.Extensions.Configuration;
using cpj.portal.Utils;

namespace cpj.portal.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ILogger<DashboardController> _logger;
        readonly IConfiguration _configuration;


        public DashboardController(ILogger<DashboardController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            if (ViewData["UsuarioNome"] == null || ViewData["UsuarioEmail"] == null || TempData["UsuarioId"] == null)
                RedirectToAction("Acesso", "Index");

            ViewBag.UsuarioNome = TempData["UsuarioNome"];
            ViewBag.UsuarioId = TempData["UsuarioId"];
            ViewBag.UsuarioEmail = TempData["UsuarioEmail"];

            ViewData["DadosJuizvsValor"] = Web.Get($"{_configuration["API_URL"]}/Data/JuizValorAcao");
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
