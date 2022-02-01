using JsonApiDotNetCore.Resources;
using JsonApiDotNetCore.Resources.Annotations;

namespace JsonApiPolymorphismExample.Models;

public abstract class AuthorBase : Identifiable<Guid>
{
    [Attr]
    public string Name { get; set; } = null!;

    [Attr]
    public string Firstname { get; set; } = null!;

}

[Resource(PublicName = "regionalAuthors")]
public class RegionalAuthor : AuthorBase
{
    [Attr]
    public string OriginalRegion { get; set; } = null!;
}

[Resource(PublicName = "internationalAuthors")]
public class InternationalAuthor : AuthorBase
{
    [Attr]
    public string OriginalCountry { get; set; } = null!;

    [HasOne]
    public Address Address { get; set; }
}
