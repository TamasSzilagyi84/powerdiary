namespace Chat.Api.Configurations
{
    using Microsoft.AspNetCore.Server.Kestrel.Core;

    internal static class HttpConfiguration
    {
        internal static void ConfigureHttp(this IWebHostBuilder webHost)
        {
            webHost.ConfigureKestrel((context, options) =>
            {
                options.ListenAnyIP(7010, listenOptions =>
                {
                    listenOptions.Protocols = HttpProtocols.Http1AndHttp2AndHttp3;
                    listenOptions.UseHttps();
                });
            });
        }
    }
}
