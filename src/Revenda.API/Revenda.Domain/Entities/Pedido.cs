namespace Revenda.Domain.Entities
{
    public class Pedido: EntidadeBase
    {
        public DateTime Data { get; set; }
        public int? ClienteID { get; set; }
        public int? RevendaID { get; set; }
        public float ValorTotal { get; set; }
        public int TipoPedido { get; set; }
        public int Status { get; set; }
        public int FormaPagamento { get; set; }
        public string Observacao { get; set; }      
        public int Tentativas { get; set; } = 0;
        public bool Sucesso { get; set; } = false;
        public string Log { get; set; } = string.Empty;
        public virtual Cliente Cliente { get; set; }
        public virtual Revenda Revenda { get; set; }
        public virtual List<ItemPedido> Itens { get; set; } = new List<ItemPedido>();
    }
}
