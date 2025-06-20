namespace Revenda.Application.DTOs.Request
{
    #region Query
    public class GetPedidoRequest
    {
        public int Id { get; set; }
        public int? ClienteId { get; set; } = 0;
        public int? RevendaId { get; set; } = 0;
    }
    #endregion

    #region Create
    public class CreatePedidoRequest
    {
        public int TipoPedido { get; set; }
        public int? ClienteId { get; set; } = 0;
        public int? RevendaId { get; set; } = 0;
        public DateTime Data { get; set; }
    }

    #endregion

    #region Update

    public class UpdatePedidoRequest { 
        public int Id { get; set; }
        public int TipoPedido { get; set; }
        public int? ClienteId { get; set; } = 0;
        public int? RevendaId { get; set; } = 0;
        public float ValorTotal { get; set; }
        public int Status { get; set; }
        public int Tentativas { get; set; }
        public string Log {  get; set; }
        public List<PedidoItemRequest> Itens { get; set; } = new List<PedidoItemRequest>();
    }

    #endregion

    #region Listas

    public class PedidoItemRequest
    {
        public int? Id { get; set; } = 0;
        public int? PedidoId { get; set; } = 0;
        public int ProdutoId { get; set; }
        public int Quantidade { get; set; }
        public float ValorUnitario { get; set; }
        public float ValorTotal { get; set; }
    }
    #endregion
}
