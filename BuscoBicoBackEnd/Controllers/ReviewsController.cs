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
    public class ReviewsController : ControllerBase
    {
        private readonly Context _context;

        public ReviewsController(Context context)
        {
            _context = context;
        }

        // GET: api/Reviews OK
        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> GetReviews()
        {
            return await _context.Reviews.Select(r => new { r.Id, r.Avaliacao, r.Comentario, Prestador = r.Prestador.Nome, Cliente = r.Autor.Nome}).ToListAsync();
        }

        //GET: api/ReviewsDoPrestador/5 OK
        [HttpGet("ReviewsDoPrestador/{id}")]
        public async Task<ActionResult<object>> ReviewsDoPrestador(int id) 
        {
            var prestador = await _context.Prestadores.Where(prest => prest.Id == id).Select(
                p => new
                { 
                    p.Id,
                    Reviews = p.Reviews.Select( r => new
                    {
                        r.Id,
                        r.Avaliacao,
                        r.Comentario,
                        Autor = new {r.Autor.Id, r.Autor.Nome,r.Autor.Telefone,r.Autor.Localizacao}
                    }).ToList()
                }).ToListAsync();
            
            return prestador[0].Reviews;
        }


        // GET: api/Reviews/5 OK
        [HttpGet("{id}")]
        public async Task<ActionResult<object>> GetReview(int id)
        {
            var review = await _context.Reviews.Where(rev => rev.Id == id).Select(
                r=> new
                {
                    r.Id,r.Avaliacao, r.Comentario,
                    Autor= new { r.Autor.Id, r.Autor.Nome, r.Autor.Telefone, r.Autor.Localizacao},
                    Prestador = new {r.Prestador.Id, r.Prestador.Nome,r.Prestador.Telefone,r.Prestador.Email,
                    r.Prestador.Localizacao,r.Prestador.Funcao,r.Prestador.Descricao,r.Prestador.PrecoDiaria}
                }).ToListAsync();

            if (review == null)
            {
                return NotFound();
            }

            return review[0];
        }

        // PUT: api/Reviews/5 OK
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReview(int id, Review review)
        {
            if (id != review.Id)
            {
                return BadRequest();
            }
            Cliente c = _context.Clientes.Find(review.Autor.Id);
            Prestador p = _context.Prestadores.Find(review.Prestador.Id);
            review.Autor = c;
            review.Prestador = p;

            _context.Entry(review).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReviewExists(id))
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

        // POST: api/Reviews OK
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Review>> PostReview(Review review)
        {
            Review revFinal = new Review();
            revFinal.Comentario= review.Comentario;
            revFinal.Avaliacao= review.Avaliacao;
            revFinal.Autor = _context.Clientes.Find(review.Autor.Id);
            revFinal.Prestador = _context.Prestadores.Find(review.Prestador.Id);
            _context.Reviews.Add(revFinal);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetReview", new { id = review.Id }, review);
        }


        // DELETE: api/Reviews/5 testar
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReview(int id)
        {
            var review = await _context.Reviews.FindAsync(id);
            if (review == null)
            {
                return NotFound();
            }

            _context.Reviews.Remove(review);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ReviewExists(int id)
        {
            return _context.Reviews.Any(e => e.Id == id);
        }
    }
}
