namespace Revenda.Application.DTOs.Response
{
    public class ClienteResponse
    {
        public int Id { get; set; }
        public int TipoPessoa { get; set; }
        public string NomeCompleto { get; set; }
        public string Nome { get; set; }
        public string Documento { get; set; }
        public string Email { get; set; }
        public List<EnderecoClienteResponse> Enderecos { get; set; } = new List<EnderecoClienteResponse>();
        public List<ContatoClienteResponse> Contatos { get; set; } = new List<ContatoClienteResponse>();
    }

    public class EnderecoClienteResponse
    {
        public int Id { get; set; }
        public int EnderecoTipo { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string Cep { get; set; }
    }

    public class ContatoClienteResponse
    {
        public int Id { get; set; }
        public bool Principal { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
    }
}
