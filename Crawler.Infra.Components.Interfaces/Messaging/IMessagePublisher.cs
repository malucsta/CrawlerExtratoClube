namespace Crawler.Infra.Components.Interfaces.Messaging;

public interface IMessagePublisher
{
    void PublishMessageAtExchange(string exchangeName, string message);
    void PublishMessageAtQueue(string queueName, string message);
}
