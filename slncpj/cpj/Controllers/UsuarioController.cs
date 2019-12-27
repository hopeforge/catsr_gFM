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
    /// <summary>
    /// Controller de usuário do sistema
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepository _usuarioRepository;
        public UsuarioController(IUsuarioRepository usuario)
        {
            _usuarioRepository = usuario;
        }

        /// <summary>
        /// Método para cadastrar usuário na base.
        /// </summary>
        /// <param name="usuario"></param>
        [HttpPost]
        [Route("Cadastrar")]
        public ActionResult CadastrarUsuario([FromBody] Usuario usuario)
        {
            try
            {
                _usuarioRepository.Add(usuario);
                return Ok();
            }
            catch (Exception ex)
            {

                return StatusCode(500);

            }
        }

        /// <summary>
        /// Enviar email e senha preenchidos.
        /// </summary>
        /// <param name = "usuario" ></ param >
        /// < returns ></ returns >
        [HttpPost]
        [Route("Validar")]
        public Usuario ValidaUsuario([FromBody] Usuario usuario)
        {
            try
            {
                var usuarioRecebido = _usuarioRepository.GetAll().FirstOrDefault(x => x.Email == usuario.Email && x.Senha == usuario.Senha);

                return usuarioRecebido;
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        public Usuario Get(int id)
        {
            try
            {
                var usuarioRecebido = _usuarioRepository.Find(id);

                return usuarioRecebido;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        [HttpPut]
        public IActionResult Put(Usuario usuario)
        {
            try
            {                
                _usuarioRepository.Update(usuario);
                
                return Ok(); 
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }


    }
}
