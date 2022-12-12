using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BuscoBicoBackEnd;
using BuscoBicoBackEnd.Models;

namespace BuscoBicoBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrestadoresController : ControllerBase
    {

        private readonly Context _context;

        public PrestadoresController(Context context)
        {
            _context = context;
        }

        // GET: api/Prestadores
        [HttpGet]        
        public async Task<object> GetPrestadores()
        {
            return await _context.Prestadores.Select(p => new
            {   
                p.Id, p.Nome, p.Telefone, p.Email, p.Localizacao, p.Funcao,
                p.Descricao, p.PrecoDiaria,
                Review = p.Reviews.Select(r => new 
                {r.Id, Cliente = r.Autor.Nome,Prestador=r.Prestador.Nome, r.Comentario, r.Avaliacao}).ToList()
            }).ToListAsync();
        }

        // GET: api/Prestadores/5
        [HttpGet("{id}")]
        public async Task<ActionResult<object>> GetPrestador(int id)
        {
            var prestador = await _context.Prestadores.Where(prest => prest.Id == id).Select(
                p => new
                {
                    p.Id,p.Nome, p.Telefone, p.Email,p.Localizacao,p.Funcao,p.Descricao,
                    p.PrecoDiaria,Review = p.Reviews.Select(r => new
                    {
                        r.Id,r.Avaliacao,r.Comentario,Cliente=r.Autor.Nome
                    }).ToList()
                }).ToListAsync();

            if (prestador == null)
            {
                return NotFound();
            }

            return prestador[0];
        }

        // PUT: api/Prestadores/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPrestador(int id, Prestador prestador)
        {
            if (id != prestador.Id)
            {
                return BadRequest();
            }

            _context.Entry(prestador).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PrestadorExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Prestadores
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Prestador>> PostPrestador(Prestador prestador)
        {
            _context.Prestadores.Add(prestador);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPrestador", new { id = prestador.Id }, prestador);
        }

        // DELETE: api/Prestadores/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePrestador(int id)
        {
            var prestador = await _context.Prestadores.FindAsync(id);
            if (prestador == null)
            {
                return NotFound();
            }

            _context.Prestadores.Remove(prestador);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PrestadorExists(int id)
        {
            return _context.Prestadores.Any(e => e.Id == id);
        }
    }
}
