namespace Revenda.Domain.Entities
{
    public class Contato : EntidadeBase
    {
        public bool Principal { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
    }

    public class ContatoRevenda : EntidadeBase
    {
        public int RevendaID { get; set; }
        public Revenda Revenda { get; set; }
        public Contato Contato { get; set; }
    }
    public class ContatoCliente : EntidadeBase
    {
        public int ClienteID { get; set; }
        public Cliente Cliente { get; set; }
        public Contato Contato { get; set; }
    }
}
