namespace Chat.Api.Configurations
{
    using Chat.Api.Options;

    internal static class OptionsConfiguration
    {
        internal static void ConfigureOptionSettings(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ApplicationSettings>(configuration.GetSection(nameof(ApplicationSettings)));
        }
    }
}
