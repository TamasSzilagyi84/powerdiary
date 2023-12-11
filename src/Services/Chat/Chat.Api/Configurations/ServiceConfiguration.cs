namespace Chat.Api.Configurations
{
    using Chat.Infrastructure.Repositories;

    using Domain.Contracts;
    using Domain.Services;

    internal static class ServiceConfiguration
    {
        internal static void ConfigureServices(this IServiceCollection services)
        {
            // Services
            services.AddScoped<IChatMessageDomainService, ChatMessageDomainService>();

            // Repositories
            services.AddScoped<IChatMessageRepository, ChatMessageRepository>();
        }
    }
}
