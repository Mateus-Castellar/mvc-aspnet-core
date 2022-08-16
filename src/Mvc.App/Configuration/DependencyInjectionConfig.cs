using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Mvc.App.Extensions;
using Mvc.Business.Interfaces;
using Mvc.Data.Context;
using Mvc.Data.Repository;

namespace Mvc.App.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependences(this IServiceCollection services)
        {
            services.AddScoped<AppMvcContext>();
            services.AddScoped<IFornecedorRepository, FornecedorRepository>();
            services.AddScoped<IEnderecoRepository, EnderecoRepository>();
            services.AddScoped<IProdutoRepository, ProdutoRepository>();
            services.AddSingleton<IValidationAttributeAdapterProvider, MoedaValidationAttributeAdapterProvider>();

            return services;
        }
    }
}