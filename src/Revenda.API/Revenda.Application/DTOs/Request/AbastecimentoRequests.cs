namespace Revenda.Application.DTOs.Request
{
    public class AbastecimentoRequest
    {
        public int RevendaId { get; set; }
        public int PedidoId { get; set; }
        public DateTime Data { get; set; }
        public float ValorTotal { get; set; }
        public List<PedidoItemRequest> Itens { get; set; } = new List<PedidoItemRequest>();
    }

    public class CreateAbastecimentoRequest
    {
        public int PedidoId { get; set; }
        public List<PedidoItemRequest> Itens { get; set; } = new List<PedidoItemRequest>();
    }

    public class ProcessaAbastecimentosRequest
    {
        public int Quantidade { get; set; }
    }
}
