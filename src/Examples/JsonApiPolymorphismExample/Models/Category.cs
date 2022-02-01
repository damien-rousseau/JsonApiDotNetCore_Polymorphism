using JsonApiDotNetCore.Resources;
using JsonApiDotNetCore.Resources.Annotations;

namespace JsonApiPolymorphismExample.Models;

[Resource]
public abstract class CategoryBase : Identifiable<Guid>
{
    [Attr]
    public string Name { get; set; } = null!;
}

[Resource(PublicName = "sportCategories")]
public class SportCategory : CategoryBase
{
    [Attr]
    public string SportCategoryName { get; set; } = null!;
}
