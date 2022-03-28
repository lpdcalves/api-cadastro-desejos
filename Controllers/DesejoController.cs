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
    public class DesejoController : ControllerBase
    {
        private readonly ContextoAPI _context;

        public DesejoController(ContextoAPI context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult ConsultarDesejos()
        {
            try
            {
                IEnumerable<Desejo> desejos = _context.Desejo.Where(desejo => desejo.DataExcluido == null).ToList();

                return Ok(new
                {
                    mensagem = "Desejos recuperados com sucesso",
                    objeto = desejos
                });
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpPost]
        public IActionResult InserirDesejo([FromBody] Desejo desejo)
        {
            try
            {
                desejo.DataInserido = DateTime.Now;
                _context.Desejo.Add(desejo);
                _context.SaveChanges();

                return Ok(new
                {
                    mensagem = "Desejo inserido com sucesso",
                    objeto = desejo
                });
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpPut("{idDesejo}")]
        public IActionResult AlterarDesejo(int idDesejo, Desejo desejo)
        {
            try
            {
                if (validaDesejo(idDesejo))
                {
                    _context.Desejo.Update(desejo);
                    _context.SaveChanges();

                    return Ok(new
                    {
                        mensagem = "Desejo alterado com sucesso",
                        objeto = desejo
                    });
                }
                else
                {
                    return Conflict("Desejo não encontrado");
                }
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpDelete("{idDesejo}")]
        public IActionResult ExcluirDesejo(int idDesejo)
        {
            try
            {
                if (validaDesejo(idDesejo))
                {
                    Desejo desejo = _context.Desejo.Find(idDesejo);
                    desejo.DataExcluido = DateTime.Now;
                    _context.Desejo.Update(desejo);
                    _context.SaveChanges();

                    return NoContent();
                }
                else
                {
                    return Conflict("Desejo inexistente");
                }
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        private bool validaDesejo(int idDesejo)
        {
            return _context.Desejo.Any(desejo => desejo.Id == idDesejo && desejo.DataExcluido == null);
        }
    }
}
