using Crawler.Domain.Enrollments;
using Crawler.Infra.Components.Interfaces.Messaging;
using Crawler.Infra.RabbitMq.Pub;
using Crawler.Infra.RabbitMq.Sub;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;

namespace Crawler.Infra.RabbitMq;

public static class ExtensionMethods
{
    public static IServiceCollection AddRabbitMq(this IServiceCollection services, IConfiguration configuration)
    {
        var settings = new RabbitMqSettings();
        configuration.GetSection("RabbitMq").Bind(settings);
        services.AddSingleton(settings);

        if (settings.HostName is null /*|| settings.User is null || settings.Pass is null*/)
            throw new Exception("Invalid rabbitmq credentials");

        var connection = services.CreateConnection(settings);
        var channel = services.CreateChannel(connection);
        var manager = CreateManager(channel);

        services.ConfigurePublisher();
        services.AddScoped<IMessagePublisher, MessagePublisher>();

        // add queues and consumers
        services
            .AddQueueAndConsumer(manager, Components.Interfaces.Constants.Queues.ToBeConsultedByCrawler)
            .AddQueueAndConsumer(manager, Components.Interfaces.Constants.Queues.ConsultedByCrawler);

        return services;
    }

    private static IServiceCollection ConfigurePublisher(this IServiceCollection services)
    {
        services.AddSingleton<MessagePublisher>();
        return services;
    }

    private static IConnection CreateConnection(this IServiceCollection services, RabbitMqSettings settings)
    {
        var factory = new ConnectionFactory
        {
            HostName = settings.HostName,
            //UserName = settings.User,
            //Password = settings.Pass
        };
        
        //Thread.Sleep(5000);
        var connection = factory.CreateConnection();
        services.AddSingleton<IConnection>(connection);
        return connection;
    }

    private static IModel CreateChannel(this IServiceCollection services, IConnection connection)
    {
        var channel = connection.CreateModel();
        services.AddSingleton<IModel>(channel);
        return channel;
    }

    private static QueueManager CreateManager(IModel channel)
    {
        return new QueueManager(channel);
    }

    public static IServiceCollection AddQueueAndConsumer(this IServiceCollection services, QueueManager manager, string queue)
    {
        manager.DeclareQueue(queue);
        services.AddConsumer(queue);
        return services;
    }

    public static QueueManager AddExchange(this QueueManager manager, string exchange, string queueType)
    {
        manager.DeclareExchange(exchange, queueType);
        return manager;
    }

    public static QueueManager BindQueueToExchange(this QueueManager manager, string exchange, string queue)
    {
        manager.BindQueueToExchange(queue, exchange);
        return manager;
    }

    public static QueueManager AddQueue(this QueueManager manager, string queue)
    {
        manager.DeclareQueue(queue);
        return manager;
    }

    public static IServiceCollection AddConsumer(this IServiceCollection services, string queueName)
    {
        services.AddSingleton<IHostedService>(provider =>
        {
            var channel = provider.GetRequiredService<IModel>();

            return new MessageConsumerBackgroundService(channel, queueName, provider);
        });

        return services;
    }
}
