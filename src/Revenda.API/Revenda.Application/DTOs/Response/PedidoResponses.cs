namespace Revenda.Application.DTOs.Response
{
    public class PedidoResponse
    {
        public int Id { get; set; }
        public DateTime Data { get; set; }
        public int? ClienteId { get; set; } = 0;
        public virtual ClienteResponse Cliente { get; set; }
        public int? RevendaId { get; set; } = 0;
        public virtual RevendaResponse Revenda { get; set; }
        public float ValorTotal { get; set; }
        public int TipoPedido { get; set; }
        public int Status { get; set; }
        public int FormaPagamento { get; set; }
        public string Observacao { get; set; }
        public int Tentativas { get; set; } = 0;
        public string Log { get; set; } = string.Empty;
        public bool Sucesso { get; set; } = false;
        public List<ItemPedidoResponse> Itens { get; set; } = new List<ItemPedidoResponse>();
    }

    public class ItemPedidoResponse
    {
        public int? Id { get; set; } = 0;
        public int? PedidoId { get; set; } = 0;
        public int ProdutoId { get; set; }
        public int Quantidade { get; set; }
        public float ValorUnitario { get; set; }
        public float ValorTotal { get; set; }
    }
}