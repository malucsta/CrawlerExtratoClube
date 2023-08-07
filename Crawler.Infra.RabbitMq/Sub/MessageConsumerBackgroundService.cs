using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text;
using Microsoft.Extensions.Hosting;
using Crawler.Domain.Enrollments;
using System.Text.Json;
using Crawler.Domain.Enrollments.Requests;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Crawler.Infra.RabbitMq.Sub;

public class MessageConsumerBackgroundService : BackgroundService
{
    private readonly IModel _channel;
    private readonly string _queueName;
    private readonly IServiceProvider _provider;

    public MessageConsumerBackgroundService(IModel channel, string queueName, IServiceProvider provider)
    {
        _channel = channel;
        _queueName = queueName;
        _provider = provider;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var scope = _provider.CreateScope();
        var enrollmentService = scope.ServiceProvider.GetRequiredService<IEnrollmentService>();

            var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += async (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);

            // Process the messsage based on the queue name
            switch (_queueName)
            {
                case Components.Interfaces.Constants.Queues.ToBeConsultedByCrawler:
                    Console.WriteLine($"{Components.Interfaces.Constants.Queues.ToBeConsultedByCrawler} - Received message: {message}");
                    var request = JsonSerializer.Deserialize<SearchEnrollmentsRequest>(body) 
                                    ?? throw new Exception($"{Components.Interfaces.Constants.Queues.ToBeConsultedByCrawler} - Search request is null");
                    
                    await enrollmentService.CrawlEnrollments(request);
                    break;
                case Components.Interfaces.Constants.Queues.ConsultedByCrawler:
                    Console.WriteLine($"{Components.Interfaces.Constants.Queues.ConsultedByCrawler} - Received message: {message}");
                    break;
                
                default:
                    break;
            }
        };

        _channel.BasicConsume(queue: _queueName, autoAck: true, consumer: consumer);

        return Task.CompletedTask;
    }
}
