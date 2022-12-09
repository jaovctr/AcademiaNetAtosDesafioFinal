namespace BuscoBicoFrontEnd.Models
{
    public class ReviewModel
    {
        public int Id { get; set; }
        public virtual ClienteModel Autor { get; set; }
        public virtual PrestadorModel Prestador { get; set; }
        public string? Comentario { get; set; }
        public int Avaliacao { get; set; }
    }
}
