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
    public class ContaController : Controller
    {
        readonly IConfiguration _configuration;

        public ContaController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IActionResult Index(int id)
        {
            Dictionary<dynamic, dynamic> paramsReq = new Dictionary<dynamic, dynamic>();
            paramsReq.Add("u", id);

            List<ContaViewModel> usrVM = JsonConvert.DeserializeObject<List<ContaViewModel>>(Web.Get($"{_configuration["API_URL"]}/Conta/ConsultarTodasContas", paramsReq));


            if (usrVM.Any())
            {
                foreach (var item in usrVM)
                {
                    Dictionary<dynamic, dynamic> paramsReq2 = new Dictionary<dynamic, dynamic>();
                    paramsReq2.Add("idPortal", item.PortalID);
                    PortalViewModel prVM = JsonConvert.DeserializeObject<PortalViewModel>(Web.Get($"{_configuration["API_URL"]}/Portal/BuscarPorId", paramsReq2));
                    item.PortalNome = prVM.Descricao;
                    item.PortalURL = prVM.Url;
                }
            }
           


            return View(usrVM);
        }

        public IActionResult Conta()
        {
            
            return View();
        }
    }
}