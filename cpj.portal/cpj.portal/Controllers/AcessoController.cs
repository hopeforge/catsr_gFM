using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cpj.portal.Models;
using cpj.portal.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;

namespace cpj.portal.Controllers
{
    public class AcessoController : Controller
    {
        readonly IConfiguration _configuration;

        public AcessoController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult Index()
        {
            if (TempData["cadastroUsrSucesso"] != null)
                ViewData["cadastroUsrSucesso"] = TempData["cadastroUsrSucesso"];

            return View();
        }

        [HttpPost]
        public IActionResult Index(IFormCollection frm)
        {
            // Validar buscar dados do usuario
            var p = new { email = frm["username"].ToString(), senha = frm["password"].ToString() };
            UsuarioViewModel usrVM = JsonConvert.DeserializeObject<UsuarioViewModel>(Web.Post($"{_configuration["API_URL"]}/Usuario/Validar", p));

            if (usrVM == null)
            {
                ViewData["usuarioNaoEncontrado"] = 1;
                return View();
            }
            
            TempData["UsuarioNome"] = usrVM.Nome;
            TempData["UsuarioId"] = usrVM.ID;
            TempData["UsuarioEmail"] = frm["username"].ToString();
            return RedirectToAction("Index", "Dashboard");
        }

        [HttpGet]
        public IActionResult Cadastro()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Cadastro(UsuarioViewModel usr)
        {
            var retornoApi = Web.Post($"{_configuration["API_URL"]}/Usuario/Cadastrar", usr);
            
            TempData["cadastroUsrSucesso"] = retornoApi != null ? 1 : 0;            
            return RedirectToAction("Index", "Acesso");
        }

        public IActionResult Sair()
        {
            return RedirectToAction("Index", "Acesso");
        }
    }
}