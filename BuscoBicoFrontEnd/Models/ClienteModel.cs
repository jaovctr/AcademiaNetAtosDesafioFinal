namespace BuscoBicoFrontEnd.Models
{
    public class ClienteModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string Localizacao { get; set; }
        public virtual ICollection<ReviewModel> Reviews { get; set; }
    }
}
