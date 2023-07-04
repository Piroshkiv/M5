using Microsoft.AspNetCore.Mvc;

namespace Catalog.Resource.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ResourceController : ControllerBase
    {
        private readonly ILogger<ResourceController> _logger;
        public ResourceController(ILogger<ResourceController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetResource")]
        public IEnumerable<Resource> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new Resource
            {
                Id = index,
                Name = $"Name{index}",
                Color = $"#000000",
                PantoneValue = "17-2031"
            })
            .ToArray();
        }
    }
}
