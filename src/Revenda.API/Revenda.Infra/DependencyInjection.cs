using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Revenda.Application.Repositories;
using Revenda.Infra.Persistence.Context;
using Revenda.Infra.Persistence.Repositories;
using System;
using System.Reflection;

namespace Revenda.Infra
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfra(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddPersistence(configuration);
            return services;
        }

        public static void AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            var migrationAssembly = typeof(ApplicationId)
                                    .GetTypeInfo().Assembly.GetName().Name;
            
           // AddSqlServer(services, configuration);
            AddInMemoryDB(services, configuration);          

            services.AddScoped<DatabaseContext, DatabaseContext>();

            #region Repositorios
            services.AddScoped<IClienteRepository, ClienteRepository>();
            services.AddScoped<IPedidoRepository, PedidoRepository>();
            services.AddScoped<IRevendaRepository, RevendaRepository>();
            // services.AddScoped<IEnderecoRepository, EnderecoRepository>();
            //services.AddScoped<IUsuarioPerfilRepository, UsuarioPerfilRepository>();
            //services.AddScoped<IServicoRepository, ServicoRepository>();
            //services.AddScoped<IMensagemRepository, MensagemRepository>();
            //services.AddScoped<IPubRepository, PubRepository>();
            //services.AddScoped<ICongRepository, CongRepository>();
            //services.AddScoped<IArranjoRepository, ArranjoRepository>();
            //services.AddScoped<ITemaRepository, TemaRepository>();
            //services.AddScoped<IOradorTemaRepository, OradorTemaRepository>();
            //services.AddScoped<IDiscursoRepository, DiscursoRepository>();
            //services.AddScoped<IQueueRepository, QueueRepository>();
            #endregion
        }
        private static void AddSqlServer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DatabaseContext>(opt =>
            {
                opt.UseSqlServer(
                        configuration.GetConnectionString("DefaultConnection"),
                        sql => sql.MigrationsAssembly("Revenda.API").EnableRetryOnFailure())
                   .LogTo(Console.WriteLine, LogLevel.Information)
                   .EnableDetailedErrors();
            });
        }

        private static void AddInMemoryDB(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DatabaseContext>(opt =>
            {
                opt.UseInMemoryDatabase("REVENDA_DB")
                   .LogTo(Console.WriteLine, LogLevel.Information)
                   .EnableDetailedErrors();
            });          
        }
    }
}
