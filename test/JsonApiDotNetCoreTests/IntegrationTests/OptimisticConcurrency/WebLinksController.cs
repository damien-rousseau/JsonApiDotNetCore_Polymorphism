using JsonApiDotNetCore.Configuration;
using JsonApiDotNetCore.Controllers;
using JsonApiDotNetCore.Services;
using Microsoft.Extensions.Logging;

namespace JsonApiDotNetCoreTests.IntegrationTests.OptimisticConcurrency
{
    public sealed class WebLinksController : JsonApiController<WebLink, long>
    {
        public WebLinksController(IJsonApiOptions options, IResourceGraph resourceGraph, ILoggerFactory loggerFactory,
            IResourceService<WebLink, long> resourceService)
            : base(options, resourceGraph, loggerFactory, resourceService)
        {
        }
    }
}
