using JsonApiDotNetCore.Controllers.Annotations;
using JsonApiPolymorphismExample.Controllers.Base;
using JsonApiPolymorphismExample.Managers.Contracts;
using JsonApiPolymorphismExample.Models;
using JsonApiPolymorphismExample.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace JsonApiPolymorphismExample.Controllers.V2
{
}

// TODO DRO: add support of versioning

//[DisableRoutingConvention]
//[Route("v2/articles")]
//public class ArticlesV2Controller : JsonApiControllerBase<ArticleBase>
//{
//    public ArticlesV2Controller(IConstraintsManager constraintsManager, IService<ArticleBase> service) : base(constraintsManager, service)
//    {
//    }
//}
