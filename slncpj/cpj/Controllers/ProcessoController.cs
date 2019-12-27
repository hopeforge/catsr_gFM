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
    public class ProcessoController : ControllerBase
    {
        private readonly IProcessoRepository _processoRepository;
        public ProcessoController(IProcessoRepository processo)
        {
            _processoRepository = processo;
        }


        /// <summary>
        /// Método para cadastrar processo na base
        /// </summary>
        /// <param name="processo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Cadastrar")]
        public ActionResult CadastrarProcesso([FromBody] Processo processo)
        {
            try
            {

                _processoRepository.Add(processo);
                return Ok();
            }
            catch (Exception ex)
            {

                return StatusCode(500);
            }
        }

        /// <summary>
        /// Executa busca por requerente
        /// </summary>
        /// <param name="requerente"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Busca/Requerente")]
        public IEnumerable<Processo> BuscaPorRequerente(string requerente)
        {
            try
            {
                return _processoRepository.BuscaPorRequerente(requerente);
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Executa busca por Requerido
        /// </summary>
        /// <param name="requerido"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Busca/Requerido")]
        public IEnumerable<Processo> BuscaPorRequerido(string requerido)
        {
            try
            {
                return _processoRepository.BuscaPorRequerido(requerido);
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Busca todos os processos da base.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("Busca")]
        public IEnumerable<Processo> BuscaTodos()
        {
            try
            {
                return _processoRepository.GetAll();
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        public Processo Get(int id)
        {
            try
            {
                return _processoRepository.Find(id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("{id}/Favoritar")]
        public IActionResult Favoritar(int id)
        {
            try
            {
                var retorno =_processoRepository.GetAll().FirstOrDefault(x => x.Id == id);
                retorno.Favoritado = retorno.Favoritado == null ? true : !retorno.Favoritado;
                _processoRepository.Update(retorno);
                return Ok();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}