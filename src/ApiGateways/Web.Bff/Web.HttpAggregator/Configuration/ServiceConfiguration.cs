namespace Web.HttpAggregator.Configuration
{
    using System.Net;

    using Contracts;

    using Services;

    using Shared;

    internal static class ServiceConfiguration
    {
        internal static void ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Services
            services.AddHttpClient<IChatMessageService, ChatMessageService>(options =>
            {
                options.DefaultRequestVersion = HttpVersion.Version30;
                options.DefaultVersionPolicy = HttpVersionPolicy.RequestVersionExact;

                // TODO Validate connection string exists
                options.BaseAddress = new Uri(configuration.GetSection(Const.ConfigurationKeys.ServiceUrls.ChatMessageService).Value);
            });

            services.AddScoped<ICorrelationIdGenerator, CorrelationIdGenerator>();
        }
    }
}
