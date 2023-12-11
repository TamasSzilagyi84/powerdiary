namespace Chat.Api.Configurations
{
    using System.Reflection;

    internal static class AutoMapperConfiguration
    {
        internal static void ConfigureAutoMapper(this IServiceCollection services, Assembly assembly)
        {
            services.AddAutoMapper(assembly);
        }
    }
}
