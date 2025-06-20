namespace Revenda.Application.DTOs.Request
{
    #region Query

    public class GetRevendaRequest
    {
        public int? Id { get; set; } = 0;
        public string Nome { get; set; }
    }

    #endregion

    #region Create
    public class CreateRevendaRequest
    {
        public string RazaoSocial { get; set; }
        public string NomeFantasia { get; set; }
        public string CNPJ { get; set; }
        public string Email { get; set; }
        public List<RevendaEnderecoRequest> Enderecos { get; set; } = new List<RevendaEnderecoRequest>();
        public List<RevendaContatoRequest> Contatos { get; set; } = new List<RevendaContatoRequest>();
    }
    #endregion

    #region Update
    public class UpdateRevendaRequest
    {
        public int Id { get; set; }
        public string RazaoSocial { get; set; }
        public string NomeFantasia { get; set; }
        public string CNPJ { get; set; }
        public string Email { get; set; }
        public List<RevendaEnderecoRequest> Enderecos { get; set; } = new List<RevendaEnderecoRequest>();
        public List<RevendaContatoRequest> Contatos { get; set; } = new List<RevendaContatoRequest>();
    }
    #endregion

    #region Listas

    public class RevendaEnderecoRequest
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

    public class RevendaContatoRequest
    {
        public int? Id { get; set; } = 0;
        public bool Principal { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
    }
    #endregion
}
