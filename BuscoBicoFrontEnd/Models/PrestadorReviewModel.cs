namespace BuscoBicoFrontEnd.Models
{
    public class PrestadorReviewModel
    {
        public PrestadorModel Prestador { get; set; }
        public ClienteModel Cliente { get; set; }
        public virtual ICollection<ClienteModel>? ListaClientes { get; set; }
        public ReviewModel Review { get; set; }

        public int IdCliente { get; set; }
        public int IdPrestador { get; set; }
    }
}
