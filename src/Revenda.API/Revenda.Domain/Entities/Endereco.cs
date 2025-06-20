using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Revenda.Domain.Entities
{
    public class Endereco : EntidadeBase
    {
        public int EnderecoTipo { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string Cep { get; set; }
    }

    public class EnderecoRevenda : EntidadeBase
    {
        public int RevendaID { get; set; }
        public Revenda Revenda { get; set; }
        public Endereco Endereco { get; set; }
    }
    public class EnderecoCliente : EntidadeBase
    {
        public int ClienteID { get; set; }
        public Cliente Cliente { get; set; }
        public Endereco Endereco { get; set; }
    }
}
