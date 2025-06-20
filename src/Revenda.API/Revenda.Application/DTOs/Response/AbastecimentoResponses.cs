using Revenda.Application.DTOs.Request;

namespace Revenda.Application.DTOs.Response
{
    public class AbastecimentoResponse
    {
        public int Id { get; set; }
        public bool Sucesso { get; set; }
        public DateTime Data { get; set; }
        public float ValorTotal { get; set; }
        public List<PedidoItemRequest> Itens { get; set; } = new List<PedidoItemRequest>();
    }
}
