using RabbitMQ.Client;

namespace Crawler.Infra.RabbitMq;

public class QueueManager
{
    private readonly IModel _channel;

    public QueueManager(IModel channel)
    {
        _channel = channel;
    }

    public void DeclareExchange(string exchangeName, string exchangeType = ExchangeType.Fanout)
    {
        _channel.ExchangeDeclare(exchangeName, exchangeType);
    }

    public void DeclareQueue(string queueName)
    {
        _channel.QueueDeclare(queueName, durable: true, exclusive: false, autoDelete: false, arguments: null);
    }

    public void BindQueueToExchange(string queueName, string exchangeName)
    {
        _channel.QueueBind(queueName, exchangeName, routingKey: "");
    }
}
