using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DesejosWebAPI.Models;

namespace DesejosWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly ContextoAPI _context;

        public UsuarioController(ContextoAPI context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult ConsultarUsuarios()
        {
            try
            {
                IEnumerable<Usuario> usuarios = _context.Usuario.Where(usr => usr.DataExcluido == null).ToList();

                return Ok(new
                {
                    mensagem = "Usuários recuperados com sucesso",
                    objeto = usuarios
                });
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpPost]
        public IActionResult InserirUsuario([FromBody] Usuario usuario)
        {
            try
            {
                usuario.DataInserido = DateTime.Now;
                _context.Usuario.Add(usuario);
                _context.SaveChanges();

                return Ok(new
                {
                    mensagem = "Usuário inserido com sucesso",
                    objeto = usuario
                });
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpPut("{idUsuario}")]
        public IActionResult AlterarUsuario(int idUsuario, Usuario usuario)
        {
            try
            {
                if (validaUsuario(idUsuario))
                {
                    _context.Usuario.Update(usuario);
                    _context.SaveChanges();

                    return Ok(new
                    {
                        mensagem = "Usuário alterado com sucesso",
                        objeto = usuario
                    });
                }
                else
                {
                    return Conflict("Usuário não encontrado");
                }
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpDelete("{idUsuario}")]
        public IActionResult ExcluirUsuario(int idUsuario)
        {
            try
            {
                if (validaUsuario(idUsuario))
                {
                    Usuario usuario = _context.Usuario.Find(idUsuario);
                    usuario.DataExcluido = DateTime.Now;
                    _context.Usuario.Update(usuario);
                    _context.SaveChanges();

                    return NoContent();
                }
                else
                {
                    return Conflict("Usuário não encontrado");
                }
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        private bool validaUsuario(int idUsuario)
        {
            return _context.Usuario.Any(usuario => usuario.Id == idUsuario && usuario.DataExcluido == null);
        }
    }
}
