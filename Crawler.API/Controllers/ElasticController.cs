using Crawler.Infra.Components.Interfaces.Cache;
using Crawler.Infra.Components.Interfaces.Search;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Crawler.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ElasticController : ControllerBase
    {
        private readonly ISearchRepository<TesteClass> _repository;

        public ElasticController(ISearchRepository<TesteClass> repository)
        {
            _repository = repository;
        }

        [HttpPost]
        public async Task<IActionResult> Set([FromBody] TesteClass value)
        {
            await _repository.IndexDocumentAsync(value);
            return Ok();
        }

        [HttpGet("{query}")]
        public async Task<IActionResult> Get([FromRoute] string query)
        {
            return Ok(await _repository.Search(query));
        }
    }
}
