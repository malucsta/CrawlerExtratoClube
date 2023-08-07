using Crawler.Domain.Enrollments;
using Crawler.Domain.Enrollments.Requests;
using Crawler.Infra.Components.Interfaces.Cache;
using Crawler.Infra.Components.Interfaces.Messaging;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using FluentResults;
using Crawler.Domain.Enrollments.Responses;

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

    public async Task<Result<SearchEnrollmentsResponse>> ProcessEnrollmentSearchRequest(SearchEnrollmentsRequest request)
    {
        // consultar o cache
        var cachedResponse = await _cacheRepository.GetAsync<SearchEnrollmentsResponse>(request.CPF);
        
        if(cachedResponse is not null) 
            return Result.Ok(cachedResponse);

        // consultar o elastic 

        // mandar para o crawler em última instância
        var queue = Infra.Components.Interfaces.Constants.Queues.ToBeConsultedByCrawler;
        _messagePublisher.PublishMessageAtQueue(queue, JsonSerializer.Serialize(request));
        
        return Result.Ok();
    } 

    public async Task CrawlEnrollments()
    {

    }
}
