using JsonApiDotNetCore.Resources;
using JsonApiDotNetCore.Resources.Annotations;

namespace JsonApiPolymorphismExample.Models
{
    [Resource(PublicName="addresses")]
    public class Address : Identifiable<Guid>
    {
        [Attr]
        public string Street { get; set; } = null!;

        [Attr]
        public int ZipCode { get; set; }
    }
}
