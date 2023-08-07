using Crawler.Domain.Enrollments;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Crawler.Application;

public static class ExtensionMethods
{
    public static IServiceCollection InjectServices(this IServiceCollection services, IConfiguration configuration)
    {
        var settings = new EnrollmentsSettings();
        configuration.GetSection("Enrollments").Bind(settings);
        services.AddSingleton(settings);

        services.AddScoped<IEnrollmentService, EnrollmentService>();

        return services;
    }
}


