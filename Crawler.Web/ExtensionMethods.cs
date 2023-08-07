using Crawler.Infra.Components.Interfaces.Crawler;

namespace Crawler.Web
{
    public static class ExtensionMethods
    {
        public static IServiceCollection AddCrawler(this IServiceCollection services)
        {
            services.InjectServices();
            return services;
        }

        private static IServiceCollection InjectServices(this IServiceCollection services)
        {
            services.AddScoped<ICrawlerService, CrawlerService>();
            return services;
        }
    }
}
