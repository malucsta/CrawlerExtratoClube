using Crawler.Domain.Enrollments;
using Microsoft.Extensions.DependencyInjection;

namespace Crawler.Application;

public static class ExtensionMethods
{
    public static IServiceCollection InjectServices(this IServiceCollection services)
    {
        services.AddScoped<IEnrollmentService, EnrollmentService>();

        return services;
    }
}


