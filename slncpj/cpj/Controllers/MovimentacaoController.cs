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
    public class MovimentacaoController : ControllerBase
    {
        private readonly IMovimentacaoRepository _movimentacaoRepository;
        public MovimentacaoController(IMovimentacaoRepository movimentacaoRepository)
        {
            _movimentacaoRepository = movimentacaoRepository;
        }

        /// <summary>
        /// Método para cadastrar movimentacao do processo.
        /// </summary>
        /// <param name="movimentacao"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Cadastrar/Movimentacao")]
        public ActionResult CadastrarMovimentacao([FromBody] Movimentacao movimentacao)
        {
            try
            {
                _movimentacaoRepository.Add(movimentacao);
                return Ok();
            }
            catch (Exception e)
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
        public IEnumerable<Movimentacao> BuscarPorProcesso(int idProcesso)
        {
            try
            {
                return _movimentacaoRepository.BuscarPorProcesso(idProcesso);
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}