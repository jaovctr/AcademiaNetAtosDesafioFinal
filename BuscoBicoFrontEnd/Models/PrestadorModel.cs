using Microsoft.AspNetCore.Mvc.ViewEngines;

namespace BuscoBicoFrontEnd.Models
{
    public class PrestadorModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string? Email { get; set; }
        public string Localizacao { get; set; }
        public string Funcao { get; set; }
        public string Descricao { get; set; }
        public float? PrecoDiaria { get; set; }
        public virtual ICollection<ReviewModel> Reviews { get; set; }
    }
}
