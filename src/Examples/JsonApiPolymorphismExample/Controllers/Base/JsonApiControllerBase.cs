using JsonApiDotNetCore.Controllers;
using JsonApiPolymorphismExample.Managers.Contracts;
using JsonApiPolymorphismExample.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace JsonApiPolymorphismExample.Controllers.Base
{
    public abstract class JsonApiControllerBase<T> : CoreJsonApiController
    {
        protected readonly IConstraintsManager ConstraintsManager;
        protected readonly IService<T> Service;

        protected JsonApiControllerBase(IConstraintsManager constraintsManager, IService<T> service)
        {
            ConstraintsManager = constraintsManager ?? throw new NotImplementedException(nameof(constraintsManager));
            Service = service ?? throw new NotImplementedException(nameof(service));
        }

        [HttpGet]
        [HttpHead]
        public virtual async Task<IActionResult> GetAsync(CancellationToken cancellationToken)
        {
            var constraints = ConstraintsManager.Build();

            var result = await Service.GetAsync(cancellationToken);
            return Ok(result);
        }
    }
}
