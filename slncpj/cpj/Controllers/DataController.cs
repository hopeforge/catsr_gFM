using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cpj.DAO.Repositories.Interfaces;
using cpj.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace cpj.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataController : ControllerBase
    {
        private readonly IProcessoRepository _processoRepository;
        public DataController(IProcessoRepository processoRepository)
        {
            _processoRepository = processoRepository;
        }

        /// <summary>
        /// Método usado para buscar dados da base sobre Juiz x valor da ação
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("JuizValorAcao")]
        public List<DataJuizValorAcao> BuscarDadoJuizValorAcao()
        {
            List<DataJuizValorAcao> listaJuiz = new List<DataJuizValorAcao>();
            try
            {
                var todosProcessos = _processoRepository.GetAll();

                if (!todosProcessos.Any())
                    return null;

                var listaNomes = todosProcessos.Select(p => p.NomeJuiz).Distinct();

                if (!listaNomes.Any())
                    return null;


                foreach (var item in listaNomes)
                {
                    listaJuiz.Add(new DataJuizValorAcao { NomeJuiz = item});
                }

                foreach (var item in todosProcessos)
                {
                    var index =  listaJuiz.FindIndex(x => x.NomeJuiz == item.NomeJuiz);
                    listaJuiz[index].ValorTotal += item.ValorAcao;
                }

                return listaJuiz;

            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Método usado para buscar dados da base sobre Empresa x valor da ação
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("EmpresaValorAcao")]
        public List<DataEmpresaValorAcao> BuscarDadoEmpresaValorAcao()
        {
            List<DataEmpresaValorAcao> listaEmpresa = new List<DataEmpresaValorAcao>();
            try
            {
                var todosProcessos = _processoRepository.GetAll();

                if (!todosProcessos.Any())
                    return null;

                var listaRequeridos = todosProcessos.Select(p => p.Requerido).Distinct();

                if (!listaRequeridos.Any())
                    return null;


                foreach (var item in listaRequeridos)
                {
                    listaEmpresa.Add(new DataEmpresaValorAcao { NomeEmpresa = item });
                }

                foreach (var item in todosProcessos)
                {
                    var index = listaEmpresa.FindIndex(x => x.NomeEmpresa == item.Requerido);
                    listaEmpresa[index].ValorTotal += item.ValorAcao;
                }

                return listaEmpresa;

            }
            catch (Exception)
            {

                throw;
            }
        }



    }
}