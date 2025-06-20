namespace Revenda.Domain.Entities
{
    public class Revenda: EntidadeBase
    {
        public string RazaoSocial { get; set; }
        public string NomeFantasia { get; set; }
        public string CNPJ { get; set; }
        public string Email { get; set; }
        public virtual List<EnderecoRevenda> Enderecos { get; set; } = new List<EnderecoRevenda>();
        public virtual List<ContatoRevenda> Contatos { get; set; } = new List<ContatoRevenda>();
    }
}
