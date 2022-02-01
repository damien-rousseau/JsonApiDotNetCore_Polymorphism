using JsonApiDotNetCore.Controllers.Annotations;
using JsonApiPolymorphismExample.Controllers.Base;
using JsonApiPolymorphismExample.Managers.Contracts;
using JsonApiPolymorphismExample.Models;
using JsonApiPolymorphismExample.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace JsonApiPolymorphismExample.Controllers.V1;

[DisableRoutingConvention]
[Route("v1/articles")]
public class ArticlesV1Controller : JsonApiControllerBase<ArticleBase>
{
    public ArticlesV1Controller(IConstraintsManager constraintsManager, IService<ArticleBase> service) : base(constraintsManager, service)
    {
    }
}
