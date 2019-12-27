using System;
using System.Collections.Generic;
using System.Linq;
using cpj.DAO.Repositories.Interfaces;
using cpj.Entities;
using Microsoft.AspNetCore.Mvc;

namespace cpj.Controllers
{
    /// <summary>
    /// Contas cadatradas em portais da justiça brasileira.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ContaController : ControllerBase
    {
        private readonly IContaRepository _contaRepo;
        public ContaController(IContaRepository conta)
        {
            _contaRepo = conta;
        }

        /// <summary>
        /// Método para cadastrar nova conta em algum portal.
        /// </summary>
        /// <param name="conta"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Cadastrar")]
        public ActionResult CadastrarConta([FromBody]Conta conta)
        {
            try
            {
                _contaRepo.Add(conta);
                return Ok();
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Método para resgatar contas cadastradas em portais.
        /// </summary>
        /// <param name="conta"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("ConsultarTodasContas")]
        public IEnumerable<Conta> ConsultarTodasContas(int u)
        {
            try
            {
                return _contaRepo.GetAll().Where(x => x.UsuarioId == u);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Método para resgatar contas cadastradas em portais.
        /// </summary>
        /// <param name="conta"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("ConsultarPorPortal")]
        public Conta ConsultarPorPortal(int idPortal, int idUsuario)
        {
            try
            {
                return _contaRepo.GetAll().FirstOrDefault(x=> x.PortalId == idPortal && x.UsuarioId == idUsuario) ?? null;
            }
            catch (Exception)
            {

                return null;
            }
        }



    }
}