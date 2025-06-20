namespace Revenda.Domain.Entities
{
    public class ItemPedido: EntidadeBase
    {
        public int PedidoID { get; set; }
        public int ProdutoID { get; set; }
        public float ValorUnitario { get; set; }
        public int Quantidade { get; set; }
        public float ValorTotal { get; set; }
        public virtual Pedido Pedido { get; set; }
        public virtual Produto Produto { get; set; }
    }

}
