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
    public class PeticaoController : ControllerBase
    {
        private readonly IPeticaoRepository _peticaoRepository;
        public PeticaoController(IPeticaoRepository peticaoRepository)
        {
            _peticaoRepository = peticaoRepository;
        }

        /// <summary>
        /// Método para cadastrar peticao do processo.
        /// </summary>
        /// <param name="peticao"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Cadastrar")]
        public ActionResult CadastrarPeticao([FromBody] Peticao peticao)
        {
            try
            {
                _peticaoRepository.Add(peticao);
                return Ok();
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Busca pelo id do processo.
        /// </summary>
        /// <param name="idProcesso"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Busca/Processo")]
        public IEnumerable<Peticao> BuscarPorProcesso(int idProcesso)
        {
            try
            {
                return _peticaoRepository.BuscarPorProcesso(idProcesso);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}