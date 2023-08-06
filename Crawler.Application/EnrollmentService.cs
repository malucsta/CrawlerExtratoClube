using Crawler.Domain.Enrollments;
using Crawler.Domain.Enrollments.Requests;
using Crawler.Infra.Components.Interfaces.Cache;
using Crawler.Infra.Components.Interfaces.Messaging;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using FluentResults;

namespace Crawler.Application;

public class EnrollmentService : IEnrollmentService
{
    private readonly ICacheRepository _cacheRepository;
    private readonly IMessagePublisher _messagePublisher;
    private readonly ILogger<EnrollmentService> _logger;

    public EnrollmentService(
        ICacheRepository cacheRepository, 
        IMessagePublisher messagePublisher, 
        ILogger<EnrollmentService> logger)
    {
        _cacheRepository = cacheRepository;
        _messagePublisher = messagePublisher;
        _logger = logger;
    }

    public Result SendEnrollmentSearchRequest(SearchEnrollmentsRequest request)
    {
        // consultar o cache

        // consultar o elastic 

        // mandar para o crawler em última instância
        var queue = Infra.Components.Interfaces.Constants.Queues.ToBeConsultedByCrawler;
        _messagePublisher.PublishMessageAtQueue(queue, JsonSerializer.Serialize(request));
        
        return Result.Ok();
    } 
}
