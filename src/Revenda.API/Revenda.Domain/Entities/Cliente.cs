namespace Revenda.Domain.Entities
{
    public class Cliente:EntidadeBase
    {
        public int Id { get; set; }
        public int TipoPessoa { get; set; }
        public string NomeCompleto { get; set; }
        public string Nome { get; set; }
        public string Documento { get; set; }
        public string Email { get; set; }
        public virtual List<EnderecoCliente> Enderecos { get; set; } = new List<EnderecoCliente>();
        public virtual List<ContatoCliente> Contatos { get; set; } = new List<ContatoCliente>();
    }
}
