using JsonApiDotNetCore.Resources;
using JsonApiDotNetCore.Resources.Annotations;

namespace JsonApiPolymorphismExample.Models;

public abstract class ArticleBase : Identifiable<Guid>
{
    [Attr]
    public string Test { get; set; } = null!;

    [Attr]
    public string Title { get; set; } = null!;

    [HasMany(CanInclude = true)]
    public IList<AuthorBase> Authors { get; set; } = null!;
}

[Resource(PublicName = "sportArticles")]
public class SportArticle : ArticleBase
{
    [Attr]
    public string TypeArticle { get; set; } = null!;

    [HasOne(PublicName = "category")]
    public CategoryBase Category { get; set; } = null!;
}
