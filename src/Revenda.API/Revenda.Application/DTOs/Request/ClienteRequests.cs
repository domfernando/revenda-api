namespace Revenda.Application.DTOs.Request
{
    #region Query
    public class GetClienteRequest
    {
        public int? Id { get; set; } = 0;
        public string? Nome { get; set; }
    }

    #endregion

    #region Create

    public class CreateClienteRequest
    {
        public int TipoPessoa { get; set; }
        public string NomeCompleto { get; set; }
        public string Nome { get; set; }
        public string Documento { get; set; }
        public string Email { get; set; }
        public List<EnderecoClienteRequest> Enderecos { get; set; } = new List<EnderecoClienteRequest>();
    }
    #endregion

    #region Update

    public class UpdateClienteRequest
    {
        public int Id { get; set; }
        public int TipoPessoa { get; set; }
        public string NomeCompleto { get; set; }
        public string Nome { get; set; }
        public string Documento { get; set; }
        public string Email { get; set; }
        public List<EnderecoClienteRequest> Enderecos { get; set; } = new List<EnderecoClienteRequest>();
        public List<ContatoClienteRequest> Contatos { get; set; } = new List<ContatoClienteRequest>();
    }
    #endregion

    #region Listas
    public class EnderecoClienteRequest
    {
        public int? Id { get; set; } = 0;
        public int EnderecoTipo { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string Cep { get; set; }
    }

    public class ContatoClienteRequest
    {
        public int? Id { get; set; } = 0;
        public bool Principal { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
    }
    #endregion
}
