using System;
using System.Collections.Generic;
using System.Linq;
using cpj.DAO.Repositories.Interfaces;
using cpj.Entities;
using Microsoft.AspNetCore.Mvc;

namespace cpj.Controllers
{
    /// <summary>
    /// Portais cadastrados préviamente no sistema
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class PortalController : ControllerBase
    {
        private readonly IPortalRepository _portalREpository;
        public PortalController(IPortalRepository portalREpository)
        {
            _portalREpository = portalREpository;
        }

        /// <summary>
        /// Método para cadastrar novo portal.
        /// </summary>
        /// <param name="portal"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Cadastrar")]
        public ActionResult CadastrarPortal([FromBody]Portal portal)
        {
            try
            {
                _portalREpository.Add(portal);
                return Ok();
            }
            catch (Exception)
            {

                return StatusCode(500);
            }
        }

     
        /// <summary>
        /// Método para retornar todas os portais.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("BuscarTodos")]
        public IEnumerable<Portal> BuscarTodos()
        {
            try
            {
                return _portalREpository.GetAll();
            }
            catch (Exception e)
            {

                return null;
            }
        }

        /// <summary>
        /// Método para retornar portal por id.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("BuscarPorId")]
        public Portal BuscarPorId(int idPortal)
        {
            try
            {
                
                return _portalREpository.Find(idPortal);
            }
            catch (Exception e)
            {

                return null;
            }
        }
    }
}