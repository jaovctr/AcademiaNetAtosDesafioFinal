namespace BuscoBicoBackEnd.Models
{
    public class Cliente
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string Localizacao { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }

    }
}