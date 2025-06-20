namespace Revenda.Domain.Entities
{
    public class Produto: EntidadeBase
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public float Preco { get; set; }
        public string Categoria { get; set; }
    }    
}
