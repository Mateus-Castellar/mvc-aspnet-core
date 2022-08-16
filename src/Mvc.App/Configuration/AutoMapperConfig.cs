namespace Mvc.App.Configuration
{
    public static class AutoMapperConfig
    {
        public static IServiceCollection AddAutoMapperConfiguration(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(Program));
            return services;
        }
    }
}