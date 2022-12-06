using Microsoft.AspNetCore.Mvc;

namespace BuscoBicoBackEnd.Models
{
    public class Review
    {
        public int Id { get; set; }
        public virtual Cliente Autor { get; set; }
        public virtual Prestador Prestador { get; set; }
        public string? Comentario { get; set; }
        public int Avaliacao { get; set; }

    }
}