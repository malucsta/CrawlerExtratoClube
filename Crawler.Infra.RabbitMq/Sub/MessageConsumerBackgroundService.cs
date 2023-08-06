using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text;
using Microsoft.Extensions.Hosting;

namespace Crawler.Infra.RabbitMq.Sub;

public class MessageConsumerBackgroundService : BackgroundService
{
    private readonly IModel _channel;
    private readonly string _queueName;

    public MessageConsumerBackgroundService(IModel channel, string queueName)
    {
        _channel = channel;
        _queueName = queueName;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);

            // Process the messsage based on the queue name
            switch (_queueName)
            {
                case Components.Interfaces.Constants.Queues.ToBeConsultedByCrawler:
                    Console.WriteLine($"{Components.Interfaces.Constants.Queues.ToBeConsultedByCrawler} - Received message: {message}");
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
