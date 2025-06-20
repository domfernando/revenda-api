namespace Revenda.Application.DTOs.Response
{
    public class RevendaResponse
    {
        public int Id { get; set; }
        public string RazaoSocial { get; set; }
        public string NomeFantasia { get; set; }
        public string CNPJ { get; set; }
        public string Email { get; set; }
        public List<RevendaEnderecoResponse> Enderecos { get; set; } = new List<RevendaEnderecoResponse>();
        public List<RevendaContatoResponse> Contatos { get; set; } = new List<RevendaContatoResponse>();
    }

    public class RevendaEnderecoResponse
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

    public class RevendaContatoResponse
    {
        public int Id { get; set; }
        public bool Principal { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
    }
}
