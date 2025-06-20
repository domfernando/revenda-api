using Microsoft.EntityFrameworkCore;
using Revenda.Domain.Entities;

namespace Revenda.Infra.Persistence.Context
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext() { }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

        public DbSet<Cliente> Cliente { get; set; }
        public DbSet<Revenda.Domain.Entities.Revenda> Revenda { get; set; }
        public DbSet<Pedido> Pedido { get; set; }
        public DbSet<ItemPedido> ItemPedido { get; set; }
        public DbSet<Produto> Produto { get; set; }
        public DbSet<Contato> Contato { get; set; }
        public DbSet<ContatoRevenda> ContatoRevenda { get; set; }
        public DbSet<ContatoCliente> ContatoCliente { get; set; }
        public DbSet<EnderecoCliente> EnderecoCliente { get; set; }
        public DbSet<EnderecoRevenda> EnderecoRevenda { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}