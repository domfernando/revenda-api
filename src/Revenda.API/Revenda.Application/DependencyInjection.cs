using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Revenda.Application.Util;
using FluentValidation.AspNetCore;
using FluentValidation;

namespace Revenda.Application
{
    public static class DependencyInjection
    {
        public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            AddAutoMapper(services);
            AddValidation(services);
        }

        private static void AddAutoMapper(this IServiceCollection services)
        {
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MapperProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();

            services.AddSingleton(mapper);
        }

        private static void AddValidation(this IServiceCollection services)
        {
            services.AddFluentValidationAutoValidation(); 
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        }   
    }
}