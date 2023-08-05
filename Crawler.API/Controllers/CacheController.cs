using Crawler.Infra.Components.Interfaces.Cache;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Crawler.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CacheController : ControllerBase
    {
        private readonly ICacheRepository _cacheRepository;
        public CacheController(ICacheRepository cacheRepository)
        {
            _cacheRepository = cacheRepository;
        }

        [HttpPost("{key}")]
        public async Task<IActionResult> SetCache([FromRoute] string key, [FromBody] object value)
        {
            await _cacheRepository.SetAsync(key, value);
            return Ok();
        }

        [HttpGet("{key}")]
        public async Task<IActionResult> GetCache([FromRoute] string key)
        {
            return Ok(await _cacheRepository.GetAsync<object>(key));
        }
    }
}
