using Crawler.Domain.Enrollments;
using Crawler.Domain.Enrollments.Requests;
using Crawler.Infra.Components.Interfaces.Cache;
using Crawler.Infra.Components.Interfaces.Messaging;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using FluentResults;
using Crawler.Domain.Enrollments.Responses;
using Crawler.Infra.Components.Interfaces.Crawler;

namespace Crawler.Application;

public class EnrollmentService : IEnrollmentService
{
    private readonly ICacheRepository _cacheRepository;
    private readonly IMessagePublisher _messagePublisher;
    private readonly ILogger<EnrollmentService> _logger;
    private readonly ICrawlerService _crawlerService;
    private readonly EnrollmentsSettings _settings;

    public EnrollmentService(
        ICacheRepository cacheRepository, 
        IMessagePublisher messagePublisher, 
        ILogger<EnrollmentService> logger,
        ICrawlerService crawlerService,
        EnrollmentsSettings settings)
    {
        _cacheRepository = cacheRepository;
        _messagePublisher = messagePublisher;
        _crawlerService = crawlerService;
        _settings = settings;
        _logger = logger;
    }

    public async Task<Result<SearchEnrollmentsResponse>> ProcessEnrollmentSearchRequest(SearchEnrollmentsRequest request)
    {
        // consultar o cache -> ver se já expirou
        var cachedResponse = await _cacheRepository.GetAsync<SearchEnrollmentsResponse>(request.CPF);
        
        if(cachedResponse is not null && !(cachedResponse.CreatedAt.AddDays(_settings.DaysToExpireSearch) < DateTime.UtcNow))
            return Result.Ok(cachedResponse);

        // consultar o elastic 

        // mandar para o crawler em última instância
        var queue = Infra.Components.Interfaces.Constants.Queues.ToBeConsultedByCrawler;
        _messagePublisher.PublishMessageAtQueue(queue, JsonSerializer.Serialize(request));
        
        return Result.Ok();
    } 

    public async Task CrawlEnrollments(SearchEnrollmentsRequest request)
    {
        // TODO
        // conferir cache

        var enrollments = _crawlerService.CrawlExtratoClubeEnrollments(request.User, request.Password, request.CPF);
        var response = new SearchEnrollmentsResponse
        {
            CPF = request.CPF,
            CreatedAt = DateTime.UtcNow,
            Enrollments = enrollments.ToList()
        };

        // salvar no cache
        await _cacheRepository.SetAsync(response.CPF, response);

        // salvar no elastic

        var queue = Infra.Components.Interfaces.Constants.Queues.ConsultedByCrawler;
        _messagePublisher.PublishMessageAtQueue(queue, JsonSerializer.Serialize(response));
    }
}
